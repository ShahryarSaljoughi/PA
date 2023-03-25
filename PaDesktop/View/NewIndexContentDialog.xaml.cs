using ModernWpf.Controls;
using PaDesktop.ViewModel;
using System;
using System.Windows;

namespace PaDesktop.View
{
    public partial class NewIndexContentDialog
    {
        public NewIndexDialogViewModel ViewModel { get; }
        public NewIndexContentDialog(NewIndexDialogViewModel vm)
        {
            InitializeComponent();
            DataContext = ViewModel = vm;
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
