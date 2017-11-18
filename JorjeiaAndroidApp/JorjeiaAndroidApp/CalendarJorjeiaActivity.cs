using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using CalendarJorjeia;
using CalendarJorjeia.Models;
using JorjeiaAndroidApp.Resources.DataHelper;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "CalendarJorjeiaActivity", Icon = "@drawable/icon",  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class CalendarJorjeiaActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        DataBase db;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            db = new DataBase();
            db.CreateDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);

            var schedule = db.SelectTableSchedule();
            var lstSource = db.SelectTableMission();
            List<ScheduleModel> data = new List<ScheduleModel>();

            foreach (var item in schedule)
            {
                data.Add(new ScheduleModel { Id = item.Id, Date = item.Date, IsPassed = item.IsPassed, IsPassed2 = item.IsPassed2, IsPassed3 = item.IsPassed3});
            }

            global::Xamarin.Forms.Forms.Init(this, bundle);
            XamForms.Controls.Droid.Calendar.Init();
            LoadApplication(new App(data, lstSource[0].IsTwoTime));
        }
        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(CurrentMissionActivity));
            StartActivity(intent);
            Finish();
        }
    }
}

