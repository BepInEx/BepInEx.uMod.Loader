using System;
using UnityEngine;

namespace UModFramework.API
{
    [Serializable]
    public class UMFBind
    {
        public KeyCode[] keyCodes;

        public string stringKeyCodes;

        public string command;

        public bool userDefined;

        public Action action;

        public string modName;

        public string bindName;
    }
}