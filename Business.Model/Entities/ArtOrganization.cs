namespace Business.Model.Entities;

public class ArtOrganization
{
    public int ArtOrganizationId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }

    // Address

    public virtual ICollection<User>? Users { get; set; }
}