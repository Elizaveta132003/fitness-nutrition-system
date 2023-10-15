using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workouts.API.Attributes;
using Workouts.BusinessLogic.Dtos.RequestDtos;
using Workouts.BusinessLogic.Dtos.ResponseDtos;
using Workouts.BusinessLogic.Services.Interfaces;

namespace Workouts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [Admin]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ExerciseResponseDto>> CreateExerciseAsync([FromBody] ExerciseRequestDto exerciseRequestDto,
            CancellationToken cancellationToken = default)
        {
            var result = await _exerciseService.CreateAsync(exerciseRequestDto);

            return Ok(result);
        }

        [Admin]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ExerciseResponseDto>> DeleteExerciseAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            var result = await _exerciseService.DeleteAsync(id);

            return Ok(result);
        }

        [Admin]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ExerciseResponseDto>> UpdateExerciseAsync(Guid id,
            ExerciseRequestDto exerciseRequestDto,
            CancellationToken cancellationToken = default)
        {
            var result = await _exerciseService.UpdateAsync(id, exerciseRequestDto);

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ExerciseResponseDto>>> GetAllExercisesAsync(CancellationToken cancellationToken = default)
        {
            var result = await _exerciseService.GetAllAsync();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExerciseResponseDto>> GetExerciseByName(string name, CancellationToken cancellationToken = default)
        {
            var result = await _exerciseService.GetByNameAsync(name);

            return Ok(result);
        }
    }
}
