namespace Business.Model.Entities
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? VenueId { get; set; }
        public Venue? Venue { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
        public virtual ICollection<Seat>? Seats { get; set; }
    }
}
