using Business.Model.Data;
using Business.Model.Dtos.ArtEventDtos;
using Business.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtEventsController : ControllerBase
    {
        private readonly ArtBookingDbContext _dbContext;

        public ArtEventsController(ArtBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<ReadArtEventDto>> CreateEvent([FromBody] CreateArtEventDto eventCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                var artEvent = new ArtEvent
                {
                    Name = eventCreateDto.Name,
                    Date = eventCreateDto.Date,
                };

                await _dbContext.AddAsync(artEvent, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                var eventResponseDto = MapToReadArtEventDto(artEvent);

                return CreatedAtAction(nameof(CreateEvent), new { artEvent.Id }, eventResponseDto);
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
        public async Task<ActionResult<ReadArtEventDto>> GetEvent(int id, CancellationToken cancellationToken)
        {
            try
            {
                var artEvent = await _dbContext.ArtEvents.FindAsync(id, cancellationToken);

                if (artEvent == null) return Problem(
                    statusCode: 404,
                    title: "Event cannot be found",
                    detail: $"Event with id:{id} cannot be found!"
                );

                var eventResponseDto = MapToReadArtEventDto(artEvent);

                return Ok(eventResponseDto);
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
        public async Task<ActionResult<List<ReadArtEventDto>>> GetEvents(CancellationToken cancellationToken)
        {
            try
            {
                var events = await _dbContext.ArtEvents.ToListAsync(cancellationToken);
                var eventResponseDtos = events.ConvertAll(MapToReadArtEventDto);

                return Ok(eventResponseDtos);
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
        public async Task<ActionResult<ReadArtEventDto>> UpdateEvent(int id, [FromBody] UpdateArtEventDto eventUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                var artEvent = await _dbContext.ArtEvents.FindAsync(id, cancellationToken);
                if (artEvent == null) return Problem(
                    statusCode: 404,
                    title: "Event cannot be found",
                    detail: $"Event with id:{id} cannot be found!"
                );

                artEvent.Name = eventUpdateDto.Name;
                artEvent.Date = eventUpdateDto.Date;

                await _dbContext.SaveChangesAsync(cancellationToken);

                var eventResponseDto = MapToReadArtEventDto(artEvent);

                return Ok(eventResponseDto);
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
        public async Task<ActionResult> DeleteEvent(int id, CancellationToken cancellationToken)
        {
            try
            {
                var artEvent = await _dbContext.ArtEvents.FindAsync(id, cancellationToken);
                if (artEvent == null) return Problem(
                    statusCode: 404,
                    title: "Event cannot be found",
                    detail: $"Event with id:{id} cannot be found!"
                );

                _dbContext.Remove(artEvent);
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
        private ReadArtEventDto MapToReadArtEventDto(ArtEvent artEvent)
        {
            return new ReadArtEventDto
            {
                Id = artEvent.Id,
                Name = artEvent.Name,
                Date = artEvent.Date,
                ArtOrganizationId = artEvent.ArtOrganizationId,
                ScheduleItems = artEvent.ScheduleItems,
            };
        }
    }
}
