using DataModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    public partial class MainWindow
    {
        public ITimeBoxService TimeBoxService { get; set; }
        public IndexEditWindow? IndexEditWindow { get; set; }
        public MainWindow(MainWindowViewModel vm, ITimeBoxService timeBoxService, IndexEditWindow indexEditWindow)
        {
            InitializeComponent();
            DataContext = vm;
            TimeBoxService = timeBoxService;
            //IndexEditWindow = indexEditWindow;
        }

        private void escacalnav_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((MainWindowViewModel)DataContext).GoToEscallationInputPage.Execute(null);
        }

        private void indexeditnav_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenEditWindow();
            //((MainWindowViewModel)DataContext).GoToIndexEditPage.Execute(null);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await TimeBoxService.LoadAsync();
        }

        private void OpenEditWindow()
        {
            if (Application.Current.Windows.OfType<IndexEditWindow>().Any())
            {
                IndexEditWindow = Application.Current.Windows.OfType<IndexEditWindow>().First();
            }
            else
            {
                IndexEditWindow = App.Current.Services.GetService<IndexEditWindow>();
            }
            IndexEditWindow.Show();
            var activated = IndexEditWindow.Activate();
            IndexEditWindow.Focus();
        }
    }
}
