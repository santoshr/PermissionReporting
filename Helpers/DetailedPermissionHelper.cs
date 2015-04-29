using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;

namespace PermissionReporting
{
    public static class DetailedPermissionHelper
    {
        #region Fields

        public static SPSecurableObject TopSecurableObject = new SPSecurableObject();
        public static BackgroundWorker bgwWorker;
        static int TotalWebCount;
        static int CurrentWebCount;
        static int StackCount;
        static int BranchCount;
        static bool bAnalyzeListItems;
        static bool bGetFullPermissionStructure;
        static bool bLoadAllItems;
        static SPSiteGroups spSiteGroups;
        static SPSiteUsers spSiteUsers;

        #endregion

        #region Methods

        #region public methods

        /// <summary>
        /// Returns the complete site structure. Depending on parameter values, the structure may omit list items and permission nodes
        /// </summary>
        /// <param name="url">Url of the site collection to connect to</param>
        /// <param name="bgw">Background worker which invoked this method. Reference used to raise progress events</param>
        /// <param name="AnalyzeListItems">Returns List Item level nodes in the structure, if true</param>
        /// <param name="GetFullPermissionStructure">Returns permission nodes for objects with broken inheritance, if set to true</param>
        /// <param name="PreLoadPermissions">Loads permissions on the site as a first step. Has to be true if full permission structure is being fetched</param>
        /// <returns>SPSecurable object instance representing the entire site collection</returns>
        public static SPSecurableObject GetStructure(string url,
            BackgroundWorker bgw,
            bool AnalyzeListItems,
            bool GetFullPermissionStructure,
            bool PreLoadPermissions,
            bool LoadAllItems)
        {
            bgwWorker = bgw;
            bAnalyzeListItems = AnalyzeListItems;
            bGetFullPermissionStructure = GetFullPermissionStructure;
            bLoadAllItems = LoadAllItems;

            using (ClientContext context = new ClientContext(url))
            {
                if (PreLoadPermissions)
                {
                    LoadSiteGroups(context);
                }

                Site site = context.Site;
                Web rootWeb = site.RootWeb;
                context.Load(site);
                context.Load(rootWeb);
                WebCollection webCollection = rootWeb.Webs;
                context.Load(webCollection);
                context.ExecuteQuery();
                TotalWebCount = webCollection.Count;
                ProcessWebs(context, site, rootWeb, ref TopSecurableObject);
            }

            return TopSecurableObject;
        }

        /// <summary>
        /// Returns permission structure for a particular object
        /// </summary>
        /// <param name="bgw">Background worker which invoked this method. Reference used to raise progress events</param>
        /// <param name="Object">a SPSecurable object representing the object being queried for permissions</param>
        /// <returns>List of SPRoleAssignments for the object</returns>
        public static List<SPRoleAssignment> GetSpecificPermissions(BackgroundWorker bgw, SPSecurableObject Object)
        {
            bgwWorker = bgw;
            SPSecurableObject spSecurableObject = new SPSecurableObject();

            using (ClientContext context = new ClientContext(Object.ClientContextUrl))
            {
                if (spSiteGroups == null || spSiteUsers == null)
                {
                    LoadSiteGroups(context);
                }

                Site site = context.Site;
                Web rootWeb = site.RootWeb;
                context.Load(site);
                context.Load(rootWeb);
                context.ExecuteQuery();
                object securableObject = GetSecurableObject(context, Object);
                GetRoleAssignments(context, rootWeb, securableObject, spSecurableObject);
            }

            return spSecurableObject.RoleAssignments;
        }

        /// <summary>
        /// Returns permission structure for a particular object
        /// </summary>
        /// <param name="bgw">Background worker which invoked this method. Reference used to raise progress events</param>
        /// <param name="Object">a SPSecurable object representing the object being queried for permissions</param>
        /// <returns>List of SPRoleAssignments for the object</returns>
        public static SPSecurableObject GetSpecificListItems(BackgroundWorker bgw, SPSecurableObject Object, bool LoadAllItems)
        {
            bgwWorker = bgw;
            SPSecurableObject spSecurableObject = new SPSecurableObject();
            bLoadAllItems = LoadAllItems;

            using (ClientContext context = new ClientContext(Object.ClientContextUrl))
            {
                if (spSiteGroups == null || spSiteUsers == null)
                {
                    LoadSiteGroups(context);
                }

                Site site = context.Site;
                Web rootWeb = site.RootWeb;
                context.Load(site);
                context.Load(rootWeb);
                context.ExecuteQuery();
                object securableObject = GetSecurableObject(context, Object);
                GetSpecificListItems(context, rootWeb, securableObject, Object);
            }

            return Object;
        }

