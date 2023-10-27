using Identity.API.Configurations;
using Identity.API.Middleware;
using Identity.Application.Configurations;
using Identity.Infrastructure.Configurations;
using Shared.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.ConfigureCors();
builder.Services.AddKafkaMessageBus();
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureKafka(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseMiddleware<ExceptionMiddleware>();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.ApplyMigration();
app.ApplyMigrations();
app.Run();
