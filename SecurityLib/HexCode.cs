using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityLib
{
	public class HexCode
	{
		public string StringToHexString(String input, System.Text.Encoding encoding)
		{
			Byte[] stringBytes = encoding.GetBytes(input);
			StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
			foreach(byte b in stringBytes)
			{
				sbBytes.AppendFormat("{0:X2}", b);
			}
			return sbBytes.ToString();
		}

		public string HexStringToByteString(string hexString, System.Text.Encoding encoding)
		{
			if(hexString == null)
				throw new ArgumentNullException("hexString");
			if(hexString.Length % 2 != 0)
				throw new ArgumentException("hexString must have an even length", "hexString");
			var bytes = new byte[hexString.Length / 2];
			for(int i = 0; i < bytes.Length; i++)
			{
				string currentHex = hexString.Substring(i * 2, 2);
				bytes[i] = Convert.ToByte(currentHex, 16);
			}
			string result = encoding.GetString(bytes);
			return result;
		}

		public byte[] HexStringToBytes(string hexString, System.Text.Encoding encoding)
		{
			if(hexString == null)
				throw new ArgumentNullException("hexString");
			if(hexString.Length % 2 != 0)
				throw new ArgumentException("hexString must have an even length", "hexString");
			var bytes = new byte[hexString.Length / 2];
			for(int i = 0; i < bytes.Length; i++)
			{
				string currentHex = hexString.Substring(i * 2, 2);
				bytes[i] = Convert.ToByte(currentHex, 16);
			}
			return bytes;
		}

		public byte[] StringToByteArray(string hex)
		{
			return Enumerable.Range(0, hex.Length)
							 .Where(x => x % 2 == 0)
							 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
							 .ToArray();
		}

		public byte[] GetBytesFromString(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		public string GetStringFromBytes(byte[] bytes)
		{
			char[] chars = new char[bytes.Length / sizeof(char)];
			System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}
	}
}
