using Business.Model.Enums;

namespace Business.Model.Entities;

public class User
{
    public int Id { get; set; }
    public string LoginName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserRole? Role { get; set; }
    public int? ArtOrganizationId { get; set; }
    public virtual ArtOrganization? ArtOrganization { get; set; }
}
