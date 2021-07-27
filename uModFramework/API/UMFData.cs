using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace UModFramework.API
{
	public class UMFData
    {
        public static Version Version { get; } = new Version("0.53.0.0");

        public static List<string> ModNames { get; } = new List<string>();

		public static List<string> ModNamesEnabled { get; } = new List<string>();

		public static List<string> ModNamesActive { get; } = new List<string>();

		public static List<Assembly> Mods { get; } = new List<Assembly>();

		public static List<Harmony> HarmonyInstances { get; } = new List<Harmony>();

		public static long TimeToLoad { get; internal set; } = 0;

		public static bool LoaderIsDone { get; internal set; }

        public static int LoaderUnixTimeDone { get; internal set; } = 0;

        public static int ModsUpdated { get; internal set; } = 0;

        public static long ModsUpdatedTime { get; internal set; } = 0;

        public static string UMFPath { get; }
            = Path.Combine(Paths.GameRootPath, "uModFramework");
        
		public static string AssembliesPath { get; }
            = Path.Combine(Paths.GameRootPath, "uModFramework");

		public static string GamePath { get; }
            = Paths.GameRootPath;

		public static string ModsPath { get; }
            = Path.Combine(UMFPath, "Mods");

		public static string ConfigsPath { get; }
            = Path.Combine(ModsPath, "Configs");

		public static string LibrariesPath { get; }
            = Path.Combine(ModsPath, "Libraries");

		public static string ModInfosPath { get; }
            = Path.Combine(ModsPath, "ModInfos");

		public static string LogsPath { get; }
            = Path.Combine(UMFPath, "Logs");

		public static string CachePath { get; }
            = Path.Combine(UMFPath, "Cache");

		public static string LocalePath { get; }
            = Path.Combine(UMFPath, "Locale");

		public static string LocaleModsPath => throw new NotImplementedException();

		public static string BackupsPath => throw new NotImplementedException();

		public static string GameSavePath => throw new NotImplementedException();

		public static string AssetsPath => throw new NotImplementedException();

		public static string AssetsSharedPath => throw new NotImplementedException();

		public static string TempPath => throw new NotImplementedException();

		public static string SourceModsPath => throw new NotImplementedException();

		public static string LuaModsPath => throw new NotImplementedException();

		public static string UMFPatchesPath => throw new NotImplementedException();

		public static string ResourcesPath => throw new NotImplementedException();

        public static string ManagedPath
            => Paths.ManagedPath;

		public static string LoadOrderFile => throw new NotImplementedException();

		public static string DisabledModsFile => throw new NotImplementedException();

		public static void AddTip(string tip)
		{
			//tips.Add(tip + " (" + Assembly.GetCallingAssembly().GetName().Name + ")");
			// stubbed out
		}

		public static int UnixTime => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
	}
}
