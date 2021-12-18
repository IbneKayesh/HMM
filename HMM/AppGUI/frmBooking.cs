using Aio;
using HMM.AioDal;
using HMM.AioUtility;
using Infragistics.Win.UltraWinGrid;
using MetroFramework.Forms;
using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using HMM.AppModel;

namespace HMM.AppGUI
{
    public partial class frmBooking : MetroForm
    {
        public frmBooking()
        {
            InitializeComponent();
        }

        private void frmBooking_Load(object sender, EventArgs e)
        {
            Initialize_Controls();
            Initialize_DropdownList();
            dteStartDate.Value = DateTime.Now;
        }
        private void Initialize_Controls()
        {
            //set baloon tooltip text
            // AioControls.SetToolTip(this, "mbtnClear=Clear Form/F2,mbtnSave=Save/F5,mbtnFind=Find/F3");

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
            e.Layout.Bands[0].Columns[0].Hidden = true;
            e.Layout.Bands[0].Columns[1].Hidden = true;

            e.Layout.Bands[0].Columns[2].Header.Caption = "Service";
            e.Layout.Bands[0].Columns[2].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[3].Header.Caption = "Qty";

            e.Layout.Bands[0].Columns[4].Header.Caption = "Rate";
            //e.Layout.Bands[0].Columns[4].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[5].Header.Caption = "Total";
            e.Layout.Bands[0].Columns[5].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns[6].Header.Caption = "Note";

            e.Layout.Bands[0].Columns[7].Hidden = true;
        }
        #endregion
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (AioControls.TextBoxIsNullOrWhiteSpace(this, "txtPhone,txtGuestName"))
            {
                MsgBox.Required("Rate", this.Text);
                return;
            }



            if (MsgBox.Confirm(this.Text) == DialogResult.Yes)
            {
                AioControls.SavePButtonBeforeAction(this, btnSave, After: true);
                pBar.Show();
                List<string> sqlList = new List<string>();

                string sql_1 = $@"select t.Bed_Rate_Id,t.Start_Date,t.End_Date from Bed_Rate t where t.Is_Active=1
						and (t.Start_Date>='{dteStartDate.DateTime.ToString("dd-MMM-yyyy")}'
						or t.End_Date <='{dteStartDate.DateTime.ToString("dd-MMM-yyyy")}' )
                        and t.Bed_Id={cmbBeds.ActiveRow.Cells[0].Value}";

                Tuple<DataTable, string> _tpl = FilterDataTable.Filter(await AioDLL.GetAsDataTableSqlDB(sql_1));
                if (_tpl.Item2 == AppKeys.PostSuccess)
                {
                    if (_tpl.Item1.Rows.Count > 0)
                    {
                        if (MsgBox.Dialog("Already added rate from " + Convert.ToDateTime(_tpl.Item1.Rows[0][1]).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(_tpl.Item1.Rows[0][2]).ToString("dd-MMM-yyyy")
                    + "\nDo you want to update rate and end date?", this.Text) == DialogResult.No)
                        {
                            AioControls.SavePButtonBeforeAction(this, btnSave, Before: true);
                            pBar.Hide();
                            return;
                        }
                        sqlList.Add($@"update Bed_Rate Set End_Date='{dteStartDate.DateTime.ToString("dd-MMM-yyyy")}',Daily_Rate={AioControls.TxtNumber(txtBookingId)},
                                Create_By='{AioData.UserId}',Create_Dev='{AioData.DevicesId}' where Bed_Rate_Id={_tpl.Item1.Rows[0][0]}");
                    }
                }
                else
                {
                    sqlList.Add($"insert into Bed_Rate(Bed_Rate_Id,Bed_Id,Start_Date,End_Date,Daily_Rate,Create_By,Create_Dev)values('{Booking_Id()}',{cmbBeds.ActiveRow.Cells[0].Value},'{dteStartDate.DateTime.ToString("dd-MMM-yyyy")}','{dteStartDate.DateTime.ToString("dd-MMM-yyyy")}',{AioControls.TxtNumber(txtBookingId)},'{AioData.UserId}','{AioData.DevicesId}')");
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
                    Tuple<DataTable, string> _tpl = FilterDataTable.Filter(await CommonDal.getBedFreeByRoomId(cmbRooms.ActiveRow.Cells[0].Value.ToString()));
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

        private void frmBooking_Activated(object sender, EventArgs e)
        {
            AioControls.ShowNavigation(this);
        }

        private string Booking_Id()
        {
            return dteStartDate.DateTime.Day.ToString() + dteStartDate.DateTime.Month.ToString() + dteStartDate.DateTime.Year.ToString() + AioControls.TxtNumber(txtPhone);
        }

        private async void load_Details()
        {
            try
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
            catch (Exception)
            {
                MsgBox.Required("Floor", this.Text);
            }
        }

        private void frmBooking_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MsgBox.Exit(this.Text) == DialogResult.Yes) { this.Dispose(); } else { e.Cancel = true; }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AioControls.ClearControls(this, "grdDetails");
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            load_Details();
        }

        private async void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = await CommonDal.getGuestByPhone(AioControls.TxtNumber(txtPhone));
                if (dt.Rows.Count == 1)
                {
                    txtGuestId.Text = dt.Rows[0]["GUEST_ID"].ToString();
                    txtGuestName.Text = dt.Rows[0]["GUEST_NAME"].ToString();
                    load_default_services();
                }
                else
                {
                    MsgBox.Failed("Not found", this.Text);
                }
            }
        }
        private async void load_default_services()
        {
            try
            {
                DataTable dt = await CommonDal.getServices();
                if (dt.Rows.Count > 0)
                {
                    List<Booking_Details_VM> objList = new List<Booking_Details_VM>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Booking_Details_VM obj = new Booking_Details_VM();
                        obj.Booking_Id = "0";
                        obj.Service_Id = Convert.ToInt32(row["SERVICE_ID"]);
                        obj.Service_Name = row["SERVICE_NAME"].ToString();
                        obj.Service_Qty = 1;
                        obj.Service_Rate = Convert.ToDecimal(row["SERVICE_CHARGE"]);
                        obj.Total_Rate = obj.Service_Qty * obj.Service_Rate;
                        obj.Booking_Desc = "";
                        obj.Charge_Type = row["CHARGE_TYPE"].ToString();
                        objList.Add(obj);
                    }
                    grdDetails.DataSource = objList;
                }
            }
            catch { }
        }
    }
}
