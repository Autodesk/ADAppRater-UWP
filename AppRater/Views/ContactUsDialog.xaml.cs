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

using AppRater.Controls.Dialogs;
using AppRater.Models.FrameworkTools;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AppRater.Controls.Dialogs
{

    public sealed partial class ContactUsDialog : BlankContentDialog
    {
        public ContactUsDialog()
        {
            this.InitializeComponent();
        }

        private void ContactUsButtonClick(object sender, RoutedEventArgs e)
        {
            Hide();

            AppRater.Workflow.ContactUsButtonClick();
        }

        private void NoThanksButtonClick(object sender, RoutedEventArgs e)
        {
            Hide();

            AppRater.Workflow.NoThanksAtContactUsButtonClick();
        }
    }
}
