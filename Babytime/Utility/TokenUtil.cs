using System;
namespace babytime.Utility
{
    public static class TokenUtil
    {
        private static SimpleStorage _storage;

        private static void InitStorage()
        {
            if (_storage == null)
            {
                _storage = new SimpleStorage();
            }
        }

        public static string GetToken()
        {
            InitStorage();
            return _storage.GetString("TOKEN");
        }

        public static void SaveToken(string token)
        {
            InitStorage();
            _storage.SaveString("TOKEN", token);
        }
    }
}
