using System;
namespace babytime.Utility
{
    public static class Settings
    {
        const string TOKEN = "TOKEN";
        const string ISPUSHAPP = "ISPUSHAPP";

        public static string GetToken()
        {
            return StoreUtil.GetString(TOKEN);
        }

        public static void SetToken(string token)
        {
            StoreUtil.SaveString(TOKEN, token);
        }

        public static bool GetIsPushApp()
        {
            return StoreUtil.GetBool(ISPUSHAPP);
        }

        public static void SetIsPushApp(bool value)
        {
            StoreUtil.SaveBool(ISPUSHAPP, value);
        }
    }
}
