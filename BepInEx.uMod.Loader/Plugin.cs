using BepInEx;

namespace BepInEx.uMod.Loader
{
	[BepInPlugin("io.bepinex.umodloader", "uMod Loader", "1.0")]
	public class Plugin : BaseUnityPlugin
	{
		private void Awake()
		{
			UModFramework.Loader.Start();
		}
	}
}
