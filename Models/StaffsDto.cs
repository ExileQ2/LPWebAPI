namespace LPWebAPI.Models
{
    /// <summary>Return payload for staff lookup.</summary>
    public class StaffsDto
    {
        public string FullName  { get; set; }
        public string WorkJob   { get; set; }
        public string WorkPlace { get; set; }
    }
}
