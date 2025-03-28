namespace Business.Model.Entities;

public class ArtOrganization
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }

    // Address

    public virtual ICollection<User>? Users { get; set; }
    public virtual ICollection<ArtEvent>? ArtEvents { get; set; }
}