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
        private Button newMissionButton;
        private Button newMission2Button;
        private Button currentMissionButton;
        //private Button about2Button;
        //private Button contacts2Button;

        private List<Mission> lstSource;

        //private Button aboutButton;
        //private Button contactsButton;
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
                SetContentView(Resource.Layout.Main2);
                FindViews2();
                HandleEvents2();
            }
            else
            {
                SetContentView(Resource.Layout.Main);
                FindViews1();
                HandleEvents1();
            }

        }

        private void HandleEvents2()
        {
            newMission2Button.Click += NewMissionButton_Click;
            currentMissionButton.Click += CurrentMissionButton_Click;
            //aboutButton.Click += AboutButton_Click;
            //contactsButton.Click += ContactsButton_Click;
        }

     

        private void FindViews2()
        {
            currentMissionButton = FindViewById<Button>(Resource.Id.currentMissionButton);
            newMission2Button = FindViewById<Button>(Resource.Id.newMission2Button);
            //contacts2Button = FindViewById<Button>(Resource.Id.contacts2Button);
            //about2Button = FindViewById<Button>(Resource.Id.about2Button);

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
        }
    }
}

