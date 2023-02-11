// See https://aka.ms/new-console-template for more information
using PopulateDbJob;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.File;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLogging();
        services.AddSingleton<PaDbContext, PaDbContext>();
        //services.AddDbContext<PaDbContext>(ServiceLifetime.Singleton);
        services.AddScoped<Populator, Populator>();

    })
    .ConfigureLogging((context, logBuilder) =>
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .WriteTo.File("/logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        logBuilder.AddSerilog(logger);
    })
    .Build();

host.Start();


var populator = host.Services.GetService<Populator>() ?? throw new Exception("IoC not working");
await populator.PopulateAsync();