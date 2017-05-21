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
using JorjeiaAndroidApp.Utility;
using JorjeiaAndroidApp.Resources.Model;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "NewMissionActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class NewMissionActivity : Activity
    {
        private Spinner skinSpinner;
        private Spinner missionsSpinner;
        private Button nextButton;

        private string skin= "Суха";
        private string missions = "Мисия 1";
        private string gender = "Жена";

        private int typeOfMission = 1;
        private int typeOfSkin = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewMissionView);
            FindViews();
            FillSpinners();
            HandleEvents();
            // Create your application here
        }
        private void FindViews()
        {
            nextButton = FindViewById<Button>(Resource.Id.nextNMButton);
            skinSpinner = FindViewById<Spinner>(Resource.Id.spinnerSkinType);
            missionsSpinner = FindViewById<Spinner>(Resource.Id.spinnerMissionsType);
        }

        private void HandleEvents()
        {
            nextButton.Click += NextButton_Click;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(PersonalDetailsActivity));
         
            intent.PutExtra("TypeOfMission", typeOfMission);
            intent.PutExtra("TypeOfSkin", typeOfSkin);
            StartActivity(intent);
            Finish();
        }

        protected void FillSpinners()
        {
            var skinType = new string[]
            {
                "Суха","Нормална"
            };
            var missions = new string[]
            {
                "Мисия 1", "Мисия 2","Мисия 3","Мисия 1+2","Мисия 1+3","Мисия 2+3","Мисия 1+2+3","Мисия 1 FOR MEN","Мисия 3 + 1 FOR MEN"
            };
        
           
            //fill spinners
            skinSpinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, skinType);
            missionsSpinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, missions);

            //get selected values from spinners
            skinSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(skinSpinner_ItemSelected);
            missionsSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(missionsSpinner_ItemSelected);
        }

        private void missionsSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            missions = spinner.GetItemAtPosition(e.Position).ToString();
            TypeOfMission(missions);
        }

        private void skinSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            skin = spinner.GetItemAtPosition(e.Position).ToString();
            TypeOfSkin(skin);
        }

        private void TypeOfMission(string mission)
        { 
            switch (mission)
            {
                case "Мисия 1":
                    typeOfMission = 1;
                    break;
                case "Мисия 2":
                    typeOfMission = 2;
                    break;
                case "Мисия 1 + 2":
                    typeOfMission = 3;
                    break;
                case "Мисия 1 + 3":
                    typeOfMission = 4;
                    break;
                case "Мисия 2 + 3":
                    typeOfMission = 5;
                    break;
                case "Мисия 1 + 2 + 3":
                    typeOfMission = 6;
                    break;
                case "Мисия 1 FOR MEN":
                    typeOfMission = 7;
                    break;
                case "Мисия 3 + 1 FOR MEN":
                    typeOfMission = 8;
                    break;
            }
        }

        private void TypeOfSkin(string skin)
        {
            switch (skin)
            {
                case "Суха":
                    typeOfSkin = 1;
                    break;
                case "Нормална":
                    typeOfSkin = 2;
                    break;
                case "Мазна":
                    typeOfSkin = 3;
                    break;
            }
        }
    }
}