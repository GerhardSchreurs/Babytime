
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
using babytime.Utility;
using static Android.App.TimePickerDialog;

namespace babytime
{
    [Activity(Label = "ClockActivity", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    public class DaddyActivity : AppCompatActivity, IOnTimeSetListener
    {
        private Android.Support.V7.Widget.Toolbar _toolbar;
        private TextView _textViewTime;
        private Switch _switchAlarm;
        private int _alarmHour;
        private int _alarmMinute;
        private AlarmManager _alarmManager;

        private LinearLayout _linearLayoutClockSettings;
        private LinearLayout _linearLayoutClockIsRinging;
        private Button _buttonStop;

        const string TAG = "DaddyActivity";

        public void WL(string arg1)
        {
           System.Diagnostics.Debug.WriteLine(arg1);
        }
        public void WL(string arg1, string arg2)
        {
            System.Diagnostics.Debug.WriteLine(arg1 + " - " + arg2);
        }
        public void WT(string arg1)
        {
            System.Diagnostics.Debug.WriteLine(TAG + " :: " + arg1);
        }
        public void WT(string arg1, string arg2)
        {
            System.Diagnostics.Debug.WriteLine(TAG + ":: " + arg1 + " - " + arg2);
            ApplicationContext.
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            WT("OnCreate");

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.A_Daddy);

            _alarmManager = (AlarmManager)GetSystemService(Context.AlarmService).JavaCast<AlarmManager>();
                
            _toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            _textViewTime = FindViewById<TextView>(Resource.Id.textViewTime);
            _switchAlarm = FindViewById<Switch>(Resource.Id.switchAlarm);

            _linearLayoutClockSettings = FindViewById<LinearLayout>(Resource.Id.linearLayoutClockSettings);
            _linearLayoutClockIsRinging = FindViewById<LinearLayout>(Resource.Id.linearLayoutClockIsRinging);
            _buttonStop = FindViewById<Button>(Resource.Id.buttonStop);

            SetSupportActionBar(_toolbar);
            InitClock(); //TODO: Set again after update
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

                _textViewTime.Alpha = .5f;
            }
            else
            {
                _textViewTime.Alpha = 1;
            }

            var strHour = _alarmHour.ToString("D2");
            var strMinute = _alarmMinute.ToString("D2");

            _textViewTime.Text = $"{strHour}:{strMinute}";
        }

        protected override void OnResume()
        {
            WT("OnResume");

            base.OnResume();

            _textViewTime.Click += Handle_textViewTime_Click;
            _switchAlarm.Click += Handle_switchAlarm_Click;
            EventController.AlarmIsRinging += HandleEventController_AlarmIsRinging;
            _buttonStop.Click += Handle_ButtonStop_Click;

            if (Manager.Instance.IsAlarmGoingOf)
            {
                ShowAlarmView();
            }
            else
            {
                ShowSettingsView();
            }
        }

        private void ShowAlarmView()
        {
            _linearLayoutClockSettings.Visibility = ViewStates.Gone;
            _linearLayoutClockIsRinging.Visibility = ViewStates.Visible;
        }

        private void ShowSettingsView()
        {
            _linearLayoutClockSettings.Visibility = ViewStates.Visible;
            _linearLayoutClockIsRinging.Visibility = ViewStates.Gone;
        }

        protected override void OnPause()
        {
            WT("OnPause");

            base.OnPause();

            _textViewTime.Click -= Handle_textViewTime_Click;
            _switchAlarm.Click -= Handle_switchAlarm_Click;
            EventController.AlarmIsRinging -= HandleEventController_AlarmIsRinging;
            _buttonStop.Click -= Handle_ButtonStop_Click;
        }

        private void Handle_ButtonStop_Click(object sender, EventArgs e)
        {
            Manager.Instance.StopAlarm();
            ShowSettingsView();
        }

        private void HandleEventController_AlarmIsRinging(object sender, EventArgs e)
        {
            ShowAlarmView();
        }

        void SetAlarmTest()
        {
            var alarmIntent = new Intent(this, typeof(AlarmReceiver));
            var pendingIntent = PendingIntent.GetBroadcast(this, 1, alarmIntent, PendingIntentFlags.UpdateCurrent);
            //var date = DateTime.Now.AddSeconds(60);

            var calendar = Calendar.Instance;
            calendar.Set(CalendarField.HourOfDay, DateTime.Now.Hour);
            calendar.Set(CalendarField.Minute, DateTime.Now.Minute + 1);

            _alarmManager.SetExact(AlarmType.RtcWakeup, calendar.TimeInMillis, pendingIntent);
        }

        void SetAlarm()
        {
            var calendar = Calendar.Instance;
            calendar.Set(CalendarField.HourOfDay, _alarmHour);
            calendar.Set(CalendarField.Minute, _alarmMinute);
            calendar.Set(CalendarField.Second, 0);

            var alarmIntent = new Intent(this, typeof(AlarmReceiver));
            var pendingIntent = PendingIntent.GetBroadcast(this, 1, alarmIntent, PendingIntentFlags.UpdateCurrent);
            _alarmManager.SetExact(AlarmType.RtcWakeup, calendar.TimeInMillis, pendingIntent);
        }


        void CancelAlarm()
        {
            var alarmIntent = new Intent(this, typeof(AlarmReceiver));
            var pendingIntent = PendingIntent.GetBroadcast(this, 1, alarmIntent, PendingIntentFlags.UpdateCurrent);
            _alarmManager.Cancel(pendingIntent);
        }

        private void Handle_switchAlarm_Click(object sender, EventArgs e)
        {
            if (_switchAlarm.Checked)
            {
                SetAlarmTest();

                //var calendar = Calendar.Instance;
                //calendar.Set(CalendarField.HourOfDay, _alarmHour);
                //calendar.Set(CalendarField.Minute, _alarmMinute);

                //var alarmIntent = new Intent(this, typeof(AlarmReceiver));
                //var pendingIntent = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
                //var date = DateTime.Now;
                //date.AddMilliseconds(3000);
                //_alarmManager.Set(AlarmType.Rtc, date.Millisecond, pendingIntent);

                //Intent myIntent = new Intent(AlarmActivity.this, AlarmReceiver.class);
                //pendingIntent = PendingIntent.getBroadcast(AlarmActivity.this, 0, myIntent, 0);
                //alarmManager.set(AlarmManager.RTC, calendar.getTimeInMillis(), pendingIntent);
            }
            else
            {
                CancelAlarm();
                //TODO
                //_alarmManager.Cancel(this);
            }

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

            CancelAlarm();

            if (_switchAlarm.Checked == false)
            {
                _switchAlarm.Checked = true;
            }

            SetAlarmTest();
        }
    }
}
