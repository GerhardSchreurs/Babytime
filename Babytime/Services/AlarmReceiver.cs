using System;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using babytime.Utility;

namespace babytime.Services
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public AlarmReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            Manager.Instance.PlayAlarm();
        }
    }
}
    