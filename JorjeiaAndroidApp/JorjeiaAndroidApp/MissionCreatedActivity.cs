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
using Android.Graphics;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "MissionCreatedActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MissionCreatedActivity : Activity
    {
        private Button continueBtn;
        private TextView text1;

        DataBase db;
        private int type;

        private int[] specificTypeOfMission =
        {
            1, 3, 4, 7, 8, 10, 12, 13, 15, 16, 17, 19, 20, 21, 23, 24, 25, 27, 28, 30, 32, 35, 36, 39, 40, 41, 43, 44,
            47, 48, 49, 51, 52, 53, 55, 56
        };

        private bool isTwoTime = true;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MissionCreatedView);
            FindViews();
            
            continueBtn.Click += continueBtnClick;
            var mission = Intent.GetIntExtra("TypeOfMission", 1);
            var skin =  Intent.GetIntExtra("TypeOfSkin", 1);
            var age = Intent.GetIntExtra("Ages", 34);
            var scar = Intent.GetIntExtra("Scar", 4);
            var weight = Intent.GetIntExtra("Weight", 50);
            var schedule = Methods.Calculate(age);
            Mission miss;
            SetTypeOfMission(mission, skin, age, scar);
            
            //Create DataBase
            db = new DataBase();
            var isOkS = db.DeleteTableSchedule();
            var isOkM = db.DeleteTableMission();
            if (specificTypeOfMission.Contains(type))
            {
                miss = new Mission()
                {
                    HasMission = 1,
                    TypeMission = type,
                    WaterInMl = weight * 30,
                    IsTwoTime = false
                };
            }
            else
            {
                miss = new Mission()
                {
                    HasMission = 1,
                    TypeMission = type,
                    WaterInMl = weight * 30,
                    IsTwoTime = true
                };
            }
            //TODO delete records from table
            db.InsertIntoTableMission(miss);
            db.InsertIntoTableSchedule(schedule);
            // Create your application here
        }

        private void FindViews()
        {
            continueBtn = FindViewById<Button>(Resource.Id.nextMCButton);
            text1 = FindViewById<TextView>(Resource.Id.textViewmc1);
            Typeface tf = Typeface.CreateFromAsset(Assets, "MinionPro-Regular.ttf");
            text1.SetTypeface(tf, TypefaceStyle.Normal);
        }

        private void continueBtnClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AskForNotificationActivity));
            StartActivity(intent);
            Finish();
        }

        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(AskForNotificationActivity));
            StartActivity(intent);
            Finish();
        }

        private void SetTypeOfMission(int mission, int skin, int age, int scar)
        {
            ////missions : 1-mission 1, 2-mission 2, 3-mission 1+2, 4-mission 1+3, 5-mission 2+3, 6-mission 1+2+3, 7-mission men, 8-mission men + mission 3

            if(mission == 1 && skin == 1 && age <= 33 && scar <=3)
            {
                type = 1;
            }
            else if (mission == 1 && skin == 1 && age <= 33 && scar > 3)
            {
                type = 2;
            }
            else if(mission == 1 && skin == 1 && age > 33 && scar <= 3)
            {
                type = 3;
            }
            else if (mission == 1 && skin == 1 && age > 33 && scar > 3)
            {
                type = 4;
            }
            else if (mission == 1 && skin == 2 && age <= 33 && scar <= 3)
            {
                type = 5;
            }
            else if (mission == 1 && skin == 2 && age <= 33 && scar > 3)
            {
                type = 6;
            }
            else if (mission == 1 && skin == 2 && age > 33 && scar <= 3)
            {
                type = 7;
            }
            else if (mission == 1 && skin == 2 && age > 33 && scar > 3)
            {
                type = 8;
            }
            else if (mission == 2 && skin == 1 && age <= 33)
            {
                type = 9;
            }
            else if (mission == 2 && skin == 1 && age > 33)
            {
                type = 10;
            }
            else if (mission == 2 && skin == 2 && age <= 33)
            {
                type = 11;
            }
            else if (mission == 2 && skin == 2 && age > 33)
            {
                type = 12;
            }

            else if (mission == 3 && skin == 1 && age <= 33 && scar <= 3)
            {
                type = 13;
            }
            else if (mission == 3 && skin == 1 && age <= 33 && scar > 3)
            {
                type = 14;
            }
            else if (mission == 3 && skin == 1 && age > 33 && scar <= 3)
            {
                type = 15;
            }
            else if (mission == 3 && skin == 1 && age > 33 && scar > 3)
            {
                type = 16;
            }
            else if (mission == 3 && skin == 2 && age <= 33 && scar <= 3)
            {
                type = 17;
            }
            else if (mission == 3 && skin == 2 && age <= 33 && scar > 3)
            {
                type = 18;
            }
            else if (mission == 3 && skin == 2 && age > 33 && scar <= 3)
            {
                type = 19;
            }
            else if (mission == 3 && skin == 2 && age > 33 && scar > 3)
            {
                type = 20;
            }

            else if (mission == 4 && skin == 1 && age <= 33 && scar <= 3)
            {
                type = 21;
            }
            else if (mission == 4 && skin == 1 && age <= 33 && scar > 3)
            {
                type = 22;
            }
            else if (mission == 4 && skin == 1 && age > 33 && scar <= 3)
            {
                type = 23;
            }
            else if (mission == 4 && skin == 1 && age > 33 && scar > 3)
            {
                type = 24;
            }
            else if (mission == 4 && skin == 2 && age <= 33 && scar <= 3)
            {
                type = 25;
            }
            else if (mission == 4 && skin == 2 && age <= 33 && scar > 3)
            {
                type = 26;
            }
            else if (mission == 4 && skin == 2 && age > 33 && scar <= 3)
            {
                type = 27;
            }
            else if (mission == 4 && skin == 2 && age > 33 && scar > 3)
            {
                type = 28;
            }

            else if (mission == 5 && skin == 1 && age <= 33)
            {
                type = 29;
            }
            else if (mission == 5 && skin == 1 && age > 33)
            {
                type = 30;
            }
            else if (mission == 5 && skin == 2 && age <= 33)
            {
                type = 31;
            }
            else if (mission == 5 && skin == 2 && age > 33)
            {
                type = 32;
            }

            else if (mission == 6 && skin == 1 && age <= 33 && scar <= 3)
            {
                type = 33;
            }
            else if (mission == 6 && skin == 1 && age <= 33 && scar > 3)
            {
                type = 34;
            }
            else if (mission == 6 && skin == 1 && age > 33 && scar <= 3)
            {
                type = 35;
            }
            else if (mission == 6 && skin == 1 && age > 33 && scar > 3)
            {
                type = 36;
            }
            else if (mission == 6 && skin == 2 && age <= 33 && scar <= 3)
            {
                type = 37;
            }
            else if (mission == 6 && skin == 2 && age <= 33 && scar > 3)
            {
                type = 38;
            }
            else if (mission == 6 && skin == 2 && age > 33 && scar <= 3)
            {
                type = 39;
            }
            else if (mission == 6 && skin == 2 && age > 33 && scar > 3)
            {
                type = 40;
            }

            else if (mission == 7 && skin == 1 && age <= 33 && scar <= 3)
            {
                type = 41;
            }
            else if (mission == 7 && skin == 1 && age <= 33 && scar > 3)
            {
                type = 42;
            }
            else if (mission == 7 && skin == 1 && age > 33 && scar <= 3)
            {
                type = 43;
            }
            else if (mission == 7 && skin == 1 && age > 33 && scar > 3)
            {
                type = 44;
            }
            else if (mission == 7 && skin == 2 && age <= 33 && scar <= 3)
            {
                type = 45;
            }
            else if (mission == 7 && skin == 2 && age <= 33 && scar > 3)
            {
                type = 46;
            }
            else if (mission == 7 && skin == 2 && age > 33 && scar <= 3)
            {
                type = 47;
            }
            else if (mission == 7 && skin == 2 && age > 33 && scar > 3)
            {
                type = 48;
            }

            else if (mission == 8 && skin == 1 && age <= 33 && scar <= 3)
            {
                type = 49;
            }
            else if (mission == 8 && skin == 1 && age <= 33 && scar > 3)
            {
                type = 50;
            }
            else if (mission == 8 && skin == 1 && age > 33 && scar <= 3)
            {
                type = 51;
            }
            else if (mission == 8 && skin == 1 && age > 33 && scar > 3)
            {
                type = 52;
            }
            else if (mission == 8 && skin == 2 && age <= 33 && scar <= 3)
            {
                type = 53;
            }
            else if (mission == 8 && skin == 2 && age <= 33 && scar > 3)
            {
                type = 54;
            }
            else if (mission == 8 && skin == 2 && age > 33 && scar <= 3)
            {
                type = 55;
            }
            else if (mission == 8 && skin == 2 && age > 33 && scar > 3)
            {
                type = 56;
            }
        }
    }
}