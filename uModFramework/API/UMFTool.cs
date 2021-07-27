using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace UModFramework.API
{
	public static class UMFTool
	{
		// if i were to implement these two calls, i would use https://stackoverflow.com/a/4897700 as a reference

		public static void CreateShortcut(string location, string linkName, string targetPath, string workingDir = null, string arguments = null, string description = null)
		{
			throw new NotImplementedException();
		}
		
		public static void DeleteShortcut(string location, string linkName)
		{
			throw new NotImplementedException();
		}
		
		public static bool ShortcutExists(string location, string linkName)
		{
			throw new NotImplementedException();
		}
		
		public static void CreateDesktopShortcut(string linkName, string targetPath, string workingDir = null, string arguments = null, string description = null)
		{
			throw new NotImplementedException();
		}
		
		public static void DeleteDesktopShortcut(string linkName)
		{
			throw new NotImplementedException();
		}
		
		public static bool Unzip(string pathFrom, string pathTo, bool delete = false)
		{
			using var zip = ZipFile.Read(pathFrom);

			zip.ExtractAll(pathTo, delete ? ExtractExistingFileAction.OverwriteSilently : ExtractExistingFileAction.DoNotOverwrite);

			return true;
		}
		
		public static bool Unzip(string path, bool delete = false)
		{
			return Unzip(path, Path.GetDirectoryName(path), delete);
		}
		
		public static bool Zip(string fileName, string rootDir, List<string> files)
		{
			using var zip = new ZipFile(fileName);

			foreach (var file in files)
			{
				using var fileStream = File.OpenRead(Path.Combine(rootDir, file));
				
				zip.AddEntry(file, fileStream);
			}

			zip.Save();
			return true;
		}
		
		public static void Backup(string name)
		{
            throw new NotImplementedException();
		}
		
		public static void Restore(string name)
		{
            throw new NotImplementedException();
		}
	}
}
