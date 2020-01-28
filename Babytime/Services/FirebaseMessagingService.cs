using System;
using Android.App;
using Android.Util;
using Firebase.Messaging;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Annotation;

namespace babytime.Services
{
    [Service(Name = "wroah.babytime.FirebaseMessagingService")]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseMessagingService : Firebase.Messaging.FirebaseMessagingService
    {
        const string TAG = "FirebaseMessagingService";
        const string TOPIC = "babytime";
        const string ADMIN_CHANNEL_ID = "admin_channel";

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            FirebaseMessaging.Instance.SubscribeToTopic(TOPIC);
            Log.Info(TAG, "OnNewToken " + token);
        }

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);

            var intent = new Intent(this, typeof(MainActivity));
            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            var notificationId = new Random().Next(3000);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                SetupChannels(notificationManager);
            }

            intent.AddFlags(ActivityFlags.ClearTop);

            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);
            var largeIcon = BitmapFactory.DecodeResource(Resources, Resource.Mipmap.ic_launcher);
            var notificationSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            var notificationBuilder = new NotificationCompat.Builder(this, ADMIN_CHANNEL_ID)
                .SetSmallIcon(Resource.Mipmap.ic_launcher)
                .SetLargeIcon(largeIcon)
                .SetContentTitle(remoteMessage.Data["title"])
                .SetContentText(remoteMessage.Data["message"])
                .SetAutoCancel(true)
                .SetSound(notificationSoundUri)
                .SetContentIntent(pendingIntent);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                notificationBuilder.SetColor(ContextCompat.GetColor(ApplicationContext, Resource.Color.colorPrimaryDark));
            }

            notificationManager.Notify(notificationId, notificationBuilder.Build());
        }

        [TargetApi(Value = 26)]
        private void SetupChannels(NotificationManager notificationManager)
        {
            var adminChannelName = "New notitication";
            var adminChannelDescription = "Bla bla dunno";

            var adminChannel = new NotificationChannel(ADMIN_CHANNEL_ID, adminChannelName, NotificationImportance.High);
            adminChannel.Description = adminChannelDescription;
            adminChannel.EnableLights(true);
            adminChannel.LightColor = Color.Red;
            adminChannel.EnableVibration(true);

            if (notificationManager != null)
            {
                notificationManager.CreateNotificationChannel(adminChannel);
            }
        }
    }
}
