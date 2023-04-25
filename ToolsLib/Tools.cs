using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurityLib;
using WebServiceLib;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace ToolsLib
{
	public class Tools
	{
		public Tools()
		{
		}

		public void DeviceInfoFromBase64(Device DeviceObject)
		{
			// Decode Base64
			Base64 b64 = new Base64();
			DeviceObject.deviceNikeName = b64.Base64Decoding(DeviceObject.deviceNikeName, Encoding.UTF8);
			DeviceObject.deviceCity = b64.Base64Decoding(DeviceObject.deviceCity, Encoding.UTF8);
			DeviceObject.deviceLocation = b64.Base64Decoding(DeviceObject.deviceLocation, Encoding.UTF8);
		}

		public void DeviceInfoToBase64(Device DeviceObject)
		{
			// Decode Base64
			Base64 b64 = new Base64();
			DeviceObject.deviceNikeName = b64.Base64Encoding(DeviceObject.deviceNikeName, Encoding.UTF8);
			DeviceObject.deviceCity = b64.Base64Encoding(DeviceObject.deviceCity, Encoding.UTF8);
			DeviceObject.deviceLocation = b64.Base64Encoding(DeviceObject.deviceLocation, Encoding.UTF8);
		}

		public Sensor GetSensorThresholdValues(SensorName SName)
		{
			Sensor tmp = new Sensor();

			switch(SName)
			{
				case SensorName.ACAMPERE:
					{
						tmp.sensorMinimumValue = Constants.ACAmpereMinimum;
						tmp.sensorMaximumValue = Constants.ACAmpereMaximum;
						break;
					}
				case SensorName.ACVOLTAGE:
					{
						tmp.sensorMinimumValue = Constants.ACVoltageMinimum;
						tmp.sensorMaximumValue = Constants.ACVoltageMaximum;
						break;
					}
				case SensorName.DCAMPERE:
					{
						tmp.sensorMinimumValue = Constants.DCAmpereMinimum;
						tmp.sensorMaximumValue = Constants.DCAmpereMaximum;
						break;
					}
				case SensorName.DCVOLTAGE:
					{
						tmp.sensorMinimumValue = Constants.DCVoltageMinimum;
						tmp.sensorMaximumValue = Constants.DCVoltageMaximum;
						break;
					}
				case SensorName.HUMIDITY:
					{
						tmp.sensorMinimumValue = Constants.HumidityMinimum;
						tmp.sensorMaximumValue = Constants.HumidityMaximum;
						break;
					}
				case SensorName.TEMPERATURE:
					{
						tmp.sensorMinimumValue = Constants.TemperatureMinimum;
						tmp.sensorMaximumValue = Constants.TemperatureMaximum;
						break;
					}
				default:
					{
						tmp.sensorMinimumValue = 0;
						tmp.sensorMaximumValue = 100;
						break;
					}
			}

			return tmp;
		}

		public List<string> splitter(string Delimiter, string Input)
		{
			string[] delimiterChars = { Delimiter };
			StringSplitOptions sso = StringSplitOptions.None;

			string[] words = Input.Split(delimiterChars, sso);

			List<string> list = new List<string>(words);

			return list;
		}

		public enum CRC8_POLY
		{
			CRC8 = 0xd5,
			CRC8_CCITT = 0x07,
			CRC8_DALLAS_MAXIM = 0x31,
			CRC8_SAE_J1850 = 0x1D,
			CRC_8_WCDMA = 0x9b,
		};
	}
}
