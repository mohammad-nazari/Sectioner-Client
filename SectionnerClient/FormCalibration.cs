using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebServiceLib;

namespace SectionnerClient
{
	public partial class FormCalibration : Form
	{
		private Device _deviceObject = new Device();

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

		public FormCalibration(Device DeviceObject)
		{
			this._deviceObject = DeviceObject;
			InitializeComponent();
		}

		private void FormCalibration_Load(object sender, EventArgs e)
		{
			if(this._deviceObject.deviceSensors != null)
			{
				if(this._deviceObject.deviceSensors.Count() > 0)
				{
					int rowIndex = 0;
					foreach(SensorExtension sensor in this._deviceObject.deviceSensors)
					{
						if(sensor.sensorName == WebServiceLib.SensorName.TEMPERATURE || sensor.sensorName == WebServiceLib.SensorName.HUMIDITY || sensor.sensorName == WebServiceLib.SensorName.ACAMPERE || sensor.sensorName == WebServiceLib.SensorName.ACVOLTAGE || sensor.sensorName == WebServiceLib.SensorName.DCAMPERE || sensor.sensorName == WebServiceLib.SensorName.DCVOLTAGE || sensor.sensorName == WebServiceLib.SensorName.COSQ)
						{
							// Number of this type of sensor
							decimal sensorCounts = sensor.deviceSensors.Count();
							// Numbers of data grid rows to show this type of sensor values
							decimal sensorRows = Math.Ceiling(sensorCounts / 8);

							rowIndex = 0;
							for(int sensorRowIndex = 0; sensorRowIndex < sensorRows; sensorRowIndex++)
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
									c.Rows[rowIndex].Tag = sensor.sensorName.ToString();
									c.Rows[rowIndex].Cells["SensorName"].Value = rowIndex.ToString();
									c.Rows[rowIndex].Cells["SensorMin"].Value = sensor.deviceSensors[0].sensorMinimumValue.ToString();
									c.Rows[rowIndex].Cells["SensorMax"].Value = sensor.deviceSensors[0].sensorMaximumValue.ToString();
								});
							}
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
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
	}
}
