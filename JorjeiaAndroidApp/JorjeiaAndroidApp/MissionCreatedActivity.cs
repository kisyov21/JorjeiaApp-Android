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
using JorjeiaAndroidApp.Resources.DataHelper;
using JorjeiaAndroidApp.Resources.Model;
using JorjeiaAndroidApp.Utility;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "MissionCreatedActivity")]
    public class MissionCreatedActivity : Activity
    {
        DataBase db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MissionCreatedView);
            var mission = Intent.GetIntExtra("TypeOfMission", 1);
            var skin =  Intent.GetIntExtra("TypeOfSkin", 1);
            var age = Intent.GetIntExtra("Ages", 0);
            var schedule = Methods.Calculate(mission);

            //Create DataBase
            db = new DataBase();
            var isOkS = db.deleteTableSchedule();
            var isOkM = db.deleteTableMission();
            Mission miss = new Mission() { hasMission = 1, typeMission = 2 };

            //TODO delete records from table
            db.insertIntoTableMission(miss);
            db.insertIntoTableSchedule(schedule);
            // Create your application here
        }
    }
}