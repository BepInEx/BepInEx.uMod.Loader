using System;

namespace UModFramework.API
{
    public static class UMFCache
    {
        public static void Write(string key, string data)
        {
            throw new NotImplementedException();
        }

        public static void Expire(string key)
        {
            throw new NotImplementedException();
        }

        public static string Read(string key)
        {
            throw new NotImplementedException();
        }

        public static string[] ReadStartsWith(string keyStartsWith)
        {
            throw new NotImplementedException();
        }

        public static bool IsOlderThan(string key, int seconds)
        {
            throw new NotImplementedException();
        }

        public static bool Exists(string key)
        {
            throw new NotImplementedException();
        }
    }
}