using System;

namespace UModFramework.API
{
    public static class UMFDownload
    {
        public static string DownloadString(string url)
        {
            throw new NotImplementedException();
        }

        public static void DownloadFile(string url, string directory, string file = null, bool extract = false, Action<string, bool, string> action = null)
        {
            throw new NotImplementedException();
        }

        public static bool IsDownloading => false;
    }
}