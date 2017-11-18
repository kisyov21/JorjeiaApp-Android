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
using JorjeiaAndroidApp.Resources.Model;

namespace JorjeiaAndroidApp.Utility
{
    public static class Methods
    {
        public static List<Schedule> Calculate(int age)
        {
            List<Schedule> schedule = new List<Schedule>();
            int days = 0;
            int type;
            type = age <= 33 ? 1 : 2;

            switch (type)
            {
                case 1:
                    while(days < 60)
                    {
                        schedule.Add(new Schedule() { Date = DateTime.Today.AddDays(days), IsPassed = false, IsPassed2 = false, IsPassed3 = false, Day = days+1 });
                        days++;
                    }
                    break;
                case 2:
                    while (days < 90)
                    {
                        schedule.Add(new Schedule() { Date = DateTime.Today.AddDays(days), IsPassed = false, IsPassed2 = false, IsPassed3 = false, Day = days+1 });
                        days++;
                    }
                    break;
            }
            return schedule;

        }
    }
}