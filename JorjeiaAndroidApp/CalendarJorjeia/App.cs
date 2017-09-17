using System;
using System.Collections.Generic;
using CalendarJorjeia.Models;
using Xamarin.Forms;
using XamForms.Controls;

namespace CalendarJorjeia
{
    public class App : Application
    {
        Calendar calendar;
        CalendarVM _vm;

        public App(List<ScheduleModel> data)
        {
            var specialDates = new List<SpecialDate>();
            foreach (var item in data)
            {
                if (item.IsPassed == true && item.IsPassed2 == true)
                {
                    specialDates.Add(
                        new SpecialDate(item.Date)
                        {
                            Selectable = true,
                            BorderColor = Color.DarkGreen,
                            BackgroundImage = FileImageSource.FromFile("Icon.png") as FileImageSource
                        }
                    );
                }
                else if (item.IsPassed == true || item.IsPassed2 == true)
                {
                    specialDates.Add(
                        new SpecialDate(item.Date)
                        {
                            //BackgroundColor = Color.LightGreen,
                            TextColor = Color.Accent,
                            BorderColor = Color.White,
                            BorderWidth = 4,
                            Selectable = true,
                            BackgroundPattern = new BackgroundPattern(1)
                            {
                                Pattern = new List<Pattern>
                                       {
                                           new Pattern{ WidthPercent = 1f, HightPercent = 0.50f, Color = Color.White},
                                           new Pattern{ WidthPercent = 1f, HightPercent = 0.50f, Color = Color.LightGreen}
                                       }
                            }
                        }
                    );
                }
                else
                {
                    if (DateTime.Now > item.Date)
                    {
                        specialDates.Add(
                            new SpecialDate(item.Date)
                            {
                                BackgroundColor = Color.Red,
                                TextColor = Color.White,
                                BorderColor = Color.White,
                                BorderWidth = 4,
                                Selectable = true
                            }
                        );
                    }
                    else
                    {
                        specialDates.Add(
                            new SpecialDate(item.Date)
                            {
                                BackgroundColor = Color.LightBlue,
                                TextColor = Color.Accent,
                                BorderColor = Color.White,
                                BorderWidth = 4,
                                Selectable = true
                            }
                        );
                    }
                }
            }

            calendar = new Calendar
            {
                //MaxDate = DateTime.Now.AddDays(30),
                //MinDate = DateTime.Now.AddDays(-1),
                //DisableDatesLimitToMaxMinRange = true,
                MultiSelectDates = false,
                DisableAllDates = false,
                WeekdaysShow = true,
                ShowNumberOfWeek = false,
                //BorderWidth = 1,
                //BorderColor = Color.Transparent,
                //OuterBorderWidth = 0,
                //SelectedBorderWidth = 1,
                ShowNumOfMonths = 3,
                EnableTitleMonthYearView = true,
                WeekdaysTextColor = Color.Teal,
                StartDay = DayOfWeek.Monday,
                SelectedTextColor = Color.Fuchsia,
                SpecialDates = specialDates
            };


            calendar.DateClicked += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine(calendar.SelectedDates);
            };
            _vm = new CalendarVM();
            var c2 = new CalendarXamlView();

            c2.BindingContext = _vm;

            // The root page of your application
            MainPage = new ContentPage
            {
                BackgroundColor = Color.White,
                Content = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Padding = new Thickness(5, Device.RuntimePlatform == Device.iOS ? 25 : 5, 5, 5),
                        Children =
                            {
                                calendar //,c2
                            }
                    }
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //calendar.SpecialDates.Add(new SpecialDate(DateTime.Now.AddDays(5)) { BackgroundColor = Color.Fuchsia, TextColor = Color.Accent, BorderColor = Color.Maroon, BorderWidth = 8 });
            //calendar.SpecialDates.Add(new SpecialDate(DateTime.Now.AddDays(6)) { BackgroundColor = Color.Fuchsia, TextColor = Color.Accent, BorderColor = Color.Maroon, BorderWidth = 8 });
            //calendar.RaiseSpecialDatesChanged();

            //var dates = new List<SpecialDate>();

            //var specialDate = new SpecialDate(new DateTime(2017, 04, 26));
            //specialDate.BackgroundColor = Color.Green;
            //specialDate.TextColor = Color.White;

            //dates.Add(specialDate);

            //_vm.Attendances = new ObservableCollection<SpecialDate>(dates);
            calendar.SelectedDate = (DateTime.Now);

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Ob resumes
        }
    }
}
