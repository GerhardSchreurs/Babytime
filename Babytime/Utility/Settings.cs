using System;
using Xamarin.Essentials;

namespace babytime.Utility
{
    public static class Settings
    {
        const string TOKEN = "TOKEN";
        const string ISMOMMYAPP = "ISMOMMYAPP";
        const string MINORVERSION = "MINORVERSION";
        const string ALARMHOUR = "ALARMHOUR";
        const string ALARMMINUTE = "ALARMMINUTE";


        public static string GetToken()
        {
            return StoreUtil.GetString(TOKEN);
        }

        public static void SetToken(string token)
        {
            StoreUtil.SaveString(TOKEN, token);
        }

        public static bool GetIsMommyApp()
        {
            return StoreUtil.GetBool(ISMOMMYAPP);
        }

        public static void SetIsMommyApp(bool value)
        {
            StoreUtil.SaveBool(ISMOMMYAPP, value);
        }

        public static bool IsNewVersionAndStore()
        {
            var buildVersion = AppInfo.Version.Minor;
            var storedVersion = StoreUtil.GetInt(MINORVERSION);
            var isNewVersion = false;

            if ((storedVersion == -1) || (buildVersion != storedVersion))
            {
                isNewVersion = true;
            }

            if (isNewVersion)
            {
                StoreUtil.SaveInt(MINORVERSION, AppInfo.Version.Minor);
            }

            return isNewVersion;
        }

        public static int GetAlarmHour()
        {
            return StoreUtil.GetInt(ALARMHOUR, -1);
        }

        public static int GetAlarmMinute()
        {
            return StoreUtil.GetInt(ALARMMINUTE, -1);
        }

        public static void SetAlarmTime(int hour, int minute)
        {
            StoreUtil.SaveInt(ALARMHOUR, hour);
            StoreUtil.SaveInt(ALARMMINUTE, minute);
        }

        public static void ClearAlarm()
        {
            StoreUtil.RemoveObject(ALARMHOUR);
            StoreUtil.RemoveObject(ALARMMINUTE);
        }
    }
}
