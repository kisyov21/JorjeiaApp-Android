using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Json;

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
using Android.Locations;
using static Android.Gms.Common.Apis.GoogleApiClient;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Util;
using JorjeiaAndroidApp.Utility;
using Newtonsoft.Json;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "ContactActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ContactActivity : Activity, IConnectionCallbacks, IOnConnectionFailedListener, Android.Gms.Location.ILocationListener
    {
        private TextView phoneNumber1TextView;
        private TextView text1;
        private TextView text2;
        private ImageView facebookTextView;
        private Android.Widget.Button back;
        private int main;
        private Android.Net.Uri Uri;
        private string url = "https://testinglocations.herokuapp.com/getlocations";
        GoogleApiClient apiClient;
        LocationRequest locRequest;
        Location _currentLocation;
        //LocationManager _locationManager;
        //string _locationProvider;
        bool _isGooglePlayServicesInstalled;
        //NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
        //bool isOnline = networkInfo.IsConnected;

        readonly string[] PermissionsLocation =
        {
            //Manifest.Permission.CallPhone,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.Internet
            //, more
        };

        enum Method
        {
            Call,
            OpenGoogleMap
        };


        const int RequestLocationId = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ContactView);
            main = Intent.GetIntExtra("Main", 0);
            FindViews();
            HandleEvents();
            InitializeLocationManager();
        }

        private async Task<string> FetchWeatherAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
                //WebResponse response = await request.GetResponseAsync();
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                return sr.ReadToEnd();
                //JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                // Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                // Return the JSON document:
                //return jsonDoc.ToString();
            }
            catch (Exception ex)
            {
                var excep = ex.Message;
                return "false";
            }
            // Send the request to the server and wait for the response:
            //using (WebResponse response = await request.GetResponseAsync())
            //{
            //    // Get a stream representation of the HTTP web response:
            //    using (Stream stream = response.GetResponseStream())
            //    {
            //        // Use this stream to build a JSON document object:
            //        JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
            //        Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

            //        // Return the JSON document:
            //        return jsonDoc.ToString();
            //    }
            //}
        }

        bool IsGooglePlayServicesInstalled()
        {
            int queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info("MainActivity", "Google Play Services is installed on this device.");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                string errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Log.Error("ManActivity", "There is a problem with Google Play Services on this device: {0} - {1}", queryResult, errorString);

                // Show error dialog to let user debug google play services
            }
            return false;
        }

        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }

        private void FindViews()
        {
            phoneNumber1TextView = FindViewById<TextView>(Resource.Id.phoneNumber1TextView);
            facebookTextView = FindViewById<ImageView>(Resource.Id.facebookTextView);
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
            //phoneNumber1TextView.Click += PhoneNumber1TextView_Click;
            facebookTextView.Click += facebookTextView_Click;
            back.Click += OpenGoogleMapsButton_Click;
        }

        private async void PhoneNumber1TextView_Click(object sender, EventArgs e)
        {
            Uri = Android.Net.Uri.Parse("tel:" + phoneNumber1TextView.Text);
            if ((int)Build.VERSION.SdkInt < 23)
            {
                await Call();
                return;
            }

            await GetPermissionAsync(Method.Call);
        }

        private void facebookTextView_Click(object sender, EventArgs e)
        {
            //Device.OpenUri(new Uri("fb://jorjeia/?fref=ts"));
            var uri = Android.Net.Uri.Parse("https://www.facebook.com/jorjeia/?fref=ts");
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }


        private async Task GetPermissionAsync(Method method)
        {
            try
            {
                const string permission1 = Manifest.Permission.CallPhone;
                const string permission2 = Manifest.Permission.AccessFineLocation;
                const string permission3 = Manifest.Permission.AccessCoarseLocation;
                const string permission4 = Manifest.Permission.Internet;

                if (CheckSelfPermission(Manifest.Permission.CallPhone) == (int)Permission.Granted && method == Method.Call)
                {
                    await Call();
                    return;
                }
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) == (int)Permission.Granted
                    && CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) == (int)Permission.Granted
                    && CheckSelfPermission(Manifest.Permission.Internet) == (int)Permission.Granted
                    && method == Method.OpenGoogleMap)
                {
                    await OpenGoogleMaps();
                    return;
                }

                //need to request permission
                if (ShouldShowRequestPermissionRationale(permission1) || ShouldShowRequestPermissionRationale(permission2) || ShouldShowRequestPermissionRationale(permission3) || ShouldShowRequestPermissionRationale(permission4))
                {

                    var callDialog = new AlertDialog.Builder(this);
                    callDialog.SetTitle("Въпрос");
                    callDialog.SetMessage("Разрешавате ли Jorjeia да има достъп.");
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
            catch (Exception)
            {

                throw;
            }
        }

        private async Task Call()
        {
            var intent = new Intent(Intent.ActionCall);
            intent.SetData(Uri);
            StartActivity(intent);
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

        private async void OpenGoogleMapsButton_Click(object sender, EventArgs e)
        {
            try
            {

                ProgressDialog progressbar = new ProgressDialog(this);
                progressbar.Indeterminate = true;
                progressbar.SetProgressStyle(ProgressDialogStyle.Spinner);
                progressbar.SetMessage("Моля изчакайте...");
                progressbar.SetCancelable(false);
                //progressbar.Show();

                if (CheckPhone())
                {
                    if ((int)Build.VERSION.SdkInt < 23)
                    {
                        await OpenGoogleMaps();
                        progressbar.Dismiss();
                    }
                    else
                    {
                        await GetPermissionAsync(Method.OpenGoogleMap);
                        progressbar.Dismiss();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task OpenGoogleMaps()
        {
            try
            {
                string text = await FetchWeatherAsync(url);

                RootObject locations = JsonConvert.DeserializeObject<RootObject>(text);

                string addres = "";

                if (apiClient.IsConnected)
                {
                    Location location = LocationServices.FusedLocationApi.GetLastLocation(apiClient);
                    if (location != null)
                    {
                        if (_currentLocation != null)
                        {
                            string message = "Не можем да определим вашето местонахождение. Моля опитайте по-късно.";
                            Toast.MakeText(this, message, ToastLength.Long).Show();
                            return;
                        }
                        _currentLocation = location;
                    }
                    addres = ClosetLocation.GetClosetPoint(_currentLocation, locations);
                }
                else
                {
                    Log.Info("LocationClient", "Please wait for client to connect");
                }

                Android.Net.Uri jorjeiaLocationUri = Android.Net.Uri.Parse("geo:0,0?q=" + addres);
                Intent mapIntent = new Intent(Intent.ActionView, jorjeiaLocationUri);
                StartActivity(mapIntent);
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        void InitializeLocationManager()
        {
            _isGooglePlayServicesInstalled = IsGooglePlayServicesInstalled();

            if (_isGooglePlayServicesInstalled)
            {
                // pass in the Context, ConnectionListener and ConnectionFailedListener
                apiClient = new GoogleApiClient.Builder(this, this, this)
                    .AddApi(LocationServices.API).Build();

                // generate a location request that we will pass into a call for location updates
                locRequest = new LocationRequest();

            }
            else
            {
                Toast.MakeText(this, "Google Play Services is not installed", ToastLength.Long).Show();
                Finish();
            }
        }

        public bool CheckPhone()
        {
            Android.Net.ConnectivityManager connectivityManager = (Android.Net.ConnectivityManager)GetSystemService(ConnectivityService);
            Android.Net.NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
            if (isOnline == false)
            {
                Toast.MakeText(this, "Мобилните данни не са включени.", ToastLength.Long).Show();
                return false;
            }
            else
            {
                LocationManager mlocManager = (LocationManager)GetSystemService(LocationService); ;
                bool enabled = mlocManager.IsProviderEnabled(LocationManager.GpsProvider);
                if (enabled == false)
                {
                    Toast.MakeText(this, "Местоположението не е включено.", ToastLength.Long).Show();
                    return false;
                }
            }
            return true;
        }

        protected override void OnResume()
        {
            base.OnResume();
            Log.Debug("OnResume", "OnResume called, connecting to client...");

            apiClient.Connect();
        }

        protected override async void OnPause()
        {
            base.OnPause();
            Log.Debug("OnPause", "OnPause called, stopping location updates");

            if (apiClient.IsConnected)
            {
                // stop location updates, passing in the LocationListener
                await LocationServices.FusedLocationApi.RemoveLocationUpdates(apiClient, this);

                apiClient.Disconnect();
            }
        }


        ////Interface methods

        public void OnConnected(Bundle bundle)
        {
            // This method is called when we connect to the LocationClient. We can start location updated directly form
            // here if desired, or we can do it in a lifecycle method, as shown above 

            // You must implement this to implement the IGooglePlayServicesClientConnectionCallbacks Interface
            Log.Info("LocationClient", "Now connected to client");
        }

        public void OnDisconnected()
        {
            // This method is called when we disconnect from the LocationClient.

            // You must implement this to implement the IGooglePlayServicesClientConnectionCallbacks Interface
            Log.Info("LocationClient", "Now disconnected from client");
        }

        public void OnConnectionFailed(ConnectionResult bundle)
        {
            // This method is used to handle connection issues with the Google Play Services Client (LocationClient). 
            // You can check if the connection has a resolution (bundle.HasResolution) and attempt to resolve it

            // You must implement this to implement the IGooglePlayServicesClientOnConnectionFailedListener Interface
            Log.Info("LocationClient", "Connection failed, attempting to reach google play services");
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            // This method returns changes in the user's location if they've been requested

            // You must implement this to implement the Android.Gms.Locations.ILocationListener Interface
            Log.Debug("LocationClient", "Location updated");

            _currentLocation = location;
        }

        public void OnConnectionSuspended(int i)
        {

        }
    }
}

