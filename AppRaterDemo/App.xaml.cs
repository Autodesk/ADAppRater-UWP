using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace AppRaterDemo
{
    sealed partial class App : Application
    {

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            InitAppRater();
        }

        void InitAppRater()
        {
            //====================AppRaterCriteria====================
            AppRater.Criteria.SetEventCriteria("event1", 10);
            AppRater.Criteria.SetEventCriteria("event2", 10);
            AppRater.Criteria.SetEventCriteria("event3", 10);

            string[,] groups = new string[,] { { "event2", "event3" } };
            AppRater.Criteria.GroupEventCriteria (groups);

            AppRater.Criteria.InitFirstTimeLaunchTimestr();

            //====================Uwp Store Uri for RateUs============
            AppRater.RatingUs.SetRatingUrl("your_product_id_on_uwp_store");

            //====================Feedback Mailbox====================
            AppRater.ContactUs.SetFeedbackEmail("your_mailbox_to_receive_feedback@xxx.com");
            AppRater.ContactUs.SetEmailTitle("your mail title");
            AppRater.ContactUs.SetEmailNote("your mail note");
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }


        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
