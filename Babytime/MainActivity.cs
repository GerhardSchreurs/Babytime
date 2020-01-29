using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using babytime.Utility;

namespace babytime
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Settings.IsNewVersionAndStore())
            {
                using (var intent = new Intent(this, typeof(SettingsActivity)))
                {
                    StartActivity(intent);
                }
            }
            else
            {
                if (Settings.GetIsMommyApp())
                {
                    using (var intent = new Intent(this, typeof(MommyActivity)))
                    {
                        StartActivity(intent);
                    }
                }
                else
                {
                    using (var intent = new Intent(this, typeof(DaddyActivity)))
                    {
                        StartActivity(intent);
                    }
                }
            }

            Finish();
        }


        //private FCMPushNotification _FCMPushNotification;

        //private EditText _editTitle;
        //private EditText _editMessage;
        //private Button _buttonSend;

        //const string TAG = "NOTIFICATION TAG";

        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);
        //    Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        //    SetContentView(Resource.Layout.activity_main);

        //    _editTitle = FindViewById<EditText>(Resource.Id.textTitle);
        //    _editMessage = FindViewById<EditText>(Resource.Id.textMessage);
        //    _buttonSend = FindViewById<Button>(Resource.Id.buttonSend);
        //}

        //private void SendNotification()
        //{
        //    if (_FCMPushNotification == null)
        //    {
        //        _FCMPushNotification = new FCMPushNotification();
        //    }

        //    _FCMPushNotification.SendNotification("title", "message", "babytime");
        //}


        //protected override void OnPause()
        //{
        //    base.OnPause();

        //    _buttonSend.Click -= Handle_ButtonSend_Click;
        //}

        //protected override void OnResume()
        //{
        //    base.OnResume();

        //    _buttonSend.Click += Handle_ButtonSend_Click;
        //}

        //private void Handle_ButtonSend_Click(object sender, EventArgs e)
        //{
        //    SendNotification();
        //}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

