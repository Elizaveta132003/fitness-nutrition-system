using FluentValidation.AspNetCore;
using Workouts.API.Configurations;
using Workouts.API.Middleware;
using Workouts.BusinessLogic.Extensions;
using Workouts.DataAccess.DataContext;
using Workouts.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddBusinessLogicService();
builder.Services.ConfigureDatabaseServices(builder.Configuration);
builder.Services.AddDbContext<WorkoutDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddConfigureAuthentication(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swaggerUiOptions =>
    {
        swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
