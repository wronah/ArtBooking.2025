namespace Business.Model.Entities
{
    public class PriceList
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public int? ArtEventId { get; set; }
        public ArtEvent? ArtEvent { get; set; }
        public int? VenueId { get; set; }
        public Venue? Venue { get; set; }
        public virtual ICollection<PriceEntry>? PriceEntries { get; set; }
    }
}
