using AccountinPoint.TestWinApp.Data;
using AccountinPoint.TestWinApp.ViewModels;
using System;
using System.IO;
using System.Windows.Forms;

namespace AccountinPoint.TestWinApp.UI
{
    public partial class Form1 : Form
    {

        private const string testFileName = "fileExample.po";

        public Form1()
        {
            InitializeComponent();
            CreateMenu();
            SetupFileDialog();
        }

        private void SetupFileDialog()
        {
            openFileDialog.FileName = string.Empty;
            var appFolder = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog.InitialDirectory = appFolder;
            openFileDialog.Filter = "Portable object files(*.po)|*.po|Text files(*.txt)|*.txt";

            if (File.Exists($"{appFolder}\\{testFileName}"))
            {
                openFileDialog.FileName = testFileName;
            }
        }

        public void CreateMenu()
        {
            var fileItem = new ToolStripMenuItem("Файл");
            var openItem = new ToolStripMenuItem("Открыть");
            openItem.Click += OpenFile_Click;
            fileItem.DropDownItems.Add(openItem);
            menuStrip.Items.Add(fileItem);
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private void LoadFile()
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            listBox.Items.Clear();
            listBox.Items.Add("Загрузка...");
            var filename = openFileDialog.FileName;
            var fileText = File.ReadAllLines(filename);

            var loader = new Loader();
            var root = loader.LoadStrings(fileText);

            var treeRoot = GetTreeNode(root);
            treeRoot.Text = "...";
            treeRoot.ExpandAll();
            treeView1.Nodes.Add(treeRoot);
            treeView1.AfterSelect += NodeSelected;
            listBox.Items.Clear();
        }

        private TreeNode GetTreeNode(ItemViewModel item)
        {
            var node = new TreeNode()
            {
                Name = item.Name,
                Text = item.Name,
                Tag = item
            };

            foreach (var childItem in item.Items)
            {
                node.Nodes.Add(GetTreeNode(childItem));
            }
            return node;
        }

        private void NodeSelected(object sender, TreeViewEventArgs e)
        {
            var item = e.Node.Tag as ItemViewModel;
            if (item == null) return;
            listBox.Items.Clear();
            foreach (var keyValPair in item.Values)
            {
                listBox.Items.Add(keyValPair.Key);
            }
        }

    }
}
