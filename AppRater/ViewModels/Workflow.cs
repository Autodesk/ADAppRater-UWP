using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRater.Services;
using AppRater.Controls.Dialogs;
using AppRater.Models.FrameworkTools;
using Windows.UI.Xaml.Controls;

public delegate void AdditionalAction();

namespace AppRater
{
    public class Workflow
    {
        public static AdditionalAction addOnPopUpEnjoymentDialog = null;
        public static AdditionalAction addOnUsefulButtonClick = null;
        public static AdditionalAction addOnNotUsefulButtonClick = null;
        public static AdditionalAction addOnPopUpContactUsDialog = null;
        public static AdditionalAction addOnContactUsDialogClick = null;
        public static AdditionalAction addOnNoThanksAtContactUsButtonClick = null;
        public static AdditionalAction addOnPopUpRateUsDialog = null;
        public static AdditionalAction addOnRateUsButtonClick = null;
        public static AdditionalAction addOnReminderButtonClick = null;
        public static AdditionalAction addOnNoThanksAtRateUsButtonClick = null;

        public static void AskForRating()
        {
            if (Criteria.SatisfyCriteria())
            {
                Workflow.PopupEnjoymentDialog();
            }
        }

        //Enjoyment Dialog
        public static void PopupEnjoymentDialog()
        {
            if (addOnPopUpEnjoymentDialog != null)
            {
                addOnPopUpEnjoymentDialog();
            }


            Criteria.PopUpEnjoyment();

            var dialog = new EnjoymentDialog();
            dialog.ShowAsyncQueue();
        }

        public static void UsefulButtonClick()
        {
            if (addOnUsefulButtonClick != null)
            {
                addOnUsefulButtonClick();
            }

            PopupRateUsDialog();
        }

        public static void NotUsefulButtonClick()
        {
            if (addOnNotUsefulButtonClick != null)
            {
                addOnNotUsefulButtonClick();
            }

            PopupContactUsDialog();
        }

        //Contact us dialog
        public static void PopupContactUsDialog()
        {
            if (addOnPopUpContactUsDialog != null)
            {
                addOnPopUpContactUsDialog();
            }

            ContactUsDialog contactUs = new ContactUsDialog();
            contactUs.ShowAsyncQueue();
        }

        public static void ContactUsButtonClick()
        {
            if (addOnContactUsDialogClick != null)
            {
                addOnContactUsDialogClick();
            }

            ContactUs.Contact();
        }

        public static void NoThanksAtContactUsButtonClick()
        {
            if (addOnNoThanksAtContactUsButtonClick != null)
            {
                addOnNoThanksAtContactUsButtonClick();
            }
        }

        //Rate us dialog
        public static void PopupRateUsDialog()
        {
            if (addOnPopUpRateUsDialog != null)
            {
                addOnPopUpRateUsDialog();
            }

            RateUsDialog rateUs = new RateUsDialog();
            rateUs.storeUri = RatingUs.GetRatingUrl();
            rateUs.ShowAsyncQueue();
        }

        public static void RateUsButtonClick()
        {
            if (addOnRateUsButtonClick != null)
            {
                addOnRateUsButtonClick();
            }
        }

        public static void ReminderButtonClick()
        {
            if (addOnReminderButtonClick != null)
            {
                addOnReminderButtonClick();
            }

            Criteria.ClickReminderMe();
        }

        public static void NoThanksAtRateUsButtonClick()
        {
            if (addOnNoThanksAtContactUsButtonClick != null)
            {
                addOnNoThanksAtContactUsButtonClick();
            }
        }
    }
}
