using Business.Model.Entities;

namespace Business.Model.Dtos.ArtOrganizationDtos
{
    public class ReadArtOrganizationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
