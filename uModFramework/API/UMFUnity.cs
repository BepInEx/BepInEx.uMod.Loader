using UnityEngine;

namespace UModFramework.API
{
	public static class UMFUnity
	{
		public static Texture2D ColorToTexture2D(int width, int height, Color color)
		{
			Color[] pixelArray = new Color[width * height];

			for (int i = 0; i < pixelArray.Length; i++)
				pixelArray[i] = color;

			Texture2D texture2D = new Texture2D(width, height);
			texture2D.SetPixels(pixelArray);
			texture2D.Apply();

			return texture2D;
		}
	}
}