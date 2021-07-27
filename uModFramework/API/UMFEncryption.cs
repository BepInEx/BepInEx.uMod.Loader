using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace UModFramework.API
{
	public static class UMFEncryption
	{
		private static readonly SecureRandom secureRandom = new SecureRandom();

		internal static string GetPasswordFromAssemblyName(string inputString)
		{
			string rawStr = $"4567845769345987{inputString}FuXJPw7TbcqD6sYM";

			StringBuilder builder = new StringBuilder();
			byte[] hash;

			using (SHA256 sha = SHA256.Create())
			{
				hash = sha.ComputeHash(Encoding.UTF8.GetBytes(rawStr));
			}

			for (int i = 0; i < hash.Length; i++)
				builder.Append(hash[i].ToString("x2"));

			return builder.ToString();
		}

		internal static byte[] AesTransform(byte[] data, string password, byte[] salt, byte[] aesNonce)
		{
			Pkcs5S2ParametersGenerator pkcs5S2ParametersGenerator = new Pkcs5S2ParametersGenerator();

			var pkcsPassword = PbeParametersGenerator.Pkcs5PasswordToBytes(password.ToCharArray());

			pkcs5S2ParametersGenerator.Init(pkcsPassword, salt, 10000);

			KeyParameter keyParameter = (KeyParameter)pkcs5S2ParametersGenerator.GenerateDerivedMacParameters(256);

			GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesEngine());
			AeadParameters parameters = new AeadParameters(keyParameter, 128, aesNonce, salt);
			gcmBlockCipher.Init(true, parameters);

			byte[] encryptedData = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
			int outOff = gcmBlockCipher.ProcessBytes(data, 0, data.Length, encryptedData, 0);
			gcmBlockCipher.DoFinal(encryptedData, outOff);

			return encryptedData;
		}

		public static string Encrypt(string message, bool output = false)
		{
			var assemblyName = Assembly.GetCallingAssembly().GetName().Name;

			return Encrypt(message, GetPasswordFromAssemblyName(assemblyName), output);
		}

		public static string Encrypt(string message, string password, bool output = false)
		{
			byte[] salt = new byte[16];
			secureRandom.NextBytes(salt);

			byte[] aesNonce = new byte[16];
			secureRandom.NextBytes(aesNonce);

			byte[] data = Encoding.UTF8.GetBytes(message);

			var encryptedData = AesTransform(data, password, salt, aesNonce);
			string outputText;

			using (var memoryStream = new MemoryStream())
			{
				using var binaryWriter = new BinaryWriter(memoryStream);

				binaryWriter.Write(salt);
				binaryWriter.Write(aesNonce);
				binaryWriter.Write(encryptedData);

				outputText = Convert.ToBase64String(memoryStream.ToArray());
			}

			string text = "UMFENC|" + outputText;
			if (output)
			{
				using StreamWriter streamWriter = new StreamWriter(Path.Combine(UMFData.UMFPath,
					Assembly.GetCallingAssembly().GetName().Name + ".txt"), true);

				streamWriter.WriteLine(text);
			}
			return text;
		}

		public static string Decrypt(string encryptedMessage)
		{
			var assemblyName = Assembly.GetCallingAssembly().GetName().Name;

			return Decrypt(encryptedMessage, GetPasswordFromAssemblyName(assemblyName));
		}

		public static string Decrypt(string encryptedMessage, string password)
		{
			byte[] rawData = Convert.FromBase64String(encryptedMessage.Replace("UMFENC|", ""));

			using var memoryStream = new MemoryStream(rawData);
			using var binaryReader = new BinaryReader(memoryStream);

			byte[] salt = binaryReader.ReadBytes(16);
			byte[] aesNonce = binaryReader.ReadBytes(16);

			byte[] payload = binaryReader.ReadBytes(rawData.Length - 32);

			return Encoding.UTF8.GetString(AesTransform(payload, password, salt, aesNonce));
		}
	}
}
