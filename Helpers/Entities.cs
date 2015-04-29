using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PermissionReporting
{
    public class SPSecurableObject : ICloneable
    {
        public string ObjectName { get; set; }
        public Guid ObjectId { get; set; }
        public int ObjectIdInt { get; set; }
        public Guid ParentId { get; set; }
        public Guid ParentListId { get; set; }
        public string ClientContextUrl { get; set; }
        public string ObjectUrl { get; set; }
        public bool HasUniquePermissions { get; set; }
        public bool PermissionsLoaded { get; set; }
        public ChildLoadDepth ChildrenLoaded { get; set; }
        public SPSecurableObjectTypes ObjectType { get; set; }
        public List<SPSecurableObject> ChildObjects { get; set; }
        public List<SPRoleAssignment> RoleAssignments { get; set; }

        public object Clone()
        {
            SPSecurableObject cloneObject = (SPSecurableObject)this.MemberwiseClone();
            cloneObject.ChildObjects = new List<SPSecurableObject>();
            cloneObject.RoleAssignments = new List<SPRoleAssignment>();

            if (this.ChildObjects != null)
            {
                for (int i = 0; i < this.ChildObjects.Count; i++)
                {
                    cloneObject.ChildObjects.Add((SPSecurableObject)this.ChildObjects[i].Clone());
                }
            }

            if (this.RoleAssignments != null)
            {
                for (int i = 0; i < this.RoleAssignments.Count; i++)
                {
                    cloneObject.RoleAssignments.Add((SPRoleAssignment)this.RoleAssignments[i].Clone());
                }
            }

            return cloneObject;
        }
    }

    public class SPRoleAssignment : ICloneable
    {
        public PrincipalType PrincipalType { get; set; }
        public SPMember Member { get; set; }
        public SPRoleDefBindings RoleDefBindings { get; set; }

        public object Clone()
        {
            SPRoleAssignment cloneObject = (SPRoleAssignment)this.MemberwiseClone();
            cloneObject.Member = (SPMember)this.Member.Clone();
            cloneObject.RoleDefBindings = (SPRoleDefBindings)this.RoleDefBindings.Clone();

            return cloneObject;
        }
    }

    public class SPMember : ICloneable
    {
        public SPGroup Group { get; set; }
        public SPUser User { get; set; }

        public object Clone()
        {
            SPMember cloneObject = new SPMember();
            if(this.Group!=null)
            cloneObject.Group = (SPGroup)this.Group.Clone();
            if (this.User != null)
            cloneObject.User = (SPUser)this.User.Clone();
            return cloneObject;
        }
    }

    public class SPRoleDefBindings : ICloneable
    {
        public string RoleName;
        public List<SPBasePermissions> Permissions;

        public object Clone()
        {
            SPRoleDefBindings cloneObject = (SPRoleDefBindings)this.MemberwiseClone();
            cloneObject.Permissions = new List<SPBasePermissions>();

            for (int i = 0; i < this.Permissions.Count; i++)
            {
                cloneObject.Permissions.Add((SPBasePermissions)this.Permissions[i].Clone());
            }

            return cloneObject;
        }
    }

    public class SPGroup : ICloneable
    {
        public string GroupName { get; set; }
        public List<SPUser> Users;

        public object Clone()
        {
            SPGroup cloneObject = (SPGroup)this.MemberwiseClone();
            cloneObject.Users = new List<SPUser>();

            for (int i = 0; i < this.Users.Count; i++)
            {
                cloneObject.Users.Add((SPUser)this.Users[i].Clone());
            }

            return cloneObject;
        }
    }

    public class SPUser : ICloneable
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string LoginId { get; set; }

        public object Clone()
        {
            return (SPUser)this.MemberwiseClone();
        }
    }

    public class SPSiteGroups : ICloneable
    {
        public List<SPGroup> SiteGroups { get; set; }

        public object Clone()
        {
            SPSiteGroups cloneObject = (SPSiteGroups)this.MemberwiseClone();
            cloneObject.SiteGroups = new List<SPGroup>();

            for (int i = 0; i < this.SiteGroups.Count; i++)
            {
                cloneObject.SiteGroups.Add((SPGroup)this.SiteGroups[i].Clone());
            }

            return cloneObject;
        }
    }

    public class SPSiteUsers : ICloneable
    {
        public List<SPUser> SiteUsers { get; set; }

        public object Clone()
        {
            SPSiteUsers cloneObject = (SPSiteUsers)this.MemberwiseClone();
            cloneObject.SiteUsers = new List<SPUser>();

            for (int i = 0; i < this.SiteUsers.Count; i++)
            {
                cloneObject.SiteUsers.Add((SPUser)this.SiteUsers[i].Clone());
            }

            return cloneObject;
        }
    }

    public class SPBasePermissions : ICloneable
    {
        public string PermissionName { get; set; }

        public object Clone()
        {
            return (SPBasePermissions)this.MemberwiseClone();
        }
    }

    public enum SPSecurableObjectTypes
    {
        Site,
        Web,
        List,
        Library,
        Folder,
        Item,
        Document
    };

    public enum PrincipalType
    {
        User,
        Group
    };

    public struct ImageKeys
    {
        public static string Site = "Site";
        public static string SecureSite = "SecureSite";
        public static string Web = "Web";
        public static string SecureWeb = "SecureSite";
        public static string List = "List";
        public static string SecureList = "SecureList";
        public static string Library = "Library";
        public static string SecureLibrary = "SecureLibrary";
        public static string Folder = "Folder";
        public static string SecureFolder = "SecureFolder";
        public static string File = "File";
        public static string SecureFile = "SecureFile";
        public static string ListItem = "ListItem";
        public static string SecureListItem = "SecureListItem";
        public static string User = "User";
        public static string Users = "Users";
        public static string Group = "Group";
        public static string PermissionDetails = "PermissionDetails";
        public static string BasePermissions = "BasePermissions";
        public static string BasePermission = "BasePermission";
    }

    public struct UpdateType
    {
        public const int StatusUpdateOnly = -1;
    }

    public enum ChildLoadDepth
    {
        None,
        First,
        All
    };

}
