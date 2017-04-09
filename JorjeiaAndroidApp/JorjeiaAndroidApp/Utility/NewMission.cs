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

namespace JorjeiaAndroidApp.Utility
{
    public class NewMission
    {
        public string age { get; set; }
        public string skin { get; set; }
        public string missions { get; set; }
        public string gender { get; set; }

        public NewMission()
        {

        }
        /// <summary>
        /// Constructor for clas NewMission
        /// </summary>
        /// <param name="_age"></param>
        /// <param name="_skin"></param>
        /// <param name="_missions"></param>
        /// <param name="_gender"></param>
        public NewMission(string _age, string _skin, string _missions, string _gender)
        {
            this.age = _age;
            this.skin = _skin;
            this.missions = _missions;
            this.gender = _gender;
        }
    }
}