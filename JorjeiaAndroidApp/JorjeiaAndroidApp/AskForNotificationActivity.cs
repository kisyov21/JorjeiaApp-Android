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

    [Activity(Label = "AskForNotificationActivity", Theme="@style/Theme.AppCompat.Light.NoActionBar")]
    public class AskForNotificationActivity : Activity
    {
        private Button yesBtn;
        private Button noBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AskForNotificationView);
            FindViews();
            HandleEvents();
        }

        private void HandleEvents()
        {
            yesBtn.Click += YesButton_Click;
            noBtn.Click += NoButton_Click;
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            StartAlarm();
            var intent = new Intent(this, typeof(CurrentMissionActivity));
            StartActivity(intent);
            Finish();
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CurrentMissionActivity));
            StartActivity(intent);
            Finish();
        }

        private void FindViews()
        {
            yesBtn = FindViewById<Button>(Resource.Id.notifYesBtn);
            noBtn = FindViewById<Button>(Resource.Id.notifNoBtn);
        }

        private void StartAlarm()
        {
            var isRepeating = true; //just for the test
            AlarmManager manager = (AlarmManager)GetSystemService(Context.AlarmService);
            Intent myIntent;
            PendingIntent pendingIntent;
            myIntent = new Intent(this, typeof(AlarmNotificationReceiver));
            pendingIntent = PendingIntent.GetBroadcast(this, 0, myIntent, 0);
            

            //if (!isRepeating)
            //{
            //    var triggerAtMilis = 0;

            //    var datetime = DateTime.Now;
            //    TimeSpan ts = new TimeSpan(7, 45, 0);
            //    datetime = datetime.Date + ts;
            //    var triggerTime = Convert.ToInt64(GetTimeInterval(datetime));
            //    if (Convert.ToInt32(Android.OS.Build.VERSION.Sdk) == 19)
            //    {
            //        manager.SetExact(AlarmType.RtcWakeup, triggerAtMilis, pendingIntent);
            //    }
            //    else if (Convert.ToInt32(Android.OS.Build.VERSION.Sdk) < 19)
            //    {
            //        manager.Set(AlarmType.RtcWakeup, triggerAtMilis, pendingIntent);
            //    }
            //    else
            //    {
            //        manager.Set(AlarmType.RtcWakeup, triggerAtMilis, pendingIntent);
            //    }
            //}

            //else
            //{
                var triggerAtMilis = SystemClock.ElapsedRealtime() + 3000;

                var datetime = DateTime.Now;
                TimeSpan ts = new TimeSpan(20, 35, 0);
                datetime = datetime.Date + ts;
                var triggerTime = Convert.ToInt64(GetTimeInterval(datetime));

                manager.SetRepeating(AlarmType.RtcWakeup, triggerTime, AlarmManager.IntervalDay, pendingIntent);
            //}
        }

        public double GetTimeInterval(DateTime dt)
        {
            DateTime now = DateTime.Now;
            if (dt <= now)
            {
                dt = dt.AddDays(1);
            }

            return (dt - now).TotalMilliseconds;
        }
    }
}