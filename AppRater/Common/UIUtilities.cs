using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;

namespace AppRater.Common
{
    public class Hyperlink : DependencyObject
    {
        // Using a DependencyProperty as the backing store for Address.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddressProperty = DependencyProperty.RegisterAttached(
            "Address", typeof(Uri), typeof(Hyperlink), new PropertyMetadata(
                null, (dpObj, args) =>
                {
                    var element = dpObj as FrameworkElement;
                    if (element != null && !attachedElements.Contains(element.GetHashCode()))
                    {
                        element.Tapped += Element_Tapped;
                        attachedElements.Add(element.GetHashCode());
                    }
                }));

        private static SortedSet<int> attachedElements = new SortedSet<int>();

        public static Uri GetAddress(DependencyObject obj)
        {
            return (Uri)obj.GetValue(AddressProperty);
        }

        public static void SetAddress(DependencyObject obj, Uri value)
        {
            obj.SetValue(AddressProperty, value);
        }

        private static async void Element_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var dpObj = sender as DependencyObject;
            if (dpObj == null)
                return;

            var uriAddress = GetAddress(dpObj);
            if (uriAddress == null)
                return;

            await Launcher.LaunchUriAsync(uriAddress);
        }
    }
}
