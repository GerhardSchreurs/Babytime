using System;
using Android.Content;
using Android.Preferences;

namespace babytime.Utility
{
    public static class StoreUtil
    {
        private static SimpleStorage _simpleStorage = new SimpleStorage();

        public static bool GetBool(string key)
        {
            return _simpleStorage.GetBool(key);
        }

        public static void SaveBool(string key, bool value)
        {
            _simpleStorage.SaveBool(key, value);
        }

        public static string GetString(string key)
        {
            return _simpleStorage.GetString(key);
        }

        public static void SaveString(string key, string value)
        {
            _simpleStorage.SaveString(key, value);
        }

        public static int GetInt(string key, int defaultValue = -1)
        {
            return _simpleStorage.GetInt(key, defaultValue);
        }

        public static void SaveInt(string key, int value)
        {
            _simpleStorage.SaveInt(key, value);
        }

        public static void RemoveObject(string key)
        {
            _simpleStorage.RemoveObject(key);
        }
        
    }

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

        public int GetInt(string key, int defaultValue)
        {
            return _prefs.GetInt(key, defaultValue);
        }

        public void SaveInt(string key, int value)
        {
            ISharedPreferencesEditor editor = _prefs.Edit();
            editor.PutInt(key, value);
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
