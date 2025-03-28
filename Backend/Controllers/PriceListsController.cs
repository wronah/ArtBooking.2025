using Business.Model.Data;
using Business.Model.Dtos.PriceListDtos;
using Business.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceListsController : ControllerBase
    {
        private readonly ArtBookingDbContext _dbContext;

        public PriceListsController(ArtBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<ReadPriceListDto>> CreatePriceList([FromBody] CreatePriceListDto createPriceListDto, CancellationToken cancellationToken)
        {
            try
            {
                var priceList = new PriceList
                {
                    Name = createPriceListDto.Name,
                };

                await _dbContext.AddAsync(priceList, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                var readPriceListDto = MapToReadPriceListDto(priceList);

                return CreatedAtAction(nameof(CreatePriceList), new { priceList.Id }, readPriceListDto);
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

        [HttpPost("{id}/copy")]
        public async Task<ActionResult<ReadPriceListDto>> CopyPriceList(int id, CancellationToken cancellationToken)
        {
            try
            {
                var priceList = await _dbContext.PriceLists.FindAsync(id, cancellationToken);

                if (priceList == null) return Problem(
                    statusCode: 404,
                    title: "Price List cannot be found",
                    detail: $"Price List with id:{id} cannot be found!"
                );

                var newPriceList = new PriceList
                {
                    Name = priceList.Name,
                    PriceEntries = priceList.PriceEntries,
                };

                await _dbContext.AddAsync(newPriceList, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                var readPriceListDto = MapToReadPriceListDto(newPriceList);

                return Ok(readPriceListDto);
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
        public async Task<ActionResult<ReadPriceListDto>> GetPriceList(int id, CancellationToken cancellationToken)
        {
            try
            {
                var priceList = await _dbContext.PriceLists.FindAsync(id, cancellationToken);

                if (priceList == null) return Problem(
                    statusCode: 404,
                    title: "Price List cannot be found",
                    detail: $"Price List with id:{id} cannot be found!"
                );

                var readPriceListDto = MapToReadPriceListDto(priceList);

                return Ok(readPriceListDto);
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
        public async Task<ActionResult<List<ReadPriceListDto>>> GetPriceLists(CancellationToken cancellationToken)
        {
            try
            {
                var organizations = await _dbContext.PriceLists.ToListAsync(cancellationToken);
                var readPriceListDtos = organizations.ConvertAll(MapToReadPriceListDto);

                return Ok(readPriceListDtos);
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
        public async Task<ActionResult<ReadPriceListDto>> UpdatePriceList(int id, [FromBody] UpdatePriceListDto updatePriceListDto, CancellationToken cancellationToken)
        {
            try
            {
                var priceList = await _dbContext.PriceLists.FindAsync(id, cancellationToken);

                if (priceList == null) return Problem(
                    statusCode: 404,
                    title: "Price List cannot be found",
                    detail: $"Price List with id:{id} cannot be found!"
                );

                priceList.Name = updatePriceListDto.Name;

                await _dbContext.SaveChangesAsync(cancellationToken);

                var readPriceListDto = MapToReadPriceListDto(priceList);

                return Ok(readPriceListDto);
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
                var priceList = await _dbContext.PriceLists.FindAsync(id, cancellationToken);
                if (priceList == null) return Problem(
                    statusCode: 404,
                    title: "Price List cannot be found",
                    detail: $"Price List with id:{id} cannot be found!"
                );

                _dbContext.Remove(priceList);
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
        private ReadPriceListDto MapToReadPriceListDto(PriceList priceList)
        {
            return new ReadPriceListDto
            {
                Id = priceList.Id,
                Name = priceList.Name,
                ArtEventId = priceList.ArtEventId,
                VenueId = priceList.VenueId,
                PriceEntries = priceList.PriceEntries,
            };
        }
    }
}
