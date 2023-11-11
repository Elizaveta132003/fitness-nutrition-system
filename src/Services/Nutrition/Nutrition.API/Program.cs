using FluentValidation;
using Hangfire;
using Nutrition.API.Configurations;
using Nutrition.API.Hangfire;
using Nutrition.API.Hubs;
using Nutrition.API.Middleware;
using Nutrition.API.SignalR;
using Nutrition.Application.Extensions;
using Nutrition.Application.Validators.RequestValidators;
using Nutrition.Infrastructure.Data.DataContext;
using Nutrition.Infrastructure.Extensions;
using Nutrition.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.ConfigureCors();
builder.Services.AddValidatorsFromAssemblyContaining<FoodRequestValidator>();
builder.Services.ConfigureMediatR();
builder.Services.ConfigureServices();
builder.Services.ConfigureHangfire(builder.Configuration);
builder.Services.AddScoped<IMyHubHelper, MyHubHelper>();
builder.Services.AddSignalR();
builder.Services.ApplyMigrations(builder.Configuration);
builder.Services.AddDbContext<NutritionDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddConfigureAuthentication(builder.Configuration);

var app = builder.Build();
app.UseCors("CorsPolicy");
app.UseMiddleware<ExceptionMiddleware>();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(swaggerUIOptions =>
    {
        swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Nutrition API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() }
});

app.MapHub<CaloriesHub>("/caloriesHub");

app.Run();
