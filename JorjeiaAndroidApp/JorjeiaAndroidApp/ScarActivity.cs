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
    [Activity(Label = "ScarActivity")]
    public class ScarActivity : Activity
    {
        Button next;
        RadioButton button1;
        RadioButton button2;
        RadioButton button3;
        RadioButton button4;
        RadioButton button5;
        int scarType = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ScarView);
            FindViews();
            HandleEvents();
        }

        private void HandleEvents()
        {
            next.Click += NextButton_Click;
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            button5.Click += Button5_Click;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            scarType = 5; // >24
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            scarType = 4; //24
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            scarType = 3; //12
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            scarType = 2; //6
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            scarType = 1; //1
        }

        private void FindViews()
        {
            next = FindViewById<Button>(Resource.Id.nextScarButton);
            button1 = FindViewById<RadioButton>(Resource.Id.radioButton1);
            button2 = FindViewById<RadioButton>(Resource.Id.radioButton2);
            button3 = FindViewById<RadioButton>(Resource.Id.radioButton3);
            button4 = FindViewById<RadioButton>(Resource.Id.radioButton4);
            button5 = FindViewById<RadioButton>(Resource.Id.radioButton5);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CameraIntroActivity));
            intent.PutExtra("TypeOfMission", Intent.GetIntExtra("TypeOfMission", 0));
            intent.PutExtra("TypeOfSkin", Intent.GetIntExtra("TypeOfSkin", 0));
            intent.PutExtra("Ages", Intent.GetIntExtra("Ages", 0));
            intent.PutExtra("Scar", scarType);
            StartActivity(intent);
            Finish();
        }
    }
}