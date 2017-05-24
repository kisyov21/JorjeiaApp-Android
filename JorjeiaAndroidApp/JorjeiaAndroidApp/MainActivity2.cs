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
    [Activity(Label = "MainActivity2", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity2 : Activity
    {
        private Button newMission2Button;
        private Button currentMissionButton;
        private Button about2Button;
        private Button contacts2Button;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main2);
            FindViews();
            HandleEvents();
            // Create your application here
        }

        private void HandleEvents()
        {
            newMission2Button.Click += NewMissionButton_Click;
            currentMissionButton.Click += CurrentMissionButton_Click;
            about2Button.Click += AboutButton_Click;
            contacts2Button.Click += ContactsButton_Click;
        }



        private void FindViews()
        {
            currentMissionButton = FindViewById<Button>(Resource.Id.currentMissionButton);
            newMission2Button = FindViewById<Button>(Resource.Id.newMission2Button);
            contacts2Button = FindViewById<Button>(Resource.Id.contacts2Button);
            about2Button = FindViewById<Button>(Resource.Id.about2Button);

        }

        private void NewMissionButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(NewMissionIntroActivity));
            StartActivity(intent);
            Finish();
        }

        private void CurrentMissionButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CurrentMissionActivity));
            StartActivity(intent);
            Finish();
        }

        private void ContactsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ContactActivity));
            intent.PutExtra("Main", 2);
            StartActivity(intent);
            Finish();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AboutActivity));
            intent.PutExtra("Main", 2);
            StartActivity(intent);
            Finish();
        }
    }
}