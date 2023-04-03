using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using PaDesktop.ViewModel;
using Services.Abstractions;
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
    /// <summary>
    /// Interaction logic for EscallationResultPage.xaml
    /// </summary>
    public partial class EscallationResultPage : UserControl
    {
        ILogger Logger { get; set; }
        EscallationResultPageViewModel ViewModel { get; set; }

        //public EscallationResultPage(EscallationResultPageViewModel vm)
        //{
        //    InitializeComponent();
        //    ViewModel = vm;
        //}

        public EscallationResultPage()
        {
            Logger = App.Current.Services.GetService<ILogger>();
            ViewModel = App.Current.Services.GetService<EscallationResultPageViewModel>();
            this.DataContext = ViewModel;
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await ViewModel.CalculateAsync();

            }
            catch (Exception error)
            {
                Logger.Log(error);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel (*.xlsx)|*xlsx";
                saveFileDialog.ShowDialog();
                await ViewModel.ExportExcelAsync(saveFileDialog.FileName);

            }
            catch (Exception error)
            {
                Logger.Log(error);
            }
        }
    }
}
