using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workouts.BusinessLogic.Dtos.RequestDtos;
using Workouts.BusinessLogic.Dtos.ResponseDtos;
using Workouts.BusinessLogic.Services.Interfaces;
using Workouts.DataAccess.Enums;

namespace Workouts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutExercisesController : ControllerBase
    {
        private readonly IWorkoutExerciseService _workoutExerciseService;

        public WorkoutExercisesController(IWorkoutExerciseService workoutExerciseService)
        {
            _workoutExerciseService = workoutExerciseService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WorkoutExerciseResponseDto>> CreateWorkoutExerciseAsync(
            [FromBody] WorkoutExerciseRequestDto workoutExerciseRequestDto,
            CancellationToken cancellationToken)
        {
            var result = await _workoutExerciseService.CreateAsync(workoutExerciseRequestDto, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WorkoutExerciseResponseDto>> DeleteWorkoutExerciseAsync(
            Guid id, CancellationToken cancellationToken)
        {
            var result = await _workoutExerciseService.DeleteAsync(id, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{userId:guid}/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<WorkoutExerciseResponseDto>>> GetAllWorkoutExercisesByUserIdAndDateAsync(
            Guid userId,
            DateTime date,
            CancellationToken cancellationToken)
        {
            var result = await _workoutExerciseService.GetAllWorkoutExercisesByUserIdAndDateAsync(
                userId, date, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{userId:guid}/{date}/{type}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<WorkoutExerciseResponseDto>>> GetAllWorkoutExercisesByUserIdAndDateAndTypeAsync(
            Guid userId,
            DateTime date,
            ExerciseType type,
            CancellationToken cancellationToken)
        {
            var result = await _workoutExerciseService.GetAllWorkoutExercisesByUserIdAndDateAndTypeAsync(
                userId, date, type, cancellationToken);

            return Ok(result);
        }
    }
}
