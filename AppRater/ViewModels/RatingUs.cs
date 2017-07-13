using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRater
{
    public class RatingUs
    {
        private static String storeRatingUri = "ms-windows-store://review/?ProductId=9wzdncrfjctk";

        public static void SetRatingUrl(String productId)
        {
            storeRatingUri = "ms-windows-store://review/?ProductId=" + productId;
        }

        public static string GetRatingUrl()
        {
            return storeRatingUri;
        }
    }
}
