using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "JorjeiaAndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button newMissionButton;
        //private Button aboutButton;
        //private Button contactsButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            FindViews();

            HandleEvents();
        }

        private void FindViews()
        {
            newMissionButton = FindViewById<Button>(Resource.Id.newMissionButton);
            // aboutButton = FindViewById<Button>(Resource.Id.about);
            //contactsButton = FindViewById<Button>(Resource.Id.contacts);
        }

        private void HandleEvents()
        {
            newMissionButton.Click += NewMissionButton_Click;
            //aboutButton.Click += AboutButton_Click;
            //contactsButton.Click += ContactsButton_Click;
        }

        //private void ContactsButton_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void AboutButton_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void NewMissionButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(NewMissionIntroActivity));
            StartActivity(intent);
        }
    }
}

