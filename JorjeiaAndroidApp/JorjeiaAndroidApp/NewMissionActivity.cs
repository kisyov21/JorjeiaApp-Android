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

namespace JorjeiaAndroidApp
{
    [Activity(Label = "NewMissionActivity")]
    public class NewMissionActivity : Activity
    {
        private EditText age;
        private Spinner skinSpinner;
        private Spinner missionsSpinner;
        private Spinner genderSpinner;
        private Button nextButton;

        private string skin= "Суха";
        private string missions = "Мисия 1";
        private string gender = "Мъж";

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
            age = FindViewById<EditText>(Resource.Id.ageEditTextView);
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
            var mission = new NewMission();
            mission = CollectInfo();
            Methods.Calculate(mission);
            var intent = new Intent(this, typeof(CameraIntroActivity));
            StartActivity(intent);
        }

        private NewMission CollectInfo()
        {
            return new NewMission(age.Text, skin, missions, gender);
        }

        protected void FillSpinners()
        {
            var skinType = new string[]
            {
                "Суха","Нормална","Мазна"
            };
            var missions = new string[]
            {
                "Мисия 1", "Мисия 2","Мисия 3","Мисии 1+2","Мисии 1+3","Мисии 2+3","Мисии 1+2+3","Мисия 1 FOR MEN"
            };
            var gender = new string[]
            {
                "Мъж", "Жена"
            };
           
            //fill spinners
            skinSpinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, skinType);
            missionsSpinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, missions);
            genderSpinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, gender);

            //get selected values from spinners
            skinSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(skinSpinner_ItemSelected);
            missionsSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(missionsSpinner_ItemSelected);
            genderSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(genderSpinner_ItemSelected);
        }

        private void genderSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            gender = spinner.GetItemAtPosition(e.Position).ToString();
        }

        private void missionsSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            missions = spinner.GetItemAtPosition(e.Position).ToString();
        }

        private void skinSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            skin = spinner.GetItemAtPosition(e.Position).ToString();
        }

        //public void Remind(DateTime dateTime, string title, string message)
        //{

        //    Intent alarmIntent = new Intent(Forms.Context, typeof(AlarmReceiver));
        //    alarmIntent.PutExtra("message", message);
        //    alarmIntent.PutExtra("title", title);

        //    PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
        //    AlarmManager alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);

        //    //TODO: For demo set after 5 seconds.
        //    alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 5 * 1000, pendingIntent);

        //}
    }
}