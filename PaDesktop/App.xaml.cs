using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaDesktop.Core;
using PaDesktop.Service;
using PaDesktop.View;
using PaDesktop.ViewModel;
using Serilog;
using Serilog.Core;
using Services;
using Services.Abstractions;
using Services.Abstractions.Excel;
using Services.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;



namespace PaDesktop
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; }
        public new static App Current => (App)Application.Current;
        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<IndexEditWindow>();
            services.AddSingleton<IndexEditViewModel>();
            services.AddSingleton<EscallationInputViewModel>();
            services.AddSingleton<PriceInputViewModel>();
            services.AddSingleton<EscallationResultPageViewModel>();
            services.AddSingleton<AboutViewModel>();
            services.AddSingleton<Core.PaDbContext, Core.PaDbContext>();
            services.AddSingleton<Services.PaDbContext, Services.PaDbContext>();
            services.AddSingleton<ITimeBoxService, TimeBoxService>();
            services.AddSingleton<ISubfieldService, SubfieldService>();
            services.AddSingleton<IIndexService, IndexService>();
            services.AddSingleton<IEscallationCalculator, EscallationCalculator>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddScoped<IExcelExportService, ExcelExportService>();
            services.AddScoped<IExcelCoverSheetCreator, ExcelCoverSheetCreator>();
            services.AddScoped<IExcelSubfieldsSumSheetCreator, ExcelSubfieldsSumSheetCreator>();
            services.AddTransient<Services.Abstractions.ILogger, Services.Logger>();
            RegisterSeriLogger(services);

            return services.BuildServiceProvider();
        }

        private static void RegisterSeriLogger(ServiceCollection services)
        {
            services.AddSingleton<Serilog.ILogger>(sp =>
            {
                var config = new LoggerConfiguration()
                .WriteTo.File($"log{DateTime.Now.Ticks}.txt")
                .WriteTo.EventLog("Price Adjustment App");
                var logger = config.CreateLogger();
                Serilog.Log.Logger = logger;
                return logger;
            });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Log.Logger.Information($"App Started on {DateTime.Now}");
            var mainWindow = Services.GetService<MainWindow>();
            try
            {
                mainWindow?.Show();
            }
            catch (Exception error)
            {
                var logger = Services.GetService<Serilog.ILogger>();
                logger.Error(error.ToString());
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Serilog.Log.CloseAndFlush();

        }
    }
}
