using System;

namespace UModFramework.API
{
	[AttributeUsage(AttributeTargets.Method)]
	public class UMFConfigAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class UMFHarmonyAttribute : Attribute
	{
		public UMFHarmonyAttribute() { }

		public int TotalPatches { get; }

		public bool Debug { get; }

		public UMFHarmonyAttribute(int totalPatches = 0, bool debug = false)
		{
			TotalPatches = totalPatches;
			Debug = debug;
		}
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class UMFScriptAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Method)]
	public class UMFStartAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Method)]
	public class UMFUnloadAttribute : Attribute { }
}
