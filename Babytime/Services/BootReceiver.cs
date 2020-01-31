using System;
using Android;
using Android.App;
using Android.Content;
using Android.Widget;
using babytime.Utility;

namespace babytime.Services
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText (context, "Intent-action: " + intent.Action, ToastLength.Long).Show ();
            Manager.Instance.SetAlarmOnBoot();
        }
    }
}
