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
    [Activity(Label = "ContactActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ContactActivity : Activity
    {
        private TextView phoneNumber1TextView;
        private TextView phoneNumber2TextView;
        private Button back;
        private int main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ContactView);
            main = Intent.GetIntExtra("Main", 0);
            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            phoneNumber1TextView = FindViewById<TextView>(Resource.Id.phoneNumber1TextView);
            phoneNumber2TextView = FindViewById<TextView>(Resource.Id.phoneNumber2TextView);
            back = FindViewById<Button>(Resource.Id.backContactButton);

        }

        private void HandleEvents()
        {
            phoneNumber1TextView.Click += PhoneNumber1TextView_Click;
            phoneNumber2TextView.Click += PhoneNumber2TextView_Click;
            back.Click += BackButton_Click;
        }

        private void PhoneNumber1TextView_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse("tel:" + phoneNumber1TextView.Text);
            var intent = new Intent(Intent.ActionCall);
            intent.SetData(uri);
            StartActivity(intent);
        }
        private void PhoneNumber2TextView_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse("tel:" + phoneNumber2TextView.Text);
            var intent = new Intent(Intent.ActionCall);
            intent.SetData(uri);
            StartActivity(intent);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Intent intent;
            if (main == 1)
            {
                intent = new Intent(this, typeof(MainActivity));
            }
            else
            {
                intent = new Intent(this, typeof(MainActivity2));
            }
            StartActivity(intent);
            Finish();
        }
    }
}
