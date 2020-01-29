
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using babytime.Utility;

namespace babytime
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        private RadioButton _radioButtonMom;
        private RadioButton _radioButtonDad;
        private Button _buttonSave;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.A_Settings);

            _radioButtonMom = FindViewById<RadioButton>(Resource.Id.radioButtonMom);
            _radioButtonDad = FindViewById<RadioButton>(Resource.Id.radioButtonDad);
            _buttonSave = FindViewById<Button>(Resource.Id.btnSave);

            if (Settings.GetIsMommyApp())
            {
                _radioButtonMom.Checked = true;
            }
            else
            {
                _radioButtonDad.Checked = true;
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            _buttonSave.Click += Handle_ButtonSave_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();

            _buttonSave.Click -= Handle_ButtonSave_Click;
        }

        private void Handle_ButtonSave_Click(object sender, EventArgs e)
        {
            var isMommyApp = _radioButtonMom.Checked;
            Settings.SetIsMommyApp(isMommyApp);

            if (isMommyApp)
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
    }
}
