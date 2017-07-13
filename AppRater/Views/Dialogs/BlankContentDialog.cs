using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppRater.Controls.Dialogs
{
    public class BlankContentDialog : Windows.UI.Xaml.Controls.ContentDialog
    {
        public BlankContentDialog()
        {
            Template = Application.Current.Resources["TemplateBlankContentDialog"] as ControlTemplate;
            ScreenBoundsChanged += ResetLayout;
        }

        public void Dismiss()
        {
            Hide();
        }

        private void ResetLayout(ApplicationView sender, object args = null)
        {
            OnResetLayoutBase(sender.VisibleBounds);
        }

        protected virtual void OnResetLayoutBase(Rect bounds)
        {
            try
            {
                if (bounds.Width <= 480)
                {
                    bounds = OnResetLayoutForPhone(bounds);
                }
                else
                {
                    OnResetLayoutForTablet(bounds);
                }
            }
            finally { }
        }

        protected virtual void OnResetLayoutForTablet(Rect bounds)
        {
            FullSizeDesired = false;
            MinWidth = 360;
            MinHeight = 184;
        }

        protected virtual Rect OnResetLayoutForPhone(Rect bounds)
        {
            FullSizeDesired = true;
            if (Window.Current != null)
                bounds = Window.Current.Bounds;
            MinWidth = bounds.Width;
            MinHeight = bounds.Height;

            return bounds;
        }

        /*
        /// <summary>
        /// Use it to specify the custom commands you are going to use in the user defined Content Dialog.
        /// </summary>
        /// <param name="Commands">Pass the commands collection</param>
        public virtual void AddCommands(IList<RelayCommand> Commands)
        {

        }
        */

        public event TypedEventHandler<ApplicationView, Object> ScreenBoundsChanged;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var appView = ApplicationView.GetForCurrentView();
            if (appView != null)
            {
                try
                {
                    appView.VisibleBoundsChanged += ScreenBoundsChanged;
                    ResetLayout(appView);
                }
                finally { }
            }
        }
    }
}