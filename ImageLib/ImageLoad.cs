using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageLib
{
	public class ImageLoad
	{
		public Image LoadImageFromBase64String(string devicePicture)
		{
			try
			{
				//get a temp image from bytes, instead of loading from disk
				//data:image/gif;base64,
				//this image is a single pixel (black)
				File.WriteAllText("test1.png", devicePicture, Encoding.Default);

				//byte[] imageBytes = this._toolsObject.GetBytesFromString(devicePicture);
				byte[] imageBytes = Encoding.Default.GetBytes(devicePicture);
				MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
				int sizePic = devicePicture.Length;

				Image image = Image.FromStream(ms);

				return image;
			}
			catch
			{
				return null;
			}
		}

		public Image LoadImageFromByte(byte[] devicePicture)
		{
			try
			{
				MemoryStream ms = new MemoryStream(devicePicture, 0, devicePicture.Length);
				int sizePic = devicePicture.Length;

				Image image = Image.FromStream(ms);

				return image;
			}
			catch
			{
				return null;
			}
		}

		public Stream GenerateStreamFromString(string s)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
	}
}
