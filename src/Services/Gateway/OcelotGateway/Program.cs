using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddOcelotConfiguration();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

app.UseSwaggerForOcelotUI(options =>
    options.PathToSwaggerGenerator = "/swagger/docs");

await app.UseOcelot();

app.Run();
