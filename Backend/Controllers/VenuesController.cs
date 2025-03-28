using Business.Model.Data;
using Business.Model.Dtos.VenueDtos;
using Business.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenuesController : ControllerBase
    {
        private readonly ArtBookingDbContext _dbContext;

        public VenuesController(ArtBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<ReadVenueDto>> CreateVenue([FromBody] CreateVenueDto createVenueDto, CancellationToken cancellationToken)
        {
            try
            {
                var venue = new Venue
                {
                    Name = createVenueDto.Name,
                };

                await _dbContext.AddAsync(venue, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                ReadVenueDto readVenueDto = MapToReadVenueDto(venue);

                return CreatedAtAction(nameof(CreateVenue), new { venue.Id }, readVenueDto);
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
        public async Task<ActionResult<ReadVenueDto>> GetVenue(int id, CancellationToken cancellationToken)
        {
            try
            {
                var venue = await _dbContext.Venues.FindAsync(id, cancellationToken);

                if (venue == null) return Problem(
                    statusCode: 404,
                    title: "Venue cannot be found",
                    detail: $"Venue with id:{id} cannot be found!"
                );

                ReadVenueDto readVenueDto = MapToReadVenueDto(venue);

                return Ok(readVenueDto);
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
        public async Task<ActionResult<List<ReadVenueDto>>> GetVenues(CancellationToken cancellationToken)
        {
            try
            {
                var venues = await _dbContext.Venues.ToListAsync(cancellationToken);
                var readVenueDtos = venues.ConvertAll(MapToReadVenueDto);

                return Ok(readVenueDtos);
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
        public async Task<ActionResult<ReadVenueDto>> UpdateVenue(int id, [FromBody] UpdateVenueDto updateVenueDto, CancellationToken cancellationToken)
        {
            try
            {
                var venue = await _dbContext.Venues.FindAsync(id, cancellationToken);
                if (venue == null) return Problem(
                    statusCode: 404,
                    title: "Venue cannot be found",
                    detail: $"Venue with id:{id} cannot be found!"
                );

                venue.Name = updateVenueDto.Name;

                await _dbContext.SaveChangesAsync(cancellationToken);

                ReadVenueDto readVenueDto = MapToReadVenueDto(venue);

                return Ok(readVenueDto);
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

        [HttpPut("{id}/PriceLists/{priceListId}")]
        public async Task<ActionResult> AssignPriceList(int id, int priceListId, CancellationToken cancellationToken)
        {
            try
            {
                var venue = await _dbContext.Venues.FindAsync(id, cancellationToken);

                if (venue == null) return Problem(
                    statusCode: 404,
                    title: "Venue cannot be found",
                    detail: $"Venue with id:{priceListId} cannot be found!"
                );

                var priceList = await _dbContext.PriceLists.FindAsync(priceListId, cancellationToken);

                if (priceList == null) return Problem(
                    statusCode: 404,
                    title: "Price List cannot be found",
                    detail: $"Price List with id:{priceListId} cannot be found!"
                );

                venue.PriceList = priceList;
                venue.PriceListId = priceList.Id;
                priceList.Venue = venue;
                priceList.VenueId = venue.Id;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return Ok();
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
        public async Task<ActionResult> DeleteVenue(int id, CancellationToken cancellationToken)
        {
            try
            {
                var venue = await _dbContext.Venues.FindAsync(id, cancellationToken);
                if (venue == null) return Problem(
                    statusCode: 404,
                    title: "Venue cannot be found",
                    detail: $"Venue with id:{id} cannot be found!"
                );

                _dbContext.Remove(venue);
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

        private ReadVenueDto MapToReadVenueDto(Venue venue)
        {
            return new ReadVenueDto
            {
                Id = venue.Id,
                Name = venue.Name,
                ScheduleItems = venue.ScheduleItems,
                PriceListId = venue.PriceListId,
                Areas = venue.Areas,
                ArtEvents = venue.ArtEvents,
            };
        }
    }
}