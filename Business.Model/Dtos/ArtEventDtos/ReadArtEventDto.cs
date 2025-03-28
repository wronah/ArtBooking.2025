using Business.Model.Entities;

namespace Business.Model.Dtos.ArtEventDtos
{
    public class ReadArtEventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int? ArtOrganizationId { get; set; }
        public ICollection<ScheduleItem>? ScheduleItems { get; set; }
    }
}
