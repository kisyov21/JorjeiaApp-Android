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

        //public bool updateTableMission(Mission mission)
        //{
        //    try
        //    {
        //        using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
        //        {
        //            connection.Query<Mission>("UPDATE Mission set hasMission=?,typeMission=? Where Id=? ", mission.hasMission, mission.typeMission,mission.Id);
        //            return true;
        //        }
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        Log.Info("SQLitEx", ex.Message);
        //        return false;
        //    }
        //}

        public bool deleteTableMission(Mission mission)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
                {
                    connection.Delete(mission);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLitEx", ex.Message);
                return false;
            }
        }

        //public bool selectMissionById(int id)
        //{
        //    try
        //    {
        //        using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Missions.db")))
        //        {
        //            connection.Query<Mission>("SELECT * FROM Mission Where Id=? ", id);
        //            return true;
        //        }
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        Log.Info("SQLitEx", ex.Message);
        //        return false;
        //    }
        //}
    }
}