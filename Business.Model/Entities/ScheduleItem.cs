namespace Business.Model.Entities
{
    public class ScheduleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int? ArtEventId { get; set; }
        public ArtEvent? ArtEvent { get; set; }
        public int? VenueId { get; set; }
        public Venue? Venue { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
    }
}
