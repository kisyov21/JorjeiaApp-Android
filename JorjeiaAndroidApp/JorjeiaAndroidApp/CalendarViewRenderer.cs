using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JorjeiaAndroidApp;
using Square.TimesSquare;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Calendar.Controls.CalendarView), typeof(CalendarViewRenderer))]

namespace JorjeiaAndroidApp
{
    public class CalendarViewRenderer : ViewRenderer<Calendar.Controls.CalendarView, Android.Views.View>
    {
        private CalendarPickerView _pickerView;

        private Android.Views.View _view;

        public CalendarViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Calendar.Controls.CalendarView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var inflatorService = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
                _view = (LinearLayout)inflatorService.Inflate(Resource.Layout.CalendarView, null, false);
                _pickerView = _view.FindViewById<CalendarPickerView>(Resource.Id.calendar_view);
                _pickerView.Init(DateTime.Now.AddYears(-4), DateTime.Now.AddYears(4))
                    .WithSelectedDate(DateTime.Today)
                    .InMode(CalendarPickerView.SelectionMode.Single);
                _pickerView.DateSelected += (sender, args) =>
                {
                    Element.SelectedDate = args.Date;
                    Element.NotifyDateSelected(args.Date);
                };

                _pickerView.HighlightDates(Element.HighlightedDays);
                SetNativeControl(_view);
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var width = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var height = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);
            _view.Measure(width, height);
            _view.Layout(0, 0, r - l, b - t);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Calendar.Controls.CalendarView.HighlightedDaysProperty.PropertyName)
            {
                _pickerView.ClearHighlightedDates();
                _pickerView.HighlightDates(Element.HighlightedDays);
            }
        }
    }
}