using Microsoft.Extensions.DependencyInjection;
using PaDesktop.Core;
using PaDesktop.Service;
using PaDesktop.View;
using PaDesktop.ViewModel;
using Services;
using Services.Abstractions;
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
            services.AddSingleton<EscallationInputViewModel>();
            services.AddSingleton<IndexEditViewModel>();
            services.AddSingleton<PriceInputViewModel>();
            services.AddSingleton<EscallationResultPageViewModel>();
            services.AddSingleton<Core.PaDbContext, Core.PaDbContext>();
            services.AddSingleton<Services.PaDbContext, Services.PaDbContext>();
            services.AddSingleton<ITimeBoxService, TimeBoxService>();
            services.AddSingleton<ISubfieldService, SubfieldService>();
            services.AddSingleton<IIndexService, IndexService>();
            services.AddSingleton<IEscallationCalculator, EscallationCalculator>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddScoped<IExcelExportService, ExcelExportService>();
            return services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = Services.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
