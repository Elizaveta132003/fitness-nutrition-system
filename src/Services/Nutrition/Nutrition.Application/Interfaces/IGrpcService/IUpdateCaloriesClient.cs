using Nutrition.Application.Dtos.RequestDtos;

namespace Nutrition.Application.Interfaces.IGrpcService
{
    public interface IUpdateCaloriesClient
    {
        Task UpdateCaloriesAsync(MealDishRequestDto mealDishRequestDto, CancellationToken cancellationToken = default);
    }
}
