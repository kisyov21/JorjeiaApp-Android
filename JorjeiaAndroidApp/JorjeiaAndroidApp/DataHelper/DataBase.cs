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
using Android.Util;
using JorjeiaAndroidApp.Resources.Model;

namespace JorjeiaAndroidApp.Resources.DataHelper
{
    public class DataBase
    {
        readonly string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool CreateDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    connection.CreateTable<Mission>();
                    connection.CreateTable<Schedule>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool InsertIntoTableMission(Mission newMission)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    connection.Insert(newMission);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return false;
            }
        }


        public bool InsertIntoTableSchedule(List<Schedule> newSchedule)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    foreach (var item in newSchedule)
                    {
                        connection.Insert(item);
                    }

                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return false;
            }
        }

        public List<Mission> SelectTableMission()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    return connection.Table<Mission>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return null;
            }
        }

        public List<Schedule> SelectTableSchedule()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    return connection.Table<Schedule>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return null;
            }
        }

        public List<Schedule> SelectActiveDatesSchedule()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    return connection.Table<Schedule>().Where(s => s.IsPassed == true || s.IsPassed2 == true).ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return null;
            }
        }

        public bool UpdateTableSchedule(DateTime date)
        {
            TimeSpan end = new TimeSpan(12, 0, 0); //12 o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;

            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    if (now < end)
                    {
                        connection.Query<Mission>("UPDATE Schedule set IsPassed=? Where Date=? ", true, date);
                    }
                    else
                    {
                        connection.Query<Mission>("UPDATE Schedule set IsPassed2=? Where Date=? ", true, date);
                    }
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return false;
            }
        }

        public bool UpdateTableMission()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    connection.Query<Mission>("UPDATE Mission set hasMission=? Where hasMission=1 ", 0);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return false;
            }
        }

        public bool DeleteTableSchedule()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    var schedule = connection.Table<Schedule>().ToList();
                    foreach (var item in schedule)
                    {
                        connection.Delete(item);
                    }
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return false;
            }
        }

        public bool DeleteTableMission()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    var mission = connection.Table<Mission>().ToList();
                    foreach (var item in mission)
                    {
                        connection.Delete(item);
                    }
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return false;
            }
        }

        public List<Schedule> SelectScheduleByDate(DateTime date)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    var select = connection.Query<Schedule>("SELECT * FROM Schedule Where Date=? ", date);
                    return select;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return null;
            }
        }

        public object UpdateDayTableSchedule(DateTime day, bool isPassed1, bool isPassed2, bool isPassed3, bool isTwoTime)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    if (isTwoTime)
                    {
                        connection.Query<Mission>("UPDATE Schedule set IsPassed=?, IsPassed2=? Where Date=? ",
                            isPassed1, isPassed2, day);
                    }
                    else
                    {
                        connection.Query<Mission>("UPDATE Schedule set IsPassed=?, IsPassed2=?, IsPassed3=? Where Date=? ",
                            isPassed1, isPassed2, isPassed3, day);
                    }
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return false;
            }
        }
    }
}