using Business.Model.Data;
using Business.Model.Dtos.ArtOrganizationDtos;
using Business.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ArtOrganizationsController : ControllerBase
{
    private readonly ArtBookingDbContext _dbContext;

    public ArtOrganizationsController(ArtBookingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<ReadArtOrganizationDto>> CreateOrganization([FromBody] CreateArtOrganizationDto createOrganizationDto, CancellationToken cancellationToken)
    {
        try
        {
            var organization = new ArtOrganization
            {
                Name = createOrganizationDto.Name,
                Description = createOrganizationDto.Description,
                Email = createOrganizationDto.Email,
            };

            await _dbContext.AddAsync(organization, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var readOrganizationDto = MapToReadArtOrganizationDto(organization);

            return CreatedAtAction(nameof(CreateOrganization), new { organization.Id }, readOrganizationDto);
        }
        catch (Exception ex)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occured",
                // just for debugging purposes
                detail: ex.Message
            );
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReadArtOrganizationDto>> GetOrganization(int id, CancellationToken cancellationToken)
    {
        try
        {
            var organization = await _dbContext.ArtOrganizations.FindAsync(id, cancellationToken);

            if (organization == null) return Problem(
                statusCode: 404,
                title: "Organization cannot be found",
                detail: $"Organization with id:{id} cannot be found!"
            );

            var readOrganizationDto = MapToReadArtOrganizationDto(organization);

            return Ok(readOrganizationDto);
        }
        catch (Exception ex)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occured",
                // just for debugging purposes
                detail: ex.Message
            );
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<ReadArtOrganizationDto>>> GetOrganizations(CancellationToken cancellationToken)
    {
        try
        {
            var organizations = await _dbContext.ArtOrganizations.ToListAsync(cancellationToken);
            var readOrganizationDtos = organizations.ConvertAll(MapToReadArtOrganizationDto);

            return Ok(readOrganizationDtos);
        }
        catch (Exception ex)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occured",
                // just for debugging purposes
                detail: ex.Message
            );
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReadArtOrganizationDto>> UpdateOrganization(int id, [FromBody] UpdateArtOrganizationDto organizationUpdateDto, CancellationToken cancellationToken)
    {
        try
        {
            var organization = await _dbContext.ArtOrganizations.FindAsync(id, cancellationToken);

            if (organization == null) return Problem(
                statusCode: 404,
                title: "Organization cannot be found",
                detail: $"Organization with id:{id} cannot be found!"
            );

            organization.Description = organizationUpdateDto.Description;
            organization.Name = organizationUpdateDto.Name;
            organization.Email = organizationUpdateDto.Email;

            await _dbContext.SaveChangesAsync(cancellationToken);

            var readOrganizationDto = MapToReadArtOrganizationDto(organization);

            return Ok(readOrganizationDto);
        }
        catch (Exception ex)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occured",
                // just for debugging purposes
                detail: ex.Message
            );
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrganization(int id, CancellationToken cancellationToken)
    {
        try
        {
            var organization = await _dbContext.ArtOrganizations.FindAsync(id, cancellationToken);
            if (organization == null) return Problem(
                statusCode: 404,
                title: "Organization cannot be found",
                detail: $"Organization with id:{id} cannot be found!"
            );

            _dbContext.Remove(organization);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(
                statusCode: 500,
                title: "An unexpected error occured",
                // just for debugging purposes
                detail: ex.Message
            );
        }
    }
    private ReadArtOrganizationDto MapToReadArtOrganizationDto(ArtOrganization organization)
    {
        return new ReadArtOrganizationDto
        {
            Id = organization.Id,
            Name = organization.Name,
            Description = organization.Description,
            Email = organization.Email,
            Users = organization.Users
        };
    }
}