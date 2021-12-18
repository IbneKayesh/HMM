using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMM.AioDal
{
    public static class CommonDal
    {
        public static Task<DataTable> getFloor()
        {
            return AioDLL.GetAsDataTableSqlDB($"SELECT T.FLOOR_ID, T.FLOOR_NAME FROM FLOORS T WHERE T.IS_ACTIVE=1");
        }
        public static Task<DataTable> getRoomByFloorId(string floorId)
        {
            return AioDLL.GetAsDataTableSqlDB($"SELECT T.ROOM_ID,T.ROOM_NAME FROM ROOMS T WHERE IS_ACTIVE=1 AND FLOOR_ID={floorId}");
        }
        public static Task<DataTable> getBedByRoomId(string roomId)
        {
            return AioDLL.GetAsDataTableSqlDB($"SELECT T.BED_ID,T.BED_NAME FROM BEDS T WHERE IS_ACTIVE=1 AND ROOM_ID={roomId}");
        }

        public static Task<DataTable> getBedFreeByRoomId(string roomId)
        {
            return AioDLL.GetAsDataTableSqlDB($"SELECT T.BED_ID,T.BED_NAME FROM BEDS T WHERE IS_ACTIVE=1 AND IS_BOOKED=0 AND ROOM_ID={roomId}");
        }

        public static Task<DataTable> getBedRatesByFloorId(string floorId)
        {
            string joins = "";
            if (!string.IsNullOrWhiteSpace(floorId))
            {
                joins = $@"
                        join Rooms t2 on t1.Room_Id=t2.Room_Id
                        join Floors t3 on t2.Floor_Id=t3.Floor_Id and t3.Floor_Id={floorId}";
            }
            string sql_1 = $@"select t.Bed_Rate_Id,t1.Bed_Name,t.start_Date,t.end_date,t.Daily_Rate
                        from Bed_Rate t
                        join Beds t1 on t.Bed_Id=t1.Bed_Id
                        {joins}
                        where t.Is_Active=1
                        order by t1.Bed_Name";
            return AioDLL.GetAsDataTableSqlDB(sql_1);
        }

        public static Task<DataTable> getGenders()
        {
            return AioDLL.GetAsDataTableSqlDB($"SELECT T.GENDER_ID FROM GENDERS T ORDER BY ID");
        }
        public static Task<DataTable> getReligions()
        {
            return AioDLL.GetAsDataTableSqlDB($"SELECT T.RELIGION_ID FROM RELIGIONS T ORDER BY ID");
        }
        public static Task<DataTable> getRelationships()
        {
            return AioDLL.GetAsDataTableSqlDB($"SELECT T.RELATIONSHIP_ID FROM RELATIONSHIPS T ORDER BY ID");
        }
        public static Task<DataTable> getGuestByPhone(double phoneNo)
        {
            return AioDLL.GetAsDataTableSqlDB($"SELECT GUEST_ID,GUEST_NAME,PHONE_NO,EMAIL_ID,GENDER_ID,RELIGION_ID,RELATIONSHIP_ID,GUEST_AGES,GUEST_ADDRESS,GUEST_DESC,GUEST_GOVID FROM GUESTS WHERE PHONE_NO='{phoneNo}'");
        }
        public static Task<DataTable> getServices(bool is_default = true)
        {
            string criteria = "";
            if (is_default)
            {
                criteria = " AND T.IS_DEFAULT=1";
            }
            return AioDLL.GetAsDataTableSqlDB($"SELECT T.SERVICE_ID,T.SERVICE_NAME,T.SERVICE_CHARGE,T.CHARGE_TYPE FROM SERVICES T WHERE T.IS_ACTIVE=1 {criteria}");
        }

    }
}
