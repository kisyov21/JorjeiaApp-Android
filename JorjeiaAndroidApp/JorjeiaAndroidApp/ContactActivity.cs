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
using Android.Locations;
using static Android.Gms.Common.Apis.GoogleApiClient;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Util;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "ContactActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ContactActivity : Activity, IConnectionCallbacks, IOnConnectionFailedListener, Android.Gms.Location.ILocationListener
    {
        private TextView phoneNumber1TextView;
        private TextView text1;
        private TextView text2;
        private ImageView phoneNumber2TextView;
        private Android.Widget.Button back;
        private int main;
        private Android.Net.Uri Uri;

        GoogleApiClient apiClient;
        LocationRequest locRequest;
        Location _currentLocation;
        //LocationManager _locationManager;
        //string _locationProvider;
        bool _isGooglePlayServicesInstalled;

        readonly string[] PermissionsLocation =
        {
            Manifest.Permission.CallPhone,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.Internet
            //, more
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

        private void OpenGoogleMapsButton_Click(object sender, EventArgs e)
        {
            //Android.Net.Uri jorjeiaLocationUri = Android.Net.Uri.Parse("geo:42.649184,23.379688");

            //if (_currentLocation == null)
            //{
            //    string message = "Не можем да определим вашето местонахождение. Моля опитайте по-късно.";
            //    Toast.MakeText(this, message, ToastLength.Long).Show();
            //    return;
            //}
            //FindStore(_currentLocation.Latitude, _currentLocation.Longitude);

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
                FindStore(_currentLocation.Latitude, _currentLocation.Longitude);
            }
            else
            {
                Log.Info("LocationClient", "Please wait for client to connect");
            }
            
            string addres = "ул.+„Кръстю+Раковски“+20,+1729+София";

            Android.Net.Uri jorjeiaLocationUri = Android.Net.Uri.Parse("geo:0,0?q=" + addres);

            Intent mapIntent = new Intent(Intent.ActionView, jorjeiaLocationUri);
            StartActivity(mapIntent);
        }

        private void FindStore(double latitude, double longitude)
        {
            //todo:
        }

        void InitializeLocationManager()
        {
            //    _locationManager = (LocationManager)GetSystemService(LocationService);
            //    Criteria criteriaForLocationService = new Criteria
            //    {
            //        Accuracy = Accuracy.Fine
            //    };
            //    IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            //    if (acceptableLocationProviders.Any())
            //    {
            //        _locationProvider = acceptableLocationProviders.First();
            //    }
            //    else
            //    {
            //        _locationProvider = string.Empty;
            //    }
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

        public void OnLocationChanged(Location location)
        {
            // This method returns changes in the user's location if they've been requested

            // You must implement this to implement the Android.Gms.Locations.ILocationListener Interface
            Log.Debug("LocationClient", "Location updated");

            _currentLocation = location;
        }

        public void OnConnectionSuspended(int i)
        {

        }
        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    _locationManager.RemoveUpdates(this);
        //}



        //void FindStore(double latitude, double longitude)
        //{
        //    ////System.Device.Location.GeoCoordinate
        //    //var coord = new GeoCoordinate(latitude, longitude);
        //    //var nearest = locations.Select(x => new GeoCoordinate(x.Latitude, x.Longitude))
        //    //               .OrderBy(x => x.GetDistanceTo(coord))
        //    //               .First();
        //}

        //public async void OnLocationChanged(Location location)
        //{
        //    _currentLocation = location;
        //}

        //public void OnProviderDisabled(string provider) { }

        //public void OnProviderEnabled(string provider) { }

        //public void OnStatusChanged(string provider, Availability status, Bundle extras) { }
    }
}

