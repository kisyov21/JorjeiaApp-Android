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
                case 1:// "Мисия 1":
                    while(days < 61)
                    {
                        schedule.Add(new Schedule() { Date = DateTime.Today.AddDays(days), IsPassed = false });
                        days++;
                    }
                    break;
                case 2:// "Мисия 2":

                    break;
                case 3:// "Мисия 3":

                    break;
                case 4:// "Мисии 1+2":

                    break;
                case 5:// "Мисии 1+3":

                    break;
                case 6:// "Мисии 2+3":

                    break;
                case 7:// "Мисии 1+2+3":

                    break;
                case 8:// "Мисия 1 FOR MEN":

                    break;
            }
            return schedule;
            //TODO
            //Ползване на мисия 1 
            //Цикъл за мазане - 60 дни
            //При жени от 13 - 33 по 2 пъти на ден ,сутрин и вечер.
            //При жени от 33 + по 3 пъти на ден



            //Ползване на мисия 2
            //Цикъл за мазане - 45 дни
            //При жени от 13 - 30 години, по 2 пъти на ден - сутрин и вечер.
            //При жени 30 + -3 пъти на ден, сутрин, обяд и вечер.


            //Ползване на мисия 1 + мисия 2
            //Жени от 13 - 30 години - 2 пъти на ден
            //Жени 30 + 2 пъти на ден
            //Първо мисия 1, след 3 минути мисия 2.



        }


    }
}