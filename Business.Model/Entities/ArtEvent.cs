namespace Business.Model.Entities
{
    public class ArtEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public int? ArtOrganizationId { get; set; }
        public ArtOrganization? ArtOrganization { get; set; }
        public virtual ICollection<ScheduleItem>? ScheduleItems { get; set; }
    }
}