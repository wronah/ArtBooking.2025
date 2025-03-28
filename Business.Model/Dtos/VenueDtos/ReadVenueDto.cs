using Business.Model.Entities;

namespace Business.Model.Dtos.VenueDtos
{
    public class ReadVenueDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? PriceListId { get; set; }
        public virtual ICollection<ScheduleItem>? ScheduleItems { get; set; }
        public virtual ICollection<Area>? Areas { get; set; }
        public virtual ICollection<ArtEvent>? ArtEvents { get; set; }
    }
}
