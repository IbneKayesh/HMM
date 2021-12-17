using HMM.AppGUI;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HMM.AioUtility
{
    public static class AioControls
    {
        public static void LoadComboBox(UltraCombo comboBox, DataTable dataTable, string value, string display, bool toggle = false)
        {
            comboBox.DataSource = null;
            if (dataTable.Rows.Count > 0)
            {
                comboBox.DataSource = dataTable;
                if (comboBox.Rows[0].Cells.Count == 1)
                {
                    comboBox.Value = comboBox.Rows[0].Cells[0].Value;
                }
                else
                {
                    comboBox.Value = comboBox.Rows[0].Cells[0].Value;
                }
            }
            comboBox.ValueMember = value;
            comboBox.DisplayMember = display;
            //comboBox.SelectedText = comboBox.Rows[0].Cells[defaultValue].Text;
            if (toggle)
            {
                comboBox.ToggleDropdown();
            }
        }


        public static void SetToolTip(Form frm, string controlEqualcaption)
        {
            string[] ctrls = controlEqualcaption.Split(',');
            foreach (string ctrl in ctrls)
            {
                ToolTip toolTip1 = new ToolTip();
                string[] ctl = ctrl.Split('=');
                Control ct = (Control)frm.Controls.Find(ctl[0], true)[0];
                // Set up the delays for the ToolTip.
                toolTip1.AutoPopDelay = 3000;
                toolTip1.InitialDelay = 1000;
                toolTip1.ReshowDelay = 500;
                // Force the ToolTip text to be displayed whether or not the form is active.
                toolTip1.ShowAlways = true;
                toolTip1.IsBalloon = true;
                toolTip1.ToolTipIcon = ToolTipIcon.Info;
                // Set up the ToolTip text for the Button and Checkbox.
                string[] c = ctl[1].Split('/');
                toolTip1.SetToolTip(ct, c[1]);
                toolTip1.ToolTipTitle = c[0];
            }
        }
        public static void SetToolTip(MetroForm frm, string controlEqualcaption)
        {
            string[] ctrls = controlEqualcaption.Split(',');
            foreach (string ctrl in ctrls)
            {
                ToolTip toolTip1 = new ToolTip();
                string[] ctl = ctrl.Split('=');
                Control ct = (Control)frm.Controls.Find(ctl[0], true)[0];
                // Set up the delays for the ToolTip.
                toolTip1.AutoPopDelay = 3000;
                toolTip1.InitialDelay = 1000;
                toolTip1.ReshowDelay = 500;
                // Force the ToolTip text to be displayed whether or not the form is active.
                toolTip1.ShowAlways = true;
                toolTip1.IsBalloon = true;
                toolTip1.ToolTipIcon = ToolTipIcon.Info;
                // Set up the ToolTip text for the Button and Checkbox.
                string[] c = ctl[1].Split('/');
                toolTip1.SetToolTip(ct, c[1]);
                toolTip1.ToolTipTitle = c[0];
            }
        }

        //txt UltraTextEditor
        public static void NumberOnly(Form frm, string controlsUltraTextEditorEqualLenth)
        {
            //  textBox.KeyPress += textBoxControl_KeyPress;
            string[] ctrls = controlsUltraTextEditorEqualLenth.Split(',');
            foreach (string ctrl in ctrls)
            {
                string[] ctl = ctrl.Split('=');
                UltraTextEditor textBox = (UltraTextEditor)frm.Controls.Find(ctl[0], true)[0];
                textBox.KeyUp += (sender, e) =>
                {
                    double dVal;
                    if (!double.TryParse(textBox.Text, out dVal) || textBox.Text.Length > Convert.ToInt32(ctl[1]))//99 lack
                    {
                        textBox.Text = "";
                    }
                };
            }
        }

        //txt UltraTextEditor
        //grd as UltraGrid
        //cmb as UltraCombo
        public static void ClearControls(Form frm, string controls)
        {
            string[] ctrls = controls.Split(',');
            foreach (string ctrl in ctrls)
            {
                if (ctrl.StartsWith("txt"))
                {
                    UltraTextEditor textBox = (UltraTextEditor)frm.Controls.Find(ctrl, true)[0];
                    textBox.Text = "";
                }
                else if (ctrl.StartsWith("grd"))
                {
                    UltraGrid grid = (UltraGrid)frm.Controls.Find(ctrl, true)[0];
                    if (grid != null)
                    {
                        foreach (UltraGridRow dr in grid.Rows.All)
                        {
                            dr.Delete(false);
                        }
                    }
                }
                else if (ctrl.StartsWith("cmb"))
                {
                    UltraCombo cmb = (UltraCombo)frm.Controls.Find(ctrl, true)[0];
                    if (cmb != null)
                    {
                        cmb.DataSource = null;
                    }
                }
                else if (ctrl.StartsWith("chk"))
                {
                    UltraCheckEditor chk = (UltraCheckEditor)frm.Controls.Find(ctrl, true)[0];
                    if (chk != null)
                    {
                        chk.Checked = false;
                    }
                }
            }
        }


        public static void SavePButtonBeforeAction(Form frm, PictureBox pbtn, bool Before = false, bool After = false)
        {
            if (Before)
            {
                pbtn.Enabled = true;
                //pbtn.BackgroundImage = Properties.Resources.ok48;
            }
            else if (After)
            {
                //pbtn.BackgroundImage = Properties.Resources.saved48;
                pbtn.Enabled = false;
            }
        }
        public static void SavePButtonBeforeAction(Form frm, MetroButton mbtn, bool Before = false, bool After = false)
        {
            if (Before)
            {
                mbtn.Enabled = true;
            }
            else if (After)
            {
                mbtn.Enabled = false;
            }
        }
        public static void SetDefaultValueTextBoxControls(Form frm, string controlsUltraTextEditorEqualValue)
        {
            string[] ctrls = controlsUltraTextEditorEqualValue.Split(',');
            foreach (string ctrl in ctrls)
            {
                string[] ctl = ctrl.Split('=');
                UltraTextEditor textBox = (UltraTextEditor)frm.Controls.Find(ctl[0], true)[0];
                textBox.Text = ctl[1];
            }
        }
        //txt UltraTextEditor
        public static bool TextBoxIsNullOrWhiteSpace(Form frm, string controlsUltraTextEditor)
        {
            string[] ctrls = controlsUltraTextEditor.Split(',');
            foreach (string ctrl in ctrls)
            {
                UltraTextEditor textBox = (UltraTextEditor)frm.Controls.Find(ctrl, true)[0];
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    return true;
                }
            }
            return false;
        }
        public static void TextBoxReadOnly(Form frm, string controlsUltraTextEditor, bool ReadOnly = true)
        {
            string[] ctrls = controlsUltraTextEditor.Split(',');
            foreach (string ctrl in ctrls)
            {
                UltraTextEditor textBox = (UltraTextEditor)frm.Controls.Find(ctrl, true)[0];
                if (ReadOnly)
                {
                    textBox.ReadOnly = true;
                }
                else
                {
                    textBox.ReadOnly = false;
                }
            }
        }
        //txt UltraTextEditor
        public static bool UltraComboActiveRowNull(Form frm, string controlUltraCombo)
        {
            string[] ctrls = controlUltraCombo.Split(',');
            foreach (string ctrl in ctrls)
            {
                UltraCombo cmb = (UltraCombo)frm.Controls.Find(ctrl, true)[0];
                if (cmb.ActiveRow == null)
                {
                    return true;
                }
            }
            return false;
        }

        public static string TxtFilter(UltraTextEditor txtBox)
        {
            return txtBox.Text.Trim().Replace("'", "");
        }
        public static double TxtNumber(UltraTextEditor txtBox)
        {
            double dbl = 0;
            double.TryParse(txtBox.Text.Trim().Replace("'", ""), out dbl);
            return dbl;
        }

        public static void ShowNavigation(Form frm)
        {
            frmMDI parent = (frmMDI)frm.MdiParent;
            parent.tsslNavigation.Text = $"Navigation:{AioData.ModuleName} > {frm.Text}";
        }
    }
}