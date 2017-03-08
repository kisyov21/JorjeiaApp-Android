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

            }
            else
            {
                SetContentView(Resource.Layout.Main);
                FindViews1();

                HandleEvents1();
            }

        }

        private void FindViews1()
        {
            newMissionButton = FindViewById<Button>(Resource.Id.newMissionButton);
            // aboutButton = FindViewById<Button>(Resource.Id.about);
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

        private void LoadData()
        {
            lstSource = db.selectTableMission();
        }
    }
}

