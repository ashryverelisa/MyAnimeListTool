using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyAnimeWatchListTool;
using MyAnimeWatchListTool.Configuration;
using MyAnimeWatchListTool.Services;
using MyAnimeWatchListTool.Services.Interfaces;
using Serilog;

var hostBuilder = new HostBuilder();

hostBuilder.ConfigureAppConfiguration(config =>
{
    config.AddJsonFile("Configuration/Config.json", false);
});

hostBuilder.ConfigureServices((hostContext, services) =>
{
    var config = hostContext.Configuration.Get<Config>();
    services.AddSingleton(config!);
    services.AddHostedService<StartUp>();
    services.AddHttpClient();
    services.AddTransient<IMyAnimeListService, MyAnimeListService>();
    services.AddLogging(logging =>
    {
        var logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}{Exception}")
            .MinimumLevel.Information()
            .CreateLogger();
        logging.AddSerilog(logger);
    });
});

await hostBuilder.RunConsoleAsync();