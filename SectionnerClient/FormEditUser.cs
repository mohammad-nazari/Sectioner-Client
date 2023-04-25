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
    public partial class FormEditUser : Form
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
        private GetUserDevices _getUsersDevicesInfo = new GetUserDevices();
        private UpdateUser _updateCurrentUser = new UpdateUser();
        private AddUser _addNewUser = new AddUser();
        private User _userInfo = new User();
        private UserDevice _userDevicesInfo = new UserDevice();
        private User _currentUserInfo = new User();
        private UserDevice _allDeviceList = new UserDevice();
        private AssignDeviceToUser _assignDeviceToThisUser = new AssignDeviceToUser();
        private DeleteDeviceFromUser _deleteDeviceFromThisUser = new DeleteDeviceFromUser();
        private ErrorCode _result = new ErrorCode();
        private bool _editUser = true; // False: new user

        public WebServiceLib.DeleteDeviceFromUser DeleteDeviceFromThisUser
        {
            get
            {
                return _deleteDeviceFromThisUser;
            }
            set
            {
                _deleteDeviceFromThisUser = value;
            }
        }

        public bool EditUser
        {
            get
            {
                return _editUser;
            }
            set
            {
                _editUser = value;
            }
        }

        public WebServiceLib.UserDevice AllDeviceList
        {
            get
            {
                return _allDeviceList;
            }
            set
            {
                _allDeviceList = value;
            }
        }

        public User CurrentUserInfo
        {
            get
            {
                return _currentUserInfo;
            }
            set
            {
                _currentUserInfo = value;
            }
        }

        public UserDevice UserDevicesInfo
        {
            get
            {
                return _userDevicesInfo;
            }
            set
            {
                _userDevicesInfo = value;
            }
        }

        public GetUserDevices GetUsersDevicesInfo
        {
            get
            {
                return _getUsersDevicesInfo;
            }
            set
            {
                _getUsersDevicesInfo = value;
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

        public FormEditUser()
        {
            InitializeComponent();
        }

        private void FormEditUser_Load(object sender, EventArgs e)
        {
            if (this._editUser == true)
            {
                this.GetUserDevices();
                this.UpdateDeviceList();

                this.txtUserName.Enabled = false;
                this.txtUserName.Text = this._currentUserInfo.userName;
                this.txtUserFirstName.Text = this._currentUserInfo.userFirstName;
                this.txtUserLastName.Text = this._currentUserInfo.userLastName;
                this.txtUserPassWord.Text = this._currentUserInfo.userPassword;
                this.txtUserPassWordR.Text = this._currentUserInfo.userNewPassword;
                this.cbUserType.SelectedIndex = cbUserType.Items.IndexOf(this._currentUserInfo.userType.ToString());
            }
            else
            {
                this.cbUserType.SelectedIndex = 2;
            }

            this.FillGridView();
        }

        private void GetUserDevices()
        {
            // Initialize data
            this._userDevicesInfo = new UserDevice();
            this._userDevicesInfo.userDeviceDevices = new Device[] { };
            this._userDevicesInfo.userDeviceUser = new User();

            this._getUsersDevicesInfo.requestUser = this._userInfo;
            this._getUsersDevicesInfo.requestSelectedUser = this._currentUserInfo;
            try
            {
                this._userDevicesInfo = this.SectionnerObject.GetUserDevices(this._getUsersDevicesInfo);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error on get user device list: " + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDeviceList()
        {
            if (this._allDeviceList != null)
            {
                if (this._allDeviceList.userDeviceDevices != null)
                {
                    if (this._allDeviceList.userDeviceDevices != null)
                    {
                        if (this._allDeviceList.userDeviceDevices.Count() > 0)
                        {
                            if (this._userDevicesInfo != null)
                            {
                                if (this._userDevicesInfo.userDeviceDevices != null)
                                {
                                    if (this._userDevicesInfo.userDeviceDevices != null)
                                    {
                                        if (this._userDevicesInfo.userDeviceDevices.Count() > 0)
                                        {
                                            for (int j = 0; j < this._userDevicesInfo.userDeviceDevices.Count(); j++)
                                            {
                                                for (int i = 0; i < this._allDeviceList.userDeviceDevices.Count(); i++)
                                                {
                                                    if (this._allDeviceList.userDeviceDevices[i].deviceSerialNumber == this._userDevicesInfo.userDeviceDevices[j].deviceSerialNumber)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
            else
            {
            }
        }

        private void FillGridView()
        {
            // Empty DataGrid
            this.dgvUserDevices.Rows.Clear();
            if (this._allDeviceList.userDeviceError.errorMessage == "")
            {
                if (this._allDeviceList != null)
                {
                    if (this._allDeviceList.userDeviceDevices != null)
                    {
                        if (this._allDeviceList.userDeviceDevices != null)
                        {
                            if (this._allDeviceList.userDeviceDevices.Count() > 0)
                            {
                                Base64 b64 = new Base64();
                                int rowIndex = 0;
                                foreach (Device device in this._allDeviceList.userDeviceDevices)
                                {
                                    dgvUserDevices.Rows.Add();
                                    rowIndex = dgvUserDevices.Rows.Count - 1;
                                    
                                    dgvUserDevices.Rows[rowIndex].Cells["DeviceID"].Value = device.deviceSerialNumber;
                                    dgvUserDevices.Rows[rowIndex].Cells["DeviceName"].Value = b64.Base64Decoding(device.deviceNikeName, Encoding.UTF8);
                                    dgvUserDevices.Rows[rowIndex].Cells["DeviceCity"].Value = b64.Base64Decoding(device.deviceCity, Encoding.UTF8);
                                    dgvUserDevices.Rows[rowIndex].Cells["DeviceLocation"].Value = b64.Base64Decoding(device.deviceLocation, Encoding.UTF8);
                                    //dgvUserDevices.Rows[rowIndex].Cells["DeviceView"].Value = device.deviceRulesView;
                                    //dgvUserDevices.Rows[rowIndex].Cells["DeviceUpdate"].Value = device.deviceRulesUpdate;
                                    //dgvUserDevices.Rows[rowIndex].Cells["DeviceDelete"].Value = device.deviceRulesDelete;
                                }
                            }
                            else
                            {
                                MessageBox.Show(Constants.NODEVICE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(Constants.NODEVICE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Constants.NODEVICE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(Constants.NODEVICE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error on get user device list: " + Environment.NewLine + this._userDevicesInfo.userDeviceError.errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.CheckInputData() == true)
            {
                if (this._editUser == true)
                {
                    this._updateCurrentUser.requestUser = this._userInfo;
                    this._updateCurrentUser.requestNewUser = new User();

                    this._updateCurrentUser.requestNewUser.userFirstName = this.txtUserFirstName.Text;
                    this._updateCurrentUser.requestNewUser.userLastName = this.txtUserLastName.Text;
                    this._updateCurrentUser.requestNewUser.userName = this.txtUserName.Text;
                    this._updateCurrentUser.requestNewUser.userPassword = MyMD5.ToMD5(this.txtUserPassWord.Text);
                    this._updateCurrentUser.requestNewUser.userNewPassword = MyMD5.ToMD5(this.txtUserPassWordR.Text);
                    this._updateCurrentUser.requestNewUser.userType = MyEnum.ParseEnum<UserType>(this.cbUserType.Items[this.cbUserType.SelectedIndex].ToString());
                }
                else
                {

                    this._addNewUser.requestUser = this._userInfo;
                    this._addNewUser.requestNewUser = new User();

                    this._addNewUser.requestNewUser.userFirstName = this.txtUserFirstName.Text;
                    this._addNewUser.requestNewUser.userLastName = this.txtUserLastName.Text;
                    this._addNewUser.requestNewUser.userName = this.txtUserName.Text;
                    this._addNewUser.requestNewUser.userPassword = MyMD5.ToMD5(this.txtUserPassWord.Text);
                    this._addNewUser.requestNewUser.userNewPassword = MyMD5.ToMD5(this.txtUserPassWordR.Text);
                    this._addNewUser.requestNewUser.userType = MyEnum.ParseEnum<UserType>(this.cbUserType.Items[this.cbUserType.SelectedIndex].ToString());
                }

                Device deviceTemp = new Device();
                List<Device> deviceList = new List<Device>();
                List<Device> userDeviceList = new List<Device>();

                if (this._editUser == true)
                {
                    /*
					 * clone from user devices 
					 * it is default to delete all user devices
					 * if device selected again 
					 * device deleted from this list
					 */
                    if (this._userDevicesInfo != null)
                    {
                        if (this._userDevicesInfo.userDeviceDevices != null)
                        {
                            if (this._userDevicesInfo.userDeviceDevices != null)
                            {
                                userDeviceList = this._userDevicesInfo.userDeviceDevices.OfType<Device>().ToList();
                            }
                        }
                    }

                    /*
					 * Check list of user devices 
					 * if is new fill the new device list
					 * and add them to user device list in database table
					 */
                    bool isNew = true;
                    foreach (DataGridViewRow row in dgvUserDevices.Rows)
                    {
                        if ((bool)(row.Cells["DeviceSelect"].Value) == true)
                        {
                            isNew = true;
                            for (int i = 0; i < userDeviceList.Count(); i++)
                            {
                                if (Convert.ToInt32(row.Cells["DeviceID"].Value.ToString()) == userDeviceList[i].deviceSerialNumber)
                                {
                                    isNew = false;
                                    // Delete from deleted devices
                                    userDeviceList.RemoveAt(i);
                                    break;
                                }
                            }
                            if (isNew == true)
                            {
                                deviceTemp = new Device();
                                deviceTemp = new Device();
                                deviceTemp.deviceSerialNumber = Convert.ToInt32(row.Cells["DeviceID"].Value.ToString());
                                deviceTemp.deviceNikeName = row.Cells["DeviceName"].Value.ToString();
                                deviceTemp.deviceCity = row.Cells["DeviceCity"].Value.ToString();
                                deviceTemp.deviceLocation = row.Cells["DeviceLocation"].Value.ToString();

                                deviceList.Add(deviceTemp);
                            }
                        }
                    }
                }
                else
                {
                    /*
					 * Check list of user devices 
					 * if is new fill the new device list
					 * and add them to user device list in database table
					 */
                    foreach (DataGridViewRow row in dgvUserDevices.Rows)
                    {
                        if ((bool)(row.Cells["DeviceSelect"].Value) == true)
                        {
                            deviceTemp = new Device();
                            deviceTemp.deviceSerialNumber = Convert.ToInt32(row.Cells["DeviceID"].Value.ToString());
                            deviceTemp.deviceNikeName = row.Cells["DeviceName"].Value.ToString();
                            deviceTemp.deviceCity = row.Cells["DeviceCity"].Value.ToString();
                            deviceTemp.deviceLocation = row.Cells["DeviceLocation"].Value.ToString();

                            deviceList.Add(deviceTemp);
                        }
                    }
                }

                // Edit exist user
                if (this._editUser == true)
                {
                    this.UpdateUserInformatons();
                }
                else
                {
                    // It is new user
                    this.AddNewUser();
                }

                if (deviceList.Count > 0)
                {
                    // Initialize assigned device to user object
                    this._assignDeviceToThisUser.requestUserDevice = new UserDevice();
                    this._assignDeviceToThisUser.requestUserDevice.userDeviceUser = new User();

                    this._assignDeviceToThisUser.requestUserDevice.userDeviceUser.userFirstName = this.txtUserFirstName.Text;
                    this._assignDeviceToThisUser.requestUserDevice.userDeviceUser.userLastName = this.txtUserLastName.Text;
                    this._assignDeviceToThisUser.requestUserDevice.userDeviceUser.userName = this.txtUserName.Text;

                    this._assignDeviceToThisUser.requestUserDevice.userDeviceDevices = new Device[] { };

                    this._assignDeviceToThisUser.requestUserDevice.userDeviceDevices = deviceList.ToArray();
                    this.AssignDeviceToUser();
                }
                if (userDeviceList.Count > 0)
                {
                    // Initialize delete device from user object
                    this._deleteDeviceFromThisUser.requestUserDevice = new UserDevice();
                    this._deleteDeviceFromThisUser.requestUserDevice.userDeviceUser = new User();

                    this._deleteDeviceFromThisUser.requestUserDevice.userDeviceUser.userFirstName = this.txtUserFirstName.Text;
                    this._deleteDeviceFromThisUser.requestUserDevice.userDeviceUser.userLastName = this.txtUserLastName.Text;
                    this._deleteDeviceFromThisUser.requestUserDevice.userDeviceUser.userName = this.txtUserName.Text;

                    this._deleteDeviceFromThisUser.requestUserDevice.userDeviceDevices = new Device[] { };

                    this._deleteDeviceFromThisUser.requestUserDevice.userDeviceDevices = userDeviceList.ToArray();
                    this.DeleteDeviceFromUser();
                }

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
        }

        private void UpdateUserInformatons()
        {
            this._updateCurrentUser.requestUser = this._userInfo;
            // Initialize data
            try
            {
                this._result = this.SectionnerObject.UpdateUser(this._updateCurrentUser);
            }
            catch (System.Exception ex)
            {
                this._result = new ErrorCode();
                this._result.errorMessage = ex.Message;
            }

            if (this._result == null)
            {
                MessageBox.Show(Constants.UPDATEUSER, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (this._result.errorMessage == null)
                {
                    MessageBox.Show(Constants.UPDATEUSER, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (this._result.errorMessage == "")
                    {
                        MessageBox.Show(Constants.UPDATEUSER, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error on update user informations: (" + this._result.errorNumber + ") " + Environment.NewLine + this._result.errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool CheckInputData()
        {
            string strErrors = "";
            bool firstMessage = true;

            // Check user password and re password
            if (txtUserPassWordR.Text != txtUserPassWord.Text)
            {
                if (!firstMessage)
                {
                    strErrors += Environment.NewLine;
                }
                else
                {
                    firstMessage = false;
                }
                strErrors += "Password is not match.";
            }

            if (this.txtUserFirstName.Text == "")
            {
                if (!firstMessage)
                {
                    strErrors += Environment.NewLine;
                }
                else
                {
                    firstMessage = false;
                }
                strErrors += "Input first name";
            }

            if (this.txtUserLastName.Text == "")
            {
                if (!firstMessage)
                {
                    strErrors += Environment.NewLine;
                }
                else
                {
                    firstMessage = false;
                }
                strErrors += "Input last name";
            }

            if (this.txtUserPassWord.Text == "")
            {
                if (!firstMessage)
                {
                    strErrors += Environment.NewLine;
                }
                else
                {
                    firstMessage = false;
                }
                strErrors += "Input password";
            }

            if (this.txtUserPassWordR.Text == "")
            {
                if (!firstMessage)
                {
                    strErrors += Environment.NewLine;
                }
                else
                {
                    firstMessage = false;
                }
                strErrors += "Input password";
            }

            if (this.cbUserType.SelectedIndex < 0)
            {
                if (!firstMessage)
                {
                    strErrors += Environment.NewLine;
                }
                else
                {
                    firstMessage = false;
                }
                strErrors += "Select user type";
            }

            if (strErrors != "")
            {
                MessageBox.Show("Please correct follow errors: " + Environment.NewLine + strErrors, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void AddNewUser()
        {
            this._addNewUser.requestUser = this._userInfo;
            // Initialize data
            try
            {
                this._result = this.SectionnerObject.AddUser(this._addNewUser);
            }
            catch (System.Exception ex)
            {
                this._result = new ErrorCode();
                this._result.errorMessage = ex.Message;
            }

            if (this._result == null)
            {
                MessageBox.Show(Constants.ADDUSER, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (this._result.errorMessage == null)
                {
                    MessageBox.Show(Constants.ADDUSER, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (this._result.errorMessage == "")
                    {
                        MessageBox.Show(Constants.ADDUSER, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error on add new user: (" + this._result.errorNumber + ") " + Environment.NewLine + this._result.errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dgvUserDevices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvUserDevices.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvUserDevices_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //dgvUserDevices.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvUserDevices_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //dgvUserDevices.CommitEdit(DataGridViewDataErrorContexts.Commit);
            // 			if(e.ColumnIndex == 0)
            // 			{
            // 				dgvUserDevices.Rows[e.RowIndex].Cells["DeviceView"].Value = dgvUserDevices.Rows[e.RowIndex].Cells["DeviceSelect"].Value;
            // 				dgvUserDevices.Rows[e.RowIndex].Cells["DeviceUpdate"].Value = dgvUserDevices.Rows[e.RowIndex].Cells["DeviceSelect"].Value;
            // 				dgvUserDevices.Rows[e.RowIndex].Cells["DeviceDelete"].Value = dgvUserDevices.Rows[e.RowIndex].Cells["DeviceSelect"].Value;
            // 			}
        }

        private void dgvUserDevices_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void AssignDeviceToUser()
        {
            // Initialize data
            this._assignDeviceToThisUser.requestUser = this._userInfo;
            try
            {
                this._result = this.SectionnerObject.AssignDeviceToUser(this._assignDeviceToThisUser);
            }
            catch (System.Exception ex)
            {
                this._result = new ErrorCode();
                this._result.errorMessage = ex.Message;
            }

            if (this._result == null)
            {
                MessageBox.Show(Constants.ASSISGNEDEVICETOUSER, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (this._result.errorMessage == null)
                {
                    MessageBox.Show(Constants.ASSISGNEDEVICETOUSER, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (this._result.errorMessage == "")
                    {
                        MessageBox.Show(Constants.ASSISGNEDEVICETOUSER, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error on assign device to user: (" + this._result.errorNumber + ") " + Environment.NewLine + this._result.errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void DeleteDeviceFromUser()
        {
            // Initialize data
            this._deleteDeviceFromThisUser.requestUser = this._userInfo;
            try
            {
                this._result = this.SectionnerObject.DeleteDeviceFromUser(this._deleteDeviceFromThisUser);
            }
            catch (System.Exception ex)
            {
                this._result = new ErrorCode();
                this._result.errorMessage = ex.Message;
            }

            if (this._result == null)
            {
                MessageBox.Show(Constants.DELETEEVICEFROMUSER, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (this._result.errorMessage == null)
                {
                    MessageBox.Show(Constants.DELETEEVICEFROMUSER, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (this._result.errorMessage == "")
                    {
                        MessageBox.Show(Constants.DELETEEVICEFROMUSER, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error on delete device from user: (" + this._result.errorNumber + ") " + Environment.NewLine + this._result.errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
