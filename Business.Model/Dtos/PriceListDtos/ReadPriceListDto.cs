using Business.Model.Entities;

namespace Business.Model.Dtos.PriceListDtos
{
    public class ReadPriceListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ArtEventId { get; set; }
        public int? VenueId { get; set; }
        public virtual ICollection<PriceEntry>? PriceEntries { get; set; }
    }
}
