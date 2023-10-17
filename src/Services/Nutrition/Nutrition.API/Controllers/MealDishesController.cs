using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrition.Application.Dtos.RequestDtos;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Features.MealDishes.Commands.CreateMealDish;
using Nutrition.Application.Features.MealDishes.Commands.DeleteMealDish;
using Nutrition.Application.Features.MealDishes.Queries.GetAllMealDishesByUserIdAndDate;
using Nutrition.Application.Features.MealDishes.Queries.GetAllMealDishesByUserIdAndDateAndMealType;
using Nutrition.Domain.Enums;

namespace Nutrition.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealDishesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MealDishesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MealDishResponseDto>> CreateMealDishAsync(MealDishRequestDto mealDishRequestDto)
        {
            var command = new CreateMealDishCommand(mealDishRequestDto);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MealDishRequestDto>> DeleteMealDishAsync(Guid id)
        {
            var command = new DeleteMealDishCommand(id);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{userId:guid}/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MealDishResponseDto>>> GetAllMealDishesByUserIdAndDate(
            Guid userId, DateTime date)
        {
            var query = new GetAllMealDishesByUserIdAndDateQuery(userId, date);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{userId:guid}/{date}/{type}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MealDishResponseDto>>> GetAllMealDishesByUserIdAndDateAndMealType(
           Guid userId, DateTime date, MealType type)
        {
            var query = new GetAllMealDishesByUserIdAndDateAndMealTypeQuery(userId, date, type);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
