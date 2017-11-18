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
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int HasMission { get; set; }
        public int TypeMission { get; set; }
        public int WaterInMl { get; set; }
        public bool IsTwoTime { get; set; }
    }

    public class Schedule
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsPassed { get; set; }
        public bool IsPassed2 { get; set; }
        public bool IsPassed3 { get; set; }
        public int Day { get; set; }
    }
}