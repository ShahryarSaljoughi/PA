using DataModel.Model;
using ModernWpf.Controls;
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

    public partial class IndexEditWindow
    {
        public IndexEditViewModel ViewModel { get; set; }
        public IndexEditWindow(IndexEditViewModel vm)
        {
            InitializeComponent();
            DataContext = ViewModel = vm;
        }
        public IndexEditWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.PopulateDataAsync();
        }

        private void TimeBoxes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.PopulateIndexes();
            ViewModel.OnPropertyChanged(nameof(ViewModel.IndexGroupHeader));
        }

        private async void NewTimeboxClicked(object sender, RoutedEventArgs e)
        {
            var dialog = new NewTimeboxContentDialog();
            var result = await dialog.ShowAsync(placement: ModernWpf.Controls.ContentDialogPlacement.InPlace);
            if (result == ModernWpf.Controls.ContentDialogResult.Primary)
            {
                await ViewModel.SaveTimeBoxAsync(dialog.ViewModel);
            }
        }

        private async void DeleteTimeboxClicked(object sender, RoutedEventArgs e)
        {
            var dialog = new RemoveTimeboxConfirmation();
            var result = await dialog.ShowAsync(placement: ContentDialogPlacement.Popup);
            if (result == ContentDialogResult.Primary)
            {
                await ViewModel.DeleteSelectedTimeboxAsync();
            }
        }

        private async void RemoveIndexClicked(object sender, RoutedEventArgs e)
        {
            await ViewModel.DeleteSelectedIndexAsync();
        }

        private void IndexCellEdited(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit) return;
            PAIndex editedIndex = (PAIndex)e.Row.Item;
            ViewModel.OnIndexChanged(editedIndex);
        }

        private async void NewIndexClicked(object sender, RoutedEventArgs e)
        {
            var dialogVm = new NewIndexDialogViewModel(ViewModel.SelectedTimeBox);
            var dialog = new NewIndexContentDialog(dialogVm);
            await dialogVm.InitializeAsync();
            var result = await dialog.ShowAsync(ContentDialogPlacement.InPlace);
            if (result != ContentDialogResult.Primary) { return; }
            var newIndex = dialogVm.CreateIndex();
            await ViewModel.AddIndexAsync(newIndex);
        }
    }
}
