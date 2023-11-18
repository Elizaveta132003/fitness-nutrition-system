using Microsoft.AspNetCore.Mvc;
using Nutrition.Infrastructure.BackgroundTasks;

namespace Nutrition.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaloriesController : ControllerBase
    {
        private readonly ICaloriesCalculationTask _caloriesCalculationTask;

        public CaloriesController(ICaloriesCalculationTask caloriesCalculationTask)
        {
            _caloriesCalculationTask = caloriesCalculationTask;
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetCalculatedCalories(string userName, CancellationToken cancellationToken = default)
        {
            _caloriesCalculationTask.ExecuteCaloriesCalculation(userName);

            return Ok();
        }
    }
}
