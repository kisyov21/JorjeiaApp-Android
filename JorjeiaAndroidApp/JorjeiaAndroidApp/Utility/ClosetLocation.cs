using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;
using JorjeiaAndroidApp.Resources.Model;

namespace JorjeiaAndroidApp.Utility
{
   public static class ClosetLocation
    {
        public static string GetClosetPoint(Location loc1, RootObject _locations)
        {
            var closetLocation = new Locations();
            double closetDistence = 999999999999;

            foreach (var location in _locations.locations)
            {
                Location loc2 = new Location("");
                loc2.Latitude = Convert.ToDouble(location.Latitude);
                loc2.Longitude = Convert.ToDouble(location.Longitude);

                var distanceInMeters = loc1.DistanceTo(loc2);
                if(distanceInMeters < closetDistence)
                {
                    closetDistence = distanceInMeters;
                    closetLocation = location;
                }
            }

            return closetLocation.Address != null ? closetLocation.Address : "";
        }
    }
}