
using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Timers;
using Android.Support.V7.App;

namespace babytime.Utility
{
    [Activity(Label = "NotifyActivity")]
    public class MommyActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar _toolbar;
        List<string> _warningStrings;
        List<string> _toastStrings;
        TextView _textViewWarningDescription;
        Random _random;
        ImageButton _imageButtonAlarm;

        void BuildWarningStrings()
        {
            _warningStrings = new List<string>();

            _warningStrings.Add("Wat zou Jezus doen?");
            _warningStrings.Add("Elke keer dat je op de knop drukt, sterft er een kitten");
            _warningStrings.Add("Wil je echt papa zijn slaap ontnemen?");
            _warningStrings.Add("Als je op de knop drukt, wordt ofwel papa wakker, of je lanceert een russische kernraket");
            _warningStrings.Add("Wacht nog even, misschien vallen ze weer in slaap...");
            _warningStrings.Add("DOE HET NIET!!!");
            _warningStrings.Add("Hoe zou jij het vinden als de rollen omgedraaid waren? Hmm?");
            _warningStrings.Add("Ze zijn alleen maar gezellig aan het kletsen.");
            _warningStrings.Add("Papa heeft ook zijn slaap nodig...");
            _warningStrings.Add("Je start de derde wereldoorlog. Weet je het zeker?");
            _warningStrings.Add("Alluha Ahkbar!!!");
            _warningStrings.Add("Draai anders eerst nog een keertje om...");
        }

        void BuildToastStrings()
        {
            _toastStrings = new List<string>();

            _toastStrings.Add("Flauw hoor, echt heel flauw");
            _toastStrings.Add("Nou, jij kan tenminste wel lekker slapen");
            _toastStrings.Add("Voelt dit nu goed? Nee hè?");
            _toastStrings.Add("Pappa is ook erg moe");
            _toastStrings.Add("Hoor jij ook gezucht en gestommel op zolder?");
            _toastStrings.Add("De baby's hebben ook mamma nodig, hoor!");
            _toastStrings.Add("Hmmm, lekker slaapie slaapies doen");
            _toastStrings.Add("Pappa lost het wel weer op hoor");
            _toastStrings.Add("Wedden dat papa's wekker 5 minuten na nu was ingepland?");
            _toastStrings.Add("Oops! Er is nu een kernraket vanuit rusland onderweg naar Nederland");
            _toastStrings.Add("Pappa zal nog eens een app maken voor mamma...");
            _toastStrings.Add("Pappa zal toch geen snooze functionaliteit hebben ingebouwd? Nee, vast niet...");
            _toastStrings.Add("Hoe kan je toch zo harteloos zijn?");
            _toastStrings.Add("Pappa houdt van mamma en gunt jou ook je slaap. Kusje <3");
        }

        protected override void OnResume()
        {
            base.OnResume();

            _textViewWarningDescription.Text = _warningStrings[_random.Next(0, _warningStrings.Count - 1)];
            _imageButtonAlarm.Click += Handle_imageButtonAlarm_Click;

        }

        private void Handle_imageButtonAlarm_Click(object sender, EventArgs e)
        {
            Manager.Instance.SendNotification();
            Toast.MakeText(this, _toastStrings[_random.Next(0, _toastStrings.Count - 1)], ToastLength.Long).Show();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        private void Handle_timerDoom_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.A_Mommy);

            _toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            _textViewWarningDescription = FindViewById<TextView>(Resource.Id.textViewWarningDescription);
            _imageButtonAlarm = FindViewById<ImageButton>(Resource.Id.imageButtonAlarm);

            _random = new Random();
            BuildWarningStrings();
            BuildToastStrings();

            SetSupportActionBar(_toolbar);
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
    }
}
