using Business.Model.Data;
using Business.Model.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ArtOrganizationController : ControllerBase
{
    private readonly ArtBookingDbContext _dbContext;

    public ArtOrganizationController(ArtBookingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public ActionResult<ArtOrganization> CreateOrganization(ArtOrganization organization)
    {
        try
        {
            _dbContext.Add(organization);
            _dbContext.SaveChanges();
        }
        catch (Exception exp)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occured",
                // just for debugging purposes
                detail: exp.Message
            );
        }

        return CreatedAtAction(nameof(CreateOrganization), new { organization.ArtOrganizationId }, organization);
    }

    [HttpGet]
    public ActionResult<ArtOrganization> GetOrganization(int id)
    {
        try
        {
            var organization = _dbContext.ArtOrganizations.Find(id);

            if (organization == null) return Problem(
                statusCode: 404,
                title: "Organization cannot be found",
                detail: $"Organization with id:{id} cannot be found!"
            );
            return Ok(organization);
        }
        catch (Exception exp)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occured",
                // just for debugging purposes
                detail: exp.Message
            );
        }
    }
}