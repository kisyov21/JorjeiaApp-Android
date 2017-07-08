
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.View;
using JorjeiaAndroidApp.Utility;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "GalleryActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class GalleryActivity : AppCompatActivity
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        ViewPager viewPager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GalleryView);
            FindViews();
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Галерия на Jorjeia";
            ImageAdapter adapter = new ImageAdapter(this);
            viewPager.Adapter = adapter;
            // viewPage your application here
        }

        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }

        private void FindViews()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbarG);
            viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
        }
    }
}