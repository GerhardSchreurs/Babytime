
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using babytime.Services;
using static Android.App.TimePickerDialog;

namespace babytime
{
    [Activity(Label = "ClockActivity")]
    public class DaddyActivity : AppCompatActivity, IOnTimeSetListener
    {
        private Android.Support.V7.Widget.Toolbar _toolbar;
        private TextView _textViewTime;
        private Switch _switchAlarm;
        private int _alarmHour;
        private int _alarmMinute;
        private AlarmManager _alarmManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.A_Daddy);

            _alarmManager = (AlarmManager)GetSystemService(Context.AlarmService).JavaCast<AlarmManager>();
                
            _toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            _textViewTime = FindViewById<TextView>(Resource.Id.textViewTime);
            _switchAlarm = FindViewById<Switch>(Resource.Id.switchAlarm);


            SetSupportActionBar(_toolbar);
        }

        private void InitClock()
        {
            _alarmHour = Utility.Settings.GetAlarmHour();
            _alarmMinute = Utility.Settings.GetAlarmMinute();

            if (_alarmHour == -1 || _alarmMinute == -1)
            {
                var date = DateTime.Now;

                _alarmHour = date.Hour;
                _alarmMinute = date.Minute;
            }

            var strHour = String.Format("{00}", _alarmHour);
            var strMinute = String.Format("{00}", _alarmMinute);

            _textViewTime.Text = $"{strHour}:{strMinute}";
        }

        protected override void OnResume()
        {
            base.OnResume();

            InitClock();
            _textViewTime.Click += Handle_textViewTime_Click;
            _switchAlarm.Click += Handle_switchAlarm_Click;
        }

        private void Handle_switchAlarm_Click(object sender, EventArgs e)
        {
            if (_switchAlarm.Checked)
            {
                var calendar = Calendar.Instance;
                calendar.Set(CalendarField.HourOfDay, _alarmHour);
                calendar.Set(CalendarField.Minute, _alarmMinute);


                var alarmIntent = new Intent(this, typeof(AlarmReceiver));
                var pendingIntent = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

                var date = DateTime.Now;
                date.AddMilliseconds(3000);



                _alarmManager.Set(AlarmType.Rtc, date.Millisecond, pendingIntent);

                //Intent myIntent = new Intent(AlarmActivity.this, AlarmReceiver.class);
                //pendingIntent = PendingIntent.getBroadcast(AlarmActivity.this, 0, myIntent, 0);
                //alarmManager.set(AlarmManager.RTC, calendar.getTimeInMillis(), pendingIntent);
            }
            else
            {
                //TODO
                //_alarmManager.Cancel(this);
            }

        }

        protected override void OnPause()
        {
            base.OnPause();

            _textViewTime.Click -= Handle_textViewTime_Click;
        }


        private void Handle_textViewTime_Click(object sender, EventArgs e)
        {
            var timePicker = new TimePickerDialog(this, this, _alarmHour, _alarmMinute, true);
            timePicker.Show();

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.M_Main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var id = item.ItemId;

            if (id == Resource.Id.action_settings)
            {
                using (var intent = new Intent(this, typeof(SettingsActivity)))
                {
                    StartActivity(intent);
                }

            }

            return base.OnOptionsItemSelected(item);
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            Utility.Settings.SetAlarmTime(hourOfDay, minute);
            InitClock();
        }
    }
}
