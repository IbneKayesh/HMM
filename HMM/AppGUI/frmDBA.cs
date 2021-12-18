using Aio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HMM.AppGUI
{
    public partial class frmDBA : Form
    {
        public frmDBA()
        {
            InitializeComponent();
        }

        private void btnInitiate_Click(object sender, EventArgs e)
        {
            Insert_Hostel();
            Insert_Floors();
            Insert_Rooms();
            Insert_Beds();
            Insert_Gender_Religion_Relationship();
            Insert_Services();
        }
        #region Hostel
        private async void Insert_Hostel()
        {
            string sql_1 = $"insert into hostel(Hostel_Name,Owner_Name,Phone_No,Email_Id,Address_Desc,Create_By,Create_Dev)values('HMM','Ibne Kayesh','01722688266','ibnekayesh91@gmail.com','Bogra,Dhaka,Bangladesh','{AioData.UserId}','{AioData.DevicesId}')";

            string pst = await AioDLL.PostAsSqlSqlDB(sql_1);
            if (AppKeys.PostSuccess == pst)
            {
                MsgBox.Success(this.Text);
            }
            else
            {
                MsgBox.Failed(pst, this.Text);
            }
        }
        private async void Select_Hostel()
        {
            string sql_1 = $"select * from hostel";

            DataTable dt =await AioDLL.GetAsDataTableSqlDB(sql_1);

            Tuple<DataTable, string> _tpl = FilterDataTable.Filter(dt);
            if (_tpl.Item2 == AppKeys.PostSuccess)
            {
                MsgBox.Success(this.Text);
            }
            else
            {
                MsgBox.Failed(_tpl.Item2, this.Text);
            }
        }
        #endregion

        #region Floors
        private async void Insert_Floors()
        {
            List<string> sqlList = new List<string>();
            sqlList.Add($"insert into Floors(Hostel_Id,Floor_Name,Floor_Desc,Create_By,Create_Dev)values('1','1st Floor','First Floor','{AioData.UserId}','{AioData.DevicesId}')");
            sqlList.Add($"insert into Floors(Hostel_Id,Floor_Name,Floor_Desc,Create_By,Create_Dev)values('1','2nd Floor','Second Floor','{AioData.UserId}','{AioData.DevicesId}')");
            string pst = await AioDLL.PostAsSqlListSqlDB(sqlList);
            if (AppKeys.PostSuccess == pst)
            {
                MsgBox.Success(this.Text);
            }
            else
            {
                MsgBox.Failed(pst, this.Text);
            }
        }
        private async void Select_Floors()
        {
            string sql_1 = $"select * from floors";

            DataTable dt = await AioDLL.GetAsDataTableSqlDB(sql_1);

            Tuple<DataTable, string> _tpl = FilterDataTable.Filter(dt);
            if (_tpl.Item2 == AppKeys.PostSuccess)
            {
                MsgBox.Success(this.Text);
            }
            else
            {
                MsgBox.Failed(_tpl.Item2, this.Text);
            }
        }
        #endregion

        #region Rooms
        private async void Insert_Rooms()
        {
            List<string> sqlList = new List<string>();
            sqlList.Add($"insert into Rooms(Floor_Id,Room_Name,Room_Capacity,Room_Desc,Create_By,Create_Dev)values('1','101',2,'First Floor','{AioData.UserId}','{AioData.DevicesId}')");
            sqlList.Add($"insert into Rooms(Floor_Id,Room_Name,Room_Capacity,Room_Desc,Create_By,Create_Dev)values('1','102',3,'First Floor','{AioData.UserId}','{AioData.DevicesId}')");
            string pst = await AioDLL.PostAsSqlListSqlDB(sqlList);
            if (AppKeys.PostSuccess == pst)
            {
                MsgBox.Success(this.Text);
            }
            else
            {
                MsgBox.Failed(pst, this.Text);
            }
        }
        private  async void Select_Rooms()
        {
            string sql_1 = $"select * from rooms";

            DataTable dt = await AioDLL.GetAsDataTableSqlDB(sql_1);

            Tuple<DataTable, string> _tpl = FilterDataTable.Filter(dt);
            if (_tpl.Item2 == AppKeys.PostSuccess)
            {
                MsgBox.Success(this.Text);
            }
            else
            {
                MsgBox.Failed(_tpl.Item2, this.Text);
            }
        }
        #endregion

        #region Beds
        private async void Insert_Beds()
        {
            List<string> sqlList = new List<string>();
            sqlList.Add($"insert into Beds(Room_Id,Bed_Name,Bed_Desc,Is_Booked,Create_By,Create_Dev)values('1','101-1','Bed 1, Room 1, First Floor',0,'{AioData.UserId}','{AioData.DevicesId}')");
            sqlList.Add($"insert into Beds(Room_Id,Bed_Name,Bed_Desc,Is_Booked,Create_By,Create_Dev)values('1','101-2','Bed 2, Room 1, First Floor',0,'{AioData.UserId}','{AioData.DevicesId}')");


            sqlList.Add($"insert into Beds(Room_Id,Bed_Name,Bed_Desc,Is_Booked,Create_By,Create_Dev)values('2','102-1','Bed 1, Room 2, First Floor',0,'{AioData.UserId}','{AioData.DevicesId}')");
            sqlList.Add($"insert into Beds(Room_Id,Bed_Name,Bed_Desc,Is_Booked,Create_By,Create_Dev)values('2','102-2','Bed 2, Room 2, First Floor',0,'{AioData.UserId}','{AioData.DevicesId}')");
            sqlList.Add($"insert into Beds(Room_Id,Bed_Name,Bed_Desc,Is_Booked,Create_By,Create_Dev)values('2','102-3','Bed 3, Room 2, First Floor',0,'{AioData.UserId}','{AioData.DevicesId}')");

            string pst = await AioDLL.PostAsSqlListSqlDB(sqlList);
            if (AppKeys.PostSuccess == pst)
            {
                MsgBox.Success(this.Text);
            }
            else
            {
                MsgBox.Failed(pst, this.Text);
            }
        }
        private async void Select_Beds()
        {
            string sql_1 = $"select * from beds";

            DataTable dt =await AioDLL.GetAsDataTableSqlDB(sql_1);

            Tuple<DataTable, string> _tpl = FilterDataTable.Filter(dt);
            if (_tpl.Item2 == AppKeys.PostSuccess)
            {
                MsgBox.Success(this.Text);
            }
            else
            {
                MsgBox.Failed(_tpl.Item2, this.Text);
            }
        }
        #endregion


        #region Gender_Religion_Relationship
        private async void Insert_Gender_Religion_Relationship()
        {
            List<string> sqlList = new List<string>();
            sqlList.Add($"insert into Genders(Gender_Id,ID)values('Male',1)");
            sqlList.Add($"insert into Genders(Gender_Id,ID)values('Female',2)");
            sqlList.Add($"insert into Genders(Gender_Id,ID)values('Unknown',3)");

            sqlList.Add($"insert into Religions(Religion_Id,ID)values('Unknown',1)");
            sqlList.Add($"insert into Religions(Religion_Id,ID)values('Islam',2)");
            sqlList.Add($"insert into Religions(Religion_Id,ID)values('Hindu',3)");
            sqlList.Add($"insert into Religions(Religion_Id,ID)values('Christian',4)");
            sqlList.Add($"insert into Religions(Religion_Id,ID)values('Others',5)");

            sqlList.Add($"insert into Relationships(Relationship_Id,ID)values('Unmarried',1)");
            sqlList.Add($"insert into Relationships(Relationship_Id,ID)values('Married',2)");
            sqlList.Add($"insert into Relationships(Relationship_Id,ID)values('Divorced',3)");
            sqlList.Add($"insert into Relationships(Relationship_Id,ID)values('Unknown',4)");

            string pst = await AioDLL.PostAsSqlListSqlDB(sqlList);
            if (AppKeys.PostSuccess == pst)
            {
                MsgBox.Success(this.Text);
            }
            else
            {
                MsgBox.Failed(pst, this.Text);
            }
        }
        #endregion

        #region Beds
        private async void Insert_Services()
        {
            List<string> sqlList = new List<string>();
            sqlList.Add($"insert into Services(Service_Id,Service_Name,Service_Charge,Charge_Type,Create_By,Create_Dev)values(1,'Rent',0,'NotFixed','{AioData.UserId}','{AioData.DevicesId}')");
            sqlList.Add($"insert into Services(Service_Id,Service_Name,Service_Charge,Charge_Type,Create_By,Create_Dev)values(2,'Services Charge',500,'Monthly','{AioData.UserId}','{AioData.DevicesId}')");
            sqlList.Add($"insert into Services(Service_Id,Service_Name,Service_Charge,Charge_Type,Create_By,Create_Dev)values(3,'Cooker',20,'Daily','{AioData.UserId}','{AioData.DevicesId}')");
            sqlList.Add($"insert into Services(Service_Id,Service_Name,Service_Charge,Charge_Type,Create_By,Create_Dev)values(4,'Electricity',0,'NotFixed','{AioData.UserId}','{AioData.DevicesId}')");
            sqlList.Add($"insert into Services(Service_Id,Service_Name,Service_Charge,Charge_Type,Create_By,Create_Dev)values(5,'Gas',750,'Monthly','{AioData.UserId}','{AioData.DevicesId}')");
            string pst = await AioDLL.PostAsSqlListSqlDB(sqlList);
            if (AppKeys.PostSuccess == pst)
            {
                MsgBox.Success(this.Text);
            }
            else
            {
                MsgBox.Failed(pst, this.Text);
            }
        }
        #endregion


        private async void btnSelect_Click(object sender, EventArgs e)
        {
            grdDetails.DataSource = null;
            grdDetails.DataSource = await AioDLL.GetAsDataTableSqlDB(txtSQL.Text);
        }

        private async void btnIUD_Click(object sender, EventArgs e)
        {
            string pst = await AioDLL.PostAsSqlSqlDB(txtSQL.Text);
        }
    }
}