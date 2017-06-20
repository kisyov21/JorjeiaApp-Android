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
using System.Threading.Tasks;
using Android;
using Android.Content.PM;
using Xamarin.Forms;
using Android.Graphics;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "ContactActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ContactActivity : Activity
    {
        private TextView phoneNumber1TextView;
        private TextView text1;
        private TextView text2;
        private ImageView phoneNumber2TextView;
        private Android.Widget.Button back;
        private int main;
        private Android.Net.Uri Uri;

        readonly string[] PermissionsLocation =
       {
            Manifest.Permission.CallPhone //, more
        };

        const int RequestLocationId = 0;

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
            phoneNumber2TextView = FindViewById<ImageView>(Resource.Id.phoneNumber2TextView);
            back = FindViewById<Android.Widget.Button>(Resource.Id.backContactButton);
            text2 = FindViewById<TextView>(Resource.Id.callTextView);
            text1 = FindViewById<TextView>(Resource.Id.callText2View);
            Typeface tf = Typeface.CreateFromAsset(Assets, "MinionPro-Regular.ttf");
            text1.SetTypeface(tf, TypefaceStyle.Normal);
            text2.SetTypeface(tf, TypefaceStyle.Normal);
            phoneNumber1TextView.SetTypeface(tf, TypefaceStyle.Normal);
        }

        private void HandleEvents()
        {
            phoneNumber1TextView.Click += PhoneNumber1TextView_Click;
            phoneNumber2TextView.Click += PhoneNumber2TextView_Click;
            back.Click += BackButton_Click;
        }

        private async void PhoneNumber1TextView_Click(object sender, EventArgs e)
        {
            Uri = Android.Net.Uri.Parse("tel:" + phoneNumber1TextView.Text);
            if ((int)Build.VERSION.SdkInt < 23)
            {
                await Call();
                return;
            }

            await GetCallPermissionAsync();
        }

        private void PhoneNumber2TextView_Click(object sender, EventArgs e)
        {
            //Device.OpenUri(new Uri("fb://jorjeia/?fref=ts"));
            var uri = Android.Net.Uri.Parse("https://www.facebook.com/jorjeia/?fref=ts");
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }
 

        private async Task GetCallPermissionAsync()
        {
            const string permission = Manifest.Permission.CallPhone;
            if (CheckSelfPermission(Manifest.Permission.CallPhone) == (int)Permission.Granted)
            {
                await Call();
                return;
            }

            //need to request permission
            if (ShouldShowRequestPermissionRationale(permission))
            {

                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetTitle("Въпрос");
                callDialog.SetMessage("Разрешавате ли Jorjeia да има достъп до вашите повиквания.");
                callDialog.SetNeutralButton("Да", delegate
                {
                    RequestPermissions(PermissionsLocation, RequestLocationId);
                });
                callDialog.SetNegativeButton("Не", delegate { });

                callDialog.Show();
                return;
            }
            //Finally request permissions with the list of permissions and Id
            RequestPermissions(PermissionsLocation, RequestLocationId);
        }

        private async Task Call()
        {
            try
            {
                var intent = new Intent(Intent.ActionCall);
                intent.SetData(Uri);
                StartActivity(intent);
            }
            catch (Exception ex)
            {

            }
        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            //Permission granted

                            Toast.MakeText(this, "Достъп до повикванията е разрешен", ToastLength.Long).Show();
                            await Call();
                        }
                        else
                        {
                            //Permission Denied :(
                            //Disabling location functionality
                            Toast.MakeText(this, "Достъпа до повикванията е забранен", ToastLength.Long).Show();
                            ;
                        }
                    }
                    break;
            }
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
