using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Application.Interfaces.IBackgroundJobs;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Services
{
    public class BackgroundJobsService : IBackgroundJobsService
    {
        private readonly IMealDishRepository _mealDishRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFoodRepository _foodRepository;

        public BackgroundJobsService(IMealDishRepository mealDishRepository,
            IUserRepository userRepository,
            IFoodRepository foodRepository)
        {
            _mealDishRepository = mealDishRepository;
            _userRepository = userRepository;
            _foodRepository = foodRepository;
        }

        public async Task<double> CalculateCaloriesForUserAndDayAsync(string userName,
            DateTime curreentDate,
            CancellationToken cancellationToken = default)
        {
            var existingUser = await _userRepository.GetOneByAsync(user => user.Username == userName, cancellationToken);

            if (existingUser is null)
            {
                throw new NotFoundException(UserErrorMessages.UserNotFound);
            }

            var foundMealDishes = await _mealDishRepository.GetAllByAsync(mealDish =>
            mealDish.MealDetail.FoodDiary.User.Username == userName &&
            mealDish.MealDetail.Date.Date == curreentDate.Date, cancellationToken);

            if (!foundMealDishes.Any())
            {
                return 0;
            }

            var calories = (await Task.WhenAll(foundMealDishes.Select(
                async mealDish => await GetFoodCaloriesAsync(mealDish.FoodId) * mealDish.ServingSize)))
                .Sum();

            return calories;
        }

        private async Task<double> GetFoodCaloriesAsync(Guid id)
        {
            var food = await _foodRepository.GetOneByAsync(food => food.Id == id);

            return food.Calories;
        }
    }
}
