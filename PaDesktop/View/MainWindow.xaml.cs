using DataModel.Model;
using Microsoft.EntityFrameworkCore;
using PaDesktop.Core;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public ITimeBoxService TimeBoxService { get; set; }
        public MainWindow(MainWindowViewModel vm, ITimeBoxService timeBoxService)
        {
            InitializeComponent();
            DataContext = vm;
            TimeBoxService = timeBoxService;
        }

        private void escacalnav_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((MainWindowViewModel)DataContext).GoToEscallationInputPage.Execute(null);
        }

        private void indexeditnav_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((MainWindowViewModel)DataContext).GoToIndexEditPage.Execute(null);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await TimeBoxService.LoadAsync();
        }
    }
}
