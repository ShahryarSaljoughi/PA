using Microsoft.Extensions.DependencyInjection;
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

namespace PaDesktop.View
{
    public partial class PriceInputPage : UserControl
    {
        private PriceInputViewModel? ViewModel {get; set;}
        public PriceInputPage()
        {
            InitializeComponent();
            DataContext = ViewModel = App.Current.Services.GetService<PriceInputViewModel>();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.PopulateDataAsync();
        }
    }
}
