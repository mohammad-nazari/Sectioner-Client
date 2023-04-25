using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ToolsLib
{
	public static class Constants
	{
		public const int Maxsize = 1024;
		public const int Timeout = 120000;
		public const int Width = 64;
		public const int Height = 64;
		public const string SettingFileName = "Settings.xml";
		public const int SleepTime = 5000;
		public const int TryToServer = 10;

		public const int TemperatureCalibration = 5;
		public const int TemperatureMinimum = -50;
		public const int TemperatureMaximum = 150;
		public const int TemperatureMinimumThreshold = 10;
		public const int TemperatureMaximumThreshold = 10;

		public const int HumidityCalibration = 5;
		public const int HumidityMinimum = 0;
		public const int HumidityMaximum = 100;
		public const int HumidityMinimumThreshold = 10;
		public const int HumidityMaximumThreshold = 10;

		public const int ACVoltageCalibration = 5;
		public const int ACVoltageMinimum = 100;
		public const int ACVoltageMaximum = 250;
		public const int ACVoltageMinimumThreshold = 20;
		public const int ACVoltageMaximumThreshold = 20;

		public const int ACAmpereCalibration = 5;
		public const int ACAmpereMinimum = 0;
		public const int ACAmpereMaximum = 15;
		public const int ACAmpereMinimumThreshold = 5;
		public const int ACAmpereMaximumThreshold = 5;

		public const int DCVoltageCalibration = 5;
		public const int DCVoltageMinimum = 12;
		public const int DCVoltageMaximum = 60;
		public const int DCVoltageMinimumThreshold = 10;
		public const int DCVoltageMaximumThreshold = 10;

		public const int DCAmpereCalibration = 5;
		public const int DCAmpereMinimum = 0;
		public const int DCAmpereMaximum = 20;
		public const int DCAmpereMinimumThreshold = 5;
		public const int DCAmpereMaximumThreshold = 5;

		public const string ASSISGNEDEVICETOUSER = "Devices assigned to user successfully.";
		public const string DELETEEVICEFROMUSER = "Devices deleted from user successfully.";
		public const string UPDATEUSER = "User informations update successfully.";
		public const string ADDUSER = "New User added successfully.";
		public const string NODEVICE = "No Device Found";


		// JSON for default calibrations
		/*{
		   "SensorCalibration" : [
			  {
				 "Name" : "TEMPERATURE",
				 "Zero" : -50.0,
				 "Span" : 150.0,
				 "Min" : -50.0,
				 "Max" : 150.0
			  },
			  {
				 "Name" : "HUMIDITY",
				 "Zero" : 0.0,
				 "Span" : 100.0,
				 "Min" : 0.0,
				 "Max" : 100.0
			  },
			  {
				 "Name" : "ACVOLTAGE",
				 "Zero" : 0.0,
				 "Span" : 230.0,
				 "Min" : 0.0,
				 "Max" : 230.0
			  },
			  {
				 "Name" : "ACAMPERE",
				 "Zero" : 0.0,
				 "Span" : 15.0,
				 "Min" : 0.0,
				 "Max" : 15.0
			  },
			  {
				 "Name" : "DCVOLTAGE",
				 "Zero" : 12.0,
				 "Span" : 60.0,
				 "Min" : 12.0,
				 "Max" : 60.0
			  },
			  {
				 "Name" : "DCAMPERE",
				 "Zero" : 0.0,
				 "Span" : 20.0,
				 "Min" : 0.0,
				 "Max" : 20.0
			  },
			  {
				 "Name" : "COSQ",
				 "Zero" : 0.0,
				 "Span" : 5.0,
				 "Min" : 0.0,
				 "Max" : 5.0
			  }
		   ]
		}
		*/
		public const string DEFAULTJSON  = "{\"SensorCalibration\" : [{\"Name\" : \"TEMPERATURE\",\"Zero\" : -50.0,\"Span\" : 150.0,\"Min\" : -50.0,\"Max\" : 150.0},{\"Name\" : \"HUMIDITY\",\"Zero\" : 0.0,\"Span\" : 100.0,\"Min\" : 0.0,\"Max\" : 100.0},{\"Name\" : \"ACVOLTAGE\",\"Zero\" : 0.0,\"Span\" : 230.0,\"Min\" : 0.0,\"Max\" : 230.0},{\"Name\" : \",\"Zero\" : 0.0,\"Span\" : 15.0,\"Min\" : 0.0,\"Max\" : 15.0},{\"Name\" : \"DCVOLTAGE\",\"Zero\" : 12.0,\"Span\" : 60.0,\"Min\" : 12.0,\"Max\" : 60.0},{\"Name\" : \",\"Zero\" : 0.0,\"Span\" : 20.0,\"Min\" : 0.0,\"Max\" : 20.0},{\"Name\" : \"COSQ\",\"Zero\" : 0.0,\"Span\" : 5.0,\"Min\" : 0.0,\"Max\" : 5.0}]}";

		public static readonly String[] SENSORSNAMEMULTI = { "TEMPERATURE", "HUMIDITY", "H2S", "PRESSER", "ACVOLTAGE", "ACVOLTAGE1", "ACVOLTAGE2", "ACVOLTAGE3", "ACAMPERE", "ACAMPERE1", "ACAMPERE2", "ACAMPERE3", "DCVOLTAGE", "DCAMPERE" };

		public static readonly String[] SENSORSNAMEMONO = { "GAS", "SMOKE", "MAGNET", "WATER", "MOTION", "DIGITAL" };
	}
}
