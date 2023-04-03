using Microsoft.Extensions.DependencyInjection;
using PaDesktop.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using Services.Abstractions;

namespace PaDesktop.View
{
    public partial class PriceInputPage : UserControl
    {
        private PriceInputViewModel? ViewModel { get; set; }
        private ILogger Logger { get; set; }
        public PriceInputPage()
        {
            InitializeComponent();
            DataContext = ViewModel = App.Current.Services.GetService<PriceInputViewModel>();
            Logger = App.Current.Services.GetService<ILogger>();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await ViewModel.PopulateDataAsync();

            }
            catch (Exception error)
            {
                Logger.Log(error);
                throw;
            }
        }
    }
}
