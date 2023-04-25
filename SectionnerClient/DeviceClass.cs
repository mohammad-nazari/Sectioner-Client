using System;
using System.Drawing;
using System.Windows.Forms;
using WebServiceLib;

namespace SectionnerClient
{
	// Save Device Informations
	public class DevicePicture
	{
		private PictureBox _devicePictureBox = new PictureBox();
		private WebServiceLib.SettingLevel _deviceLevel = WebServiceLib.SettingLevel.Normal;
		private DeviceModel _deviceModelName = DeviceModel.SECTIONNER;
		private Color _devicePictureColor = new Color();
		private string _deviceName = "";

		public WebServiceLib.DeviceModel DeviceModelName
		{
			get
			{
				return _deviceModelName;
			}
			set
			{
				_deviceModelName = value;
			}
		}
		public string DeviceName
		{
			get
			{
				return _deviceName;
			}
			set
			{
				_deviceName = value;
			}
		}

		public System.Drawing.Color DevicePictureColor
		{
			get
			{
				return _devicePictureColor;
			}
			set
			{
				_devicePictureColor = value;
			}
		}

		public System.Windows.Forms.PictureBox DevicePictureBox
		{
			get
			{
				return _devicePictureBox;
			}
			set
			{
				_devicePictureBox = value;
			}
		}

		public WebServiceLib.SettingLevel DeviceLevel
		{
			get
			{
				return _deviceLevel;
			}
			set
			{
				_deviceLevel = value;
			}
		}

	};
}
