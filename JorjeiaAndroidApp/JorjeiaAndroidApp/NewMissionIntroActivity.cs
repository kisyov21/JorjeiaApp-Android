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
    [Activity(Label = "NewMissionIntroActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class NewMissionIntroActivity : Activity
    {
        private Button newMission;
        private TextView text1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewMissionIntroView);

            FindViews();

            HandleEvents();
        }

        private void FindViews()
        {
            newMission = FindViewById<Button>(Resource.Id.nextNMIButton);
            text1 = FindViewById<TextView>(Resource.Id.textNMI);
            Typeface tf = Typeface.CreateFromAsset(Assets, "MinionPro-Regular.ttf");
            text1.SetTypeface(tf, TypefaceStyle.Normal);
        }

        private void HandleEvents()
        {
            newMission.Click += NewMissionButton_Click;
        }

        private void NewMissionButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(NewMissionActivity));
            StartActivity(intent);
            Finish();
        }
    }
}
