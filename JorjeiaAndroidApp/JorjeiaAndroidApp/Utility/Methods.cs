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
        public static List<Schedule> Calculate(int type)
        {
            List<Schedule> schedule = new List<Schedule>();
            int days = 0;
            switch (type)
            {
                case 1:// "����� 1":
                    while(days < 61)
                    {
                        schedule.Add(new Schedule() { Date = DateTime.Today.AddDays(days), IsPassed = false });
                        days++;
                    }
                    break;
                case 2:// "����� 2":
                    while (days < 61)
                    {
                        schedule.Add(new Schedule() { Date = DateTime.Today.AddDays(days), IsPassed = false });
                        days++;
                    }
                    break;
                case 3:// "����� 3":
                    while (days < 61)
                    {
                        schedule.Add(new Schedule() { Date = DateTime.Today.AddDays(days), IsPassed = false });
                        days++;
                    }
                    break;
                case 4:// "����� 1+2":
                    while (days < 61)
                    {
                        schedule.Add(new Schedule() { Date = DateTime.Today.AddDays(days), IsPassed = false });
                        days++;
                    }
                    break;
                case 5:// "����� 1+3":
                    while (days < 61)
                    {
                        schedule.Add(new Schedule() { Date = DateTime.Today.AddDays(days), IsPassed = false });
                        days++;
                    }
                    break;
                case 6:// "����� 2+3":
                    while (days < 61)
                    {
                        schedule.Add(new Schedule() { Date = DateTime.Today.AddDays(days), IsPassed = false });
                        days++;
                    }
                    break;
                case 7:// "����� 1+2+3":
                    while (days < 61)
                    {
                        schedule.Add(new Schedule() { Date = DateTime.Today.AddDays(days), IsPassed = false });
                        days++;
                    }
                    break;
                case 8:// "����� 1 FOR MEN":
                    while (days < 61)
                    {
                        schedule.Add(new Schedule() { Date = DateTime.Today.AddDays(days), IsPassed = false });
                        days++;
                    }
                    break;
            }
            return schedule;

        }


    }
}