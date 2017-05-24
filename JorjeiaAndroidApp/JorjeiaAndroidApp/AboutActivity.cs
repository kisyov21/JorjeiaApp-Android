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
    [Activity(Label = "AboutActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class AboutActivity : Activity
    {
        Button back;
        int main;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AboutView);
            main = Intent.GetIntExtra("Main", 0);
            FindViews();

            HandleEvents();
        }

        private void FindViews()
        {
            back = FindViewById<Button>(Resource.Id.backAboutButton);
        }

        private void HandleEvents()
        {
            back.Click += BackButton_Click;
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
