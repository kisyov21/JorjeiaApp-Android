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
using JorjeiaAndroidApp.Resources.DataHelper;
using JorjeiaAndroidApp.Resources.Model;

namespace JorjeiaAndroidApp
{
    [Activity(Label = "ScheduleActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ScheduleActivity : Activity
    {
        DataBase db = new DataBase();
        private List<Mission> lstSource;
        private Mission mission;
        private string text;
        private Button newMission;
        private TextView textView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ScheduleView);
            LoadData();
            switch (mission.typeMission)
            {
                case 1:
                    text = "1";
                    break;
                case 2:
                    text = "Здравейте, това е персоналната ни препоръка към използването на продукта Мисия 1 за постигане на оптимални резултати. Нанасяте Мисия 1, два пъти на ден сутрин и вечер, на чиста кожа до пълното му абсорбиране. Въпреки, че сте с нормална кожа, ви препоръчваме епидермална хидратация с Мисия 3, специално създаден за подпомагане на действието на Мисия 1, Боди Коктейл с хиалуронова киселина, с Арома или 100 % натурален. Приема на вода мин. 30 мл на кг телено тегло е силно препоръчително.";
                    break;
                case 3:
                    text = "2";
                    break;
                case 4:
                    text = "2";
                    break;
                case 5:
                    text = "2";
                    break;
                case 6:
                    text = "2";
                    break;
                case 7:
                    text = "2";
                    break;
                case 8:
                    text = "2";
                    break;
                case 9:
                    text = "2";
                    break;
                case 10:
                    text = "2";
                    break;
                case 11:
                    text = "2";
                    break;
                case 12:
                    text = "2";
                    break;
                case 13:
                    text = "2";
                    break;
                case 14:
                    text = "2";
                    break;
                case 15:
                    text = "2";
                    break;
                case 16:
                    text = "2";
                    break;
                case 17:
                    text = "2";
                    break;
                case 18:
                    text = "2";
                    break;
                case 19:
                    text = "2";
                    break;
                case 20:
                    text = "2";
                    break;
                case 21:
                    text = "2";
                    break;
                case 22:
                    text = "2";
                    break;
                case 23:
                    text = "2";
                    break;
                case 24:
                    text = "2";
                    break;
                default:
                    text = "Възникна грешка";
                    break;
            }
            FindViews();
            HandleEvents();
            // Create your application here
        }
        private void FindViews()
        {
            newMission = FindViewById<Button>(Resource.Id.nextSButton);
            textView = FindViewById<TextView>(Resource.Id.textCurrentMissionView);
            textView.Text = text;
        }

        private void HandleEvents()
        {
            newMission.Click += NewMissionButton_Click;
        }

        private void NewMissionButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CurrentMissionActivity));
            StartActivity(intent);
        }

        private void LoadData()
        {
            lstSource = db.selectTableMission();
            mission = lstSource.First();
        }
    }
}