namespace Business.Model.Entities
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? PriceListId { get; set; }
        public PriceList? PriceList { get; set; }
        public virtual ICollection<ScheduleItem>? ScheduleItems { get; set; }
        public virtual ICollection<Area>? Areas { get; set; }
        public virtual ICollection<ArtEvent>? ArtEvents { get; set; }
    }
}
