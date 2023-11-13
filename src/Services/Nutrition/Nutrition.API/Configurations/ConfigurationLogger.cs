using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Nutrition.API.Configurations
{
    public static class ConfigurationLogger
    {
        public static void ConfigureLogger(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                optional: true)
            .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticsearchSink(configuration))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

        }

        private static ElasticsearchSinkOptions ConfigureElasticsearchSink(IConfigurationRoot configuration)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]!))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"logstash-{DateTime.Now:yyyy.MM.dd}",
                NumberOfReplicas = 1,
                NumberOfShards = 2
            };
        }
    }
}
