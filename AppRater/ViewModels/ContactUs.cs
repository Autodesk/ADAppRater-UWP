using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Launcher = Windows.System.Launcher;
using SystemContentDialog = Windows.UI.Xaml.Controls.ContentDialog;
using Windows.UI.Xaml.Navigation;

namespace AppRater
{
    public class ContactUs
    {
        private static string feedbackMailbox = "";
        private static string mailTitle = "";
        private static string mailNote = "";

        private async static void SendFeedbackViaMail(string optionalParam = "")
        {
            var mailto = new Uri("mailto:" + feedbackMailbox + "?subject=" + mailTitle + "&body=%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A " + 
                mailNote);
            await Launcher.LaunchUriAsync(mailto);
        }

        public async static void Contact()
        {
            SendFeedbackViaMail("");
        }

        public static void SetFeedbackEmail(string mailboxForFeedback)
        {
            feedbackMailbox = mailboxForFeedback;
        }

        public static void SetEmailTitle(string yourMailTitle)
        {
            mailTitle = yourMailTitle;
        }

        public static void SetEmailNote(string yourMailNote)
        {
            mailNote = yourMailNote;
        }
    }
}
