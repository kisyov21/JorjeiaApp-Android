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
        public int hasMission { get; set; }
        public int typeMission { get; set; }
        public int waterInMl { get; set; }
    }

    public class Schedule
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsPassed { get; set; }
        public bool IsPassed2 { get; set; }
        //public Schedule(DateTime date)
        //{
        //    this.Date = date;
        //    this.IsPassed = false;
        //}
    }
}