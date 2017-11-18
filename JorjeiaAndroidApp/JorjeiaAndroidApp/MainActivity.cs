using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using JorjeiaAndroidApp.Resources.DataHelper;
using JorjeiaAndroidApp.Resources.Model;
using System.Collections.Generic;
using Android.Util;
using Android.Support.V7.App;
using Android.Graphics;
using Android.Views;

namespace JorjeiaAndroidApp
{
    //[Activity(Label = "JorjeiaAndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    [Activity(Label = "HomeActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity
    {
        DataBase db;
        private DateTime day = DateTime.Today;

        private Button _calendarButton;
        //private Button cameraButton;
        private Button _scheduleButton;
        private Button _mainMenu2Button;

        private List<Mission> _lstSource;
        private List<Schedule> _lstSchedule;

        private Button _newMissionButton;
        private Button _aboutButton;
        private Button _contactsButton;

        private Button _yesBtn;
        private Button _noBtn;

        private RadioButton _yesRadioButton1;
        private RadioButton _noRadioButton1;
        private RadioButton _yesRadioButton2;
        private RadioButton _noRadioButton2;
        private RadioButton _yesRadioButton3;
        private RadioButton _noRadioButton3;
        private Button _nextToBtn;
        private TextView _textDayView;
        private TextView _textFirstView;
        private TextView _textSecondView;
        private TextView _textThirdView;
        private bool _isPassed1 = false;
        private bool _isPassed2 = false;
        private bool _isPassed3 = false;
        private int _currentDay;

        private TextView _textView;

        private Schedule _today;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            db = new DataBase();
            db.CreateDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);

            LoadData();

            TimeSpan end = new TimeSpan(12, 0, 0); //12 o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;

