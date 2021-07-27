using System;

namespace UModFramework.API
{
    [Serializable]
    public class UMFConsoleCommand
    {
        public string name;

        public string command;

        public string[] aliases;

        public int arguments;

        public string description;

        public Action action;
    }
}