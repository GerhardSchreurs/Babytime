using System;
using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.Media;
using Android.Runtime;
using babytime.Services;

namespace babytime.Utility
{
    public sealed class Manager
    {
        #region Singleton
        private static readonly Manager instance = new Manager();
        // Explicit static constructor to tell C# compiler  
        // not to mark type as beforefieldinit  
        static Manager() {}

        public static Manager Instance
        {
            get
            {
                return instance;
            }
        }

        private Manager()
        {
            Init();
            InitAlarm();
        }
        #endregion

        private Context context;
        private Ringtone _ringtone;
        private AlarmManager _alarmManager;
        private PendingIntent _alarmPendingIntent;

        public bool IsAlarmGoingOf;

        private void Init()
        {
            context = AndroidApplication.Instance.ApplicationContext;
        }

        private void InitAlarm()
        {
            //Init System AlarmManager Class
            _alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService).JavaCast<AlarmManager>();

            //Create Alarm Pending Intent
            var alarmIntent = new Intent(context, typeof(AlarmReceiver));
            _alarmPendingIntent = PendingIntent.GetBroadcast(context, 1, alarmIntent, PendingIntentFlags.UpdateCurrent);

            //Create ringtone
            var alarmUri = RingtoneManager.GetDefaultUri(RingtoneType.Alarm);
            if (alarmUri == null)
            {
                alarmUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            }

            _ringtone= RingtoneManager.GetRingtone(context, alarmUri);
        }

        private void ClearAlarm()
        {
            Settings.ClearAlarm();

            if (_alarmPendingIntent != null)
            {
                _alarmManager.Cancel(_alarmPendingIntent);
            }
        }

        private void SetAlarm(int hourOfDay, int minute)
        {
            Settings.SetAlarmTime(hourOfDay, minute);

            var calendar = Calendar.Instance;
            calendar.Set(CalendarField.HourOfDay, hourOfDay);
            calendar.Set(CalendarField.Minute, minute);
            calendar.Set(CalendarField.Second, 0);

            _alarmManager.SetExact(AlarmType.RtcWakeup, calendar.TimeInMillis, _alarmPendingIntent);
        }

        public void PlayAlarm()
        {
            if (IsAlarmGoingOf == false)
            {
                var launchIntent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
                launchIntent.SetFlags(ActivityFlags.ReorderToFront | ActivityFlags.NewTask | ActivityFlags.ResetTaskIfNeeded);
                context.StartActivity(launchIntent);

                _ringtone.Play();
                EventController.RaiseAlarmIsRinging(this);
            }

            IsAlarmGoingOf = true;
        }

        public void StopAlarm()
        {
            _ringtone.Stop();
            IsAlarmGoingOf = false;
        }
    }
}


