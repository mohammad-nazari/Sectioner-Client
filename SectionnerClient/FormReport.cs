using ImageLib;
using SettingsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebServiceLib;
using System.IO;
using System.Xml.Serialization;

namespace SectionnerClient
{
	public partial class FormReport : Form
	{
		// Generate WebService objects
		private Sectionner _sectionnerObject = new Sectionner();
		GetCityAndLocations _cities = new GetCityAndLocations();
		AllCityLocatoins _citiesList = new AllCityLocatoins();
		private GetUserDeviceList _deviceListObject = new GetUserDeviceList();
		private DeviceStatus _deviceStatusInfo = new DeviceStatus();
		private DeviceStatusLogList _deviceStatusLogList = new DeviceStatusLogList();
		private GetDeviceStatusLog _getDeviceStatusLog = new GetDeviceStatusLog();

		private GetAllDeviceStatus _allDeviceStatus = new GetAllDeviceStatus();
		private User _userInfo = new User();

		ToolsLib.Tools _toolsObject = new ToolsLib.Tools();

		// Lock when get device list and when search in device list
		private object _userDeviceListLocker = new object();

		FormDeviceStatus _formDeviceStatus = new FormDeviceStatus();
		FormDeviceSetting _formDeviceSetting = new FormDeviceSetting();

		// Save PictureBox information for each device by device serial number
		Dictionary<int,DevicePicture> _pictureBoxList = new Dictionary<int, DevicePicture>();
		// Initialize all filtered images
		Dictionary<Color, ImageHandler> _imageHandlerList = new Dictionary<Color, ImageHandler>();

		private string _errors;
		private int _locationW = 5;
		private int _locationH = 5;

		private Settings _settingObject = new Settings();

		private DataGridViewCellEventArgs _mouseLocation;

		private TreeNode _treeNode;

		public WebServiceLib.Sectionner SectionnerObject
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

		public object UserDeviceListLocker
		{
			get
			{
				return _userDeviceListLocker;
			}
			set
			{
				_userDeviceListLocker = value;
			}
		}

		public Dictionary<Color, ImageHandler> ImageHandlerList
		{
			get
			{
				return _imageHandlerList;
			}
			set
			{
				_imageHandlerList = value;
			}
		}

		public WebServiceLib.AllCityLocatoins CitiesList
		{
			get
			{
				return _citiesList;
			}
			set
			{
				_citiesList = value;
			}
		}

		public SectionnerClient.FormDeviceSetting FormDeviceSetting
		{
			get
			{
				return _formDeviceSetting;
			}
			set
			{
				_formDeviceSetting = value;
			}
		}

		public System.Windows.Forms.TreeNode TreeNode
		{
			get
			{
				return _treeNode;
			}
			set
			{
				_treeNode = value;
			}
		}
		public System.Windows.Forms.DataGridViewCellEventArgs MouseLocation
		{
			get
			{
				return _mouseLocation;
			}
			set
			{
				_mouseLocation = value;
			}
		}


		public SettingsLib.Settings SettingObject
		{
			get
			{
				return _settingObject;
			}
			set
			{
				_settingObject = value;
			}
		}
		// Lists
		// List of devices info
		private UserDevice _userDeviceList = new UserDevice();
		private UserDevice _userDeviceListBackUp = new UserDevice();

		public User UserInfo
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

		public int LocationH1
		{
			get
			{
				return _locationH;
			}
			set
			{
				_locationH = value;
			}
		}

		public int LocationW1
		{
			get
			{
				return _locationW;
			}
			set
			{
				_locationW = value;
			}
		}

		public UserDevice UserDeviceList
		{
			get
			{
				return _userDeviceList;
			}
			set
			{
				_userDeviceList = value;
			}
		}

		public UserDevice DeviceListBackUp
		{
			get
			{
				return _userDeviceListBackUp;
			}
			set
			{
				_userDeviceListBackUp = value;
			}
		}

		public string Errors
		{
			get
			{
				return _errors;
			}
			set
			{
				_errors = value;
			}
		}

		public FormReport()
		{
			InitializeComponent();
		}

		private void FormReport_Load(object sender, EventArgs e)
		{
			this.ManageDevices();
		}

