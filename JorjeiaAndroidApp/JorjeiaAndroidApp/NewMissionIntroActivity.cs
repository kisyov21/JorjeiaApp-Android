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
    [Activity(Label = "NewMissionIntroActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class NewMissionIntroActivity : Activity
    {
        private Button newMission;
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
