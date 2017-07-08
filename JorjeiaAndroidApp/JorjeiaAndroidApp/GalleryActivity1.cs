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

namespace JorjeiaAndroidApp
{
    [Activity(Label = "GalleryActivity1")]
    public class GalleryActivity1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.Main);

            //Gallery gallery = (Gallery)FindViewById<Gallery>(Resource.Id.gallery);

            //gallery.Adapter = new ImageAdapter(this);

            //gallery.ItemClick += delegate (object sender, Android.Widget.AdapterView.ItemClickEventArgs args) {
            //    Toast.MakeText(this, args.Position.ToString(), ToastLength.Short).Show();
            //};
        }
    }
}