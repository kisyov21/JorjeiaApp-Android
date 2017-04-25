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
    [Activity(Label = "CameraIntroActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class CameraIntroActivity : Activity
    {
        protected Button skipCameraButton;
        protected Button nextCameraButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CameraIntroView);
            // Create your application here
            FindViews();

            HandleEvents();
        }

        private void HandleEvents()
        {
            skipCameraButton.Click += SkipCamera_Click;
            nextCameraButton.Click += NextCamera_Click;
        }

        private void NextCamera_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CameraActivity));
            intent.PutExtra("TypeOfMission", Intent.GetIntExtra("TypeOfMission",0));
            intent.PutExtra("TypeOfSkin", Intent.GetIntExtra("TypeOfSkin",0));
            intent.PutExtra("Ages", Intent.GetIntExtra("Ages",0));
            StartActivity(intent);
        }

        private void SkipCamera_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MissionCreatedActivity));
            intent.PutExtra("TypeOfMission", Intent.GetIntExtra("TypeOfMission", 0));
            intent.PutExtra("TypeOfSkin", Intent.GetIntExtra("TypeOfSkin", 0));
            intent.PutExtra("Ages", Intent.GetIntExtra("Ages", 0));
            StartActivity(intent);
            Finish();
        }

        private void FindViews()
        {
            skipCameraButton = FindViewById<Button>(Resource.Id.skipCButton);
            nextCameraButton = FindViewById<Button>(Resource.Id.nextCButton);
        }
    }
}