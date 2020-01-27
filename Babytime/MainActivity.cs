using System;
using System.Threading.Tasks;
using System.Net.Http;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using babytime.Extensions;
using babytime.Model;
using babytime.Utility;
using Newtonsoft.Json;
using Org.Json;

namespace babytime
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText _editTitle;
        private EditText _editMessage;
        private Button _buttonSend;

        const string FCM_API = "https://fcm.googleapis.com/fcm/send";
        const string serverKey = "key=AIzaSyDyctWijRnNurs_CeXYQ4ECHpzdJXqigBY";
        const string contentType = "application/json";
        const string TAG = "NOTIFICATION TAG";

        private string _notification_title;
        private string _notification_message;
        private string _topic;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            _editTitle = FindViewById<EditText>(Resource.Id.textTitle);
            _editMessage = FindViewById<EditText>(Resource.Id.textMessage);
            _buttonSend = FindViewById<Button>(Resource.Id.buttonSend);
        }

        protected override void OnPause()
        {
            base.OnPause();

            _buttonSend.Click -= Handle_ButtonSend_Click;
        }

        protected override void OnResume()
        {
            base.OnResume();

            _buttonSend.Click += Handle_ButtonSend_Click;
        }

        private void DoSendNotification()
        {
            try
            {
                var token = TokenUtil.GetToken();
                token = "bla";

                if (token.IsNotNullOrEmpty())
                {
                    var dinges = new FCMPushNotification();
                    dinges.SendNotification("Xamarin Forms FCM Notifications", "Sample For FCM Push Notifications in Xamairn Forms", "babytime");

                    //var body = new FCMBody();

                    //var notification = new FCMNotification();
                    //notification.title = "Xamarin Forms FCM Notifications";
                    //notification.body = "Sample For FCM Push Notifications in Xamairn Forms";

                    //var data = new FCMData();
                    //data.key1 = "";
                    //data.key2 = "";
                    //data.key3 = "";
                    //data.key4 = "";
                    //body.registration_ids = new[] { token };
                    //body.notification = notification;
                    //body.data = data;

                    //var isSuccessCall = SendNotification(body).Result;
                    //if (isSuccessCall)
                    //{
                    //    Toast.MakeText(this, "Notifications Send Successfully", ToastLength.Long).Show();
                    //}
                    //else
                    //{
                    //    Toast.MakeText(this, "Notifications FAILED", ToastLength.Long).Show();
                    //}
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<bool> SendNotification(FCMBody fcmBody)
        {
            try
            {
                var httpContent = JsonConvert.SerializeObject(fcmBody);
                var client = new HttpClient();
                var authorization = string.Format("key={0}", "YOUR_KEY");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
                var stringContent = new StringContent(httpContent);
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                string uri = "https://fcm.googleapis.com/fcm/send";
                var response = await client.PostAsync(uri, stringContent).ConfigureAwait(false);
                var result = response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (TaskCanceledException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        private void Handle_ButtonSend_Click(object sender, EventArgs e)
        {

            DoSendNotification();
            return;

            _topic = "/topics/babytime";
            _notification_title = _editTitle.Text;
            _notification_message = _editMessage.Text;

            var notification = new JSONObject();
            var notificationBody = new JSONObject();

            try
            {
                notificationBody.Put("title", _notification_title);
                notificationBody.Put("message", _notification_message);

                notification.Put("to", _topic);
                notification.Put("data", notificationBody);
            }
            catch (JSONException ex)
            {
                Log.Error(TAG, "onCreate: " + ex.Message);
            }

            SendNotification(notification);
        }

        private void SendNotification(JSONObject notification)
        {
            var x = "";

            //var  jsonObjectRequest = new JsonObjectRequest(FCM_API, notification,
    //    new Response.Listener<JSONObject>() {
    //                @Override
    //                public void onResponse(JSONObject response)
    //        {
    //            Log.i(TAG, "onResponse: " + response.toString());
    //            edtTitle.setText("");
    //            edtMessage.setText("");
    //        }
    //    },
    //            new Response.ErrorListener() {
    //                @Override
    //                public void onErrorResponse(VolleyError error)
    //    {
    //        Toast.makeText(MainActivity.this, "Request error", Toast.LENGTH_LONG).show();
    //        Log.i(TAG, "onErrorResponse: Didn't work");
    //    }
    //}){
    //        @Override
    //        public Map<String, String> getHeaders() throws AuthFailureError
    //{
    //    Map<String, String> params = new HashMap<>();
    //            params.put("Authorization", serverKey);
    //            params.put("Content-Type", contentType);
    //            return params;
    //        }
    //    };
    //    MySingleton.getInstance(getApplicationContext()).addToRequestQueue(jsonObjectRequest);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