        /// <summary>
        /// Validates if a url points to a valid site collection
        /// </summary>
        /// <param name="url">Url to validate</param>
        /// <returns>true if the url points to a valids sharepoint site</returns>
        public static bool IsValidSite(string url, ref Exception exception)
        {
            bool isValid = true;
            Site site = null;

            try
            {
                using (ClientContext context = new ClientContext(url))
                {
                    site = context.Site;
                    context.Load(site);
                    context.Load(site.RootWeb);
                    context.ExecuteQuery();
                }
            }
            catch (Exception ex)
            {
                isValid = false;
                exception = ex;
            }

            return isValid;
        }

        /// <summary>
        /// Validates if the current user is site collection administrator
        /// This will not work if user is not site collection admin 
        /// and has been greanted Full Control as part of web application policy
        /// </summary>
        /// <param name="url">Link to site collection</param>
        /// <returns>True if the user is a site collection administrator</returns>
        public static bool IsUserAdmin(string url, ref Exception exception)
        {
            bool isValid = true;

            try
            {
                using (ClientContext context = new ClientContext(url))
                {
                    context.Load(context.Site);
                    context.Load(context.Site.RootWeb);

                    BasePermissions bpFullMask = new BasePermissions();
                    bpFullMask.Set(PermissionKind.FullMask);
                    ClientResult<bool> isAdmin = context.Site.RootWeb.DoesUserHavePermissions(bpFullMask);

                    context.ExecuteQuery();

                    if (isAdmin.Value == false)
                    {
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isValid = false;
                exception = ex;
            }

            return isValid;
        }

        public static void ResetState()
        {
            TopSecurableObject = new SPSecurableObject();
            bgwWorker = null;
            TotalWebCount = 0;
            CurrentWebCount = 0;
            StackCount = 0;
            BranchCount = 0;
            bAnalyzeListItems = false;
            bGetFullPermissionStructure = false;
            spSiteGroups = null;
            spSiteUsers = null;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Preloads site groups and users
        /// </summary>
        /// <param name="Context">Client context</param>
        private static void LoadSiteGroups(ClientContext Context)
        {
            RaiseStatusUpdate("Preloading: Initiating", UpdateType.StatusUpdateOnly);
            spSiteGroups = new SPSiteGroups();
            spSiteGroups.SiteGroups = new List<SPGroup>();

            spSiteUsers = new SPSiteUsers();
            spSiteUsers.SiteUsers = new List<SPUser>();

            Site site = Context.Site;
            Web web = site.RootWeb;
            GroupCollection siteGroups = web.SiteGroups;
            Context.Load(site);
            Context.Load(web);
            Context.Load(siteGroups);
            Context.ExecuteQuery();

            RaiseStatusUpdate("Preloading: Site Groups", UpdateType.StatusUpdateOnly);

            // Populate spSiteGroups object with all site groups
            foreach (Group group in siteGroups)
            {
                Context.Load(group);
                Context.Load(group.Users);
                Context.ExecuteQuery();

                SPGroup spGroup = new SPGroup();
                spGroup.GroupName = group.Title;
                spGroup.Users = new List<SPUser>();

                RaiseStatusUpdate("Preloading: Group " + group.LoginName, UpdateType.StatusUpdateOnly);

                foreach (User usr in group.Users)
                {
                    SPUser spUser = new SPUser
                    {
                        DisplayName = usr.Title,
                        Email = usr.Email,
                        LoginId = usr.LoginName
                    };

                    spGroup.Users.Add(spUser);
                }
                spSiteGroups.SiteGroups.Add(spGroup);
                RaiseStatusUpdate("Preloading: Finished", UpdateType.StatusUpdateOnly);
            }

            RaiseStatusUpdate("Preloading: Finished Loading Site Groups", UpdateType.StatusUpdateOnly);
            RaiseStatusUpdate("Preloading: Site Users", UpdateType.StatusUpdateOnly);

            // Populate spSiteUsers object with all site users
            List userList = web.SiteUserInfoList;
            Context.Load(userList);

            CamlQuery query = new CamlQuery();
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='ContentType'/><Value Type='Text'>Person</Value></Eq></Where></Query></View>";

            ListItemCollection collection = userList.GetItems(query);
            Context.Load(collection, pj => pj.Include(
                p => p["Name"],
                p => p["Title"],
                p => p["EMail"],
                p => p["IsSiteAdmin"],
                p => p["IsActive"]));

            Context.ExecuteQuery();

            foreach (ListItem item in collection)
            {
                string loginId = (item["Name"] != null) ? item["Name"].ToString() : string.Empty;
                string displayName = (item["Title"] != null) ? item["Title"].ToString() : loginId;
                string email = (item["EMail"] != null) ? item["EMail"].ToString() : string.Empty;

                SPUser usr = new SPUser { DisplayName = displayName, Email = email, LoginId = loginId };
                spSiteUsers.SiteUsers.Add(usr);
            }

            RaiseStatusUpdate("Preloading: Finished Loading Site Users", UpdateType.StatusUpdateOnly);
            RaiseStatusUpdate("Preloading Finished, Initiating main processing", UpdateType.StatusUpdateOnly);
        }

        /// <summary>
        /// Returns a sharepoint securable object based on parameters
        /// </summary>
        /// <param name="Context">Client context</param>
        /// <param name="Object">SPSecurable Object</param>
        /// <returns>SharePoint object</returns>
        private static object GetSecurableObject(ClientContext Context, SPSecurableObject Object)
        {
            object objToReturn = null;

            switch (Object.ObjectType)
            {
                case SPSecurableObjectTypes.Web:
                    Web web = Context.Site.OpenWebById(Object.ObjectId);
                    Context.Load(web);
                    Context.ExecuteQuery();
                    objToReturn = web;
                    break;

                case SPSecurableObjectTypes.List:
                case SPSecurableObjectTypes.Library:
                    Web listParentWeb = Context.Site.OpenWebById(Object.ParentId);
                    Context.Load(listParentWeb);
                    Context.Load(listParentWeb.Lists);
                    List list = listParentWeb.Lists.GetById(Object.ObjectId);
                    Context.Load(list);
                    Context.ExecuteQuery();
                    objToReturn = list;
                    break;

                case SPSecurableObjectTypes.Item:
                case SPSecurableObjectTypes.Document:
                case SPSecurableObjectTypes.Folder:
                    Web listItemParentWeb = Context.Site.OpenWebById(Object.ParentId);
                    Context.Load(listItemParentWeb);
                    Context.Load(listItemParentWeb.Lists);
                    List parentList = listItemParentWeb.Lists.GetById(Object.ParentListId);
                    Context.Load(parentList);
                    ListItem listItem = parentList.GetItemById(Object.ObjectIdInt);
                    Context.Load(listItem);
                    Context.ExecuteQuery();
                    objToReturn = listItem;
                    break;
            }

            return objToReturn;
        }

        /// <summary>
        /// Recursiverly iterates through all web objects and builds a list
        /// </summary>
        /// <param name="context"></param>
        /// <param name="CurrentWeb"></param>
        /// <param name="CurrentSecurableObject"></param>
        private static void ProcessWebs(ClientContext context, Site site, Web CurrentWeb, ref SPSecurableObject CurrentSecurableObject)
        {
            RaiseStatusUpdate("Processing Web: " + CurrentWeb.ServerRelativeUrl, UpdateType.StatusUpdateOnly);

            string currentWebUrl = site.Url.Replace(site.ServerRelativeUrl, CurrentWeb.ServerRelativeUrl);
            StackCount++;
            BranchCount++;

            context.Load(CurrentWeb, w => w.HasUniqueRoleAssignments);
            context.ExecuteQuery();

            // 1. Process the Current Node

            CurrentSecurableObject = new SPSecurableObject
            {
                ObjectId = CurrentWeb.Id,
                ObjectName = CurrentWeb.Title,
                ObjectUrl = currentWebUrl,
                ClientContextUrl = context.Url,
                HasUniquePermissions = CurrentWeb.HasUniqueRoleAssignments,
                ObjectType = SPSecurableObjectTypes.Web
            };

            if (CurrentSecurableObject.HasUniquePermissions && bGetFullPermissionStructure)
            {
                RaiseStatusUpdate("Processing Web: " + CurrentWeb.ServerRelativeUrl + " Permissions", UpdateType.StatusUpdateOnly);
                GetRoleAssignments(context, CurrentWeb, CurrentWeb, CurrentSecurableObject);
                CurrentSecurableObject.PermissionsLoaded = true;
            }

            CurrentSecurableObject.ChildObjects = new List<SPSecurableObject>();

            // Process all webs
            WebCollection webCollection = CurrentWeb.Webs;
            context.Load(webCollection);
            context.ExecuteQuery();

            if (webCollection.Count > 0)
            {
                // 2. Get a list of sub webs
                foreach (Web web in webCollection)
                {
                    RaiseStatusUpdate("Processing Web: " + web.ServerRelativeUrl, UpdateType.StatusUpdateOnly);
                    SPSecurableObject childSecurableObject = new SPSecurableObject();
                    ProcessWebs(context, site, web, ref childSecurableObject);
                    childSecurableObject.ParentId = CurrentSecurableObject.ObjectId;
                    CurrentSecurableObject.ChildObjects.Add(childSecurableObject);
                }
            }

            // Process all list and libraries
            ListCollection Lists = CurrentWeb.Lists;
            context.Load(Lists);
            context.ExecuteQuery();

            foreach (List lst in Lists)
            {
                if (!lst.Hidden)
                {
                    context.Load(lst, w => w.HasUniqueRoleAssignments);
                    context.Load(lst, w => w.RootFolder);
                    context.Load(lst, w => w.RootFolder.ServerRelativeUrl);
                    context.ExecuteQuery();
                    RaiseStatusUpdate("Processing List: " + lst.RootFolder.ServerRelativeUrl, UpdateType.StatusUpdateOnly);

                    SPSecurableObject childSecurableObject = new SPSecurableObject
                    {
                        ObjectId = lst.Id,
                        ObjectName = lst.Title,
                        ObjectUrl = currentWebUrl.Replace(CurrentWeb.ServerRelativeUrl, lst.RootFolder.ServerRelativeUrl),
                        ChildrenLoaded = ChildLoadDepth.All,
                        HasUniquePermissions = lst.HasUniqueRoleAssignments,
                        ObjectType = lst.BaseType == BaseType.DocumentLibrary ? SPSecurableObjectTypes.Library : SPSecurableObjectTypes.List,
                        ParentId = CurrentSecurableObject.ObjectId,
                        ClientContextUrl = context.Url,
                    };

                    if (childSecurableObject.HasUniquePermissions && bGetFullPermissionStructure)
                    {
                        RaiseStatusUpdate("Processing List: " + lst.RootFolder.ServerRelativeUrl + " Permissions", UpdateType.StatusUpdateOnly);
                        GetRoleAssignments(context, CurrentWeb, lst, childSecurableObject);
                        childSecurableObject.PermissionsLoaded = true;
                    }

                    if (lst.ItemCount > 0 )
                    {
                        if (bAnalyzeListItems)
                        {
                            RaiseStatusUpdate("Processing List: " + lst.RootFolder.ServerRelativeUrl + " Items", UpdateType.StatusUpdateOnly);
                            GetListItems(context, CurrentWeb, lst, string.Empty, childSecurableObject);
                            childSecurableObject.ChildrenLoaded = ChildLoadDepth.All;
                        }
                        else
                        {
                            childSecurableObject.ChildrenLoaded = ChildLoadDepth.None;
                        }
                    }
                    else
                    {
                        childSecurableObject.ChildrenLoaded = ChildLoadDepth.All;
                    }

                    CurrentSecurableObject.ChildObjects.Add(childSecurableObject);
                }
            }

            StackCount--;

            if (StackCount == 1)
            {
                CurrentWebCount++;
                if (CurrentWebCount <= TotalWebCount)
                {
                    RaiseStatusUpdate("Processing Web: " + CurrentWeb.Title, (CurrentWebCount * 100) / TotalWebCount);
                }
            }
        }

        /// <summary>
        /// Returns all list items based on the current folder and list
        /// </summary>
        /// <param name="context">Client context</param>
        /// <param name="CurrentWeb">Current web</param>
        /// <param name="lst">Current list</param>
        /// <param name="FolderName">Current folder</param>
        /// <param name="CurrentSecurableObject">Current securable object, a container to hold all list items under it</param>
        private static void GetListItems(ClientContext context, Web CurrentWeb, List lst, string FolderName, SPSecurableObject CurrentSecurableObject)
        {
            try
            {
                ListItemCollectionPosition itemPosition = null;
                CurrentSecurableObject.ChildObjects = new List<SPSecurableObject>();

                while (true)
                {
                    CamlQuery camlQuery = new CamlQuery();

                    if (FolderName.Length > 0)
                    {
                        camlQuery.FolderServerRelativeUrl = FolderName;
                    }

                    camlQuery.ListItemCollectionPosition = itemPosition;
                    camlQuery.ViewXml =
                        @"<View>
                            <ViewFields>
                                <FieldRef Name='Title'/>
                            </ViewFields>
                            <RowLimit>4000</RowLimit>
                        </View>";

                    ListItemCollection listItems = lst.GetItems(camlQuery);
                    context.Load(listItems);
                    context.ExecuteQuery();

                    itemPosition = listItems.ListItemCollectionPosition;
                    foreach (ListItem listItem in listItems)
                    {
                        context.Load(listItem, i => i.HasUniqueRoleAssignments);
                        context.ExecuteQuery();

                        SPSecurableObject childSecurableObject;

                        if (lst.BaseType == BaseType.DocumentLibrary)
                        {
                            string ItemTitle = string.Empty;
                            string ItemUrl = string.Empty;

                            if (listItem.FileSystemObjectType == FileSystemObjectType.File ||
                                listItem.FileSystemObjectType == FileSystemObjectType.Folder)
                            {
                                ItemTitle = listItem["FileLeafRef"].ToString();
                                ItemUrl = listItem["FileRef"].ToString();
                            }

                            childSecurableObject = new SPSecurableObject
                            {
                                ObjectName = ItemTitle,
                                ObjectUrl = ItemUrl,
                                ObjectIdInt = listItem.Id,
                                ParentId = CurrentWeb.Id,
                                ParentListId = lst.Id,
                                HasUniquePermissions = listItem.HasUniqueRoleAssignments,
                                ObjectType = (listItem.FileSystemObjectType == FileSystemObjectType.File) ? SPSecurableObjectTypes.Document : SPSecurableObjectTypes.Folder,
                                ClientContextUrl = context.Url,
                            };
                        }
                        else
                        {
                            string ItemTitle = string.Empty;
                            string ItemUrl = string.Empty;

                            if (listItem["Title"] != null)
                            {
                                ItemTitle = listItem["Title"].ToString();
                                ItemUrl = listItem["FileRef"].ToString();
                            }
                            else
                            {
                                ItemTitle = listItem["FileRef"].ToString();
                                ItemUrl = listItem["FileRef"].ToString();
                            }

                            childSecurableObject = new SPSecurableObject
                            {

                                ObjectName = ItemTitle,
                                ObjectUrl = ItemUrl,
                                ObjectIdInt = listItem.Id,
                                ParentId = CurrentWeb.Id,
                                ParentListId = lst.Id,
                                ChildrenLoaded = ChildLoadDepth.None,
                                HasUniquePermissions = listItem.HasUniqueRoleAssignments,
                                ObjectType = (listItem.FileSystemObjectType == FileSystemObjectType.File) ? SPSecurableObjectTypes.Item : SPSecurableObjectTypes.Folder,
                                ClientContextUrl = context.Url,
                            };
                        }

                        if (listItem.HasUniqueRoleAssignments && bGetFullPermissionStructure)
                        {
                            GetRoleAssignments(context, CurrentWeb, listItem, childSecurableObject);
                            childSecurableObject.PermissionsLoaded = true;
                        }

                        if (childSecurableObject.ObjectType == SPSecurableObjectTypes.Folder && bLoadAllItems)
                        {
                            GetListItems(
                               context,
                               CurrentWeb,
                               lst,
                               childSecurableObject.ObjectUrl,
                               childSecurableObject);                         

                            childSecurableObject.ChildrenLoaded = ChildLoadDepth.All;
                        }

                        CurrentSecurableObject.ChildObjects.Add(childSecurableObject);
                    }
                    if (itemPosition == null)
                        break;
                }
            }
            catch (Exception ex)
            {
                RaiseStatusUpdate("ERROR: " + ex.Message, UpdateType.StatusUpdateOnly);
            }
        }

        /// <summary>
        /// Returns role assignments for the current sharepoint objects
        /// </summary>
        /// <param name="context"></param>
        /// <param name="CurrentWeb"></param>
        /// <param name="Object"></param>
        /// <param name="childSecurableObject"></param>
        private static void GetRoleAssignments(ClientContext context, Web CurrentWeb, object Object, SPSecurableObject childSecurableObject)
        {
            try
            {
                string objectDetails = string.Empty;
                childSecurableObject.RoleAssignments = new List<SPRoleAssignment>();
                RoleAssignmentCollection roleAssignments = null;

                Type spObjectType = Object.GetType();

                switch (spObjectType.Name)
                {
                    case "Web":
                        Web web = Object as Web;
                        objectDetails = web.ServerRelativeUrl;
                        roleAssignments = web.RoleAssignments;
                        break;
                    case "List":
                        List list = Object as List;
                        objectDetails = "List " + list.Title;
                        roleAssignments = list.RoleAssignments;
                        break;
                    case "ListItem":
                        ListItem listItem = Object as ListItem;
                        objectDetails = "List Item ID " + listItem.Id;
                        roleAssignments = listItem.RoleAssignments;
                        break;
                }

                context.Load(roleAssignments);
                context.ExecuteQuery();

                foreach (RoleAssignment roleAssignment in roleAssignments)
                {
                    SPRoleAssignment spRoleAssignment = new SPRoleAssignment();
                    spRoleAssignment.Member = new SPMember();
                    spRoleAssignment.RoleDefBindings = new SPRoleDefBindings();

                    context.Load(roleAssignment);
                    context.Load(roleAssignment.Member);
                    context.ExecuteQuery();

                    Principal roleMember = roleAssignment.Member;

                    if (roleMember.PrincipalType == Microsoft.SharePoint.Client.Utilities.PrincipalType.SharePointGroup)
                    {
                        spRoleAssignment.PrincipalType = PrincipalType.Group;
                        spRoleAssignment.Member.Group = new SPGroup();

                        var query = from g in spSiteGroups.SiteGroups where g.GroupName == roleMember.LoginName select g;
                        SPGroup group = query.First();

                        spRoleAssignment.Member.Group.GroupName = group.GroupName;
                        spRoleAssignment.Member.Group.Users = new List<SPUser>();

                        foreach (SPUser usr in group.Users)
                        {
                            SPUser spUser = new SPUser
                            {
                                DisplayName = usr.DisplayName,
                                Email = usr.Email,
                                LoginId = usr.LoginId
                            };

                            spRoleAssignment.Member.Group.Users.Add(spUser);
                        }
                    }
                    else if (roleMember.PrincipalType == Microsoft.SharePoint.Client.Utilities.PrincipalType.User)
                    {
                        spRoleAssignment.PrincipalType = PrincipalType.User;
                        spRoleAssignment.Member.User = new SPUser
                        {
                            DisplayName = roleMember.Title,
                            LoginId = roleMember.LoginName
                        };
                    }

                    context.Load(roleAssignment, r => r.RoleDefinitionBindings);
                    context.ExecuteQuery();

                    spRoleAssignment.RoleDefBindings.Permissions = new List<SPBasePermissions>();

                    foreach (RoleDefinition roleDef in roleAssignment.RoleDefinitionBindings)
                    {
                        if (string.IsNullOrEmpty(spRoleAssignment.RoleDefBindings.RoleName))
                        {
                            spRoleAssignment.RoleDefBindings.RoleName = roleDef.Name;
                        }
                        else
                        {
                            spRoleAssignment.RoleDefBindings.RoleName = spRoleAssignment.RoleDefBindings.RoleName + ", " + roleDef.Name;
                        }

                        //enumerate the enum and check each permission 
                        //type to see if the perm is included
                        string[] keys = Enum.GetNames(typeof(PermissionKind));

                        context.Load(roleDef, r => r.BasePermissions);
                        context.ExecuteQuery();

                        //get a reference to the base permissions 
                        //in this RoleDefinition
                        BasePermissions bp = roleDef.BasePermissions;

                        //enumerate the enum
                        foreach (string key in keys)
                        {
                            if (bp.Has((PermissionKind)Enum.Parse(typeof(PermissionKind), key)))
                            {
                                SPBasePermissions spBasePermission = new SPBasePermissions { PermissionName = key };
                                spRoleAssignment.RoleDefBindings.Permissions.Add(spBasePermission);
                            }
                        }
                    }

                    childSecurableObject.RoleAssignments.Add(spRoleAssignment);
                }
            }
            catch (Exception ex)
            {
                RaiseStatusUpdate("ERROR: " + CurrentWeb.ServerRelativeUrl + ". Stack Trace: " + ex.Message, UpdateType.StatusUpdateOnly);
            }
        }

        /// <summary>
        /// Returns role assignments for the current sharepoint objects
        /// </summary>
        /// <param name="context"></param>
        /// <param name="CurrentWeb"></param>
        /// <param name="Object"></param>
        /// <param name="childSecurableObject"></param>
        private static void GetSpecificListItems(ClientContext Context, Web CurrentWeb, object Object, SPSecurableObject childSecurableObject)
        {
            try
            {
                Type spObjectType = Object.GetType();

                switch (spObjectType.Name)
                {
                    case "List":
                        Web listParentWeb = Context.Site.OpenWebById(childSecurableObject.ParentId);
                        Context.Load(listParentWeb);
                        Context.Load(listParentWeb.Lists);
                        List list = listParentWeb.Lists.GetById(childSecurableObject.ObjectId);
                        Context.Load(list);
                        Context.ExecuteQuery();
                        GetListItems(Context, listParentWeb, list, string.Empty, childSecurableObject);
                        break;

                    case "ListItem":
                        Web listItemParentWeb = Context.Site.OpenWebById(childSecurableObject.ParentId);
                        Context.Load(listItemParentWeb);
                        Context.Load(listItemParentWeb.Lists);
                        List parentList = listItemParentWeb.Lists.GetById(childSecurableObject.ParentListId);
                        Context.Load(parentList);
                        ListItem listItem = parentList.GetItemById(childSecurableObject.ObjectIdInt);
                        Context.Load(listItem);
                        Context.ExecuteQuery();

                        string ItemTitle = string.Empty;

                        if (listItem.FileSystemObjectType == FileSystemObjectType.File ||
                            listItem.FileSystemObjectType == FileSystemObjectType.Folder)
                        {
                            ItemTitle = listItem["FileLeafRef"].ToString();
                        }

                        GetListItems(Context,
                            listItemParentWeb,
                            parentList,
                            childSecurableObject.ObjectUrl,
                            childSecurableObject);
                        break;
                }
            }
            catch (Exception ex)
            {
                RaiseStatusUpdate("ERROR: " + CurrentWeb.ServerRelativeUrl + ". Stack Trace: " + ex.Message, UpdateType.StatusUpdateOnly);
            }
        }


        /// <summary>
        /// Get site relative url of a folder for using in CAML queries
        /// </summary>
        /// <param name="lst">List object</param>
        /// <param name="ParentFolderName">Parent folder name, emtpy if top level folder</param>
        /// <param name="CurrentFolderName">Current Folder Name</param>
        /// <returns></returns>
        private static string FolderServerRelativeUrl(List lst, string ParentFolderName, string CurrentFolderName)
        {
            string listRootUrl = lst.RootFolder.ServerRelativeUrl;
            if (string.IsNullOrEmpty(ParentFolderName))
            {
                return string.Format("{0}/{1}/", listRootUrl, CurrentFolderName);
            }
            else
            {
                return string.Format("{0}{1}/", ParentFolderName, CurrentFolderName);
            }
        }

        /// <summary>
        /// Raises progess update event on the current background worker thread
        /// </summary>
        /// <param name="StatusMessage">Status message to pass as progress update</param>
        /// <param name="IncrementProgressBar">Pass negative value for only updates, positive value for incrementing status bar</param>
        private static void RaiseStatusUpdate(string StatusMessage, int IncrementProgressBarAmount)
        {
            bgwWorker.ReportProgress(IncrementProgressBarAmount, StatusMessage);
        }

        #endregion

        #endregion
    }
}
