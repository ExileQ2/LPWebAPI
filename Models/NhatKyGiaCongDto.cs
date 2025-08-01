namespace LPWebAPI.Models
{
    public class NhatKyGiaCongDto
    {
        public string ProcessNo { get; set; }
        public string JobControlNo { get; set; }
        public string StaffNo { get; set; }
        public string McName { get; set; }
        public string Note { get; set; }
        public string ProOrdNo { get; set; }
        public string Serial { get; set; }
        public bool setup { get; set; }
        public bool rework { get; set; }
        public int QtyGood { get; set; }
        public int QtyReject { get; set; }
        public int QtyRework { get; set; }
    }
}
