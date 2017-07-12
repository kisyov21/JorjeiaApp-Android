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
using Android.Support.V4.View;
using Java.Lang;

namespace JorjeiaAndroidApp.Utility
{
    public class ImageAdapter : PagerAdapter
    {
        public Context context;
        private int[] imageList = {
            Resource.Drawable.scar1,
            Resource.Drawable.scar2,
            Resource.Drawable.scar3,
            Resource.Drawable.scar4
        };

        public ImageAdapter(Context c)
        {
            this.context = c;
        }

        public override int Count
        {
            get
            {
                return imageList.Length;
            }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
        {
            return view == ((ImageView)objectValue);
        }

        public override Java.Lang.Object InstantiateItem(View container, int position)
        {
            ImageView imageView = new ImageView(context);
            imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
            imageView.SetImageResource(imageList[position]);
            ((ViewPager)container).AddView(imageView, 0);
            return imageView;
        }

        public override void DestroyItem(View container, int position, Java.Lang.Object objectValue)
        {
            ((ViewPager)container).RemoveView((ImageView)objectValue);
        }
    }

    //public class ImageAdapter : BaseAdapter
    //{
    //    Context context;

    //    public ImageAdapter(Context c)
    //    {
    //        context = c;
    //    }

    //    public override int Count { get { return thumbIds.Length; } }

    //    public override Java.Lang.Object GetItem(int position)
    //    {
    //        return null;
    //    }

    //    public override long GetItemId(int position)
    //    {
    //        return 0;
    //    }

    //    // create a new ImageView for each item referenced by the Adapter
    //    public override View GetView(int position, View convertView, ViewGroup parent)
    //    {
    //        ImageView i = new ImageView(context);

    //        i.SetImageResource(thumbIds[position]);
    //        i.LayoutParameters = new Gallery.LayoutParams(150, 100);
    //        i.SetScaleType(ImageView.ScaleType.FitXy);

    //        return i;
    //    }

    //    // references to our images
    //    int[] thumbIds = {
    //        Resource.Drawable.Logo,
    //        Resource.Drawable.drink_water
    // };
    //}
}