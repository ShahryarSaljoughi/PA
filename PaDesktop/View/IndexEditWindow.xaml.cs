using DataModel.Model;
using ModernWpf.Controls;
using PaDesktop.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using Services.Abstractions;

namespace PaDesktop.View
{

    public partial class IndexEditWindow
    {
        public IndexEditViewModel ViewModel { get; set; }
        private ILogger Logger { get; set; }
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
            try
            {
                await ViewModel.PopulateDataAsync();
            }
            catch (Exception error)
            {
                Logger.Log(error);
            }
        }

        private void TimeBoxes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.PopulateIndexes();
            ViewModel.OnPropertyChanged(nameof(ViewModel.IndexGroupHeader));
        }

        private async void NewTimeboxClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new NewTimeboxContentDialog();
                var result = await dialog.ShowAsync(placement: ModernWpf.Controls.ContentDialogPlacement.InPlace);
                if (result == ModernWpf.Controls.ContentDialogResult.Primary)
                {
                    await ViewModel.SaveTimeBoxAsync(dialog.ViewModel);
                }
            }
            catch (Exception error)
            {
                Logger.Log(error);
            }
        }

        private async void DeleteTimeboxClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new RemoveTimeboxConfirmation();
                var result = await dialog.ShowAsync(placement: ContentDialogPlacement.Popup);
                if (result == ContentDialogResult.Primary)
                {
                    await ViewModel.DeleteSelectedTimeboxAsync();
                }
            }
            catch (Exception error)
            {
                Logger.Log(error);
            }
        }

        private async void RemoveIndexClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                await ViewModel.DeleteSelectedIndexAsync();
            }
            catch (Exception error)
            {
                Logger.Log(error);
            }
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
            try
            {
                await dialogVm.InitializeAsync();
                var result = await dialog.ShowAsync(ContentDialogPlacement.InPlace);
                if (result != ContentDialogResult.Primary) { return; }
                var newIndex = dialogVm.CreateIndex();
                await ViewModel.AddIndexAsync(newIndex);

            }
            catch (Exception error)
            {
                Logger.Log(error);
            }
        }
    }
}
