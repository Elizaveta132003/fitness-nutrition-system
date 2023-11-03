using Identity.API.Configurations;
using Identity.API.Middleware;
using Identity.Application.Configurations;
using Identity.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureCors();
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureServices();
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

app.ApplyMigrations();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
