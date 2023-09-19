using Identity.API.Configurations;
using Identity.Application.Configurations;
using Identity.Infrastructure.Configurations;
using Identity.Infrastructure.DataContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenConfiguration();

builder.Services.AddAuthenticationConfiguration(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
