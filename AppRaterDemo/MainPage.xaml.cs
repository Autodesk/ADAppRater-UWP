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
using System.Diagnostics;

using AppRater.Controls.Dialogs;
using AppRater.Models.FrameworkTools;


namespace AppRaterDemo
{
    public sealed partial class MainPage : Page
    {
        private TextBlock condition1Status;
        private TextBlock condition2Status;
        private TextBlock condition3Status;
        private TextBlock launchTimeStatus;
        private TextBlock popedStatus;

        public MainPage()
        {
            this.InitializeComponent();

            condition1Status = this.FindName("Condition1Status") as TextBlock;
            condition2Status = this.FindName("Condition2Status") as TextBlock;
            condition3Status = this.FindName("Condition3Status") as TextBlock;
            launchTimeStatus = this.FindName("LaunchTimeStatus") as TextBlock;
            popedStatus =  this.FindName("PoppedStatus") as TextBlock;
            
            UpdateStatusText();

            //AppRater.Criteria.SetFirstTimeLaunchTimestr(DateTime.Now.AddDays(-1));
        }

        //Here is a demo for delegate function
        //Any void func() can be added
        void WhenPopUpTheEnjoyment()
        {
            Debug.WriteLine("Hello, enjoyment dialog is here!");
        }

        private void ShowAppRater_Click(object sender, RoutedEventArgs e)
        {
            AppRater.Workflow.addOnPopUpEnjoymentDialog = new AdditionalAction(WhenPopUpTheEnjoyment);

            AppRater.Workflow.PopupEnjoymentDialog();

            UpdateStatusText();
        }

        private void AskForRating_Click(object sender, RoutedEventArgs e)
        {
            AppRater.Workflow.AskForRating();

            UpdateStatusText();
        }

        private void Condition1_Click(object sender, RoutedEventArgs e)
        {
            AppRater.Criteria.EventOccured("event1");

            UpdateStatusText();
        }

        private void Condition2_Click(object sender, RoutedEventArgs e)
        {
            AppRater.Criteria.EventOccured("event2", 1);

            UpdateStatusText();
        }

        private void Condition3_Click(object sender, RoutedEventArgs e)
        {
            AppRater.Criteria.EventOccured("event3", 1);

            UpdateStatusText();
        }

        private void ResetCriteria_Click(object sender, RoutedEventArgs e)
        {
            AppRater.Criteria.ResetCriteria();

            UpdateStatusText();
        }

        private void UpdateStatusText()
        {
            condition1Status.Text = "Event1: " + AppRater.Criteria.GetEventCount("event1") 
                                        + "/" + AppRater.Criteria.GetEventCriteria("event1");
            if (AppRater.Criteria.GetEventCount("event1") >= AppRater.Criteria.GetEventCriteria("event1"))
            {
                condition1Status.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
            }else
            {
                condition1Status.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }

            condition2Status.Text = "Event2: " + AppRater.Criteria.GetEventCount("event2")
                                        + "/" + AppRater.Criteria.GetEventCriteria("event2");
            if (AppRater.Criteria.GetEventCount("event2") >= AppRater.Criteria.GetEventCriteria("event2"))
            {
                condition2Status.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
            }
            else
            {
                condition2Status.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }

            condition3Status.Text = "Event3: " + AppRater.Criteria.GetEventCount("event3")
                                        + "/" + AppRater.Criteria.GetEventCriteria("event3");
            if (AppRater.Criteria.GetEventCount("event3") >= AppRater.Criteria.GetEventCriteria("event3"))
            {
                condition3Status.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
            }
            else
            {
                condition3Status.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }

            launchTimeStatus.Text = "FirstLaunchTime: " + AppRater.Criteria.GetFirstTimeLaunchTimestr();
            if (AppRater.Criteria.EnoughTimeAfterFirstLaunch())
            {
                launchTimeStatus.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
            }
            else
            {
                launchTimeStatus.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }

            popedStatus.Text = "AlreadyPoped: " + AppRater.Criteria.AlreadyPopup();
        }
    }
}
