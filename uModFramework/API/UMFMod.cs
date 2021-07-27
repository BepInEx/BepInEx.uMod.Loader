using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UModFramework.API
{
	public static class UMFMod
	{
        public static Version GetModVersion(Assembly mod = null)
        {
            return (mod ?? Assembly.GetCallingAssembly()).GetName().Version;
        }
        
        public static Version GetModVersion(string modName)
        {
            throw new NotImplementedException();
        }
        
        public static string GetModName()
        {
            throw new NotImplementedException();
        }
        
        public static Assembly GetMod(string modName, bool caseInsensitive = false)
        {
            throw new NotImplementedException();
        }
        
        public static string GetModProductName(string modName, bool caseInsensitive = false)
        {
            throw new NotImplementedException();
        }
        
        public static string GetModDescription(string modName, bool caseInsensitive = false)
        {
            throw new NotImplementedException();
        }
        
        public static int GetModHarmonyPatchCount(Assembly mod = null)
        {
            throw new NotImplementedException();
        }
        
        public static int GetModHarmonyPatchCount(string modName)
        {
            throw new NotImplementedException();
        }
	}
}
