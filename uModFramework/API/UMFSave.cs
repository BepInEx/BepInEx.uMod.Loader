using System.IO;
using System.Reflection;
using TinyJSON;

namespace UModFramework.API
{
	public static class UMFSave
	{
		internal static string GenerateFilename(string name, string directPath)
		{
			// each of the public methods contains a "json" parameter that does nothing with the data format but only changes the extension
			// it also saves these config files in the LocalLow folder for this unity game, which is really dumb
			// so i will not be doing the same

			return directPath ?? Path.Combine(UMFData.ConfigsPath, name + ".json");
		}

		// generic param but still uses objects??? why????
		public static void Save<T>(object saveObject, bool json = false, string file = null)
		{
			string filename = GenerateFilename(Assembly.GetCallingAssembly().GetName().Name, file);

			var jsonOutput = Encoder.Encode(saveObject, EncodeOptions.PrettyPrint);

			File.WriteAllText(filename, jsonOutput);
		}
		
		public static T Load<T>(bool json = false, string file = null)
		{
            string filename = GenerateFilename(Assembly.GetCallingAssembly().GetName().Name, file);

            return Decoder.Decode(File.ReadAllText(filename)).Make<T>();
        }
		
		public static bool SaveExists(bool json = false, string file = null)
        {
            string filename = GenerateFilename(Assembly.GetCallingAssembly().GetName().Name, file);

			return File.Exists(filename);
		}
	}
}
