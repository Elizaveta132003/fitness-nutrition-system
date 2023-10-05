using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nutrition.API.Attributes;
using Nutrition.Application.Dtos.RequestDtos;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Features.Food.Commands.CreateFood;

namespace Nutrition.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly IMediator _meadiator;

        public FoodController(IMediator meadiator)
        {
            _meadiator = meadiator;
        }

        [Admin]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<FoodResponseDto>> CreateFoodAsync(FoodRequestDto foodRequestDto)
        {
            var command = new CreateFoodCommand(foodRequestDto);
            var result = await _meadiator.Send(command);

            return Ok(result);
        }
    }
}
