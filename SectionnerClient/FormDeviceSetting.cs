using SecurityLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToolsLib;
using WebServiceLib;

namespace SectionnerClient
{
	public partial class FormDeviceSetting : Form
	{
		private Sectionner _sectionnerObject = new Sectionner();
		public Sectionner SectionnerObject
		{
			get
			{
				return _sectionnerObject;
			}
			set
			{
				_sectionnerObject = value;
			}
		}
		private Device _deviceObject = new Device();
		private User _userInfo = new User();
		private List<ErrorCode> _errorCode = new List<ErrorCode>();
		private SetDeviceSetting _setDeviceSetting = new SetDeviceSetting();
		private List<string> _errors = new List<string>();
		private ToolsLib.Tools _toolsObject = new ToolsLib.Tools();
		private FormMain _formMain;

		public SectionnerClient.FormMain FormMain
		{
			get
			{
				return _formMain;
			}
			set
			{
				_formMain = value;
			}
		}
		public WebServiceLib.User UserInfo
		{
			get
			{
				return _userInfo;
			}
			set
			{
				_userInfo = value;
			}
		}

		public List<WebServiceLib.ErrorCode> ErrorCode
		{
			get
			{
				return _errorCode;
			}
			set
			{
				_errorCode = value;
			}
		}

		public ToolsLib.Tools ToolsObject
		{
			get
			{
				return _toolsObject;
			}
			set
			{
				_toolsObject = value;
			}
		}

		public Device DeviceObject
		{
			get
			{
				return _deviceObject;
			}
			set
			{
				_deviceObject = value;
			}
		}

		public FormDeviceSetting()
		{
			InitializeComponent();
		}

		private void FormDeviceSetting_Load(object sender, EventArgs e)
		{
			// Disable Enable Fields
			if(this._deviceObject.deviceModel == DeviceModel.MANAGER)
			{
				this.txtDeviceKeyType.Visible = false;
				this.lblDevKeyType.Visible = false;
			}
			else
			{
				this.nudDeviceTransPower.Visible = false;
				this.lblDevTransPower.Visible = false;

				this.nudDeviceTableCapacity.Visible = false;
				this.lblDevTableCapacity.Visible = false;

				this.lblDevNikeName.Text = "نام سر خط:";
			}
			/* 
			 * manage device threads
			 */
			this.pbDevicePicture.BackColor = Color.Transparent;
			this.pbDevicePicture.SizeMode = PictureBoxSizeMode.StretchImage;

			this.UpdateGUI();
		}

		private void UpdateGUI()
		{
			this.txtDeviceNikeName.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceNikeName;
			});

