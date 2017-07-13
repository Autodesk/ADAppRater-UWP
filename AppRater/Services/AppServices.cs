using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.UI;

namespace AppRater.Services
{
    class AppServices
    {
        public static bool IsConnectedToInternet()
        {
            ConnectionProfile connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            return ((connectionProfile != null) && (connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess));
        }
    }
}
