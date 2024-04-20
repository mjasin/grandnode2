using Grand.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Grand.Web.Common.Extensions;

public static class ConfigurationExtensions
{

    public static void AddAppSettingsJsonFile(this IConfigurationManager configuration, string[] args, IWebHostEnvironment environment)
    {
        const string appDataPath = "App_Data";

        configuration.AddJsonFile(Path.Combine(appDataPath, "appsettings.json"), optional: false, reloadOnChange: true);
        configuration.AddJsonFile(Path.Combine(appDataPath, $"appsettings.{environment.EnvironmentName}.json"), optional: true, reloadOnChange: true);
        configuration.AddEnvironmentVariables();
        if (args.Length != 0)
        {
            configuration.AddCommandLine(args);
            var appSettings = configuration["appsettings"];
            if (!string.IsNullOrEmpty(appSettings))
            {
                var pathRoot = Path.Combine(appDataPath, appSettings);
                configuration.AddJsonFile(Path.Combine(pathRoot, "appsettings.json"), optional: false, reloadOnChange: true);
                configuration.AddJsonFile(Path.Combine(pathRoot, $"appsettings.{environment.EnvironmentName}.json"), optional: true, reloadOnChange: true);
            }
        }
    }
    public static void ConfigureApplicationSettings(this WebApplicationBuilder builder)
    {
        //Allow non ASCII chars in headers
        var config = new AppConfig();
        builder.Configuration.GetSection("Application").Bind(config);
        if (config.AllowNonAsciiCharInHeaders)
        {
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ResponseHeaderEncodingSelector = _ => Encoding.UTF8;
            });
        }
        if (config.MaxRequestBodySize.HasValue)
        {
            builder.WebHost.ConfigureKestrel(host =>
            {
                host.Limits.MaxRequestBodySize = config.MaxRequestBodySize.Value;
            });

            builder.Services.Configure<FormOptions>(opt =>
            {
                opt.MultipartBodyLengthLimit = config.MaxRequestBodySize.Value;
            });
        }
    }
}