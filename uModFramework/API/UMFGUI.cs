using System;
using System.Collections.Generic;

namespace UModFramework.API
{
	public class UMFGUI
	{
		public static void SendCommand(string command, bool openConsole = false)
        {
            throw new NotImplementedException();
        }

		public static bool RegisterCommand(string name, string command, string[] aliases, int arguments, string description, Action action, string sendCommand = null)
		{
            throw new NotImplementedException();
		}

		public static bool RegisterBind(string bindName, string keys, Action action)
		{
            throw new NotImplementedException();
		}

		public static bool UnregisterBind(string bindName)
		{
            throw new NotImplementedException();
		}

		public static bool ModifyBind(string bindName, string newKeys, Action newAction = null)
		{
            throw new NotImplementedException();
		}

		public static bool BindExistsByKeys(string keys, bool allBinds = false)
		{
            throw new NotImplementedException();
		}

		public static bool BindExistsByName(string bindName, bool allBinds = false)
		{
            throw new NotImplementedException();
		}

		public static void AddConsoleText(string text, bool openConsole = false)
		{
			UMFLog.BepInExLog.LogMessage(text);
		}

		public static void ToggleConsole()
		{
			// stubbed out
		}

		public static void RegisterPauseHandler(Action<bool> action)
		{
            throw new NotImplementedException();
		}

        public static string[] Args { get; } = new string[0];

		public static bool IsConsoleOpen => false;

        public static bool IsMenuOpen => false;

		public static bool DefaultCommandsRegistered
		{
			get
			{
                throw new NotImplementedException();
			}
		}

		public static List<UMFBind> GetBinds(string searchPattern = null)
		{
            throw new NotImplementedException();
		}

		public static List<UMFConsoleCommand> GetCommands(string searchPattern = null)
		{
            throw new NotImplementedException();
		}
	}
}
