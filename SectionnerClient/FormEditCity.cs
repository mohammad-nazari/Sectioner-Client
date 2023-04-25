using SecurityLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebServiceLib;

namespace SectionnerClient
{

	public partial class FormEditCity:Form
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
		private User _userInfo = new User();
		
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

		public FormEditCity()
		{
			InitializeComponent();
		}

		private void FormEditCity_Load(object sender, EventArgs e)
		{
			this.rbNewDeviceName.Checked = true;
			this.txtNewCityName.Enabled = true;
			this.cbCityList.Enabled = false;


		}

		private void rbChangeName_CheckedChanged(object sender, EventArgs e)
		{
			this.txtNewCityName.Enabled = this.rbNewDeviceName.Checked;
			this.cbCityList.Enabled = !this.rbNewDeviceName.Checked;
		}

		private void rbExistNames_CheckedChanged(object sender, EventArgs e)
		{
			this.txtNewCityName.Enabled = !this.rbCityList.Checked;
			this.cbCityList.Enabled = this.rbCityList.Checked;
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			ChangeCityName changeCityNameObject = new ChangeCityName();

			changeCityNameObject.requestUser = this._userInfo;
			changeCityNameObject.newCityName = "";

			Base64 b64 = new Base64();

			changeCityNameObject.oldCityName = b64.Base64Encoding(this.lblCurrentCityName.Text, Encoding.UTF8);
			if(this.rbNewDeviceName.Checked)
			{
				if(this.txtNewCityName.Text != "")
				{
					changeCityNameObject.newCityName = this.txtNewCityName.Text;
				}
				else
				{
					MessageBox.Show("Please input new name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				if(this.cbCityList.SelectedIndex > -1)
				{
					changeCityNameObject.newCityName = this.cbCityList.Items[this.cbCityList.SelectedIndex].ToString();
				}
				else
				{
					MessageBox.Show("No item selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			if(changeCityNameObject.newCityName != "")
			{
				changeCityNameObject.newCityName = b64.Base64Encoding(changeCityNameObject.newCityName, Encoding.UTF8);
				ErrorCode result = new ErrorCode();

				try
				{
					result = this._sectionnerObject.ChangeCityName(changeCityNameObject);

					if(result != null)
					{
						if(result.errorMessage != null)
						{
							if(result.errorMessage == "")
							{
								MessageBox.Show("City name was changed successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							else
							{
								MessageBox.Show(result.errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
						else
						{
							MessageBox.Show("City name was changed successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					}
					else
					{
						MessageBox.Show("City name was changed successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				catch(System.Exception ex)
				{
					MessageBox.Show("Could not update city name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				this.Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
