using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AppRater.Models.FrameworkTools;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AppRater.Controls.Dialogs
{
    public sealed partial class RateUsDialog : BlankContentDialog
    {
        public string storeUri { get; set; }

        public RateUsDialog()
        {
            this.InitializeComponent();
        }

        private void RateUsButtonClick(object sender, RoutedEventArgs e)
        {
            Hide();

            AppRater.Workflow.RateUsButtonClick();
        }

        private void ReminderButtonClick(object sender, RoutedEventArgs e)
        {
            Hide();

            AppRater.Workflow.ReminderButtonClick();
        }
        private void NoThanksButtonClick(object sender, RoutedEventArgs e)
        {
            Hide();

            AppRater.Workflow.NoThanksAtRateUsButtonClick();
        }
    }
}
