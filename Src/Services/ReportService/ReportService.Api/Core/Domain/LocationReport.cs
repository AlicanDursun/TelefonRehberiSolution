namespace ReportService.Api.Core.Domain
{
    public class LocationReport
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }
}
