using System;

namespace UModFramework.API
{
    // this is all config related stuff. can get thrown away
    public static class UMFLocale
    {
        public static string Get(string key, params object[] formatArgs)
        {
            throw new NotImplementedException();
        }

        public static void Add(string locale, string key, string text)
        {
            throw new NotImplementedException();
        }

        public static void Add(string modName, string locale, string key, string text)
        {
            throw new NotImplementedException();
        }

        public static string Locale => "en-US";
    }
}