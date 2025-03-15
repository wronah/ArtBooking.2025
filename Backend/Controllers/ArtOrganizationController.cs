using Business.Model.Data;
using Business.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ArtOrganizationController : ControllerBase
    {
        private readonly ArtBookingDbContext dbContext;

        public ArtOrganizationController(ArtBookingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<ArtOrganization>> CreateArtOrganization(ArtOrganization artOrganization, CancellationToken cancellationToken) 
        {
            await dbContext.AddAsync(artOrganization, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Ok(artOrganization);
        }

        [HttpGet]
        public async Task<ActionResult<ArtOrganization>> GetOrganization(int id, CancellationToken cancellationToken)
        {
            var artOrganization = await dbContext.ArtOrganizations.FindAsync(id, cancellationToken);

            return Ok(artOrganization);
        }
    }
}
