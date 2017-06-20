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
using Android.Graphics;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "PersonalDetailsActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class PersonalDetailsActivity : Activity
    {
        private EditText age;
        private Spinner genderSpinner;
        private EditText weight;
        private Button nextButton;
        private TextView text1;
        private TextView text2;
        private TextView text3;
        private TextView text4;

        private string gender = "Жена";

        private int mission = 1;
        private int weightK = 45;
        private int years = 18;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PersonalDetailsView);
            mission = Intent.GetIntExtra("TypeOfMission", 1);
            FindViews();
            FillSpinners();
            HandleEvents();
            // Create your application here
        }
        private void FindViews()
        {
            age = FindViewById<EditText>(Resource.Id.ageEditTextView);
            nextButton = FindViewById<Button>(Resource.Id.nextWeightButton);
            genderSpinner = FindViewById<Spinner>(Resource.Id.spinnerGender);
            weight = FindViewById<EditText>(Resource.Id.weightTextView);
            text1 = FindViewById<TextView>(Resource.Id.textPD1);
            text2 = FindViewById<TextView>(Resource.Id.textPD2);
            text3 = FindViewById<TextView>(Resource.Id.textPD3);
            text4 = FindViewById<TextView>(Resource.Id.textPD4);
            Typeface tf = Typeface.CreateFromAsset(Assets, "MinionPro-Regular.ttf");
            text1.SetTypeface(tf, TypefaceStyle.Normal);
            text2.SetTypeface(tf, TypefaceStyle.Normal);
            text3.SetTypeface(tf, TypefaceStyle.Normal);
            text4.SetTypeface(tf, TypefaceStyle.Normal);
            weight.SetTypeface(tf, TypefaceStyle.Normal);
            age.SetTypeface(tf, TypefaceStyle.Normal);
        }

        private void HandleEvents()
        {
            nextButton.Click += NextButton_Click;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            years = Convert.ToInt32(age.Text);
            weightK = Convert.ToInt32(weight.Text);
            Intent intent;
            if (mission == 1 || mission == 3 || mission == 4 || mission == 6)
            {
                intent = new Intent(this, typeof(ScarActivity));
            }
            else
            {
                intent = new Intent(this, typeof(CameraIntroActivity));
            }
            intent.PutExtra("TypeOfMission", Intent.GetIntExtra("TypeOfMission", 0));
            intent.PutExtra("TypeOfSkin", Intent.GetIntExtra("TypeOfSkin", 0));
            intent.PutExtra("Weight", weightK);
            intent.PutExtra("Ages", years);
            StartActivity(intent);
            Finish();
        }

        protected void FillSpinners()
        {
            var gender = new string[]
            {
                "Жена", "Мъж"
            };

            //fill spinners
            genderSpinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, gender);

            //get selected values from spinners

            genderSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(genderSpinner_ItemSelected);
        }

        private void genderSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            gender = spinner.GetItemAtPosition(e.Position).ToString();
        }
    }
}
