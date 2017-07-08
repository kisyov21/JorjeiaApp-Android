using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using JorjeiaAndroidApp.Resources.DataHelper;
using JorjeiaAndroidApp.Resources.Model;
using System.Collections.Generic;
using Android.Util;
using Android.Support.V7.App;
using Android.Graphics;

namespace JorjeiaAndroidApp
{
    //[Activity(Label = "JorjeiaAndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    [Activity(Label = "HomeActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity
    {
        DataBase db;
        private DateTime day = DateTime.Today;

        private Button calendarButton;
        private Button cameraButton;
        private Button scheduleButton;
        private Button mainMenu2Button;
        private Button gallaryButton;

        private List<Mission> lstSource;
        private List<Schedule> lstSchedule;

        private Button newMissionButton;
        private Button aboutButton;
        private Button contactsButton;

        private Button yesBtn;
        private Button noBtn;


        private TextView textView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            db = new DataBase();
            db.createDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);

            LoadData();

            TimeSpan end = new TimeSpan(12, 0, 0); //12 o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;

            if (lstSource.Count != 0 && lstSource != null ) // dali ima misiq
            {
                if (lstSource[0].hasMission == 1) //dali e svyrshila
                {
                    List<Schedule> list = db.selectScheduleByDate(day);
                    if (day > lstSchedule[lstSchedule.Count - 1].Date)
                    {
                        db.updateTableMission();
                        SetContentView(Resource.Layout.MissionFinish);
                        FindViews4();
                        HandleEvents4();
                    }
                    else if (list != null)
                    {
                        foreach (var item in list)
                        {
                            if (item.IsPassed && now < end)
                            {
                                SetContentView(Resource.Layout.CurrentMissionView);
                                FindViews2();
                                HandleEvents2();
                            }
                            else if (item.IsPassed2 && now >= end)
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
                }
                else
                {
                    SetContentView(Resource.Layout.CurrentMissionView);
                    FindViews2();
                    HandleEvents2();
                }
            }
            else
            {
                SetContentView(Resource.Layout.Main);
                FindViews1();
                HandleEvents1();
            }

        }

        private void HandleEvents4()
        {
            yesBtn.Click += YesButton2_Click;
        }

        private void YesButton2_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity2));
            StartActivity(intent);
            Finish();
        }

        private void FindViews4()
        {
            yesBtn = FindViewById<Button>(Resource.Id.finishYesBtn);
            textView = FindViewById<TextView>(Resource.Id.finishTextView);
            Typeface tf = Typeface.CreateFromAsset(Assets, "MinionPro-Regular.ttf");
            textView.SetTypeface(tf, TypefaceStyle.Normal);
        }
        #region Confirmation View
        private void HandleEvents3()
        {
            yesBtn.Click += YesButton_Click;
            noBtn.Click += NoButton_Click;
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(DrinkWaterActivity));
            StartActivity(intent);
            Finish();
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            var isOk = db.updateTableSchedule(day);
            var intent = new Intent(this, typeof(DrinkWaterActivity));
            StartActivity(intent);
            Finish();
        }

        private void FindViews3()
        {
            yesBtn = FindViewById<Button>(Resource.Id.confirmYesBtn);
            noBtn = FindViewById<Button>(Resource.Id.confirmNoBtn);
            textView = FindViewById<TextView>(Resource.Id.textViewc);
            Typeface tf = Typeface.CreateFromAsset(Assets, "MinionPro-Regular.ttf");
            textView.SetTypeface(tf, TypefaceStyle.Normal);
        }
        #endregion

        #region Current Mission View
        private void HandleEvents2()
        {
            calendarButton.Click += CalendarButton_Click;
            mainMenu2Button.Click += MainMenu_Click;
            cameraButton.Click += Camera_Click;
            scheduleButton.Click += Schedule_Click;
        }

        private void Schedule_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ScheduleActivity));
            StartActivity(intent);
            Finish();
        }

        private void MainMenu_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity2));
            StartActivity(intent);
            Finish();
        }

        private void Camera_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CameraActivity));
            StartActivity(intent);
            Finish();
        }

        private void CalendarButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CalendarActivity));
            StartActivity(intent);
        }

        private void FindViews2()
        {
            calendarButton = FindViewById<Button>(Resource.Id.calendarBtn);
            cameraButton = FindViewById<Button>(Resource.Id.cameracCurrBtn);
            scheduleButton = FindViewById<Button>(Resource.Id.scheduleBtn);
            mainMenu2Button = FindViewById<Button>(Resource.Id.mainMenu2btn);
        }
        #endregion

        #region Main view

      
        private void FindViews1()
        {
            newMissionButton = FindViewById<Button>(Resource.Id.newMissionButton);
            aboutButton = FindViewById<Button>(Resource.Id.about);
            contactsButton = FindViewById<Button>(Resource.Id.contacts);
            gallaryButton = FindViewById<Button>(Resource.Id.galleryButton);
        }


        private void HandleEvents1()
        {
            newMissionButton.Click += NewMissionButton_Click;
            aboutButton.Click += AboutButton_Click;
            contactsButton.Click += ContactsButton_Click;
            gallaryButton.Click += GalleryButton_Click;
        }

        private void GalleryButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(GalleryActivity));
            intent.PutExtra("Main", 2);
            StartActivity(intent);
            Finish();
        }
        private void ContactsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ContactActivity));
            intent.PutExtra("Main", 1);
            StartActivity(intent);
            Finish();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AboutActivity));
            intent.PutExtra("Main", 1);
            StartActivity(intent);
            Finish();
        }

        private void NewMissionButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(NewMissionIntroActivity));
            StartActivity(intent);
            Finish();
        }

        private void CurrentMissionButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CurrentMissionActivity));
            StartActivity(intent);
            Finish();
        }
        #endregion

        private void LoadData()
        {
            lstSource = db.selectTableMission();
            lstSchedule = db.selectTableSchedule();
        }
    }
}

