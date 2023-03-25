using ModernWpf.Controls;
using PaDesktop.ViewModel;
using System;
using System.Windows;

namespace PaDesktop.View
{
    public partial class RemoveTimeboxConfirmation
    {
        public RemoveTimeboxConfirmation()
        {
            InitializeComponent();
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
        }
    }
}
