using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityLib
{
	public class Base64
	{
		public Base64()
		{

		}

		~Base64()
		{

		}

		public String Base64Encoding(String inputString, Encoding encoding)
		{
			byte[] bAsciiString = encoding.GetBytes(inputString);
			return Convert.ToBase64String(bAsciiString);
		}

		public String Base64Decoding(String inputString, Encoding encoding)
		{
			byte[] bAsciiString = Convert.FromBase64String(inputString);
			return encoding.GetString(bAsciiString);
		}
	}
}
