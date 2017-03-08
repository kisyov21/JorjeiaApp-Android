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
    [Activity(Label = "NewMissionIntroActivity")]
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
            // aboutButton = FindViewById<Button>(Resource.Id.about);
            //contactsButton = FindViewById<Button>(Resource.Id.contacts);
        }

        private void HandleEvents()
        {
            newMission.Click += NewMissionButton_Click;
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
            var intent = new Intent(this, typeof(NewMissionActivity));
            StartActivity(intent);
        }
    }
}
