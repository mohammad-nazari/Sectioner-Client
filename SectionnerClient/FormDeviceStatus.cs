using ImageLib;
using SecurityLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolsLib;
using WebServiceLib;
using System.Diagnostics;

namespace SectionnerClient
{
	public partial class FormDeviceStatus : Form
	{
		private Device _deviceObject = new Device();
		private User _userInfo = new User();
		private bool _isLogin = true;
		private ErrorCode _errorCode = new ErrorCode();

		private List<string> _errors = new List<string>();
		private ToolsLib.Tools _toolsObject = new ToolsLib.Tools();

		private FormMain _formMain;

		private Stopwatch _stopWatch = new Stopwatch();

		private bool _inProgress = false;

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

		public FormDeviceStatus()
		{
			InitializeComponent();
		}

		private void FormDeviceStatus_Load(object sender, EventArgs e)
		{
			// Disable Enable Fields
			if(this._deviceObject.deviceModel == DeviceModel.MANAGER)
			{
				this.lblDeviceKeyType.Visible = false;
				this.lblDevKeyType.Visible = false;

				this.pnlDevicePicture.Visible = false;
				this.pbLastPicture.Visible = false;
				this.gbLastPicture.Visible = false;
				this.SuspendLayout();
				this.Width = 900;
				this.ResumeLayout();
			}
			else
			{
				this.lblDevicePOK.Visible = false;
				this.lblDevPOK.Visible = false;

				this.lblDevicePTotal.Visible = false;
				this.lblDevPTotal.Visible = false;

				this.lblDeviceTransPower.Visible = false;
				this.lblDevTransPower.Visible = false;

				this.lblDeviceTableCapacity.Visible = false;
				this.lblDevTableCapacity.Visible = false;

				this.lblDeviceP1.Visible = false;
				this.lblDevP1.Visible = false;

				this.lblDeviceP2.Visible = false;
				this.lblDevP2.Visible = false;

				this.lblDeviceP3.Visible = false;
				this.lblDevP3.Visible = false;

				this.lblDeviceQ1.Visible = false;
				this.lblDevQ1.Visible = false;

				this.lblDeviceQ2.Visible = false;
				this.lblDevQ2.Visible = false;

				this.lblDeviceQ3.Visible = false;
				this.lblDevQ3.Visible = false;

				this.lblDevNikeName.Text = "نام سر خط:";
			}
		}

		/// <summary>
		/// Start as a thread
		/// Get device status from server
		/// every some minutes
		/// Manage image and tree view and error status
		/// </summary>
		/// <returns></returns>
		public void StartDeviceThread()
		{
			Device deviceBackup = MyClone.DeepClone(this._deviceObject);
			if(this._isLogin)
			{
				if(this._isLogin)
				{
					// Compute some values to assign in GUI
					//this._formMain.InitiateDeviceStatus(this._deviceObject);

					if(this._isLogin)
					{
						// Initialize Device Status
						if(this._deviceObject != deviceBackup)
						{
							// Refresh window
							this.UpdateGUI();

							// Backup Last data
							deviceBackup = MyClone.DeepClone(this._deviceObject);
						}
					}
				}
			}
		}

		private void UpdateGUI()
		{
			this.InvokeIfRequired(c =>
			{
				c.SuspendLayout();
			});
			Color newColor = Color.Green;
			if(this._deviceObject.deviceError != null)
			{
				if(this._deviceObject.deviceError.errorType != SettingLevel.Normal)
				{
					// Get color for status
					newColor = this._formMain.GetStatusColor(this._deviceObject.deviceError.errorType);
				}
			}

			// Set icon and background color
			if(newColor != Color.Green)
			{
				this.pbDevicePicture.Image = this._formMain.ImageHandlerList[newColor].CurrentBitmap;
			}
			else
			{
				this.pbDevicePicture.Image = this._formMain.ImageHandlerList[Color.Green].CurrentBitmap;
			}

			this.lblDeviceSerialNumber.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceSerialNumber.ToString();
			});

