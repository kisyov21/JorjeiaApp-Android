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
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool createDataBase()
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

        public bool insertIntoTableMission(Mission newMission)
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


        public bool insertIntoTableSchedule(List<Schedule> newSchedule)
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

        public List<Mission> selectTableMission()
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

        public List<Schedule> selectTableSchedule()
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

        public List<Schedule> selectActiveDatesSchedule()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    return connection.Table<Schedule>().Where(s => s.IsPassed==true).ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return null;
            }
        }

        public bool updateTableSchedule(DateTime date)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    connection.Query<Mission>("UPDATE Schedule set IsPassed=? Where Date=? ", true, date);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return false;
            }
        }

        public bool deleteTableSchedule()
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

        public bool deleteTableMission()
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

        public List<Schedule> selectScheduleByDate(DateTime date)
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
    }
}