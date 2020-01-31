using System;
using Android.App;

namespace babytime
{
    public class AndroidApplication : Application
    {
        private static AndroidApplication _instance;

        public static AndroidApplication Instance
        {
            get
            {
                return _instance;
            }

        }

        public override void OnCreate()
        {
            base.OnCreate();
            _instance = this;
        }
    }
}