			this.lblDeviceID.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceSerialNumber.ToString();
			});

			this.lblDeviceLocation.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceCity;
			});

			this.lblDeviceCity.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceLocation;
			});

			this.nudDeviceSamplingTime.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceSamplingTime.ToString();
			});

			this.dtsDeviceDateTime.InvokeIfRequired(c =>
			{
				c.Value = this._deviceObject.deviceDateTime;
			});

			int rowIndex = 0;

			// Fill Sensors info
			this.dgvRelays.InvokeIfRequired(c =>
			{
				c.Rows.Clear();
			});
			if(this._deviceObject.deviceSensors != null)
			{
				if(this._deviceObject.deviceSensors.Count() > 0)
				{
					foreach(SensorExtension sensor in this._deviceObject.deviceSensors)
					{
						if(sensor.sensorName == SensorName.RELAY)
						{
							// Number of this type of sensor
							decimal sensorCounts = sensor.deviceSensors.Count();
							// Numbers of data grid rows to show this type of sensor values
							decimal sensorRows = Math.Ceiling(sensorCounts / 8);

							rowIndex = 0;
							for(int sensorRowIndex = 0; sensorRowIndex < sensorRows; sensorRowIndex++)
							{
								// Add new row
								this.dgvRelays.InvokeIfRequired(c =>
								{
									rowIndex = c.Rows.Add();
								});
								// Update row
								// Sensor Name
								this.dgvRelays.InvokeIfRequired(c =>
								{
									c.Rows[rowIndex].Cells["RelayRow"].Value = rowIndex.ToString();
								});
								// Update row
								// Sensor Name
								this.dgvRelays.InvokeIfRequired(c =>
								{
									c.Rows[rowIndex].Tag = sensor.sensorName + "_" + sensorRowIndex.ToString();
								});

								for(int colIndex = 1; colIndex <= 8 && ((sensorRowIndex * 8) + colIndex) <= sensorCounts; colIndex++)
								{
									// Sensor SMS on or Off
									this.dgvRelays.InvokeIfRequired(c =>
									{
										((DataGridViewComboBoxCell)c.Rows[rowIndex].Cells[colIndex]).Value = (sensor.deviceSensors[((sensorRowIndex * 8) + colIndex - 1)].sensorValue.ToString() == "0" ? "خاموش" : "روشن");
									});
								}
							}
						}
					}
					this.btnSetRelays.InvokeIfRequired(c =>
					{
						c.Enabled = true;
					});
				}
				else
				{
					this.dgvRelays.InvokeIfRequired(c =>
					{
						c.Rows.Clear();
					});
				}
			}
			else
			{
				this.dgvRelays.InvokeIfRequired(c =>
				{
					c.Rows.Clear();
				});
			}

			this.btnChangeSettings.InvokeIfRequired(c =>
			{
				c.Enabled = true;
			});

			this.btnCalibration.InvokeIfRequired(c =>
			{
				c.Enabled = true;
			});

			this.btnResetDevice.InvokeIfRequired(c =>
			{
				c.Enabled = (DeviceObject.deviceError != null && DeviceObject.deviceError.errorType != SettingLevel.Disable);
			});
		}

		/// <summary>
		/// Check input data to be correct
		/// </summary>
		/// <returns></returns>
		private bool CheckRestrictions()
		{
			return true;
		}

		private void btnChangeSettings_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if(dialogResult == DialogResult.No)
			{
				return;
			}

			string errorsMessage = "";
			// Set public device info

			// Device serial number
			//this._deviceObject.deviceSerialNumber = Convert.ToInt32(this.lblDeviceID.Text);

			// Device name and nickname
			if(this.txtDeviceNikeName.Text != "")
			{
				this._deviceObject.deviceNikeName = this.txtDeviceNikeName.Text;
			}
			else
			{
				errorsMessage += "Enter a nickname for device" + Environment.NewLine;
			}

			// Device date and time
			if(this.dtsDeviceDateTime.Text != "")
			{
				this._deviceObject.deviceDateTime = this.dtsDeviceDateTime.Value.Value;
			}
			else
			{
				errorsMessage += "Enter a valid date and time for device" + Environment.NewLine;
			}

			// Device location
			if(this.lblDeviceLocation.Text != "")
			{
				this._deviceObject.deviceLocation = this.lblDeviceLocation.Text;
			}
			else
			{
				errorsMessage += "Enter a location for device" + Environment.NewLine;
			}

			// Device city
			if(this.lblDeviceCity.Text != "")
			{
				this._deviceObject.deviceCity = this.lblDeviceCity.Text;
			}
			else
			{
				errorsMessage += "Enter a city for device" + Environment.NewLine;
			}

			// Device sampling
			if(this.nudDeviceSamplingTime.Text != "")
			{
				this._deviceObject.deviceSamplingTime = Convert.ToInt32(this.nudDeviceSamplingTime.Value);
			}
			else
			{
				errorsMessage += "Enter a valid sampling time for device" + Environment.NewLine;
			}

			// Device position
			if(this.nudDevicePositionX.Text != "" && this.nudDevicePositionY.Text != "")
			{
				this._deviceObject.deviceGPS.gpsX = Convert.ToInt32(this.nudDevicePositionX.Value);
				this._deviceObject.deviceGPS.gpsY = Convert.ToInt32(this.nudDevicePositionY.Value);
			}
			else
			{
				errorsMessage += "Enter a valid Position X,Y for device" + Environment.NewLine;
			}

			// Device key type
			if(this.txtDeviceKeyType.Text != "")
			{
				this._deviceObject.deviceKeyName = this.txtDeviceKeyType.Text;
			}
			else
			{
				this._deviceObject.deviceKeyName = this.txtDeviceKeyType.Text;
			}

			// Device trans power
			if(this.nudDeviceTransPower.Text != "")
			{
				this._deviceObject.deviceTransPower = Convert.ToInt32(this.nudDeviceTransPower.Value);
			}
			else
			{
				errorsMessage += "Enter a valid sampling time for device" + Environment.NewLine;
			}

			// Device table capacity
			if(this.nudDeviceTableCapacity.Text != "")
			{
				this._deviceObject.deviceTableCapacity = Convert.ToInt32(this.nudDeviceTableCapacity.Value);
			}
			else
			{
				errorsMessage += "Enter a valid sampling time for device" + Environment.NewLine;
			}

			// No error
			if(errorsMessage == "")
			{
				this.ResetDeviceFlags();
				this._deviceObject.deviceRequests = new RequestType[] { RequestType.DATETIME, RequestType.SAMPLING };
				if(!this.SendSettingsToServer())
				{
					string Messages = "";
					foreach(ErrorCode errorCode in this._errorCode)
					{
						Messages += "Error (" + errorCode.errorNumber + "): " + errorCode.errorMessage + Environment.NewLine;
					}
					MessageBox.Show(Messages, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					MessageBox.Show("Device info updated successfully", "Update informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
					// Send data to server and device
					this.Close();
				}
			}
			else
			{
				// Show errors and return back to form
				MessageBox.Show(errorsMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private bool SendSettingsToServer()
		{
			try
			{
				// Send new data to server updating device info
				this._toolsObject.DeviceInfoToBase64(this._deviceObject);
				this._setDeviceSetting.requestUser = this._userInfo;
				this._setDeviceSetting.requestDevice = this._deviceObject;
				this._errorCode = (this._formMain.SectionnerObject.SetDeviceSetting(this._setDeviceSetting)).ToList();
				if(this._errorCode != null && this._errorCode.Count > 0)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			catch(Exception)
			{
				return false;
			}
		}

		private void bttDeviceLocation_Click(object sender, EventArgs e)
		{
			FormSetLocation formSetLocation = new FormSetLocation();

			this._formMain.GetCityNames();

			if(this._formMain.CitiesList != null)
			{
				if(this._formMain.CitiesList.allCityLocatoins != null)
				{
					if(this._formMain.CitiesList.allCityLocatoins.Count() > 0)
					{
						Base64 b64 = new Base64();
						foreach(CityLocation cityName in this._formMain.CitiesList.allCityLocatoins)
						{
							if(cityName.cityLocationCityName != b64.Base64Encoding(this.lblDeviceLocation.Text, Encoding.UTF8))
							{
								if(cityName.cityLocationLocationsName != null)
								{
									if(cityName.cityLocationLocationsName.Count() > 0)
									{
										foreach(string locationName in cityName.cityLocationLocationsName)
										{
											if(locationName != b64.Base64Encoding(this.lblDeviceCity.Text, Encoding.UTF8))
											{
												formSetLocation.cbLocationList.Items.Add(b64.Base64Decoding(locationName, Encoding.UTF8));
											}
										}
									}
								}
							}
						}
						if(formSetLocation.cbLocationList.Items.Count > 0)
						{
							formSetLocation.cbLocationList.SelectedIndex = 0;
						}
					}
				}
			}

			formSetLocation.lblCurrentLocationName.Text = this._deviceObject.deviceLocation;

			formSetLocation.ShowDialog();

			if(formSetLocation.DialogResult == DialogResult.OK)
			{
				this.lblDeviceCity.Text = formSetLocation.LocationName;
			}
		}

		private void bttDeviceCity_Click(object sender, EventArgs e)
		{
			FormSetCity formSetCity = new FormSetCity();

			this._formMain.GetCityNames();

			if(this._formMain.CitiesList != null)
			{
				if(this._formMain.CitiesList.allCityLocatoins != null)
				{
					if(this._formMain.CitiesList.allCityLocatoins.Count() > 0)
					{
						Base64 b64 = new Base64();
						foreach(CityLocation cityName in this._formMain.CitiesList.allCityLocatoins)
						{
							if(cityName.cityLocationCityName != b64.Base64Encoding(this.lblDeviceLocation.Text, Encoding.UTF8))
							{
								formSetCity.cbCityList.Items.Add(b64.Base64Decoding(cityName.cityLocationCityName, Encoding.UTF8));
							}
						}
						if(formSetCity.cbCityList.Items.Count > 0)
						{
							formSetCity.cbCityList.SelectedIndex = 0;
						}
					}
				}
			}

			formSetCity.lblCurrentCityName.Text = this._deviceObject.deviceCity;

			formSetCity.ShowDialog();

			if(formSetCity.DialogResult == DialogResult.OK)
			{
				this.lblDeviceLocation.Text = formSetCity.CityName;
			}
		}

		private void dgvSensors_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void btnResetDevice_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if(dialogResult == DialogResult.No)
			{
				return;
			}
			this.ResetDeviceFlags();
			this._deviceObject.deviceRequests = new RequestType[] { RequestType.RESET };
			this.SendSettingsToServer();
		}

		private void btnSetRelays_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if(dialogResult == DialogResult.No)
			{
				return;
			}
			string errorsMessage = "";
			this._deviceObject.deviceSensors = new SensorExtension[0];
			// Device sensors info
			if(dgvRelays.Rows.Count > 0)
			{
				List<bool> termsList = new List<bool>();
				// You can convert it back to an array if you would like to
				foreach(DataGridViewRow sensorRow in dgvRelays.Rows)
				{
					// Sensor SMS on or Off
					for(int colIndex = 1; colIndex <= 8; colIndex++)
					{
						termsList.Add((((DataGridViewComboBoxCell)sensorRow.Cells[colIndex]).Value.ToString() == "روشن") ? true : false);
					}
				}
				this._deviceObject.deviceRelays = termsList.ToArray();
			}

			// No error
			if(errorsMessage == "")
			{
				this.ResetDeviceFlags();
				this._deviceObject.deviceRequests = new RequestType[] { RequestType.OUTRELAY };
				if(!this.SendSettingsToServer())
				{
					string Messages = "";
					foreach(ErrorCode errorCode in this._errorCode)
					{
						Messages += "Error (" + errorCode.errorNumber + "): " + errorCode.errorMessage + Environment.NewLine;
					}
					MessageBox.Show(Messages, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					MessageBox.Show("Device relays updated successfully", "Update informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
					// Send data to server and device
					this.Close();
				}
			}
			else
			{
				// Show errors and return back to form
				MessageBox.Show(errorsMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ResetDeviceFlags()
		{
			this._deviceObject.deviceReset = false;
		}

		private void btnCalibration_Click(object sender, EventArgs e)
		{
			FormCalibration formCalibration = new FormCalibration(this._deviceObject);

			if(formCalibration.ShowDialog() == DialogResult.OK)
			{
				DialogResult dialogResult = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if(dialogResult == DialogResult.No)
				{
					return;
				}
				string errorsMessage = "";
				this._deviceObject.deviceSensors = new SensorExtension[0];
				// Device sensors info
				if(formCalibration.dgvSensors.Rows.Count > 1)
				{
					List<SensorExtension> termsList = new List<SensorExtension>();
					SensorExtension termsTemp = new SensorExtension();
					termsTemp.deviceSensors = new Sensor[0];
					// You can convert it back to an array if you would like to
					foreach(DataGridViewRow sensorRow in formCalibration.dgvSensors.Rows)
					{
						// Sensor SMS on or Off
						termsTemp.sensorName = (SensorName)MyEnum.ParseEnum<SettingLevel>(sensorRow.Cells["SensorName"].Value.ToString());
						termsTemp.deviceSensors[0].sensorMinimumValue = Convert.ToInt32(sensorRow.Cells["SensorMin"].Value.ToString());
						termsTemp.deviceSensors[0].sensorMaximumValue = Convert.ToInt32(sensorRow.Cells["SensorMax"].Value.ToString());

						termsList.Add(termsTemp);
					}
					this._deviceObject.deviceSensors = termsList.ToArray();
				}

				// No error
				if(errorsMessage == "")  
				{
					this.ResetDeviceFlags();
					this._deviceObject.deviceRequests = new RequestType[] { RequestType.CALIBRATION };
					if(!this.SendSettingsToServer())
					{
						string Messages = "";
						foreach(ErrorCode errorCode in this._errorCode)
						{
							Messages += "Error (" + errorCode.errorNumber + "): " + errorCode.errorMessage + Environment.NewLine;
						}
						MessageBox.Show(Messages, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						MessageBox.Show("Device sensor calibration done successfully", "Update informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
						// Send data to server and device
						this.Close();
					}
				}
				else
				{
					// Show errors and return back to form
					MessageBox.Show(errorsMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
