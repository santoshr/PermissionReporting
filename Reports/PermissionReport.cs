using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PermissionReporting
{
    public partial class PermissionReport : Form
    {
        #region Members

        SPSecurableObject siteStructure = new SPSecurableObject();
        TreeNode selectedContainerNodeForSpecificExpandOperation = new TreeNode();
        SPSecurableObject siteStructureForSpecificNodes = new SPSecurableObject();
        List<SPRoleAssignment> roleAssignmentForSpecificPermissionOperations = new List<SPRoleAssignment>();
        TreeNode selectedContainerNodeForSpecificPermissionOperations = new TreeNode();
        SPSecurableObject selectedTagDataForSpecificPermissionOperations = new SPSecurableObject();
        bool _dirty = false;
        bool _isTreeFilterd;

        #endregion

        #region Methods

        #region Constructor

        public PermissionReport()
        {
            InitializeComponent();
            txtSiteCollectionUrl.Focus();
        }

        #endregion

        #region Control Events

        #region Button Events

        private void btnPopulateSiteStructure_Click(object sender, EventArgs e)
        {
            tvSiteTree.Nodes.Clear();
            tvMembership.Nodes.Clear();
            tvPermission.Nodes.Clear();
            btnPopulateSiteStructure.Enabled = false;
            DetailedPermissionHelper.ResetState();

            string siteUrl = txtSiteCollectionUrl.Text.Trim();

            if (string.IsNullOrEmpty(siteUrl))
                return;

            Exception ex = null;
            bool isSiteValid = DetailedPermissionHelper.IsValidSite(siteUrl, ref ex);

            if (isSiteValid)
            {
                ex = null;
                bool isUserAdmin = DetailedPermissionHelper.IsUserAdmin(siteUrl, ref ex);

                if (!isUserAdmin)
                {
                    ProcessStatusMessage("Site Collection administrator rights are required to run this utility on a site");

                    if (ex != null)
                    {
                        ProcessStatusMessage("ERROR: " + ex.Message);
                    }

                    txtSiteCollectionUrl.SelectAll();
                    txtSiteCollectionUrl.Focus();
                    btnPopulateSiteStructure.Enabled = true;
                    return;
                }
            }
            else
            {
                ProcessStatusMessage("No valid site found at " + siteUrl);

                if (ex != null)
                {
                    ProcessStatusMessage("ERROR: " + ex.Message);
                }

                txtSiteCollectionUrl.SelectAll();
                txtSiteCollectionUrl.Focus();
                btnPopulateSiteStructure.Enabled = true;
                return;
            }

            txtSiteCollectionUrl.Text = siteUrl;
            txtSiteCollectionUrl.Enabled = false;
            bgwSiteDataWorker.RunWorkerAsync();
        }

        #endregion

        #region Menu strip events

        private void saveReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveReport(false);
        }

        private void loadReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        #endregion

        #region Radio Button Events

        private void rdoLoadStructureFull_CheckedChanged(object sender, EventArgs e)
        {
            rdoItemLevelTrue.Checked = !rdoLoadStructureFull.Checked;
            rdoItemLevelFalse.Checked = rdoLoadStructureFull.Checked;
            groupBoxListItemProcessing.Enabled = rdoLoadStructureFull.Checked;
        }

        private void rdoItemLevelTrue_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxListItemLevel.Enabled = rdoItemLevelTrue.Checked;
        }

        #endregion

        #region ContextMenu Events

        private void contextMenuTreeView_Opening(object sender, CancelEventArgs e)
        {
            tsmiSaveReport.Enabled = false;
            tsmiLoadPermission.Enabled = false;
            tsmiLoadReport.Enabled = false;
            tsmiLoadAllItems.Enabled = false;
            tsmiLoadFirstLevelItems.Enabled = false;

            if (tvSiteTree.Nodes.Count <= 0)
            {
                tsmiSaveReport.Enabled = false;
                tsmiLoadPermission.Enabled = false;
                tsmiLoadReport.Enabled = true;
                tsmiLoadAllItems.Enabled = false;
                tsmiLoadFirstLevelItems.Enabled = false;
            }
            else
            {
                tsmiSaveReport.Enabled = true;
                tsmiLoadReport.Enabled = false;

                if (tvSiteTree.SelectedNode != null && tvSiteTree.SelectedNode.Level > 0)
                {
                    if (tvSiteTree.SelectedNode.Tag != null)
                    {
                        SPSecurableObject tagObject = (SPSecurableObject)tvSiteTree.SelectedNode.Tag;
                        tsmiLoadPermission.Enabled = !tagObject.PermissionsLoaded;

                        if (tagObject.ObjectType == SPSecurableObjectTypes.Folder)
                        {
                            if (tagObject.ChildrenLoaded == ChildLoadDepth.None)
                            {
                                tsmiLoadAllItems.Enabled = true;
                                tsmiLoadFirstLevelItems.Enabled = true;
                            }
                            else if (tagObject.ChildrenLoaded == ChildLoadDepth.First)
                            {
                                tsmiLoadAllItems.Enabled = true;
                                tsmiLoadFirstLevelItems.Enabled = false;
                            }
                            else if (tagObject.ChildrenLoaded == ChildLoadDepth.All)
                            {
                                tsmiLoadAllItems.Enabled = false;
                                tsmiLoadFirstLevelItems.Enabled = false;
                            }
                        }
                        else if (tagObject.ObjectType == SPSecurableObjectTypes.Library || tagObject.ObjectType == SPSecurableObjectTypes.List)
                        {
                            if (tagObject.ChildrenLoaded == ChildLoadDepth.None)
                            {
                                tsmiLoadAllItems.Enabled = true;
                                tsmiLoadFirstLevelItems.Enabled = true;
                            }
                            else
                            {
                                tsmiLoadAllItems.Enabled = false;
                                tsmiLoadFirstLevelItems.Enabled = false;
                            }
                        }
                        else
                        {
                            tsmiLoadAllItems.Enabled = false;
                            tsmiLoadFirstLevelItems.Enabled = false;
                        }
                    }
                }
            }
        }

        private void contextMenuTreeView_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            switch (item.Name)
            {
                case "tsmiLoadReport":
                    LoadReport();
                    break;
                case "tsmiSaveReport":
                    SaveReport(false);
                    break;
                case "tsmiLoadPermission":
                    LoadPermission();
                    break;
                case "tsmiLoadFirstLevelItems":
                    LoadItems(false);
                    break;
                case "tsmiLoadAllItems":
                    LoadItems(true);
                    break;
            }
        }

        #endregion

        # region TreeView Events

        /// <summary>
        /// Populates membership treeview when a node is selected from the site treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvSiteTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode clickedNode = e.Node;

            if (tabControl1.SelectedIndex != 1)
                tabControl1.SelectedIndex = 1;

            ProcessSiteStructureNodeClick(clickedNode);
        }

        /// <summary>
        /// Populates permission treeview when a group or a user is selected from the membership treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvMembership_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode clickedNode = e.Node;
            ProcessMembershipNodeClick(clickedNode);
        }

        #endregion

        # region BackGround Worker Events

        #region Permission Background Worker

        private void bgwPermissionWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            SPSecurableObject nodeTagData = e.Argument as SPSecurableObject;

            if (nodeTagData != null && nodeTagData.HasUniquePermissions)
            {
                ProcessSpecificPermissionOperation(nodeTagData);
            }
        }

        private void bgwPermissionWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (roleAssignmentForSpecificPermissionOperations != null && roleAssignmentForSpecificPermissionOperations.Count > 0)
            {
                selectedTagDataForSpecificPermissionOperations.RoleAssignments = roleAssignmentForSpecificPermissionOperations;
                selectedTagDataForSpecificPermissionOperations.PermissionsLoaded = true;
                selectedContainerNodeForSpecificPermissionOperations.Tag = selectedTagDataForSpecificPermissionOperations;
                tvSiteTree.SelectedNode = selectedContainerNodeForSpecificPermissionOperations;
                ProcessSiteStructureNodeClick(selectedContainerNodeForSpecificPermissionOperations);
                ProcessStatusMessage("Finished");
            }
        }

        #endregion

        #region Site Background Worker

        private void bgwSiteDataWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ProcessSiteCollection(txtSiteCollectionUrl.Text);
        }

        private void bgwSiteDataWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CreateTreeView(siteStructure, tvSiteTree.Nodes);
            SaveReport(true);
            btnPopulateSiteStructure.Enabled = true;
            txtSiteCollectionUrl.Enabled = true;
            ProcessStatusMessage("Finished");
        }

        #endregion

        #region Load Item Worker

        private void bgwLoadItemWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            SPSecurableObject nodeTagData = e.Argument as SPSecurableObject;

            if (nodeTagData != null)
            {
                bgwLoadItemWorker.ReportProgress(UpdateType.StatusUpdateOnly, "Loading items for current node");
                LoadSpecificItems(nodeTagData, rdoLoadAllItems.Checked);
            }
        }

        private void bgwLoadItemWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (siteStructureForSpecificNodes != null)
            {
                foreach (SPSecurableObject childNode in siteStructureForSpecificNodes.ChildObjects)
                {
                    CreateTreeView(childNode, selectedContainerNodeForSpecificExpandOperation.Nodes);

                    // Change Children Loaded status based on current operation
                    if (selectedContainerNodeForSpecificExpandOperation.Tag != null)
                    {
                        SPSecurableObject tagData = (SPSecurableObject)selectedContainerNodeForSpecificExpandOperation.Tag;
                        tagData.ChildrenLoaded = (childNode.ChildrenLoaded == ChildLoadDepth.All) ? ChildLoadDepth.All : ChildLoadDepth.First;
                    }
                   
                }
            }

            ProcessStatusMessage("Finished Loading items for current node");
        }
        #endregion

        #region Common Progress Code

        private void bgwSiteDataWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int percentComplete = e.ProgressPercentage;

            if (percentComplete > 0)
            {
                toolStripProgressBar1.Value = e.ProgressPercentage;
            }
            ProcessStatusMessage(e.UserState.ToString());
        }

        #endregion

        #endregion

        #region Filter Picture Box Events
        private void picFilterButton_Click(object sender, EventArgs e)
        {
            // Change filter flag
            _isTreeFilterd = !_isTreeFilterd;

            // Change filter icon based on current state
            if (_isTreeFilterd)
            {
                picFilterButton.Image = Properties.Resources.FilterBrowse;
                toolTip1.SetToolTip(picFilterButton, "Filtered, click to remove filter");
                tvSiteTree.Nodes.Clear();
                CreateTreeView(FilterNodes(), tvSiteTree.Nodes);
                tvSiteTree.ExpandAll();
            }
            else
            {
                picFilterButton.Image = Properties.Resources.FILTER;
                toolTip1.SetToolTip(picFilterButton, "Click to show only unique permission nodes");
                tvSiteTree.Nodes.Clear();
                CreateTreeView(siteStructure, tvSiteTree.Nodes);
            }

        }
        #endregion

        #endregion

        #region Helper Code

        private void ProcessSiteCollection(string Url)
        {
            try
            {
                bool bGetItemLevelDetail = rdoItemLevelTrue.Checked;
                bool bLoadFullStructure = rdoLoadStructureFull.Checked;
                bool bPreloadPermissions = !rdoLoadStructurePartial.Checked;
                bool bLoadAllLevels = rdoLoadAllItems.Checked;

                siteStructure = DetailedPermissionHelper.GetStructure(Url, bgwSiteDataWorker, bGetItemLevelDetail, bLoadFullStructure, bPreloadPermissions, bLoadAllLevels);
            }
            catch (Exception ex)
            {
                bgwSiteDataWorker.ReportProgress(UpdateType.StatusUpdateOnly, "ERROR: " + ex.Message);
                bgwSiteDataWorker.ReportProgress(UpdateType.StatusUpdateOnly, "ERROR: Stack Trace - " + ex.StackTrace);
            }
        }

        private void ProcessSpecificPermissionOperation(SPSecurableObject nodeTagData)
        {
            try
            {
                roleAssignmentForSpecificPermissionOperations = DetailedPermissionHelper.GetSpecificPermissions(bgwPermissionWorker, nodeTagData);
            }
            catch (Exception ex)
            {
                bgwPermissionWorker.ReportProgress(UpdateType.StatusUpdateOnly, "ERROR: " + ex.Message);
                bgwPermissionWorker.ReportProgress(UpdateType.StatusUpdateOnly, "ERROR: Stack Trace - " + ex.StackTrace);
            }
        }

        private void LoadSpecificItems(SPSecurableObject nodeTagData, bool LoadAllItems)
        {
            try
            {
                siteStructureForSpecificNodes = DetailedPermissionHelper.GetSpecificListItems(bgwLoadItemWorker, nodeTagData, LoadAllItems);
            }
            catch (Exception ex)
            {
                bgwLoadItemWorker.ReportProgress(UpdateType.StatusUpdateOnly, "ERROR: " + ex.Message);
                bgwLoadItemWorker.ReportProgress(UpdateType.StatusUpdateOnly, "ERROR: Stack Trace - " + ex.StackTrace);
            }
        }

        private void LoadPermission()
        {
            if (tvSiteTree.SelectedNode == null)
                return;

            ProcessStatusMessage("Processing permissions for current node");

            TreeNode selectedNode = tvSiteTree.SelectedNode;
            TreeNode selectedContainerNode = tvSiteTree.SelectedNode;
            SPSecurableObject nodeTagData = new SPSecurableObject();

            // Locate the first item which has unique permissions, we will populate this.
            while (selectedContainerNode.Tag == null || !(selectedContainerNode.Tag as SPSecurableObject).HasUniquePermissions
                && selectedContainerNode.Parent != null)
            {
                selectedContainerNode = selectedContainerNode.Parent;

                if (selectedContainerNode.Tag != null)
                    nodeTagData = (SPSecurableObject)selectedContainerNode.Tag;
            }

            nodeTagData = (SPSecurableObject)selectedContainerNode.Tag;
            if (nodeTagData.HasUniquePermissions)
            {
                selectedContainerNodeForSpecificPermissionOperations = selectedContainerNode;
                selectedTagDataForSpecificPermissionOperations = nodeTagData;
                bgwPermissionWorker.RunWorkerAsync(nodeTagData);
            }
        }

        private void LoadItems(bool LoadAllLevels)
        {
            if (tvSiteTree.SelectedNode == null)
                return;

            rdoLoadAllItems.Checked = LoadAllLevels;
            rdoLoadFirstLevel.Checked = !LoadAllLevels;

            TreeNode selectedNode = tvSiteTree.SelectedNode;

            SPSecurableObject nodeTagData = new SPSecurableObject();

            if (selectedNode.Tag != null)
                nodeTagData = (SPSecurableObject)selectedNode.Tag;
            else
                return;

            if (nodeTagData.ObjectType == SPSecurableObjectTypes.Folder ||
                nodeTagData.ObjectType == SPSecurableObjectTypes.List ||
                nodeTagData.ObjectType == SPSecurableObjectTypes.Library)
            {
                selectedContainerNodeForSpecificExpandOperation = selectedNode;
                bgwLoadItemWorker.RunWorkerAsync(nodeTagData);
            }
            else
                return;
        }

        private void CreateTreeView(SPSecurableObject node, TreeNodeCollection TreeNodes)
        {
            try
            {
                if (node == null)
                    return;


                TreeNode tNode = new TreeNode(node.ObjectName);

                switch (node.ObjectType)
                {
                    case SPSecurableObjectTypes.Web:
                    case SPSecurableObjectTypes.Site:
                        if (node.HasUniquePermissions)
                        {
                            tNode.ImageKey = ImageKeys.SecureSite;
                            tNode.SelectedImageKey = ImageKeys.SecureSite;
                        }
                        else
                        {
                            tNode.ImageKey = ImageKeys.Site;
                            tNode.SelectedImageKey = ImageKeys.Site;
                        }
                        break;

                    case SPSecurableObjectTypes.Library:
                        if (node.HasUniquePermissions)
                        {
                            tNode.ImageKey = ImageKeys.SecureLibrary;
                            tNode.SelectedImageKey = ImageKeys.SecureLibrary;
                        }
                        else
                        {
                            tNode.ImageKey = ImageKeys.Library;
                            tNode.SelectedImageKey = ImageKeys.Library;
                        }
                        break;

                    case SPSecurableObjectTypes.List:
                        if (node.HasUniquePermissions)
                        {
                            tNode.ImageKey = ImageKeys.SecureList;
                            tNode.SelectedImageKey = ImageKeys.SecureList;
                        }
                        else
                        {
                            tNode.ImageKey = ImageKeys.List;
                            tNode.SelectedImageKey = ImageKeys.List;
                        }
                        break;

                    case SPSecurableObjectTypes.Folder:
                        if (node.HasUniquePermissions)
                        {
                            tNode.ImageKey = ImageKeys.SecureFolder;
                            tNode.SelectedImageKey = ImageKeys.SecureFolder;
                        }
                        else
                        {
                            tNode.ImageKey = ImageKeys.Folder;
                            tNode.SelectedImageKey = ImageKeys.Folder;
                        }
                        break;

                    case SPSecurableObjectTypes.Document:
                        if (node.HasUniquePermissions)
                        {
                            tNode.ImageKey = ImageKeys.SecureFile;
                            tNode.SelectedImageKey = ImageKeys.SecureFile;
                        }
                        else
                        {
                            tNode.ImageKey = ImageKeys.File;
                            tNode.SelectedImageKey = ImageKeys.File;
                        }
                        break;

                    case SPSecurableObjectTypes.Item:
                        if (node.HasUniquePermissions)
                        {
                            tNode.ImageKey = ImageKeys.SecureListItem;
                            tNode.SelectedImageKey = ImageKeys.SecureListItem;
                        }
                        else
                        {
                            tNode.ImageKey = ImageKeys.ListItem;
                            tNode.SelectedImageKey = ImageKeys.ListItem;
                        }
                        break;
                }

                if (node.ChildObjects != null && node.ChildObjects.Count > 0)
                {
                    foreach (SPSecurableObject childSecurableObject in node.ChildObjects)
                    {
                        CreateTreeView(childSecurableObject, tNode.Nodes);
                    }
                }

                tNode.Tag = node;
                TreeNodes.Add(tNode);
            }
            catch (Exception ex)
            {
                ProcessStatusMessage("WARNING: UI Issue " + ex.Message);
            }
        }

        private void ProcessSiteStructureNodeClick(TreeNode clickedNode)
        {
            SPSecurableObject nodeTagObject = clickedNode.Tag as SPSecurableObject;
            tvMembership.Nodes.Clear();
            tvPermission.Nodes.Clear();

            // Locate the first item which has unique permissions, we will populate this.
            while (nodeTagObject == null || !nodeTagObject.HasUniquePermissions && clickedNode.Parent != null)
            {
                clickedNode = clickedNode.Parent;
                nodeTagObject = clickedNode.Tag as SPSecurableObject;
            }

            if (nodeTagObject != null && nodeTagObject.RoleAssignments != null)
            {
                if (nodeTagObject.HasUniquePermissions && nodeTagObject.RoleAssignments.Count == 0)
                {
                    TreeNode topGroupNode = new TreeNode("Permissions not loaded");
                    topGroupNode.SelectedImageKey = ImageKeys.Group;
                    topGroupNode.ImageKey = ImageKeys.Group;
                    tvMembership.Nodes.Add(topGroupNode);

                    TreeNode tGroupNode = new TreeNode("Right click on the node and Load Permissions");
                    tGroupNode.SelectedImageKey = ImageKeys.Group;
                    tGroupNode.ImageKey = ImageKeys.Group;
                    topGroupNode.Nodes.Add(tGroupNode);

                    tvMembership.ExpandAll();
                }
                else
                {
                    try
                    {
                        TreeNode topGroupNode = new TreeNode("Groups");
                        topGroupNode.SelectedImageKey = ImageKeys.Group;
                        topGroupNode.ImageKey = ImageKeys.Group;
                        tvMembership.Nodes.Add(topGroupNode);

                        TreeNode topUserNode = new TreeNode("Users");
                        topUserNode.SelectedImageKey = ImageKeys.Users;
                        topUserNode.ImageKey = ImageKeys.Users;
                        tvMembership.Nodes.Add(topUserNode);

                        foreach (SPRoleAssignment roleAssignment in nodeTagObject.RoleAssignments)
                        {
                            switch (roleAssignment.PrincipalType)
                            {
                                case PrincipalType.Group:
                                    if (roleAssignment.Member.Group != null)
                                    {
                                        TreeNode tGroupNode = new TreeNode(roleAssignment.Member.Group.GroupName);
                                        tGroupNode.SelectedImageKey = ImageKeys.Group;
                                        tGroupNode.ImageKey = ImageKeys.Group;
                                        tGroupNode.Tag = roleAssignment.RoleDefBindings;

                                        if (roleAssignment.Member.Group.Users != null &&
                                            roleAssignment.Member.Group.Users.Count > 0)
                                        {
                                            foreach (SPUser user in roleAssignment.Member.Group.Users)
                                            {
                                                TreeNode tGroupUserNode = new TreeNode(user.DisplayName);
                                                tGroupUserNode.SelectedImageKey = ImageKeys.User;
                                                tGroupUserNode.ImageKey = ImageKeys.User;
                                                tGroupNode.Nodes.Add(tGroupUserNode);
                                            }
                                        }

                                        topGroupNode.Nodes.Add(tGroupNode);

                                    }
                                    break;

                                case PrincipalType.User:
                                    if (roleAssignment.Member.User != null)
                                    {
                                        TreeNode tUserNode = new TreeNode(roleAssignment.Member.User.DisplayName);
                                        tUserNode.SelectedImageKey = ImageKeys.User;
                                        tUserNode.ImageKey = ImageKeys.User;
                                        tUserNode.Tag = roleAssignment.RoleDefBindings;
                                        topUserNode.Nodes.Add(tUserNode);
                                    }
                                    break;
                            }
                        }

                        topGroupNode.Expand();
                    }
                    catch (Exception ex)
                    {
                        ProcessStatusMessage("WARNING: UI Issue " + ex.Message);
                    }
                }
            }
            else
            {
                TreeNode topGroupNode = new TreeNode("No records to display");
                topGroupNode.SelectedImageKey = ImageKeys.Group;
                topGroupNode.ImageKey = ImageKeys.Group;
                tvMembership.Nodes.Add(topGroupNode);

                TreeNode tGroupNode = new TreeNode("Right click on the node and Load Permissions");
                tGroupNode.SelectedImageKey = ImageKeys.Group;
                tGroupNode.ImageKey = ImageKeys.Group;
                topGroupNode.Nodes.Add(tGroupNode);

                tvMembership.ExpandAll();
            }
        }

        private void ProcessMembershipNodeClick(TreeNode clickedNode)
        {
            SPRoleDefBindings roleDefBindings = clickedNode.Tag as SPRoleDefBindings;

            // Locate the first item which has unique permissions, we will populate this.
            while (roleDefBindings == null && clickedNode.Parent != null)
            {
                clickedNode = clickedNode.Parent;
                roleDefBindings = clickedNode.Tag as SPRoleDefBindings;
            }


            if (roleDefBindings != null)
            {
                tvPermission.Nodes.Clear();
                TreeNode topPermissionNode = new TreeNode(roleDefBindings.RoleName);
                topPermissionNode.SelectedImageKey = ImageKeys.BasePermissions;
                topPermissionNode.ImageKey = ImageKeys.BasePermissions;

                foreach (SPBasePermissions spPermissions in roleDefBindings.Permissions)
                {
                    TreeNode tPermissionNode = new TreeNode(spPermissions.PermissionName);
                    tPermissionNode.SelectedImageKey = ImageKeys.BasePermission;
                    tPermissionNode.ImageKey = ImageKeys.BasePermission;
                    topPermissionNode.Nodes.Add(tPermissionNode);
                }

                tvPermission.Nodes.Add(topPermissionNode);
                tvPermission.ExpandAll();
            }
        }

        private void SaveReport(bool IsAutosave)
        {
            if (siteStructure == null)
                return;

            string sFilePath = string.Empty;

            if (IsAutosave)
            {
                sFilePath = txtSiteCollectionUrl.Text;
                sFilePath = sFilePath.Replace("http", string.Empty);
                sFilePath = sFilePath.Replace(":", string.Empty);
                sFilePath = sFilePath.Replace("\\", string.Empty);
                sFilePath = sFilePath.Replace("www", string.Empty);
                sFilePath = sFilePath.Replace(".", string.Empty);
                sFilePath = sFilePath.Replace("/", string.Empty);
                sFilePath = sFilePath.Replace("?", string.Empty);
                sFilePath = sFilePath.Replace("*", string.Empty);
                sFilePath = sFilePath.Replace("\"", string.Empty);
                sFilePath = sFilePath.Replace("'", string.Empty);
                sFilePath = sFilePath.Replace("<", string.Empty);
                sFilePath = sFilePath.Replace(">", string.Empty);
                sFilePath = string.Format("{0}-{3}{2}{1}-{4}{5}{6}{7}{8}",
                    sFilePath,
                    DateTime.Now.Day,
                    DateTime.Now.Month,
                    DateTime.Now.Year,
                    DateTime.Now.Hour,
                    DateTime.Now.Minute,
                    DateTime.Now.Second,
                    DateTime.Now.Millisecond,
                    ".spr");
            }
            else
            {
                saveFileDialog1.Filter = "Permission Report Files|*.spr";
                DialogResult result = saveFileDialog1.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    sFilePath = saveFileDialog1.FileName;
                }
            }

            if (!string.IsNullOrEmpty(sFilePath))
            {
                XmlHelper.SerializeToXML(siteStructure, sFilePath);
                ProcessStatusMessage("Report Saved to " + sFilePath);

                // Save Log as well
                string sLogOutput = txtOutputLog.Text;

                if (!string.IsNullOrEmpty(sLogOutput))
                {
                    sFilePath = string.Format("{0}.{1}", sFilePath, "log.log");
                    FileHelper.SaveFile(sLogOutput, sFilePath);
                    ProcessStatusMessage("Log Saved to " + sFilePath);
                }
            }
        }

        private void LoadReport()
        {
            openFileDialog1.Filter = "Permission Report Files|*.spr";
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string sFilePath = openFileDialog1.FileName;
                siteStructure = XmlHelper.LoadFromXml(sFilePath);
                tvSiteTree.Nodes.Clear();
                tvMembership.Nodes.Clear();
                tvPermission.Nodes.Clear();
                CreateTreeView(siteStructure, tvSiteTree.Nodes);
                txtSiteCollectionUrl.Text = siteStructure.ObjectUrl;
                ProcessStatusMessage("Report Loaded from " + sFilePath);
            }
        }

        private void ProcessStatusMessage(string Message)
        {
            toolStripStatusLabel1.Text = Message;
            txtOutputLog.Text = txtOutputLog.Text + DateTime.Now + " " + Message + Environment.NewLine;
            if (chkAutoScroll.Checked)
            {
                txtOutputLog.SelectionStart = txtOutputLog.Text.Length;
                txtOutputLog.ScrollToCaret();
                txtOutputLog.Refresh();
            }
        }

        private SPSecurableObject FilterNodes()
        {
            SPSecurableObject filteredStructure = new SPSecurableObject();
            filteredStructure = (SPSecurableObject)siteStructure.Clone();
            DoFiltering(filteredStructure);
            return filteredStructure;
        }

        private void DoFiltering(SPSecurableObject CurrentNode)
        {
            if (CurrentNode.ChildObjects != null && CurrentNode.ChildObjects.Count > 0)
            {
                List<SPSecurableObject> nodestoDelete = new List<SPSecurableObject>();

                foreach (SPSecurableObject childObject in CurrentNode.ChildObjects)
                {
                    if (!childObject.HasUniquePermissions)
                    {
                        if (childObject.ChildObjects != null && childObject.ChildObjects.Count > 0)
                        {
                            DoFiltering(childObject);
                        }
                        else
                        {
                            nodestoDelete.Add(childObject);
                        }
                    }
                    else
                    {
                        DoFiltering(childObject);
                    }

                    if (!childObject.HasUniquePermissions &&
                        (childObject.ChildObjects == null || childObject.ChildObjects.Count == 0))
                    {
                        nodestoDelete.Add(childObject);
                    }
                }

                foreach (SPSecurableObject nodeToDelete in nodestoDelete)
                {
                    CurrentNode.ChildObjects.Remove(nodeToDelete);
                }
            }

        }

        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txtOutputLog.Text = string.Empty;
        }

        #endregion
    }
}
