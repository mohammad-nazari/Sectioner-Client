﻿using System;

namespace SecurityLib
{
	public enum CRC8_POLY
	{
		CRC8 = 0xd5,
		CRC8_CCITT = 0x07,
		CRC8_DALLAS_MAXIM = 0x31,
		CRC8_SAE_J1850 = 0x1D,
		CRC_8_WCDMA = 0x9b,
	};
	/// 
	/// Class for calculating CRC8 checksums...
	/// 
	public class CRC8
	{
		private byte[] table = new byte[256];

		public byte Checksum(params byte[] val)
		{
			if(val == null)
				throw new ArgumentNullException("val");

			byte c = 0;

			foreach(byte b in val)
			{
				c = table[c ^ b];
			}

			return c;
		}

		public byte[] Table
		{
			get
			{
				return this.table;
			}
			set
			{
				this.table = value;
			}
		}

		public byte[] GenerateTable(CRC8_POLY polynomial)
		{
			byte[] csTable = new byte[256];

			for(int i = 0; i < 256; ++i)
			{
				int curr = i;

				for(int j = 0; j < 8; ++j)
				{
					if((curr & 0x80) != 0)
					{
						curr = (curr << 1) ^ (int)polynomial;
					}
					else
					{
						curr <<= 1;
					}
				}

				csTable[i] = (byte)curr;
			}

			return csTable;
		}

		public CRC8(CRC8_POLY polynomial)
		{
			this.table = this.GenerateTable(polynomial);
		}
	}
}
