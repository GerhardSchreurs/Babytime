using System;
using Android.Content;
using Android.Content.PM;
using Android.Media;

namespace babytime.Services
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public AlarmReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            //if (intent.Action.Equals("android.intent.action.BOOT_COMPLETED"))
            //{
            //}
            //raise alarm in loop continuously then use MediaPlayer and setLooping(true)
            var alarmUri = RingtoneManager.GetDefaultUri(RingtoneType.Alarm);
            if (alarmUri == null)
            {
                alarmUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            }


            var ringtone = RingtoneManager.GetRingtone(context, alarmUri);
            ringtone.Play();

            //this will send a notification message
            //var comp = new ComponentName(context.PackageName, nameof(AlarmService));
            ////https://stackoverflow.com/questions/51532059/android-gcm-wakefulbroadcastreceiver-startwakefulservice-crash-due-to-illegals

            //StartWakefulService(context, (intent.setComponent(comp)));
            //setResultCode(Activity.RESULT_OK);

            var launchIntent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);
            launchIntent.SetFlags(ActivityFlags.ReorderToFront | ActivityFlags.NewTask | ActivityFlags.ResetTaskIfNeeded);
            context.StartActivity(launchIntent);
        }
    }
}
    