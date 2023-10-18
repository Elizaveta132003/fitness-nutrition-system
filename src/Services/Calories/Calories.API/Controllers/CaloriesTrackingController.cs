using Calories.BusinessLogic.Dtos.RequestDtos;
using Calories.BusinessLogic.Dtos.ResponseDtos;
using Calories.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calories.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaloriesTrackingController : ControllerBase
    {
        private readonly ICaloriesTrackingService _caloriesTrackingService;

        public CaloriesTrackingController(ICaloriesTrackingService caloriesTrackingService)
        {
            _caloriesTrackingService = caloriesTrackingService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CaloriesTrackingResponseDto>> CreateCaloriesTrackingAsync(
            [FromBody] CaloriesTrackingRequestDto caloriesTrackingRequestDto,
            CancellationToken cancellationToken)
        {
            var result = await _caloriesTrackingService.CreateAsync(caloriesTrackingRequestDto, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CaloriesTrackingResponseDto>> DeleteCaloriesTrackingAsync(
            string id, CancellationToken cancellationToken)
        {
            var result = await _caloriesTrackingService.DeleteAsync(id, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CaloriesTrackingResponseDto>> UpdateCaloriesTrackingAsync(
            string id,
            [FromBody] CaloriesTrackingRequestDto caloriesTrackingRequestDto,
            CancellationToken cancellationToken)
        {
            var result = await _caloriesTrackingService.UpdateAsync(id, caloriesTrackingRequestDto, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CaloriesTrackingResponseDto>>> GetAllCaloriesTrackingAsync(
            CancellationToken cancellationToken)
        {
            var result = await _caloriesTrackingService.GetAllAsync(cancellationToken);

            return Ok(result);
        }
    }
}
