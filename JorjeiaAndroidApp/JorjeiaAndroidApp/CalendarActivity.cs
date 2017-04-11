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
using Android.Content.PM;
using Calendar.Views;
using JorjeiaAndroidApp.Resources.DataHelper;
using Android.Util;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "CalendarActivity", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class CalendarActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity // global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        DataBase db;
        protected override void OnCreate(Bundle bundle)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            db = new DataBase();
            db.createDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);
            var list = db.selectActiveDatesSchedule();
            List<DateTime> datesList = new List<DateTime>();
            foreach (var item in list)
            {
                if (item.IsPassed)
                {
                    datesList.Add(item.Date);
                }
            }
            global::Xamarin.Forms.Forms.Init(this, bundle);


            LoadApplication(new App(datesList));
        }
    }
}