using System;
using System.Reflection;
using BepInEx.Logging;

namespace UModFramework.API
{
    public class UMFLog : IDisposable
    {
        internal static ManualLogSource BepInExLog = Logger.CreateLogSource("uMod");

        protected string ModName { get; set; }

        public UMFLog()
        {
            ModName = Assembly.GetCallingAssembly().GetName().Name;
        }

        public void Log(string text)
        {
            BepInExLog.LogMessage($"[{ModName}] {text}");
        }

        public void Log(string text, string file = null)
            => Log(text);

        public void Log(string text, bool clean = false)
            => Log(text);

        public void Log(string text, bool clean = false, string file = null)
            => Log(text);

        public void Dispose() { }
    }
}
