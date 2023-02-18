using Microsoft.Extensions.DependencyInjection;
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
        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            return services.BuildServiceProvider();
        }
    }
}
