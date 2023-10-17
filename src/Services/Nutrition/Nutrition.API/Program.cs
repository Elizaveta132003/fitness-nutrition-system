using FluentValidation;
using Nutrition.API.Configurations;
using Nutrition.API.Middleware;
using Nutrition.Application.Extensions;
using Nutrition.Application.Validators.RequestValidators;
using Nutrition.Infrastructure.Data.DataContext;
using Nutrition.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<FoodRequestValidator>();
builder.Services.ConfigureMediatR();
builder.Services.ApplyMigrations(builder.Configuration);
builder.Services.AddDbContext<NutritionDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddConfigureAuthentication(builder.Configuration);

var app = builder.Build();
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

app.Run();
