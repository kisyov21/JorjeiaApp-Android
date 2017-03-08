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

            //Create DataBase
            db = new DataBase();
            Mission miss = new Mission() { hasMission = 1, typeMission = 2 };
            db.insertIntoTableMission(miss);
            // Create your application here
        }
    }
}