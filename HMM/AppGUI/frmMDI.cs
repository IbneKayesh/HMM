using Aio;
using System;
using System.Data;
using System.Deployment.Application;
using System.Reflection;
using System.Windows.Forms;

namespace HMM.AppGUI
{
    public partial class frmMDI : Form
    {
        private int childFormNumber = 0;

        public frmMDI()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }
        public string CurrentVersion
        {
            get
            {
                return ApplicationDeployment.IsNetworkDeployed
                       ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                       : Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;
            bool is_open = false;
            foreach (Form frm in fc)
            {
                if (frm.Name == name)
                {
                    frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                    return true;
                }
            }
            return is_open;
        }

     


        private void dBAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckOpened("frmDBA"))
            {
                frmDBA frmShow = new frmDBA();
                frmShow.MdiParent = this;
                frmShow.Icon = this.Icon;
                frmShow.Text = "DBA";
                frmShow.MaximizeBox = false;
                frmShow.MinimizeBox = false;
                frmShow.StartPosition = FormStartPosition.CenterScreen;
                frmShow.WindowState = FormWindowState.Normal;
                frmShow.Show();
            }
        }
      
        private void bedRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckOpened("frmBedRate"))
            {
                frmBedRate frmShow = new frmBedRate();
                frmShow.MdiParent = this;
                frmShow.Icon = this.Icon;
                frmShow.Text = "Bed Rate";
                frmShow.MaximizeBox = false;
                frmShow.MinimizeBox = true;
                frmShow.StartPosition = FormStartPosition.CenterScreen;
                frmShow.WindowState = FormWindowState.Normal;
                frmShow.Show();
            }
        }
    }
}
