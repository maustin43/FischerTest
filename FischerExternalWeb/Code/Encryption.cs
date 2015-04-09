using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace FischerWeb.Code
{
	public class Encryption
	{
		const String	encryptionKey	= "passcodefischerhomes1234";	// 24 bytes for 3DES/ECB encryption
		const Int16		timestampLength	= 14;

		//-------------------------------------------------------------------------------------------------------------------------------------------

		public static String EncryptData(String message, Boolean useTimestamp)
		{
			byte[]							Results;
			TripleDESCryptoServiceProvider	TDESAlgorithm	= new TripleDESCryptoServiceProvider();
			UTF8Encoding					UTF8			= new UTF8Encoding();

			TDESAlgorithm.Key		= UTF8.GetBytes(encryptionKey);
			TDESAlgorithm.Mode		= CipherMode.ECB;
			TDESAlgorithm.Padding	= PaddingMode.PKCS7;

			// Add Timestamp
			if(useTimestamp == true)
				message = DateTime.Now.ToString("yyyyMMddHHmmss") + message;

			try
			{
				byte[] DataToEncrypt	= UTF8.GetBytes(message);
				Results					= TDESAlgorithm.CreateEncryptor().TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
			}
			catch
			{
				return null;
			}
			finally
			{
				TDESAlgorithm.Clear();
			}

			return Bin2Hex(Results);
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------
		
		public static String DecryptData(String message, Boolean useTimestamp, Int32 timeoutDurationSeconds=0)
		{
			TripleDESCryptoServiceProvider	TDESAlgorithm	= new TripleDESCryptoServiceProvider();
			UTF8Encoding					UTF8			= new UTF8Encoding();

			TDESAlgorithm.Key		= UTF8.GetBytes(encryptionKey);
			TDESAlgorithm.Mode		= CipherMode.ECB;
			TDESAlgorithm.Padding	= PaddingMode.PKCS7;

			try
			{
				byte[] DataToDecrypt	= Hex2Bin(message);
				message					= UTF8.GetString(TDESAlgorithm.CreateDecryptor().TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length));
			}
			catch
			{
				return null;
			}
			finally
			{
				TDESAlgorithm.Clear();
			}

			// Validate Key Timeout
			if(useTimestamp == true)
			{
				try
				{
					if(message.Length < timestampLength)
						return null;
					String timeStampStr = message.Substring(0, timestampLength);
					message = message.Substring(14);
					DateTime timestamp = new DateTime(	Convert.ToInt16(timeStampStr.Substring(0,  4)),
														Convert.ToInt16(timeStampStr.Substring(4,  2)),
														Convert.ToInt16(timeStampStr.Substring(6,  2)),
														Convert.ToInt16(timeStampStr.Substring(8,  2)),
														Convert.ToInt16(timeStampStr.Substring(10, 2)),
														Convert.ToInt16(timeStampStr.Substring(12, 2)));

					TimeSpan elapseTime = DateTime.Now - timestamp;
					if(Convert.ToInt16(elapseTime.TotalSeconds) > timeoutDurationSeconds)
						return null;
				}
				catch
				{
					return null;
				}
			}

			return message;
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		public static String ResetTimestamp(String message)
		{
			message = DecryptData(message, false);
			
			if(message == null)
				return message;

			// Strip Timestamp
			if(message.Length < timestampLength)
				return null;
			String timeStampStr = message.Substring(0, timestampLength);
			message = message.Substring(14);

			return EncryptData(message, true);
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		static String Bin2Hex(byte[] bin) {
            StringBuilder sb = new StringBuilder(bin.Length * 2);
            foreach(byte b in bin) {
                sb.Append(b.ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

		//-------------------------------------------------------------------------------------------------------------------------------------------

		public static byte[] Hex2Bin(String hex)
		{
			int NumberChars = hex.Length;
			byte[] bytes = new byte[NumberChars / 2];
			for (int i = 0; i < NumberChars; i += 2)
			bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			return bytes;
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------
	}
}