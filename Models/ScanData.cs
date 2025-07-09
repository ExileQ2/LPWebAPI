namespace LPWebAPI.Models
{
    public class ScanData
    {
        public string Jobdetail { get; set; }
        public int PerID { get; set; }
        public string Name { get; set; }
        public int Mno { get; set; }
        public string Partno { get; set; }
        public int JobPhase { get; set; }
        public bool SetM { get; set; }
        public int Pass { get; set; }
        public int Fail { get; set; }
        public int Rework { get; set; }
        public int? CheckQuant { get; set; }
        public string Seriesno { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TimeCountM { get; set; }
        public int CycleTimeM { get; set; }
        public double? Efficiency { get; set; }
        public string Workno { get; set; }
        public string ProductOrder { get; set; }
        public string Note { get; set; }
        public string Company { get; set; }
        public string Groupname { get; set; }
        public string PhaseName { get; set; }
        public bool ReworkBit { get; set; }
        public string MachineCode { get; set; }
    }
}
