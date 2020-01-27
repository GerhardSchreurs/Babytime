using System;
using Android.Content;
using Android.Preferences;

namespace babytime.Utility
{
    public class SimpleStorage
    {
        ISharedPreferences _prefs => PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);

        public bool GetBool(string key)
        {
            return _prefs.GetBoolean(key, false);
        }

        public void SaveBool(string key, bool value)
        {
            ISharedPreferencesEditor editor = _prefs.Edit();
            editor.PutBoolean(key, value);
            editor.Apply();
        }

        public string GetString(string key)
        {
            return _prefs.GetString(key, string.Empty);
        }

        public void SaveString(string key, string value)
        {
            ISharedPreferencesEditor editor = _prefs.Edit();
            editor.PutString(key, value);
            editor.Apply();
        }

        public void RemoveObject(string key)
        {
            ISharedPreferencesEditor editor = _prefs.Edit();
            editor.Remove(key);
            editor.Apply();
        }
    }
}
