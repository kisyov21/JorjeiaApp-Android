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
    [Activity(Label = "MissionCreatedActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MissionCreatedActivity : Activity
    {
        private Button continueBtn;
        DataBase db;
        private int type;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MissionCreatedView);

            continueBtn = FindViewById<Button>(Resource.Id.nextMCButton);
            continueBtn.Click += continueBtnClick;
            var mission = Intent.GetIntExtra("TypeOfMission", 1);
            var skin =  Intent.GetIntExtra("TypeOfSkin", 1);
            var age = Intent.GetIntExtra("Ages", 0);
            var scar = Intent.GetIntExtra("Scar", 0);
            var schedule = Methods.Calculate(mission);
            SetTypeOfMission(mission, skin, age, scar);
            //Create DataBase
            db = new DataBase();
            var isOkS = db.deleteTableSchedule();
            var isOkM = db.deleteTableMission();
            Mission miss = new Mission() { hasMission = 1, typeMission = type };

            //TODO delete records from table
            db.insertIntoTableMission(miss);
            db.insertIntoTableSchedule(schedule);
            // Create your application here
        }
      
        private void continueBtnClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AskForNotificationActivity));
            StartActivity(intent);
        }

        private void SetTypeOfMission(int mission, int skin, int age, int scar)
        {
            ////missions : 1-mission 1, 2-mission 2, 3-mission 1+2, 4-mission 1+3, 5-mission 2+3, 6-mission 1+2+3, 7-mission men

            if(mission == 1 && skin == 1 && age <= 33)
            {
                type = 1;
            }
            else if(mission == 1 && skin == 1 && age > 33)
            {
                type = 2;
            }
            else if (mission == 1 && skin == 2 && age <= 33)
            {
                type = 3;
            }
            else if (mission == 1 && skin == 2 && age > 33)
            {
                type = 4;
            }
            else if (mission == 2 && skin == 1 && age <= 33)
            {
                type = 5;
            }
            else if (mission == 2 && skin == 1 && age > 33)
            {
                type = 6;
            }
            else if (mission == 2 && skin == 2 && age <= 33)
            {
                type = 7;
            }
            else if (mission == 2 && skin == 2 && age > 33)
            {
                type = 8;
            }
            else if (mission == 3 && skin == 1 && age <= 33)
            {
                type = 9;
            }
            else if (mission == 3 && skin == 1 && age > 33)
            {
                type = 10;
            }
            else if (mission == 3 && skin == 2 && age <= 33)
            {
                type = 11;
            }
            else if (mission == 3 && skin == 2 && age > 33)
            {
                type = 12;
            }
            else if (mission == 4 && skin == 1 && age <= 33)
            {
                type = 13;
            }
            else if (mission == 4 && skin == 1 && age > 33)
            {
                type = 14;
            }
            else if (mission == 4 && skin == 2 && age <= 33)
            {
                type = 15;
            }
            else if (mission == 4 && skin == 2 && age > 33)
            {
                type = 16;
            }
            else if (mission == 5 && skin == 1 && age <= 33)
            {
                type = 17;
            }
            else if (mission == 5 && skin == 1 && age > 33)
            {
                type = 18;
            }
            else if (mission == 5 && skin == 2 && age <= 33)
            {
                type = 19;
            }
            else if (mission == 5 && skin == 2 && age > 33)
            {
                type = 20;
            }
            else if (mission == 6 && skin == 1 && age <= 33)
            {
                type = 21;
            }
            else if (mission == 6 && skin == 1 && age > 33)
            {
                type = 22;
            }
            else if (mission == 6 && skin == 2 && age <= 33)
            {
                type = 23;
            }
            else if (mission == 6 && skin == 2 && age > 33)
            {
                type = 24;
            }
            else if (mission == 7 && skin == 1 && age <= 33)
            {
                //men
            }
            else if (mission == 7 && skin == 1 && age > 33)
            {
                //men
            }
            else if (mission == 7 && skin == 2 && age <= 33)
            {
                //men
            }
            else if (mission == 7 && skin == 2 && age > 33)
            {
                //men
            }
        }
    }
}