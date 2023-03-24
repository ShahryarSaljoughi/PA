using ModernWpf.Controls;
using PaDesktop.ViewModel;
using System;
using System.Windows;

namespace PaDesktop.View
{
    public partial class NewTimeboxContentDialog
    {
        public NewTimeboxDialogViewModel ViewModel { get; }
        public NewTimeboxContentDialog()
        {
            InitializeComponent();
            DataContext = ViewModel = new NewTimeboxDialogViewModel();
        }

        private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void OnSecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void OnCloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var deferral = args.GetDeferral();
            deferral.Complete();
        }

        

        private void OnClosed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            //ErrorText.Text = string.Empty;
            //ErrorText.Visibility = Visibility.Collapsed;
        }
    }
}