            if (_lstSource.Count != 0 && _lstSource != null) // dali ima misiq
            {
                if (_lstSource[0].HasMission == 1) //dali e svyrshila
                {
                    List<Schedule> list = db.SelectScheduleByDate(day);
                    if (day > _lstSchedule[_lstSchedule.Count - 1].Date)
                    {
                        db.UpdateTableMission();
                        SetContentView(Resource.Layout.MissionFinish);
                        FindViews4();
                        HandleEvents4();
                    }
                    else if (list != null)
                    {
                        if (list.Count != 0)
                        {
                            foreach (var item in list)
                            {
                                _today = item;
                                //if (item.IsPassed && now < end)
                                //{
                                //    SetContentView(Resource.Layout.CurrentMissionView);
                                //    FindViews2();
                                //    HandleEvents2();
                                //}
                                //else if (item.IsPassed2 && now >= end)
                                //{
                                //    SetContentView(Resource.Layout.CurrentMissionView);
                                //    FindViews2();
                                //    HandleEvents2();
                                //}
                                //else
                                //{
                                //    SetContentView(Resource.Layout.ConfirmationView);
                                //    FindViews3();
                                //    HandleEvents3();
                                //}
                                if (item.IsPassed && item.IsPassed2)
                                {
                                    SetContentView(Resource.Layout.CurrentMissionView);
                                    FindViews2();
                                    HandleEvents2();
                                }
                                else
                                {
                                    SetContentView(Resource.Layout.ChangeStatusView);
                                    _currentDay = item.Day;
                                    FindViews5();
                                    HandleEvents5();
                                    SetSelectedRadioButtons(_today);
                                }
                            }
                        }
                        else
                        {
                            SetContentView(Resource.Layout.CurrentMissionView);
                            FindViews2();
                            HandleEvents2();
                        }
                    }
                }
            }
            else
            {
                SetContentView(Resource.Layout.Main);
                FindViews1();
                HandleEvents1();
            }

        }

        private void SetSelectedRadioButtons(Schedule item)
        {
            if (item.IsPassed)
            {
                _yesRadioButton1.Checked = true;
                _noRadioButton1.Checked = false;
                _isPassed1 = true;
            }
            else
            {
                _yesRadioButton1.Checked = false;
                _noRadioButton1.Checked = true;
                _isPassed1 = false;
            }
            if (item.IsPassed2)
            {
                _yesRadioButton2.Checked = true;
                _noRadioButton2.Checked = false;
                _isPassed2 = true;
            }
            else
            {
                _yesRadioButton2.Checked = false;
                _noRadioButton2.Checked = true;
                _isPassed2 = false;
            }
            if (!_lstSource[0].IsTwoTime)
            {
                if (item.IsPassed3)
                {
                    _yesRadioButton3.Checked = true;
                    _noRadioButton3.Checked = false;
                    _isPassed3 = true;
                }
                else
                {
                    _yesRadioButton3.Checked = false;
                    _noRadioButton3.Checked = true;
                    _isPassed3 = false;
                }
            }
        }

        private void HandleEvents5()
        {
            _yesRadioButton1.Click += YesRadioButton1_Click;
            _noRadioButton1.Click += NoRadioButton1_Click;
            _yesRadioButton2.Click += YesRadioButton2_Click;
            _noRadioButton2.Click += NoRadioButton2_Click;
            _nextToBtn.Click += NextButton_Click;
            if (!_lstSource[0].IsTwoTime)
            {
                _yesRadioButton3.Click += YesRadioButton3_Click;
                _noRadioButton3.Click += NoRadioButton3_Click;
            }
        }

        private void NoRadioButton3_Click(object sender, EventArgs e)
        {
            _isPassed3 = false;
            _yesRadioButton3.Checked = false;
            _noRadioButton3.Checked = true;
        }

        private void YesRadioButton3_Click(object sender, EventArgs e)
        {
            _isPassed3 = true;
            _yesRadioButton3.Checked = true;
            _noRadioButton3.Checked = false;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            _isPassed1 = _yesRadioButton1.Checked;
            _isPassed2 = _yesRadioButton2.Checked;
            var isOk = db.UpdateDayTableSchedule(day, _isPassed1, _isPassed2, _isPassed3, _lstSource[0].IsTwoTime);
            var intent = new Intent(this, typeof(DrinkWaterActivity));
            StartActivity(intent);
            Finish();
        }

        private void NoRadioButton2_Click(object sender, EventArgs e)
        {
            _isPassed2 = false;
            _yesRadioButton2.Checked = false;
            _noRadioButton2.Checked = true;
        }

        private void YesRadioButton2_Click(object sender, EventArgs e)
        {
            _isPassed2 = true;
            _yesRadioButton2.Checked = true;
            _noRadioButton2.Checked = false;
        }

        private void NoRadioButton1_Click(object sender, EventArgs e)
        {
            _isPassed1 = false;
            _yesRadioButton1.Checked = false;
            _noRadioButton1.Checked = true;
        }

        private void YesRadioButton1_Click(object sender, EventArgs e)
        {
            _isPassed1 = true;
            _yesRadioButton1.Checked = true;
            _noRadioButton1.Checked = false;
        }

        private void FindViews5()
        {
            _yesRadioButton1 = FindViewById<RadioButton>(Resource.Id.yesRadioButton1);
            _noRadioButton1 = FindViewById<RadioButton>(Resource.Id.noRadioButton1);
            _yesRadioButton2 = FindViewById<RadioButton>(Resource.Id.yesRadioButton2);
            _noRadioButton2 = FindViewById<RadioButton>(Resource.Id.noRadioButton2);
            _yesRadioButton3 = FindViewById<RadioButton>(Resource.Id.yesRadioButton3);
            _noRadioButton3 = FindViewById<RadioButton>(Resource.Id.noRadioButton3);
            _nextToBtn = FindViewById<Button>(Resource.Id.next2DrinkWButton);
            _textDayView = FindViewById<TextView>(Resource.Id.textDay);
            Typeface tf = Typeface.CreateFromAsset(Assets, "MinionPro-Regular.ttf");
            _textDayView.SetTypeface(tf, TypefaceStyle.Normal);
            _textDayView.Text = "Ден " + _currentDay;
            _textFirstView = FindViewById<TextView>(Resource.Id.textFirstView);
            _textFirstView.SetTypeface(tf, TypefaceStyle.Normal);
            _textSecondView = FindViewById<TextView>(Resource.Id.textSecondView);
            _textSecondView.SetTypeface(tf, TypefaceStyle.Normal);
            _textThirdView = FindViewById<TextView>(Resource.Id.textThirdView);
            _textThirdView.SetTypeface(tf, TypefaceStyle.Normal);

            if (_lstSource[0].IsTwoTime)
            {
                _textThirdView.Visibility = ViewStates.Invisible;
                _noRadioButton3.Visibility = ViewStates.Invisible;
                _yesRadioButton3.Visibility = ViewStates.Invisible;
            }
        }

        private void HandleEvents4()
        {
            _yesBtn.Click += YesButton2_Click;
        }

        private void YesButton2_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity2));
            StartActivity(intent);
            Finish();
        }

        private void FindViews4()
        {
            _yesBtn = FindViewById<Button>(Resource.Id.finishYesBtn);
            _textView = FindViewById<TextView>(Resource.Id.finishTextView);
            Typeface tf = Typeface.CreateFromAsset(Assets, "MinionPro-Regular.ttf");
            _textView.SetTypeface(tf, TypefaceStyle.Normal);
        }
        #region Confirmation View
        private void HandleEvents3()
        {
            _yesBtn.Click += YesButton_Click;
            _noBtn.Click += NoButton_Click;
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(DrinkWaterActivity));
            StartActivity(intent);
            Finish();
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            var isOk = db.UpdateTableSchedule(day);
            var intent = new Intent(this, typeof(DrinkWaterActivity));
            StartActivity(intent);
            Finish();
        }

        private void FindViews3()
        {
            _yesBtn = FindViewById<Button>(Resource.Id.confirmYesBtn);
            _noBtn = FindViewById<Button>(Resource.Id.confirmNoBtn);
            _textView = FindViewById<TextView>(Resource.Id.textViewc);
            Typeface tf = Typeface.CreateFromAsset(Assets, "MinionPro-Regular.ttf");
            _textView.SetTypeface(tf, TypefaceStyle.Normal);
        }
        #endregion

        #region Current Mission View
        private void HandleEvents2()
        {
            _calendarButton.Click += CalendarButton_Click;
            _mainMenu2Button.Click += MainMenu_Click;
            //cameraButton.Click += Camera_Click;
            _scheduleButton.Click += Schedule_Click;
        }

        private void Schedule_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ScheduleActivity));
            StartActivity(intent);
            Finish();
        }

        private void MainMenu_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity2));
            StartActivity(intent);
            Finish();
        }

        private void Camera_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CameraActivity));
            StartActivity(intent);
            Finish();
        }

        private void CalendarButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CalendarJorjeiaActivity));
            StartActivity(intent);
        }

        private void FindViews2()
        {
            _calendarButton = FindViewById<Button>(Resource.Id.calendarBtn);
            //cameraButton = FindViewById<Button>(Resource.Id.cameracCurrBtn);
            _scheduleButton = FindViewById<Button>(Resource.Id.scheduleBtn);
            _mainMenu2Button = FindViewById<Button>(Resource.Id.mainMenu2btn);
        }
        #endregion

        #region Main view


        private void FindViews1()
        {
            _newMissionButton = FindViewById<Button>(Resource.Id.newMissionButton);
            _aboutButton = FindViewById<Button>(Resource.Id.about);
            _contactsButton = FindViewById<Button>(Resource.Id.contacts);
        }

        private void HandleEvents1()
        {
            _newMissionButton.Click += NewMissionButton_Click;
            _aboutButton.Click += AboutButton_Click;
            _contactsButton.Click += ContactsButton_Click;
        }

        private void ContactsButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ContactActivity));
            intent.PutExtra("Main", 1);
            StartActivity(intent);
            Finish();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AboutActivity));
            intent.PutExtra("Main", 1);
            StartActivity(intent);
            Finish();
        }

        private void NewMissionButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(NewMissionIntroActivity));
            StartActivity(intent);
            Finish();
        }

        private void CurrentMissionButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CurrentMissionActivity));
            StartActivity(intent);
            Finish();
        }
        #endregion

        private void LoadData()
        {
            _lstSource = db.SelectTableMission();
            _lstSchedule = db.SelectTableSchedule();
        }
    }
}

