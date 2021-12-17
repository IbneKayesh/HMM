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
    public partial class frmBedRate : MetroForm
    {
        public frmBedRate()
        {
            InitializeComponent();
        }

        private void frmBedRate_Load(object sender, EventArgs e)
        {
            Initialize_Controls();
            Initialize_DropdownList();
            dteStartDate.Value = DateTime.Now;
            dteEndDate.Value = DateTime.Now.AddYears(1);
        }
        private void Initialize_Controls()
        {
            //set baloon tooltip text
            // AioControls.SetToolTip(this, "mbtnClear=Clear Form/F2,mbtnSave=Save/F5,mbtnFind=Find/F3");
            //set number only text box
            AioControls.NumberOnly(this, "txtRate=5,txtMRate=10");
            //AioControls.TextBoxReadOnly(this, "txtStatus", ReadOnly: true);
            //progressar hide
            pBar.Hide();
        }
        private async void Initialize_DropdownList()
        {
            pBar.Show();
            Tuple<DataTable, string> _tpl = FilterDataTable.Filter(await CommonDal.getFloor());
            if (_tpl.Item2 == AppKeys.PostSuccess)
            {
                AioControls.LoadComboBox(cmbFloors, _tpl.Item1, "FLOOR_ID", "FLOOR_NAME");
            }
            else
            {
                MsgBox.Failed(_tpl.Item2, this.Text);
            }
            pBar.Hide();
        }

        private void txtRate_Leave(object sender, EventArgs e)
        {
            try
            {
                txtMRate.Text = (Convert.ToDecimal(txtRate.Text) * 30.5M).ToString("0.00");
            }
            catch { }
        }

        private void txtMRate_Leave(object sender, EventArgs e)
        {
            try
            {
                txtRate.Text = (Convert.ToDecimal(txtMRate.Text) / 30.5M).ToString("0.00");
            }
            catch { }
        }

        #region layout
        private void cmbFloors_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                e.Layout.Bands[0].Columns[0].Hidden = true;
                e.Layout.Bands[0].Columns[1].Header.Caption = "Floor Name";
                e.Layout.Bands[0].Columns[1].Width = this.cmbFloors.Width;
            }
            catch { }
        }
        private void cmbRooms_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                e.Layout.Bands[0].Columns[0].Hidden = true;
                e.Layout.Bands[0].Columns[1].Header.Caption = "Room Name";
                e.Layout.Bands[0].Columns[1].Width = this.cmbRooms.Width;
            }
            catch { }
        }

        private void cmbBeds_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                e.Layout.Bands[0].Columns[0].Hidden = true;
                e.Layout.Bands[0].Columns[1].Header.Caption = "Bed Name";
                e.Layout.Bands[0].Columns[1].Width = this.cmbBeds.Width;
            }
            catch { }
        }

        private void grdDetails_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Columns[0].Header.Caption = "Id";
            e.Layout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[1].Header.Caption = "Bed Name";
            e.Layout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[2].Header.Caption = "Start Date";
            e.Layout.Bands[0].Columns[2].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[3].Header.Caption = "End Date";
            e.Layout.Bands[0].Columns[3].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[4].Header.Caption = "Daily Rate";
            e.Layout.Bands[0].Columns[4].CellActivation = Activation.NoEdit;
        }
        #endregion
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (AioControls.TextBoxIsNullOrWhiteSpace(this, "txtRate"))
            {
                MsgBox.Required("Rate", this.Text);
                return;
            }

            if ((dteEndDate.DateTime-dteStartDate.DateTime).Days < 0)
            {
                MsgBox.Failed("End date can not be Back then Start", this.Text);
                return;
            }

            if (MsgBox.Confirm(this.Text) == DialogResult.Yes)
            {
                AioControls.SavePButtonBeforeAction(this, btnSave, After: true);
                pBar.Show();
                List<string> sqlList = new List<string>();

                string sql_1 = $@"select t.Bed_Rate_Id from Bed_Rate t where t.Is_Active=1
						and (t.Start_Date>='{dteStartDate.DateTime.ToString("dd-MMM-yyyy")}'
						or t.End_Date <='{dteEndDate.DateTime.ToString("dd-MMM-yyyy")}' )
                        and t.Bed_Id={cmbBeds.ActiveRow.Cells[0].Value}";

                Tuple<DataTable, string> _tpl = FilterDataTable.Filter(await AioDLL.GetAsDataTableSqlDB(sql_1));
                if (_tpl.Item2 == AppKeys.PostSuccess)
                {
                    if (_tpl.Item1.Rows.Count > 0)
                    {
                        if (MsgBox.Dialog("Already added, Do you want to update?", this.Text) == DialogResult.No)
                        {
                            AioControls.SavePButtonBeforeAction(this, btnSave, Before: true);
                            pBar.Hide();
                            return;
                        }
                        sqlList.Add($@"update Bed_Rate Set End_Date='{dteEndDate.DateTime.ToString("dd-MMM-yyyy")}',Daily_Rate={AioControls.TxtNumber(txtRate)},
                                Create_By='{AioData.UserId}',Create_Dev='{AioData.DevicesId}' where Bed_Rate_Id={_tpl.Item1.Rows[0][0]}");
                    }
                    else
                    {
                        sqlList.Add($"insert into Bed_Rate(Bed_Rate_Id,Bed_Id,Start_Date,End_Date,Daily_Rate,Create_By,Create_Dev)values('{Bed_Rate_Id()}',{cmbBeds.ActiveRow.Cells[0].Value},'{dteStartDate.DateTime.ToString("dd-MMM-yyyy")}','{dteEndDate.DateTime.ToString("dd-MMM-yyyy")}',{AioControls.TxtNumber(txtRate)},'{AioData.UserId}','{AioData.DevicesId}')");
                    }
                }
                string pst = await AioDLL.PostAsSqlListSqlDB(sqlList);
                if (AppKeys.PostSuccess == pst)
                {
                    MsgBox.Success(this.Text);
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

        private async void cmbFloors_ValueChanged(object sender, EventArgs e)
        {
            pBar.Show();
            AioControls.ClearControls(this, "cmbRooms,cmbBeds");
            try
            {
                if (cmbFloors.ActiveRow.Index >= 0)
                {
                    Tuple<DataTable, string> _tpl = FilterDataTable.Filter(await CommonDal.getRoomByFloorId(cmbFloors.ActiveRow.Cells[0].Value.ToString()));
                    if (_tpl.Item2 == AppKeys.PostSuccess)
                    {
                        AioControls.LoadComboBox(cmbRooms, _tpl.Item1, "ROOM_ID", "ROOM_NAME", false);
                    }
                    else
                    {
                        MsgBox.Failed(_tpl.Item2, this.Text);
                    }
                    load_Details();
                }
            }
            catch (Exception) { pBar.Hide(); }
            pBar.Hide();
        }

        private async void cmbRooms_ValueChanged(object sender, EventArgs e)
        {

            pBar.Show();
            AioControls.ClearControls(this, "cmbBeds");
            try
            {
                if (cmbRooms.ActiveRow.Index >= 0)
                {
                    Tuple<DataTable, string> _tpl = FilterDataTable.Filter(await CommonDal.getBedByRoomId(cmbRooms.ActiveRow.Cells[0].Value.ToString()));
                    if (_tpl.Item2 == AppKeys.PostSuccess)
                    {
                        AioControls.LoadComboBox(cmbBeds, _tpl.Item1, "BED_ID", "BED_NAME", false);
                    }
                    else
                    {
                        MsgBox.Failed(_tpl.Item2, this.Text);
                    }
                }
            }
            catch (Exception) { pBar.Hide(); }
            pBar.Hide();
        }

        private void frmBedRate_Activated(object sender, EventArgs e)
        {
            AioControls.ShowNavigation(this);
        }

        private string Bed_Rate_Id()
        {
            return dteStartDate.DateTime.Day.ToString() + dteStartDate.DateTime.Month.ToString() + dteStartDate.DateTime.Year.ToString() + cmbBeds.ActiveRow.Cells[0].Value.ToString();
        }

        private async void load_Details()
        {
            Tuple<DataTable, string> _tpl = FilterDataTable.Filter(await CommonDal.getBedRatesByFloorId(cmbFloors.ActiveRow.Cells[0].Value.ToString()));
            if (_tpl.Item2 == AppKeys.PostSuccess)
            {
                grdDetails.DataSource = _tpl.Item1;
            }
            else
            {
                MsgBox.Failed(_tpl.Item2, this.Text);
            }
        }
    }
}
