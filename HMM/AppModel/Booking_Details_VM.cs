using System.ComponentModel;

namespace HMM.AppModel
{
    public class Booking_Details_VM
    {
        public string Booking_Id { get; set; }
        public int Service_Id { get; set; }
        [DisplayName("Service")]
        public string Service_Name { get; set; }
        [DisplayName("Qty")]
        public decimal Service_Qty { get; set; }
        [DisplayName("Rate")]
        public decimal Service_Rate { get; set; }
        [DisplayName("Total")]
        public decimal Total_Rate { get; set; }
        [DisplayName("Note")]
        public string Booking_Desc { get; set; }
        [DisplayName("Type")]
        public string Charge_Type { get; set; }
    }
}