			this.lblDeviceTransPower.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceNikeName;
			});

			this.lblDevicePosition.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceSerialNumber.ToString();
			});

			this.lblDeviceModel.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceModel.ToString();
			});

			this.lblDeviceSamplingTime.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceSamplingTime.ToString();
			});

			if(this._deviceObject.deviceCamera != null)
			{
				this.lblDeviceCamera.InvokeIfRequired(c =>
				{
					c.Text = this._deviceObject.deviceCamera.cameraIP.ip1.ToString() + "." + this._deviceObject.deviceCamera.cameraIP.ip2.ToString() + "." + this._deviceObject.deviceCamera.cameraIP.ip3.ToString() + "." + this._deviceObject.deviceCamera.cameraIP.ip4.ToString() + ":" + this._deviceObject.deviceCamera.cameraPort.ToString();
				});
			}
			if(this._deviceObject.deviceIP != null)
			{
				this.lblDeviceIP.InvokeIfRequired(c =>
				{
					c.Text = this._deviceObject.deviceIP.ip1.ToString() + "." + this._deviceObject.deviceIP.ip2.ToString() + "." + this._deviceObject.deviceIP.ip3.ToString() + "." + this._deviceObject.deviceIP.ip4.ToString() + ":" + this._deviceObject.devicePort.ToString();
				});
			}

			this.lblDeviceCity.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceCity;
			});

			this.lblDeviceLocation.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceLocation;
			});

			this.lblDeviceDateTime.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceDateTime.Year.ToString() + "-" + this._deviceObject.deviceDateTime.Month.ToString() + "-" + this._deviceObject.deviceDateTime.Day.ToString() + " " + this._deviceObject.deviceDateTime.Hour.ToString() + ":" + this._deviceObject.deviceDateTime.Minute.ToString() + ":" + this._deviceObject.deviceDateTime.Second.ToString();
			});
			this.lblDeviceNikeName.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceNikeName;
			});
			this.lblDeviceTransPower.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceTransPower.ToString();
			});
			this.lblDeviceTableCapacity.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceTableCapacity.ToString();
			});
			this.lblDeviceKeyType.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceKeyName;
			});
			this.lblDevicePosition.InvokeIfRequired(c =>
			{
				c.Text = this._deviceObject.deviceGPS.gpsX.ToString() + " , " + this._deviceObject.deviceGPS.gpsY.ToString();
			});

			if(this._deviceObject.deviceError != null)
			{
				this.lblDeviceStatus.InvokeIfRequired(c =>
				{
					c.Text = this._deviceObject.deviceError.errorType.ToString();
				});
				this.lblDeviceStatus.InvokeIfRequired(c =>
						{
							c.BackColor = newColor;
						});

				this.rtbStatus.InvokeIfRequired(c =>
				{
					c.Text = "";
				});
				if(this._deviceObject.deviceError.errorMessage != "")
				{
					this.rtbStatus.InvokeIfRequired(c =>
					{
						c.AppendText(this._deviceObject.deviceError.errorMessage);
					});
				}
				else
				{
					this.rtbStatus.InvokeIfRequired(c =>
					{
						c.AppendText("Normal");
					});
				}
			}
			double[] voltage = new double[3];
			double[] ampere = new double[3];
			double[] cosQ = new double[3];
			double[] P = new double[3];
			double[] Q = new double[3];

			// Fill Sensors info
			if(this._deviceObject.deviceSensors != null)
			{
				if(this._deviceObject.deviceSensors.Count() > 0)
				{
					foreach(SensorExtension sensorEx in this._deviceObject.deviceSensors)
					{
						// Number of this type of sensor
						decimal sensorCounts = sensorEx.deviceSensors.Count();
						// Numbers of data grid rows to show this type of sensor values
						decimal sensorRows = Math.Ceiling(sensorCounts / 8);
						int rowIndex = 0;
						for(int sensorRowIndex = 0; sensorRowIndex < sensorRows; sensorRowIndex++)
						{
							rowIndex = 0;
							bool exist = false;
							foreach(DataGridViewRow row in this.dgvSensors.Rows)
							{
								if(row.Tag.ToString() == sensorEx.sensorName.ToString() + "_" + (sensorRowIndex + 1).ToString())
								{
									// Row exist
									// Just update its row
									rowIndex = row.Index;
									exist = true;

									break;
								}
							}
							if(exist == false)
							{
								// Add new row
								this.dgvSensors.InvokeIfRequired(c =>
								{
									rowIndex = c.Rows.Add();
								});
								// Update row
								// Sensor Name
								this.dgvSensors.InvokeIfRequired(c =>
								{
									c.Rows[rowIndex].Cells["SensorName"].Value = @"";
									c.Rows[rowIndex].Cells["SensorName"].Value = sensorEx.sensorName;
								});
								// Update row
								// Sensor Name
								this.dgvSensors.InvokeIfRequired(c =>
								{
									c.Rows[rowIndex].Tag = sensorEx.sensorName + "_" + (sensorRowIndex + 1).ToString();
								});
							}
							string label = @"";
							for(int colIndex = 1; colIndex <= 8 && ((sensorRowIndex * 8) + colIndex) <= sensorCounts; colIndex++)
							{
								label = @"";
								if(sensorEx.sensorName == WebServiceLib.SensorName.ACVOLTAGE)
								{
									// Sensor Label (Name)
									label = sensorEx.deviceSensors[((sensorRowIndex * 8) + colIndex - 1)].sensorNikeName + " :: ";
									if(sensorEx.deviceSensors[((sensorRowIndex * 8) + colIndex - 1)].sensorValue < 50)
									{
										// Sensor Label (Name)
										this.dgvSensors.InvokeIfRequired(c =>
										{
											c.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.Red;
										});
									}
									else
									{
										// Sensor Label (Name)
										this.dgvSensors.InvokeIfRequired(c =>
										{
											c.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.Transparent;
										});
									}
									if(((sensorRowIndex * 8) + colIndex) < 3)
									{
										voltage[((sensorRowIndex * 8) + colIndex - 1)] = sensorEx.deviceSensors[((sensorRowIndex * 8) + colIndex - 1)].sensorValue;
									}
								}
								else if(sensorEx.sensorName == WebServiceLib.SensorName.ACAMPERE)
								{
									// Sensor Label (Name)
									label = sensorEx.deviceSensors[((sensorRowIndex * 8) + colIndex - 1)].sensorNikeName + " :: ";
									if(sensorEx.deviceSensors[((sensorRowIndex * 8) + colIndex - 1)].sensorValue > 500)
									{
										// Sensor Label (Name)
										this.dgvSensors.InvokeIfRequired(c =>
										{
											c.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.Yellow;
										});
									}
									else
									{
										// Sensor Label (Name)
										this.dgvSensors.InvokeIfRequired(c =>
										{
											c.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.Transparent;
										});
									}
									if(((sensorRowIndex * 8) + colIndex) < 3)
									{
										ampere[((sensorRowIndex * 8) + colIndex - 1)] = sensorEx.deviceSensors[((sensorRowIndex * 8) + colIndex - 1)].sensorValue;
									}
								}
								else if(sensorEx.sensorName == WebServiceLib.SensorName.COSQ)
								{
									if(((sensorRowIndex * 8) + colIndex) < 3)
									{
										cosQ[((sensorRowIndex * 8) + colIndex - 1)] = sensorEx.deviceSensors[((sensorRowIndex * 8) + colIndex - 1)].sensorValue;
									}
								}

								if(sensorEx.sensorName == WebServiceLib.SensorName.RELAY)
								{
									// Sensor CurrentStatus
									label += sensorEx.deviceSensors[((sensorRowIndex * 8) + colIndex - 1)].sensorValue == 0 ? "خاموش" : "روشن";
								}
								else
								{
									// Sensor CurrentStatus
									label += sensorEx.deviceSensors[((sensorRowIndex * 8) + colIndex - 1)].sensorValue.ToString("F2");
								}

								// Sensor CurrentStatus
								this.dgvSensors.InvokeIfRequired(c =>
								{
									c.Rows[rowIndex].Cells[colIndex].Value = @"";
									c.Rows[rowIndex].Cells[colIndex].Value = label;
								});
							}
						}
					}
					foreach(DataGridViewRow row in this.dgvSensors.Rows)
					{
						bool exist = false;
						foreach(SensorExtension sensorEx in this._deviceObject.deviceSensors)
						{
							// Number of this type of sensor
							decimal sensorCounts = sensorEx.deviceSensors.Count();
							// Numbers of data grid rows to show this type of sensor values
							double sensorRows = (double)sensorCounts / 8;
							sensorRows = Math.Ceiling(sensorRows);
							sensorRows = Math.Round(sensorRows, MidpointRounding.AwayFromZero);

							for(int sensorRowIndex = 0; sensorRowIndex < sensorRows; sensorRowIndex++)
							{
								if(row.Tag.ToString() == sensorEx.sensorName.ToString() + "_" + (sensorRowIndex + 1).ToString())
								{
									// Row exist
									// Just update its row
									exist = true;
									break;
								}
							}
							if(exist)
							{
								break;
							}
						}
						if(!exist)
						{
							// Add new row
							this.dgvSensors.InvokeIfRequired(c =>
							{
								c.Rows.Remove(row);
							});
						}
					}
				}
				else
				{
					this.dgvSensors.InvokeIfRequired(c =>
					{
						c.Rows.Clear();
					});
				}
			}
			else
			{
				this.dgvSensors.InvokeIfRequired(c =>
				{
					c.Rows.Clear();
				});
			}

			P[0] = voltage[0] * ampere[0] * cosQ[0];
			P[1] = voltage[1] * ampere[1] * cosQ[1];
			P[2] = voltage[2] * ampere[2] * cosQ[2];

			Q[0] = voltage[0] * ampere[0] * Math.Sin(Math.Cosh(cosQ[0]));
			Q[1] = voltage[1] * ampere[1] * Math.Sin(Math.Cosh(cosQ[1]));
			Q[2] = voltage[2] * ampere[2] * Math.Sin(Math.Cosh(cosQ[2]));

			double POK = (double)this._deviceObject.deviceTransPower * 0.75;
			double PTotal = P[0] + P[1] + P[0];

			this.lblDevicePOK.InvokeIfRequired(c =>
			{
				c.Text = POK.ToString();
			});

			this.lblDevicePTotal.InvokeIfRequired(c =>
			{
				c.Text = PTotal.ToString();
			});

			if(POK < PTotal)
			{
				this.lblDevicePTotal.InvokeIfRequired(c =>
				{
					c.BackColor = Color.Red;
				});
			}
			else
			{
				this.lblDevicePTotal.InvokeIfRequired(c =>
				{
					c.BackColor = Color.Gray;
				});
			}

			this.lblDeviceP1.InvokeIfRequired(c =>
			{
				c.Text = P[0].ToString();
			});

			this.lblDeviceP2.InvokeIfRequired(c =>
			{
				c.Text = P[1].ToString();
			});

			this.lblDeviceP3.InvokeIfRequired(c =>
			{
				c.Text = P[2].ToString();
			});

			this.lblDeviceQ1.InvokeIfRequired(c =>
			{
				c.Text = Q[0].ToString();
			});

			this.lblDeviceQ2.InvokeIfRequired(c =>
			{
				c.Text = Q[1].ToString();
			});

			this.lblDeviceQ3.InvokeIfRequired(c =>
			{
				c.Text = Q[2].ToString();
			});

			this.btnChangeSettings.InvokeIfRequired(c =>
			{
				c.Enabled = true;
			});
            
			this.InvokeIfRequired(c =>
			{
				c.ResumeLayout();
			});
		}

		private void AddRowData(TableLayoutPanel T, int ColumnIndex, string InputString)
		{
			T.Controls.Add(this.GenerateLabel(InputString), ColumnIndex, T.RowCount - 1);
		}

		private Label GenerateLabel(string LabelText)
		{
			Label labelObject = new Label();
			labelObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			labelObject.AutoSize = true;
			labelObject.BackColor = System.Drawing.SystemColors.Control;
			labelObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			labelObject.ForeColor = System.Drawing.SystemColors.ControlText;
			labelObject.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			labelObject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelObject.InvokeIfRequired(c =>
				{
					c.Text = LabelText;
				});

			return labelObject;
		}

		private void btnChangeSettings_Click(object sender, EventArgs e)
		{
			this._formMain.GenerateDeviceSettingWindowForm(Convert.ToInt32(this.lblDeviceSerialNumber.Text));
		}

		private void bttGetPicture_Click(object sender, EventArgs e)
		{
			_stopWatch = Stopwatch.StartNew();
			_stopWatch.Start();
			timerStopWatch.Enabled = this._inProgress;
			timerStopWatch.Start();
			Task.Factory.StartNew(this.ManageDevicesThread);
			//this.timerLoadPicture.Enabled = true;
		}

		private void ManageDevicesThread()
		{
			this._inProgress = true;
			this.btnGetPicture.InvokeIfRequired(c =>
			{
				c.Enabled = !this._inProgress;
			});

			string errorMessage = "";
			try
			{
				int pictureSize = 0;
				GetDevicePicture getDevicePicture = new GetDevicePicture();
				getDevicePicture.requestUser = this._userInfo;
				getDevicePicture.requestDevice = this._deviceObject;
				Device deviceTemp = this._formMain.SectionnerObject.GetDevicePicture(getDevicePicture);
				if(deviceTemp.deviceError != null && (deviceTemp.deviceError.errorNumber != 0 || deviceTemp.deviceError.errorMessage != ""))
				{
					errorMessage = "Error (" + this._errorCode.errorNumber + "): " + this._errorCode.errorMessage;
				}
				else
				{
					double calcParts = (double)deviceTemp.devicePicture.pictureSize / (double)deviceTemp.devicePicture.picturePartSize;
					calcParts = Math.Ceiling(calcParts);
					calcParts = Math.Round(calcParts, MidpointRounding.AwayFromZero);
					if(deviceTemp != null && deviceTemp.devicePicture != null && (deviceTemp.devicePicture.picturePartNumbers == calcParts) && deviceTemp.devicePicture.picturePartNumbers > 0 && calcParts > 0)
					//if(deviceTemp != null)
					{
						//try to get picture parts in 5
						int tryCounter = 0;
						// Concat received picture parts
						string pictureParts = "";

						PicturePart picturePart = new PicturePart();
						GetDevicePicturePart getDevicePicturePart = new GetDevicePicturePart();
						getDevicePicturePart.requestUser = this._userInfo;
						Device deviceTemp2 = new Device();

						// Display the ProgressBar control.
						// Set Minimum to 1 to represent the first file being copied.
						// Set Maximum to the total number of files to copy.
						// Set the initial value of the ProgressBar.
						// Set the Step property to a value of 1 to represent each file being copied.
						this.prgbLoadPicture.InvokeIfRequired(c =>
						{
							c.Visible = true;
							c.Minimum = 1;
							c.Maximum = deviceTemp.devicePicture.picturePartNumbers;
							c.Value = 1;
							c.Step = 1;
						});
						int i = 1;
						while(i <= (deviceTemp.devicePicture.picturePartNumbers) && tryCounter < 5)
						{
							this._deviceObject.devicePicture.pictureParts = new PicturePart[1];
							this._deviceObject.devicePicture.pictureParts[0] = new PicturePart();
							this._deviceObject.devicePicture.pictureParts[0].picturePartIndex = i;
							this._deviceObject.devicePicture.picturePartNumbers = deviceTemp.devicePicture.picturePartNumbers;
							getDevicePicturePart.requestDevice = this._deviceObject;
							try
							{
								deviceTemp2 = this._formMain.SectionnerObject.GetDevicePicturePart(getDevicePicturePart);
								if(deviceTemp2 != null)
								{
									if(deviceTemp2.devicePicture != null)
									{
										if(deviceTemp2.devicePicture.pictureParts != null && deviceTemp2.devicePicture.pictureParts[0].picturePartData != "")
										{
											/*CRC32 crc32 = new CRC32();
											CRC16 crc16 = new CRC16();
											CRC8 crc8 = new CRC8(CRC8_POLY.CRC8_DALLAS_MAXIM);

											HexCode hex = new HexCode();
											byte[] crcBuye = hex.GetBytesFromString(deviceTemp2.devicePicture.pictureParts[0].picturePartData);
											int crc1 = Convert.ToInt32(crc32.calculateCRC32(deviceTemp2.devicePicture.pictureParts[0].picturePartData));
											int crc2 = Convert.ToInt32(crc16.compute_fcs(crcBuye));
											int crc3 = Convert.ToInt32(crc8.Checksum(crcBuye));*/

											//if(deviceTemp2.devicePicture.pictureParts[0].partCRC == crc1 || deviceTemp2.devicePicture.pictureParts[0].partCRC == crc2 || deviceTemp2.devicePicture.pictureParts[0].partCRC == crc3)
											{
												pictureSize += deviceTemp2.devicePicture.pictureParts[0].picturePartData.Length;
												pictureParts += deviceTemp2.devicePicture.pictureParts[0].picturePartData;
												this.lblLoadPicture.InvokeIfRequired(c =>
												{
													c.Text = "Part " + (i).ToString() + " of " + (deviceTemp.devicePicture.picturePartNumbers).ToString() + "(" + (100 * (i) / deviceTemp.devicePicture.picturePartNumbers).ToString() + "%)" + pictureSize;
												});
												// Perform the increment on the ProgressBar.
												this.prgbLoadPicture.InvokeIfRequired(c =>
												{
													c.PerformStep();
												});
												i++;
												tryCounter = 0;
											}
											/*else
											{
												tryCounter++;
											}*/
										}
										else
										{
											tryCounter++;
										}
									}
									else
									{
										tryCounter++;
									}
								}
								else
								{
									tryCounter++;
								}
							}
							catch
							{
								tryCounter++;
							}

						}
						//if(deviceTemp2.devicePicture.pictureCRC == crc32Object2.calculateCRC32(pictureParts))
						{
							Base64 base64 = new Base64();
							//pictureParts = base64.Base64Decoding(pictureParts, Encoding.Default);
							HexCode hex = new HexCode();
							pictureParts = hex.HexStringToByteString(pictureParts, Encoding.Default);
							//byte[] bytes = hex.StringToByteArray(pictureParts);
							//Image image = this.LoadImageFromBase64String(pictureParts);
							ImageLoad imageLoader = new ImageLoad();
							Image image = imageLoader.LoadImageFromBase64String(pictureParts);
							if(image != null)
							{
								this._formMain.DeviceLastImages[this._deviceObject.deviceSerialNumber] = image;
								//this.pbLastPicture. = image;

								this.pbLastPicture.InvokeIfRequired(c =>
								{
									c.Image = image;
								});
							}
							else
							{
								errorMessage = "Could not load new picture";
							}
						}
						/*else
						{
							errorMessage = "Could not load new picture";
						}*/
					}
					else
					{
						errorMessage = "Could not get new picture";
					}
				}
			}
			catch
			{
				errorMessage = "Could not get new picture";
			}

			if(errorMessage != "")
			{
				MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				MessageBox.Show("Device picture updated successfully", "Update Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			this._inProgress = false;
			this.btnGetPicture.InvokeIfRequired(c =>
			{
				c.Enabled = !this._inProgress;
			});
		}

		private void timerStopWatch_Tick(object sender, EventArgs e)
		{
			if(this._inProgress == true)
			{
				TimeSpan ts = _stopWatch.Elapsed;

				lblTimer.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
			}
			else
			{
				_stopWatch.Stop();
				timerStopWatch.Stop();
				timerStopWatch.Enabled = this._inProgress;
			}
		}
	}
}
