using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraTreeList.Nodes;
using SSDocumentReader.SingleControls;

namespace SSDocumentReader.MainControls
{
    public partial class DirectoryViewControl : UserControl
    {
        string filterTitle;
        DateTime? filterDate1, filterDate2;
        List<TreeListNode> listAllNode = new List<TreeListNode>();
        DirectoryInfo folderInfoRoot;
        DocViewerControlBase docViewControl = null;

        public DirectoryViewControl()
        {
            InitializeComponent();
        }

        private void DirectoryViewControl_Load(object sender, EventArgs e)
        {
            folderInfoRoot = new DirectoryInfo(StaticValues.DocumentFolder);
            if (!folderInfoRoot.Exists)
                folderInfoRoot.Create();
            btnSearch_Click(null,null);
            //docViewControl=new Control();
            //this.splitContainerControl1.Panel2.Controls.Add(docViewControl);
        }

        private void AddTreeNodesAll()
        {
            treeList1.Nodes.Clear();
            listAllNode.Clear();
            AddTreeNodesWithFilter(folderInfoRoot);
        }



         private void AddTreeNodesWithFilter(DirectoryInfo folderInfo)
        {
            var folders = folderInfo.GetDirectories();
            var files = folderInfo.GetFiles();
            foreach (var folder in folders)
            {
                if (filterTitle != string.Empty || folder.Name.Contains(filterTitle))
                    AddTreeNodes(folder);
                //else
                //if ((filterTitle==string.Empty|| folder.Name.Contains(filterTitle))
                //    && (filterDate1 == null || filterDate1 <= folder.LastWriteTime)
                //    && (filterDate2 == null || filterDate2 > folder.LastWriteTime))
                //{
                //        AddTreeNodes(folder);
                //}
                else
                {
                    AddTreeNodesWithFilter(folder);
                }
            }
            foreach (var file in files)
            {
                if (StaticValues.SupportExtentions.Contains(file.Extension.Trim('.').ToLower())
                    && (filterTitle == string.Empty || file.Name.Contains(filterTitle))
                    && (dateEdit1.EditValue == null || dateEdit1.DateTime <= file.LastWriteTime)
                    && (dateEdit2.EditValue == null || dateEdit2.DateTime.AddDays(1) > file.LastWriteTime))
                {
                    AddFileNode(file);
                }
            }
        }

         private void AddFileNode(FileInfo file)
         {
             TreeListNode nodeParent = listAllNode.SingleOrDefault(p => (string)p[1] == file.DirectoryName);
             if (nodeParent == null)
             {
                 nodeParent = AddParentNode(file.Directory);
             }
             var node = treeList1.AppendNode(new object[] { file.Name, file.FullName }, nodeParent);
             node.ImageIndex = 1;
             node.SelectImageIndex = 1;
             //node.StateImageIndex = 1;
         }

         TreeListNode AddParentNode(DirectoryInfo parentDirectory)
         {
             if (parentDirectory.FullName == folderInfoRoot.FullName)
                 return null;
             var node = listAllNode.SingleOrDefault(p =>(string)p[1] == parentDirectory.FullName);
             if (node != null)
                 return node;

             var parentNode = AddParentNode(parentDirectory.Parent);
             var node1 = treeList1.AppendNode(new object[] { parentDirectory.Name, parentDirectory.FullName }, parentNode);
             node1.ImageIndex = 0;
             node1.SelectImageIndex = 0;
             //node1.StateImageIndex = 0;
             listAllNode.Add(node1);
             return node1;
         }

        private void AddTreeNodes(DirectoryInfo folderInfo)
        {
            var folders = folderInfo.GetDirectories();
            var files = folderInfo.GetFiles();
            foreach (var folder in folders)
            {
                AddTreeNodes(folder);
            }
            foreach (var file in files)
            {
                if (StaticValues.SupportExtentions.Contains(file.Extension.Trim('.').ToLower()))
                    AddFileNode(file);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            filterTitle = textTitle.Text;
            filterDate1 =(DateTime?) dateEdit1.EditValue;
            if(dateEdit2.EditValue == null)
            filterDate2 =  null;
            else
                filterDate2=dateEdit2.DateTime.AddDays(1);
            AddTreeNodesAll();
        }



        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            if (treeList1.FocusedNode.ImageIndex == 0)
                return;
            var path =(string) treeList1.FocusedNode[1];
            var ext=GetExtension(path);
            if (ext == StaticValues.SupportExtentions[0])
            {
                if (!(docViewControl is PdfViewerControl))
                {
                    var pdfControl=new PdfViewerControl();
                    docViewControl = pdfControl;
                    this.splitContainerControl1.Panel2.Controls.Clear();
                    this.splitContainerControl1.Panel2.Controls.Add(docViewControl);
                    docViewControl.Dock = DockStyle.Fill;
                   
                }
                docViewControl.ShowDoc(path);
            }
            else if (ext == StaticValues.SupportExtentions[1])
            {
                if (!(docViewControl is RichViewerControl))
                {
                    var pdfControl = new RichViewerControl();
                    docViewControl = pdfControl;
                    this.splitContainerControl1.Panel2.Controls.Clear();
                    this.splitContainerControl1.Panel2.Controls.Add(docViewControl);
                    docViewControl.Dock = DockStyle.Fill;
                   
                }
                docViewControl.ShowDoc(path);
            }
        }

        private string GetExtension(string path)
        {
            return path.Split('.').Last().ToLower();
        }


      
    }
}
