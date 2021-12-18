using Aio;
using HMM.AioDal;
using HMM.AioUtility;
using Infragistics.Win.UltraWinGrid;
using MetroFramework.Forms;
using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HMM.AppGUI
{
    public partial class frmGuest : MetroForm
    {
        public frmGuest()
        {
            InitializeComponent();
        }

        private void frmGuest_Load(object sender, EventArgs e)
        {
            Initialize_Controls();
            Initialize_DropdownList();
        }
        private void Initialize_Controls()
        {
            //set baloon tooltip text
            // AioControls.SetToolTip(this, "mbtnClear=Clear Form/F2,mbtnSave=Save/F5,mbtnFind=Find/F3");

            AioControls.NumberOnly(this, "txtPhone=30");
            //progressar hide
            pBar.Hide();
        }
        private async void Initialize_DropdownList()
        {
            pBar.Show();
            Tuple<DataTable, string> _tpl = FilterDataTable.Filter(await CommonDal.getGenders());
            if (_tpl.Item2 == AppKeys.PostSuccess)
            {
                AioControls.LoadComboBox(cmbGenders, _tpl.Item1, "GENDER_ID", "GENDER_ID");
            }
            else
            {
                MsgBox.Failed(_tpl.Item2, this.Text);
            }
            _tpl = FilterDataTable.Filter(await CommonDal.getReligions());
            if (_tpl.Item2 == AppKeys.PostSuccess)
            {
                AioControls.LoadComboBox(cmbReligions, _tpl.Item1, "RELIGION_ID", "RELIGION_ID");
            }
            else
            {
                MsgBox.Failed(_tpl.Item2, this.Text);
            }
            _tpl = FilterDataTable.Filter(await CommonDal.getRelationships());
            if (_tpl.Item2 == AppKeys.PostSuccess)
            {
                AioControls.LoadComboBox(cmbRelationship, _tpl.Item1, "RELATIONSHIP_ID", "RELATIONSHIP_ID");
            }
            else
            {
                MsgBox.Failed(_tpl.Item2, this.Text);
            }
            pBar.Hide();
        }



        #region layout
        private void cmbGenders_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                e.Layout.Bands[0].Columns[0].Header.Caption = "Gender";
                e.Layout.Bands[0].Columns[0].Width = this.cmbGenders.Width;
            }
            catch { }

        }
        private void cmbReligions_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                e.Layout.Bands[0].Columns[0].Header.Caption = "Religion";
                e.Layout.Bands[0].Columns[0].Width = this.cmbReligions.Width;
            }
            catch { }
        }
        private void cmbRelationship_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                e.Layout.Bands[0].Columns[0].Header.Caption = "Relationship";
                e.Layout.Bands[0].Columns[0].Width = this.cmbRelationship.Width;
            }
            catch { }
        }
        private void grdDetails_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Columns[0].Header.Caption = "Id";
            e.Layout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[1].Header.Caption = "Name";
            e.Layout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[2].Header.Caption = "Phone";
            e.Layout.Bands[0].Columns[2].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[3].Header.Caption = "Email";
            e.Layout.Bands[0].Columns[3].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[4].Header.Caption = "Gender";
            e.Layout.Bands[0].Columns[4].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[5].Header.Caption = "Religion";
            e.Layout.Bands[0].Columns[5].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[6].Header.Caption = "Relation";
            e.Layout.Bands[0].Columns[6].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[7].Header.Caption = "Ages";
            e.Layout.Bands[0].Columns[7].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[8].Header.Caption = "Address";
            e.Layout.Bands[0].Columns[8].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[9].Header.Caption = "Notes";
            e.Layout.Bands[0].Columns[9].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[10].Header.Caption = "GovID";
            e.Layout.Bands[0].Columns[10].CellActivation = Activation.NoEdit;
        }
        #endregion
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (AioControls.TextBoxIsNullOrWhiteSpace(this, "txtGuestName,txtPhone,txtAges,txtAddress"))
            {
                MsgBox.Required("Name, Phone, Ages and Address", this.Text);
                return;
            }
            if (AioControls.UltraComboActiveRowNull(this, "cmbGenders,cmbReligions,cmbRelationship"))
            {
                MsgBox.Required("Gender, Religion and Relationship", this.Text);
                return;
            }


            if (MsgBox.Confirm(this.Text) == DialogResult.Yes)
            {
                AioControls.SavePButtonBeforeAction(this, btnSave, After: true);
                pBar.Show();
                string sql_1 = $"select Guest_Name from Guests where Phone_No='{AioControls.TxtFilter(txtPhone)}'";
                Tuple<DataTable, string> _tpl = FilterDataTable.Filter(await AioDLL.GetAsDataTableSqlDB(sql_1));
                if (_tpl.Item2 == AppKeys.PostSuccess)
                {
                    MsgBox.Failed(_tpl.Item1.Rows[0][0].ToString() + " already added", this.Text);
                    return;
                }
                List<string> sqlList = new List<string>();
                sqlList.Add($"insert into Guests(Guest_Id,Guest_Name,Phone_No,Email_Id,Gender_Id,Religion_Id,Relationship_Id,Guest_Ages,Guest_Address,Guest_Desc,Guest_GovID,Guest_Pics,Create_By,Create_Dev)values('{Guest_Id()}','{AioControls.TxtFilter(txtGuestName).ToUpper()}','{AioControls.TxtNumber(txtPhone)}','{AioControls.TxtFilter(txtEmailId)}','{cmbGenders.ActiveRow.Cells[0].Value}','{cmbReligions.ActiveRow.Cells[0].Value}','{cmbRelationship.ActiveRow.Cells[0].Value}','{AioControls.TxtFilter(txtAges)}','{AioControls.TxtFilter(txtAddress)}','{AioControls.TxtFilter(txtDescription)}','{AioControls.TxtFilter(txtNID)}','0','{AioData.UserId}','{AioData.DevicesId}')");

                string pst = await AioDLL.PostAsSqlListSqlDB(sqlList);
                if (AppKeys.PostSuccess == pst)
                {
                    MsgBox.Success(this.Text);
                    txtGuestId.Text = Guest_Id();
                    load_Details();
                }
                else
                {
                    MsgBox.Failed(pst, this.Text);
                }
            }
            AioControls.SavePButtonBeforeAction(this, btnSave, Before: true);
            pBar.Hide();
        }



        private void frmGuest_Activated(object sender, EventArgs e)
        {
            AioControls.ShowNavigation(this);
        }

        private string Guest_Id()
        {
            //return DateTime.Now.Ticks.ToString();
            return AioControls.TxtFilter(txtPhone) + "01";
        }

        private async void load_Details()
        {
            try
            {
                AioControls.ClearControls(this, "grdDetails");
                Tuple<DataTable, string> _tpl = FilterDataTable.Filter(await CommonDal.getGuestByPhone(AioControls.TxtNumber(txtPhone)));
                if (_tpl.Item2 == AppKeys.PostSuccess)
                {
                    grdDetails.DataSource = _tpl.Item1;
                }
                else
                {
                    MsgBox.Failed(_tpl.Item2, this.Text);
                }
            }
            catch (Exception)
            {
                MsgBox.Required("Name", this.Text);
            }
        }

        private void frmGuest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MsgBox.Exit(this.Text) == DialogResult.Yes) { this.Dispose(); } else { e.Cancel = true; }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AioControls.ClearControls(this, "grdDetails,txtGuestName,txtPhone,txtEmailId,txtAges,txtNID,txtAddress,txtDescription");
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            load_Details();
        }


    }
}
