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
using SQLite;

namespace JorjeiaAndroidApp.Resources.Model
{
    public class Mission
    {
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }
        public int hasMission { get; set; }
        public int typeMission { get; set; }
    }
}