using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using JorjeiaAndroidApp.Resources.DataHelper;
using JorjeiaAndroidApp.Resources.Model;
using System.Collections.Generic;
using Android.Util;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "JorjeiaAndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        DataBase db;
        private DateTime day = DateTime.Today;

        private Button calendarButton;
        private Button cameraButton;
        private Button scheduleButton;
        private Button mainMenu2Button;

        private List<Mission> lstSource;
        private List<Schedule> lstSchedule;

        private Button newMissionButton;
        //private Button aboutButton;
        //private Button contactsButton;

        private Button yesBtn;
        private Button noBtn;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            db = new DataBase();
            db.createDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);

            LoadData();
            if (lstSource.Count != 0 && lstSource != null)
            {
                List<Schedule> list = db.selectScheduleByDate(day);
                foreach (var item in list)
                {
                    if(item.IsPassed)
                    {
                        SetContentView(Resource.Layout.CurrentMissionView);
                        FindViews2();
                        HandleEvents2();
                    }
                    else
                    {
                        SetContentView(Resource.Layout.ConfirmationView);
                        FindViews3();
                        HandleEvents3();
                    }
                }
            }
            else
            {
                SetContentView(Resource.Layout.Main);
                FindViews1();
                HandleEvents1();
            }

        }

        private void HandleEvents3()
        {
            yesBtn.Click += YesButton_Click;
            noBtn.Click += NoButton_Click;
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CurrentMissionActivity));
            StartActivity(intent);
            Finish();
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            var isOk = db.updateTableSchedule(day);
            var intent = new Intent(this, typeof(CurrentMissionActivity));
            StartActivity(intent);
            Finish();
        }

        private void FindViews3()
        {
            yesBtn = FindViewById<Button>(Resource.Id.confirmYesBtn);
            noBtn = FindViewById<Button>(Resource.Id.confirmNoBtn);
        }

        private void HandleEvents2()
        {
            calendarButton.Click += CalendarButton_Click;
            mainMenu2Button.Click += MainMenu_Click;
            cameraButton.Click += Camera_Click;
        }

        private void MainMenu_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity2));
            StartActivity(intent);
        }

        private void Camera_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CameraActivity));
            StartActivity(intent);
        }

        private void CalendarButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CalendarActivity));
            StartActivity(intent);
        }

        private void FindViews2()
        {
            calendarButton = FindViewById<Button>(Resource.Id.calendarBtn);
            cameraButton = FindViewById<Button>(Resource.Id.cameraBtn);
            scheduleButton = FindViewById<Button>(Resource.Id.scheduleBtn);
            mainMenu2Button = FindViewById<Button>(Resource.Id.mainMenu2btn);
        }

        private void FindViews1()
        {
            newMissionButton = FindViewById<Button>(Resource.Id.newMissionButton);
            //aboutButton = FindViewById<Button>(Resource.Id.about);
            //contactsButton = FindViewById<Button>(Resource.Id.contacts);
        }

       
        private void HandleEvents1()
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

        private void CurrentMissionButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CurrentMissionActivity));
            StartActivity(intent);
        }

        private void LoadData()
        {
            lstSource = db.selectTableMission();
            lstSchedule = db.selectTableSchedule();
        }
    }
}

