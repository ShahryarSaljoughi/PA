using Serilog;
using DataModel.Model;
using Microsoft.EntityFrameworkCore;
using PaDesktop.Core;
using PaDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;

namespace PaDesktop.View
{
    public partial class EscallationInputPage : UserControl
    {
        private EscallationInputViewModel? ViewModel { get; set; }
        private ILogger Logger { get; set; }
        public EscallationInputPage()
        {
            InitializeComponent();
            ViewModel = (EscallationInputViewModel)App.Current.Services.GetService(typeof(EscallationInputViewModel));
            Logger = App.Current.Services.GetService<ILogger>();
            DataContext = ViewModel;
        }
        public EscallationInputPage(EscallationInputViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private async void EscallationInputPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await ViewModel?.PopulateDataAsync();
            }
            catch (Exception error)
            {
                Logger.Error(
                    $@"
Error:
{error.ToString()}
StackTrace:
{error.StackTrace}
InnerException:
{error.InnerException}");
                throw;
            }
        }
    }
}
