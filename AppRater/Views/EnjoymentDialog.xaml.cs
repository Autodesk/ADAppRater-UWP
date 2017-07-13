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
    public sealed partial class EnjoymentDialog : BlankContentDialog 
    {
        public EnjoymentDialog()
        {
            this.InitializeComponent();
        }

        private async void UsefulButtonClick(object sender, RoutedEventArgs e)
        {
            Hide();
            AppRater.Workflow.UsefulButtonClick();
        }

        private async void NotUsefulButtonClick(object sender, RoutedEventArgs e)
        {
            Hide();
            AppRater.Workflow.NotUsefulButtonClick();
        }
    }
}
