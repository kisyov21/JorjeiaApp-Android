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
using Java.IO;
using Android.Graphics;
using Android.Provider;
using JorjeiaAndroidApp.Utility;
using JorjeiaAndroidApp.Resources.DataHelper;
using System.Threading.Tasks;
using Android;
using Android.Content.PM;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "CameraActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class CameraActivity : Activity
    {
        DataBase db;
        private ImageView rayPictureImageView;
        private Button takePictureButton;
        private Button nextButton;
        private File imageDirectory; //file to store my folder
        private File imageFile; //file to store my image
        private Bitmap imageBitmap; //to store my bitmap
        private TextView text1;

        int count = 1;

        readonly string[] PermissionsLocation =
        {
            Manifest.Permission.Camera,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.ReadExternalStorage//, more
        };

        const int RequestLocationId = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CameraView); //linked with TakePictureView

            FindViews();

            HandleEvents();

            //imageDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
            //    Android.OS.Environment.DirectoryPictures), "Jorjeia");  //Creating folder Jorjeia in gallery in order to store you picture

            //if (!imageDirectory.Exists()) // if folder does not exists I'm going to create it
            //{
            //    imageDirectory.Mkdirs();
            //}

        }

        private void FindViews()
        {
            rayPictureImageView = FindViewById<ImageView>(Resource.Id.rayPictureImageView);
            takePictureButton = FindViewById<Button>(Resource.Id.takePictureButton);
            nextButton = FindViewById<Button>(Resource.Id.nextCalendarButton);
            text1 = FindViewById<TextView>(Resource.Id.textCameraView1);
            Typeface tf = Typeface.CreateFromAsset(Assets, "MinionPro-Regular.ttf");
            text1.SetTypeface(tf, TypefaceStyle.Normal);
        }

        private void HandleEvents()
        {
            takePictureButton.Click += TakePictureButton_Click;
            nextButton.Click += NextButton_Click;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            var mission = Intent.GetIntExtra("TypeOfMission", 0);
            Intent intent;
            if (mission != 0)
            {
                intent = new Intent(this, typeof(MissionCreatedActivity));
                intent.PutExtra("TypeOfMission", Intent.GetIntExtra("TypeOfMission", 0));
                intent.PutExtra("TypeOfSkin", Intent.GetIntExtra("TypeOfSkin", 0));
                intent.PutExtra("Ages", Intent.GetIntExtra("Ages", 0));
                intent.PutExtra("Scar", Intent.GetIntExtra("Scar", 0));
                intent.PutExtra("Weight", Intent.GetIntExtra("Weight", 45));
            }
            else
            {
                intent = new Intent(this, typeof(CurrentMissionActivity));
            }
            StartActivity(intent);
            Finish();
        }

        private async void TakePictureButton_Click(object sender, EventArgs e)
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                await TakePicture();
                return;
            }

            await GetCameraPermissionAsync();
        }

        private async Task GetCameraPermissionAsync()
        {
            const string permission = Manifest.Permission.Camera;
            const string permission2 = Manifest.Permission.WriteExternalStorage;
            const string permission3 = Manifest.Permission.ReadExternalStorage;
            if (CheckSelfPermission(Manifest.Permission.Camera) == (int)Permission.Granted || CheckSelfPermission(Manifest.Permission.WriteExternalStorage) == (int)Permission.Granted || CheckSelfPermission(Manifest.Permission.ReadExternalStorage) == (int)Permission.Granted)
            {
                await TakePicture();
                return;
            }

            //need to request permission
            if (ShouldShowRequestPermissionRationale(permission) || ShouldShowRequestPermissionRationale(permission2) || ShouldShowRequestPermissionRationale(permission3))
            {

                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetTitle("Въпрос");
                callDialog.SetMessage("Разрешавате ли Jorjeia да има достъп до вашата камера.");
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

        private async Task TakePicture()
        {
            try
            {
                imageDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                Android.OS.Environment.DirectoryPictures), "Jorjeia");  //Creating folder Jorjeia in gallery in order to store you picture

                if (!imageDirectory.Exists()) // if folder does not exists I'm going to create it
                {
                    imageDirectory.Mkdirs();
                }

                Intent intent = new Intent(MediaStore.ActionImageCapture); //allow the user to take a picture 
                //imageFile = new File(imageDirectory, String.Format("Jorjeia_{0}.jpg", Guid.NewGuid())); //giving unique name to my image
                //intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(imageFile)); //passing my URI for the imageFile
                StartActivityForResult(intent, 0); // I call StartActivity again for Result because I'm expecting a result back inside of my application
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

                            Toast.MakeText(this, "Достъпа до камерата е разрешен", ToastLength.Long).Show();
                            await TakePicture();
                        }
                        else
                        {
                            //Permission Denied :(
                            //Disabling location functionality
                            Toast.MakeText(this, "Достъпа до камерата е забранен", ToastLength.Long).Show();
                            ;
                        }
                    }
                    break;
            }
        }

        //this method will be called automatically after the user has completed taken the image with the camera activity
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            ////specify what the required width and height for my image should be
            //int height = rayPictureImageView.Height;
            //int width = rayPictureImageView.Width;

            ////then I call my ImageHelper.GetImageBitmapFromFilePath
            ////imageBitmap = ImageHelper.GetImageBitmapFromFilePath(imageFile.Path, width, height);
            //Bitmap imageBitmap = (Bitmap)data.Extras.Get("data");
            //if (imageBitmap != null) //if that went ok, I'm going to set the bitmap for my imageView
            //{
            //    rayPictureImageView.SetImageBitmap(imageBitmap);
            //    imageBitmap = null;
            //}

            ////required to avoid memory leaks!
            //GC.Collect();
            var mission = Intent.GetIntExtra("TypeOfMission", 0);
            Intent intent;
            if (mission != 0)
            {
                intent = new Intent(this, typeof(MissionCreatedActivity));
                intent.PutExtra("TypeOfMission", Intent.GetIntExtra("TypeOfMission", 0));
                intent.PutExtra("TypeOfSkin", Intent.GetIntExtra("TypeOfSkin", 0));
                intent.PutExtra("Ages", Intent.GetIntExtra("Ages", 0));
                intent.PutExtra("Scar", Intent.GetIntExtra("Scar", 0));
                intent.PutExtra("Weight", Intent.GetIntExtra("Weight", 45));
            }
            else
            {
                intent = new Intent(this, typeof(CurrentMissionActivity));
            }
            StartActivity(intent);
            Finish();
        }

        public override void OnBackPressed()
        {
            var mission = Intent.GetIntExtra("TypeOfMission", 0);
            Intent intent;
            if (mission != 0)
            {
                intent = new Intent(this, typeof(NewMissionIntroActivity));
            }
            else
            {
                intent = new Intent(this, typeof(CurrentMissionActivity));
            }
            StartActivity(intent);
            Finish();
        }

    }
}