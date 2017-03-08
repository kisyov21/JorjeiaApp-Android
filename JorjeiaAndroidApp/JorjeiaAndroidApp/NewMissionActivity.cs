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

namespace JorjeiaAndroidApp
{
    [Activity(Label = "NewMissionActivity")]
    public class NewMissionActivity : Activity
    {
        private Spinner skinSpinner;
        private Spinner missionsSpinner;
        private Spinner genderSpinner;
        private Button nextButton;

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
            genderSpinner = FindViewById<Spinner>(Resource.Id.spinnerGender);
        }

        private void HandleEvents()
        {
            nextButton.Click += NextButton_Click;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CameraIntroActivity));
            StartActivity(intent);
        }

        protected void FillSpinners()
        {
            var SkinType = new string[]
            {
                "Суха кожа","Нормална кожа"
            };
            var missions = new string[]
            {
                "Мисия 1", "Мисия 2","Мисия 3","Мисия 1 FOR MEN"
            };
            var gender = new string[]
            {
                "Мъж", "Жена"
            };
           
            skinSpinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, SkinType);
            missionsSpinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, missions);
            genderSpinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, gender);
            
        }
    }
}