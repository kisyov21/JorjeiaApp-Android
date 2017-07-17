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

namespace JorjeiaAndroidApp.Utility
{
    //https://testinglocations.herokuapp.com/getlocations
    public class Locations
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }

        public Locations()
        {

        }
        public Locations(string latitude, string longitude, string address)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Address = address;
        }
    }

    public class RootObject
    {
        public List<Locations> locations { get; set; }
    }
}