		/// <summary>
		/// Start GetDeviceList
		/// check list of device thread
		/// add or remove threads
		/// It run in a thread
		/// </summary>
		/// <returns></returns>
		private void ManageDevices()
		{
			if(this._userDeviceList.userDeviceError == null)
			{
				this._userDeviceList.userDeviceError = new ErrorCode();
			}
			if(this._userDeviceList.userDeviceDevices == null)
			{
				this._userDeviceList.userDeviceDevices = new Device[]{};
			}
			if(this._userDeviceList.userDeviceDevices == null)
			{
				this._userDeviceList.userDeviceDevices = new Device[] { };
			}
			else if(this._userDeviceList.userDeviceDevices.Count() > 0)
			{
				this._userDeviceList.userDeviceDevices = new Device[] { };
			}

			this.GetAllDeviceList();
			if(this._userDeviceList.userDeviceError == null)
			{
				this._userDeviceList.userDeviceError = new ErrorCode();
			}

			if(this._userDeviceList.userDeviceDevices == null)
			{
				this._userDeviceList.userDeviceDevices = new Device[]{};
			}
			if(this._userDeviceList.userDeviceDevices == null)
			{
				this._userDeviceList.userDeviceDevices = new Device[] { };
			}

			// Some errors occurred
			if(this._userDeviceList.userDeviceError.errorMessage != "")
			{
				MessageBox.Show(this, this._userDeviceList.userDeviceError.errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if(this._userDeviceList.userDeviceDevices.Length > 0)
			{
				// Add new devices
				this.AddDevicesList();
			}
			else
			{
				MessageBox.Show(this, @"Error(5160): No device found for this user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			this.cbSensor.SelectedIndex = 0;
		}

		private void GetAllDeviceList()
		{
			// Init to
			this._userDeviceList.userDeviceError = new ErrorCode();

			if(this._userDeviceList.userDeviceDevices == null)
			{
				this._userDeviceList.userDeviceDevices = new Device[]{};
			}
			if(this._userDeviceList.userDeviceDevices == null)
			{
				this._userDeviceList.userDeviceDevices = new Device[] { };
			}
			else if(this._userDeviceList.userDeviceDevices.Count() > 0)
			{
				this._userDeviceList.userDeviceDevices = new Device[] { };
			}

			try
			{
				this._allDeviceStatus.requestUser = this._userInfo;
				// Send request for login web service to server
				this._userDeviceList = this._sectionnerObject.GetAllDeviceStatus(this._allDeviceStatus);
				if(this._userDeviceList.userDeviceDevices != null)
				{
					foreach(Device deviceObject in this._userDeviceList.userDeviceDevices)
					{
						this._toolsObject.DeviceInfoFromBase64(deviceObject);
					}
				}
			}
			catch(Exception)
			{
				this._userDeviceList.userDeviceError.errorMessage = "Could not get user device list";
				this._userDeviceList.userDeviceError.errorNumber = 5000;

				if(this._userDeviceList.userDeviceDevices == null)
				{
					this._userDeviceList.userDeviceDevices = new Device[]{};
				}
				if(this._userDeviceList.userDeviceDevices == null)
				{
					this._userDeviceList.userDeviceDevices = new Device[] { };
				}
				else if(this._userDeviceList.userDeviceDevices.Count() > 0)
				{
					this._userDeviceList.userDeviceDevices = new Device[] { };
				}
			}
		}

		/// <summary>
		/// Get device id from new device list
		/// and search it in device thread list
		/// and add to list and generate its thread
		/// </summary>
		/// <returns></returns>
		private void AddDevicesList()
		{
			foreach(Device device in this._userDeviceList.userDeviceDevices)
			{
				this.CreateDeviceTreeViewNode(device);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="deviceInfo"></param>
		/// <param name=""></param>
		/// <returns></returns>
		private void CreateDeviceTreeViewNode(Device deviceInfo)
		{
			if(!this.tvDevices.Nodes.Find(deviceInfo.deviceCity, false).Any())
			{
				// Device city node not exist
				this.tvDevices.InvokeIfRequired(c =>
				{
					c.Nodes.Add(deviceInfo.deviceCity, deviceInfo.deviceCity);
				});
				// Set tag for first level nodes to "City"
				this.tvDevices.InvokeIfRequired(c =>
				{
					c.Nodes[deviceInfo.deviceCity].Tag = "City";
				});

				// Device location node not exist
				this.tvDevices.InvokeIfRequired(
					c =>
					{
						c.Nodes[deviceInfo.deviceCity].Nodes.Add(deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation, deviceInfo.deviceLocation);
						// Set tag for second level nodes to "Location"
						c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Tag = "Location";
					});
				// Device name node not exist
				this.tvDevices.InvokeIfRequired(
					c =>
					{
						c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes.Add(Convert.ToString(deviceInfo.deviceSerialNumber), deviceInfo.deviceNikeName + "(" + deviceInfo.deviceSerialNumber + ")");
						// Set tag for third level nodes to "Device"
						c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes[Convert.ToString(deviceInfo.deviceSerialNumber)].Tag = "Device";
					});
			}
			else if(!this.tvDevices.Nodes[deviceInfo.deviceCity].Nodes.Find(deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation, false).Any())
			{
				// Device location node not exist
				this.tvDevices.InvokeIfRequired(
					c =>
					{
						c.Nodes[deviceInfo.deviceCity].Nodes.Add(deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation, deviceInfo.deviceLocation);
						// Set tag for second level nodes to "Location"
						c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Tag = "Location";
					});
				// Device name node not exist
				this.tvDevices.InvokeIfRequired(
					c =>
					{
						c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes.Add(Convert.ToString(deviceInfo.deviceSerialNumber), deviceInfo.deviceNikeName + "(" + deviceInfo.deviceSerialNumber + ")");
						// Set tag for third level nodes to "Device"
						c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes[Convert.ToString(deviceInfo.deviceSerialNumber)].Tag = "Device";
					});
			}
			else if(!this.tvDevices.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes.Find(Convert.ToString(deviceInfo.deviceSerialNumber), false).Any())
			{
				// Device name node not exist
				this.tvDevices.InvokeIfRequired(
					c =>
					{
						c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes.Add(Convert.ToString(deviceInfo.deviceSerialNumber), deviceInfo.deviceNikeName + "(" + deviceInfo.deviceSerialNumber + ")");
						// Set tag for third level nodes to "Device"
						c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes[Convert.ToString(deviceInfo.deviceSerialNumber)].Tag = "Device";
					});
			}
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			List<Device> deviceTemp = new List<Device>();
			if(this.tvDevices.Nodes.Count > 0)
			{
				// Get list of devices
				foreach(TreeNode nodeCity in this.tvDevices.Nodes)
				{
					// Is city node
					if((string)nodeCity.Tag == "City" && nodeCity.Checked && (nodeCity.StateImageIndex == 1 || nodeCity.StateImageIndex == 2))
					{
						// Have location
						if(nodeCity.Nodes.Count > 0)
						{
							foreach(TreeNode nodeLocation in nodeCity.Nodes)
							{
								// Have device
								if(nodeLocation.Nodes.Count > 0)
								{
									foreach(TreeNode nodeDevice in nodeLocation.Nodes)
									{
										if(nodeDevice.Checked)
										{
											Device device = new Device();
											device.deviceSerialNumber = Convert.ToInt32(nodeDevice.Name);
											deviceTemp.Add(device);
										}
									}
								}
							}
						}
					}
				}
			}

			// Device and error types selected
			if(deviceTemp.Count > 0)
			{
				try
				{
					// Send selected devices to server getting log status
					this._getDeviceStatusLog.requestUser = this._userInfo;
					this._getDeviceStatusLog.requestStatusLog = new DeviceStatus();
					this._getDeviceStatusLog.requestStatusLog.deviceStatusDevice = deviceTemp.ToArray();
					this._getDeviceStatusLog.requestStatusLog.deviceStatusRangeDate = new ReportRange();
					this._getDeviceStatusLog.requestStatusLog.deviceStatusRangeDate.reportStartDate = this.dtnStartDateTime.Value.Value.ToLocalTime();
					this._getDeviceStatusLog.requestStatusLog.deviceStatusRangeDate.reportEndDate = this.dtnEndDateTime.Value.Value.ToLocalTime();

					// Send request for login web service to server
					this._deviceStatusLogList = this._sectionnerObject.GetDeviceStatusLog(this._getDeviceStatusLog);

				}
				catch(Exception expn)
				{
					this._deviceStatusLogList.statusLogListError = new ErrorCode();
					this._deviceStatusLogList.statusLogListError.errorMessage = "Could not get user device list : " + expn.Message;
					this._deviceStatusLogList.statusLogListError.errorNumber = 5000;
				}

				this.dgvReports.Rows.Clear();
				this.lblRecordsCount.Text = "";
				if(this._deviceStatusLogList.statusLogListError.errorMessage == "")
				{
					// Check response to fill report data grid view
					// If device list is not empty
					if(this._deviceStatusLogList.statusLogListStatus != null)
					{
						if(this._deviceStatusLogList.statusLogListStatus.Count() > 0)
						{
							this.lblRecordsCount.Text = this._deviceStatusLogList.statusLogListStatus.Count().ToString() + " Records found";
							int rowIndex = 0;
							foreach(DeviceStatusLog statusLog in this._deviceStatusLogList.statusLogListStatus)
							{
								this._toolsObject.DeviceInfoFromBase64(statusLog.statusLogDevice);
								this.dgvReports.Rows.Add();
								rowIndex = this.dgvReports.Rows.Count - 1;
								this.dgvReports.Rows[rowIndex].Cells["DeviceID"].Value = statusLog.statusLogDevice.deviceSerialNumber;
								this.dgvReports.Rows[rowIndex].Cells["DeviceName"].Value = statusLog.statusLogDevice.deviceNikeName;
								this.dgvReports.Rows[rowIndex].Cells["DeviceCity"].Value = statusLog.statusLogDevice.deviceCity;
								this.dgvReports.Rows[rowIndex].Cells["DeviceLocation"].Value = statusLog.statusLogDevice.deviceLocation;
								this.dgvReports.Rows[rowIndex].Cells["Error"].Value = this.GetValue(statusLog.statusLogDevice, cbSensor.Text);
								this.dgvReports.Rows[rowIndex].Cells["DateTime"].Value = statusLog.statusLogDateTime.ToString();
							}
						}
						else
						{
							MessageBox.Show(this, "No report found", "Result Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					}
					else
					{
						MessageBox.Show(this, "No report found", "Result Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				else
				{
					MessageBox.Show(this, @"Error(" + this._deviceStatusLogList.statusLogListError.errorNumber + @"): " + this._deviceStatusLogList.statusLogListError.errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				string errorMessage = "";
				if(deviceTemp.Count < 1)
				{
					errorMessage += "Please Select at least 1 device" + Environment.NewLine;
				}

				MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ShowValues()
		{
			if(this._deviceStatusLogList.statusLogListStatus != null && this._deviceStatusLogList.statusLogListStatus.Length > 0)
			{
				int rowIndex = 0;
				foreach(DeviceStatusLog statusLog in this._deviceStatusLogList.statusLogListStatus)
				{
					this.dgvReports.Rows[rowIndex++].Cells["Error"].Value = this.GetValue(statusLog.statusLogDevice, cbSensor.Text);
				}
			}
		}

		private double GetValue(Device DeviceObject, string SensorNameStr)
		{
			if(DeviceObject.deviceSensors != null && DeviceObject.deviceSensors.Length > 0)
			{
				foreach(SensorExtension sensorEx in DeviceObject.deviceSensors)
				{
					foreach(Sensor sensor in sensorEx.deviceSensors)
					{
						if(sensor.sensorNikeName == SensorNameStr)
						{
							return sensor.sensorValue;
						}
					}
				}
			}
			else
			{
				return 0;
			}
			return 0;
		}

		private Device WriteXMLToDeviceObject(string InputString)
		{
			/*XmlSerializer serializer = new XmlSerializer(typeof(Device));
			MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(InputString));
			Device resultingMessage = (Device)serializer.Deserialize(memStream);*/

			XmlSerializer serializer = new XmlSerializer(typeof(Device));
			StringReader rdr = new StringReader(InputString);
			return (Device)serializer.Deserialize(rdr);
		}

		private void ToCsV(DataGridView dGV, string filename)
		{
			string stOutput = "";
			// Export titles:
			string sHeaders = "";

			for(int j = 0; j < dGV.Columns.Count; j++)
				sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
			stOutput += sHeaders + "\r\n";
			// Export data.
			for(int i = 0; i < dGV.RowCount - 1; i++)
			{
				string stLine = "";
				for(int j = 0; j < dGV.Rows[i].Cells.Count; j++)
					stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
				stOutput += stLine + "\r\n";
			}
			Encoding utf8 = Encoding.GetEncoding(1256);
			byte[] output = utf8.GetBytes(stOutput);
			FileStream fs = new FileStream(filename, FileMode.Create);
			BinaryWriter bw = new BinaryWriter(fs);
			bw.Write(output, 0, output.Length); //write the encoded file
			bw.Flush();
			bw.Close();
			fs.Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if(this.dgvReports.RowCount > 0)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "Excel Documents (*.xls)|*.xls";
				sfd.FileName = "export.xls";
				if(sfd.ShowDialog() == DialogResult.OK)
				{
					//ToCsV(dataGridView1, @"c:\export.xls");
					ToCsV(this.dgvReports, sfd.FileName); // Here dataGridview1 is your grid view name 
				}
			}
			else
			{
				MessageBox.Show(this, "No records exist to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void cbSensor_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ShowValues();
		}

		private void btnReport_Click(object sender, EventArgs e)
		{
			FormChart formChart = new FormChart()
			{
				Dgv = this.dgvReports
			};

			formChart.ShowDialog();
		}
	}
}
