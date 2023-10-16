using Calories.API.Configurations;
using Calories.BusinessLogic.Extensions;
using Calories.DataAccess.Extensions;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddBusinessLogicService();
builder.Services.ConfigureMongo(builder.Configuration);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddConfigureAuthentication(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swaggerUiOptions =>
    {
        swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Calories API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
