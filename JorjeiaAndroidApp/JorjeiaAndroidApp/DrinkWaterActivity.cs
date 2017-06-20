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
using Android.Graphics;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "DrinkWaterActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class DrinkWaterActivity : Activity
    {
        private Button okBtn;
        private TextView textView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DrinkWaterView);
            FindViews();
            HandleEvents();
            // Create your application here
        }
        private void YesButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CurrentMissionActivity));
            StartActivity(intent);
            Finish();
        }
        private void HandleEvents()
        {
            okBtn.Click += YesButton_Click;
        }
        private void FindViews()
        {
            okBtn = FindViewById<Button>(Resource.Id.okButton);
            textView = FindViewById<TextView>(Resource.Id.text22View);
            Typeface tf = Typeface.CreateFromAsset(Assets, "MinionPro-Regular.ttf");
            textView.SetTypeface(tf, TypefaceStyle.Normal);
        }
    }
}