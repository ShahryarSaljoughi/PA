using Microsoft.Extensions.DependencyInjection;
using PaDesktop.Core;
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
            services.AddSingleton<Core.PaDbContext, Core.PaDbContext>();
            services.AddSingleton<ITimeBoxService, TimeBoxService>();
            services.AddScoped<IEscallationCalculator, EscallationCalculator>();
            return services.BuildServiceProvider();
        }
    }
}
