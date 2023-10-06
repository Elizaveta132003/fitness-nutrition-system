﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nutrition.API.Attributes;
using Nutrition.Application.Dtos.RequestDtos;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Features.Food.Commands.CreateFood;
using Nutrition.Application.Features.Food.Commands.DeleteFood;
using Nutrition.Application.Features.Food.Commands.UpdateFood;
using Nutrition.Application.Features.Food.Queries.GetAllFood;
using Nutrition.Application.Features.Food.Queries.GetByNameFood;

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

        [Admin]
        [HttpDelete("id:guid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<FoodResponseDto>> DeleteFoodAsync(Guid id)
        {
            var command = new DeleteFoodCommand(id);
            var result = await _meadiator.Send(command);

            return Ok(result);
        }

        [Admin]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<FoodResponseDto>> UpdateFoodAsync(Guid id, FoodRequestDto foodRequestDto)
        {
            var command = new UpdateFoodCommand(id, foodRequestDto);
            var result = await _meadiator.Send(command);

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<FoodResponseDto>>> GetAllFoodAsync()
        {
            var query = new GetAllFoodQuery();
            var result = await _meadiator.Send(query);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FoodResponseDto>> GetByNameFoodAsync(string name)
        {
            var query = new GetByNameFoodQuery(name);
            var result = await _meadiator.Send(query);

            return Ok(result);
        }
    }
}
