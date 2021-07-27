using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Ionic.Zip;
using UModFramework.API;
using UnityEngine;

[assembly: InternalsVisibleTo("BepInEx.uMod.Loader")]

namespace UModFramework
{
	internal static class Loader
    {
        public static List<MonoBehaviour> UMFMods { get; } = new List<MonoBehaviour>();

        private const string ZipPassword = "131328604ff894515a17e71996922f3bca2d168311117f3a3198fe2577fe7279";

        private static GameObject ManagerObject { get; set; }

        public static void Start()
        {
            Directory.CreateDirectory(UMFData.UMFPath);
            Directory.CreateDirectory(UMFData.ModsPath);
            Directory.CreateDirectory(UMFData.ConfigsPath);

            ManagerObject = new GameObject("uMod");
            UnityEngine.Object.DontDestroyOnLoad(ManagerObject);

            UMFLog.BepInExLog.LogMessage("Started");

            foreach (var modFile in Directory.GetFiles(UMFData.ModsPath, "*.umfmod"))
            {
                try
                {
                    using var zip = ZipFile.Read(modFile);

                    foreach (var entry in zip)
                    {
                        if (!entry.FileName.EndsWith(".dll"))
                            continue;

                        using var entryStream = entry.OpenReader(ZipPassword);

                        byte[] buffer = new byte[entry.UncompressedSize];

                        entryStream.Read(buffer, 0, buffer.Length);

                        var assembly = Assembly.Load(buffer);

                        foreach (var type in assembly.GetTypes())
                        {
                            if (!typeof(MonoBehaviour).IsAssignableFrom(type))
                                continue;

                            if (GetAttribute<UMFScriptAttribute>(type) == null)
                                continue;

                            UMFLog.BepInExLog.LogInfo($"Discovered [{assembly.GetName().Name}::{type}]");

                            UMFMods.Add((MonoBehaviour)ManagerObject.AddComponent(type));
                        }
                    }
                }
                catch (Exception ex)
                {
                    UMFLog.BepInExLog.LogError($"Failed to scan {Path.GetFileName(modFile)}: {ex}");
                }
            }

            foreach (var mod in UMFMods)
            {
                var type = mod.GetType();

                try
                {
                    foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                                           BindingFlags.Instance))
                    {
                        if (GetAttribute<UMFConfigAttribute>(method) != null)
                            method.Invoke(mod, new object[0]);

                        if (GetAttribute<UMFStartAttribute>(method) != null)
                            method.Invoke(mod, new object[0]);
                    }
                }
                catch (Exception ex)
                {
                    UMFLog.BepInExLog.LogError($"Failed to initialize mod {type.FullName}: {ex}");
                }
            }

            UMFLog.BepInExLog.LogMessage($"Loaded {UMFMods.Count} mods");
        }

        private static T GetAttribute<T>(MemberInfo member) where T : Attribute
        {
            return (T)member.GetCustomAttributes(typeof(T), false).FirstOrDefault();
        }
	}
}
