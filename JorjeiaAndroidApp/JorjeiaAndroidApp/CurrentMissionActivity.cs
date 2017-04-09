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
    [Activity(Label = "CurrentMissionActivity")]
    public class CurrentMissionActivity : Activity
    {
        private Button calendarButton;
        private Button cameraButton;
        private Button scheduleButton;
        private Button mainMenu2Button;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CurrentMissionView);
            FindViews();
            HandleEvent();
        }

        private void HandleEvent()
        {
            calendarButton.Click += CalendarButton_Click;
        }

        private void CalendarButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CalendarActivity));
            StartActivity(intent);
        }

        private void FindViews()
        {
            calendarButton = FindViewById<Button>(Resource.Id.calendarBtn);
            cameraButton = FindViewById<Button>(Resource.Id.cameraBtn);
            scheduleButton = FindViewById<Button>(Resource.Id.scheduleBtn);
            mainMenu2Button = FindViewById<Button>(Resource.Id.mainMenu2btn);
        }
    }
}