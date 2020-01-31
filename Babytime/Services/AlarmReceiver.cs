using System;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.Widget;
using babytime.Utility;

namespace babytime.Services
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "AlarmReceiver: Intent-action: " + intent.Action, ToastLength.Long).Show();
            Manager.Instance.PlayAlarm();
        }
    }
}
    