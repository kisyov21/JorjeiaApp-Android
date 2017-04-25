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

namespace JorjeiaAndroidApp
{
    [Activity(Label = "CameraActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class CameraActivity : Activity
    {
        private ImageView rayPictureImageView;
        private Button takePictureButton;
        private File imageDirectory; //file to store my folder
        private File imageFile; //file to store my image
        private Bitmap imageBitmap; //to store my bitmap

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CameraView); //linked with TakePictureView

            FindViews();

            HandleEvents();

            imageDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                Android.OS.Environment.DirectoryPictures), "Jorjeia");  //Creating folder Jorjeia in gallery in order to store you picture

            if (!imageDirectory.Exists()) // if folder does not exists I'm going to create it
            {
                imageDirectory.Mkdirs();
            }

        }

        private void FindViews()
        {
            rayPictureImageView = FindViewById<ImageView>(Resource.Id.rayPictureImageView);
            takePictureButton = FindViewById<Button>(Resource.Id.takePictureButton);
        }

        private void HandleEvents()
        {
            takePictureButton.Click += TakePictureButton_Click;
        }

        private void TakePictureButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture); //allow the user to take a picture 
            imageFile = new File(imageDirectory, String.Format("Jorjeia_{0}.jpg", Guid.NewGuid())); //giving unique name to my image
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(imageFile)); //passing my URI for the imageFile
            StartActivityForResult(intent, 0); // I call StartActivity again for Result because I'm expecting a result back inside of my application
        }

        //this method will be called automatically after the user has completed taken the image with the camera activity
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            //specify what the required width and height for my image should be
            int height = rayPictureImageView.Height;
            int width = rayPictureImageView.Width;

            //then I call my ImageHelper.GetImageBitmapFromFilePath
            imageBitmap = ImageHelper.GetImageBitmapFromFilePath(imageFile.Path, width, height);

            if (imageBitmap != null) //if that went ok, I'm going to set the bitmap for my imageView
            {
                rayPictureImageView.SetImageBitmap(imageBitmap);
                imageBitmap = null;
            }

            //required to avoid memory leaks!
            GC.Collect();
        }

    }
}