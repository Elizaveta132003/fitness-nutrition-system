namespace OcelotGateway.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddOcelotConfiguration(this IConfigurationBuilder builder)
        {
            var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            builder.AddJsonFile($"ocelot.{enviroment}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
    }
}
