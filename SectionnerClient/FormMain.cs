using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SettingsLib;
using WebServiceLib;
using ToolsLib;
using ImageLib;
using SecurityLib;
using System.Collections;

namespace SectionnerClient
{
    public partial class FormMain : Form
    {
        // Generate WebService objects
        private Sectionner _sectionnerObject = new Sectionner();
        GetCityAndLocations _cities = new GetCityAndLocations();
        AllCityLocatoins _citiesList = new AllCityLocatoins();
        private GetUserDeviceList _deviceListObject = new GetUserDeviceList();

        private GetAllDeviceStatus _allDeviceStatus = new GetAllDeviceStatus();
        private User _userInfo = new User();
        private string _serverAddress = ""; string _serverDNSAddress = "";
        int _serverPort = 0;
        public string ServerDNSAddress
        {
            get { return _serverDNSAddress; }
            set { _serverDNSAddress = value; }
        }
        public int ServerPort
        {
            get { return _serverPort; }
            set { _serverPort = value; }
        }


        ToolsLib.Tools _toolsObject = new ToolsLib.Tools();

        // Lock when get device list and when search in device list
        private object _userDeviceListLocker = new object();

        FormDeviceStatus _formDeviceStatus = new FormDeviceStatus();
        FormDeviceSetting _formDeviceSetting = new FormDeviceSetting();

        // Save PictureBox information for each device by device serial number
        Dictionary<int, DevicePicture> _pictureBoxList = new Dictionary<int, DevicePicture>();
        // Initialize all filtered images
        Dictionary<Color, ImageHandler> _imageHandlerList = new Dictionary<Color, ImageHandler>();
        //Save last image of device
        Dictionary<int, Image> _deviceLastImages = new Dictionary<int, Image>();

        private string _errors;
        private int _locationW = 5;
        private int _locationH = 5;
        private bool _isLogin = false;

        private Settings _settingObject = new Settings();

        private DataGridViewCellEventArgs _mouseLocation;

        private TreeNode _treeNode;
        // Lists
        // List of devices info
        private UserDevice _userDeviceList = new UserDevice();
        private UserDevice _userDeviceListBackUp = new UserDevice();

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

        public Dictionary<int, Image> DeviceLastImages
        {
            get
            {
                return _deviceLastImages;
            }
            set
            {
                _deviceLastImages = value;
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

        public DataGridView DgvStatus
        {
            get
            {
                return dgvStatus;
            }
            set
            {
                dgvStatus = value;
            }
        }

        public TreeView TvDevices
        {
            get
            {
                return tvDevices;
            }
            set
            {
                tvDevices = value;
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

        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
            set
            {
                _isLogin = value;
            }
        }

        public FormMain()
        {
            this._errors = "";
            this._userDeviceList.userDeviceError = new ErrorCode();
            this._userDeviceList.userDeviceDevices = new Device[] { };
            this._userDeviceListBackUp = MyClone.DeepClone(this._userDeviceList);

            InitializeComponent();
        }

        /// <summary>
        /// Show login form and authenticate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Go to login window dialog
            this.DoUserLogin();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void DoUserLogin()
        {
            this._locationH = 0;
            this._locationW = 0;
            FormLogin frmLogin = new FormLogin();
            frmLogin.ShowDialog();

            // Determine if the OK button was clicked on the dialog box. 
            if (frmLogin.DialogResult == DialogResult.Cancel)
            {
                // Close Program Dialog Window
                this.Close();
            }
            else
            {
                // Generate filtered images
                this.GenerateFilteredImages();

                /* User is logged in
				 * and all thread run until
				 * user is logged in
				 */
                this._isLogin = true;

                // Get information from login form
                this._userInfo = frmLogin.UserInfo;
                this._serverDNSAddress = frmLogin.ServerDNSAddress;
                this._serverPort = frmLogin.ServerPort;

                // Initialize WebService objects
                this._serverAddress = "http://" + this._serverDNSAddress + ":" + this._serverPort.ToString();
                // Assign web service server address
                this._sectionnerObject.Url = this._serverAddress;
                // Web service timeout
                this._sectionnerObject.Timeout = Constants.Timeout;
                // GetDevice web service object
                this._deviceListObject.requestUser = this._userInfo;

                this._allDeviceStatus.requestUser = this._userInfo;

                this.statusStripPrimary.InvokeIfRequired(c =>
                {
                    c.Items[2].Text = "User Name : " + this._userInfo.userName;
                });
                this.statusStripPrimary.InvokeIfRequired(c =>
                {
                    c.Items[2].BackColor = Color.Green;
                });
                this.statusStripPrimary.InvokeIfRequired(c =>
                {
                    c.Items[2].ForeColor = Color.Black;
                });

                this.statusStripPrimary.InvokeIfRequired(c =>
                {
                    c.Items[3].Text = "User Type : " + this._userInfo.userType.ToString();
                });
                this.statusStripPrimary.InvokeIfRequired(c =>
                {
                    c.Items[3].BackColor = Color.Green;
                });
                this.statusStripPrimary.InvokeIfRequired(c =>
                {
                    c.Items[3].ForeColor = Color.Black;
                });

                // Start threads
                this.StartThreads();

                //Load settings
                this.LoadSettings();
            }
        }

        /// <summary>
        /// Initialize and generate
        /// a dictionary of all images we have
        /// </summary>
        /// <returns></returns>
        private void GenerateFilteredImages()
        {
            Color colorObject = Color.Green;
            Bitmap bitmapObject = new Bitmap(this.imgListDevice.Images["Device.png"]);
            this._imageHandlerList[colorObject] = new ImageHandler();
            this._imageHandlerList[colorObject].SetColorFilter(bitmapObject, colorObject);

            foreach (SettingsLib.SettingColors ColorObject in Enum.GetValues(typeof(SettingsLib.SettingColors)))
            {
                colorObject = Color.FromName(ColorObject.ToString());
                this._imageHandlerList[colorObject] = new ImageHandler();
                this._imageHandlerList[colorObject].SetColorFilter(bitmapObject, colorObject);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void StartThreads()
        {
            /* Thread for get device info
			 * and manage device threads
			 */
            Task.Factory.StartNew(this.ManageDevicesThread);
        }

        /// <summary>
        /// Start GetDeviceList
        /// check list of device thread
        /// add or remove threads
        /// It run in a thread
        /// </summary>
        /// <returns></returns>
        private void ManageDevicesThread()
        {
            if (this._userDeviceList.userDeviceError == null)
            {
                this._userDeviceList.userDeviceError = new ErrorCode();
            }
            if (this._userDeviceList.userDeviceDevices == null)
            {
                this._userDeviceList.userDeviceDevices = new Device[] { };
            }
            if (this._userDeviceList.userDeviceDevices == null)
            {
                this._userDeviceList.userDeviceDevices = new Device[] { };
            }
            else if (this._userDeviceList.userDeviceDevices.Count() > 0)
            {
                this._userDeviceList.userDeviceDevices = new Device[] { };
            }

            // It run until user is logged in
            while (this._isLogin)
            {
                // Get user device list from server
                Thread.Sleep(Constants.SleepTime); // Every some second

                this.GetAllDeviceList();
                if (this._userDeviceList.userDeviceError == null)
                {
                    this._userDeviceList.userDeviceError = new ErrorCode();
                }

                if (this._userDeviceList.userDeviceDevices == null)
                {
                    this._userDeviceList.userDeviceDevices = new Device[] { };
                }
                if (this._userDeviceList.userDeviceDevices == null)
                {
                    this._userDeviceList.userDeviceDevices = new Device[] { };
                }

                /*
				 * Refresh devices thread
				 * and remove and or add device thread
				 */
                if (this._isLogin)
                {
                    // Some errors occurred
                    if (this._userDeviceList.userDeviceError.errorMessage != "")
                    {
                        this.SetServerStatusBar(@"Error(" + this._userDeviceList.userDeviceError.errorNumber + "): " + this._userDeviceList.userDeviceError.errorMessage, Color.IndianRed, Color.Black);

                        // No device found for this user
                        // Remove all threads

                        if (this._isLogin)
                        {
                            this.RemoveAllDevicesThread();
                            this.statusStripPrimary.InvokeIfRequired(c =>
                            {
                                c.Items[1].Text = @"Number of Devices : 0";
                            });
                            if (this._userDeviceList.userDeviceDevices == null)
                            {
                                this._userDeviceList.userDeviceDevices = new Device[] { };
                            }
                            if (this._userDeviceList.userDeviceDevices == null)
                            {
                                this._userDeviceList.userDeviceDevices = new Device[] { };
                            }
                            else if (this._userDeviceList.userDeviceDevices.Count() > 0)
                            {
                                this._userDeviceList.userDeviceDevices = new Device[] { };
                            }
                        }
                    }
                    else if (this._userDeviceList.userDeviceDevices.Length > 0)
                    {
                        // List have any change
                        if (this._userDeviceListBackUp != this._userDeviceList)
                        {
                            if (this._isLogin)
                            {
                                // Remove old devices (deleted)
                                this.RemoveDevicesThread();
                                if (this._isLogin)
                                {
                                    // Add new devices
                                    this.AddDevicesThread();
                                    this.SetServerStatusBar(@"Connection to Server: Normal", Color.CadetBlue, Color.Black);
                                    this.statusStripPrimary.InvokeIfRequired(c =>
                                    {
                                        c.Items[1].Text = @"Number of Devices : " + this._userDeviceList.userDeviceDevices.Length;
                                    });

                                    // backUp
                                    this._userDeviceListBackUp = MyClone.DeepClone(this._userDeviceList);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this._isLogin)
                        {
                            this.SetServerStatusBar(@"Error(5160): No device found for this user", Color.IndianRed, Color.Black);
                            // No device found for this user
                            // Remove all threads
                            this.RemoveAllDevicesThread();
                            if (this._userDeviceList.userDeviceDevices == null)
                            {
                                this._userDeviceList.userDeviceDevices = new Device[] { };
                            }
                            if (this._userDeviceList.userDeviceDevices == null)
                            {
                                this._userDeviceList.userDeviceDevices = new Device[] { };
                            }
                            else if (this._userDeviceList.userDeviceDevices.Count() > 0)
                            {
                                this._userDeviceList.userDeviceDevices = new Device[] { };
                            }
                            this.statusStripPrimary.InvokeIfRequired(c =>
                            {
                                c.Items[1].Text = @"Number of Devices : 0";
                            });
                        }
                    }
                }
                else if (this._userDeviceList.userDeviceDevices.Length > 0)
                {
                    // List have any change
                    if (this._userDeviceListBackUp != this._userDeviceList)
                    {
                        if (this._isLogin)
                        {
                            // First remove old devices thread
                            this.RemoveDevicesThread();
                            if (this._isLogin)
                            {
                                this.AddDevicesThread();
                                this.SetServerStatusBar(@"Connection to Server: Normal", Color.CadetBlue, Color.Black);
                                this.statusStripPrimary.InvokeIfRequired(c =>
                                {
                                    c.Items[1].Text = @"Number of Devices : " + this._userDeviceList.userDeviceDevices.Length;
                                });

                                // backUp
                                this._userDeviceListBackUp = MyClone.DeepClone(this._userDeviceList);
                            }
                        }
                    }
                }
                else
                {
                    if (this._isLogin)
                    {
                        this.SetServerStatusBar(@"No device found for this user", Color.IndianRed, Color.Black);
                        if (this._userDeviceList.userDeviceDevices == null)
                        {
                            this._userDeviceList.userDeviceDevices = new Device[] { };
                        }
                        if (this._userDeviceList.userDeviceDevices == null)
                        {
                            this._userDeviceList.userDeviceDevices = new Device[] { };
                        }
                        else if (this._userDeviceList.userDeviceDevices.Count() > 0)
                        {
                            this._userDeviceList.userDeviceDevices = new Device[] { };
                        }

                        if (this._isLogin)
                        {
                            this.RemoveAllDevicesThread();
                            this.statusStripPrimary.InvokeIfRequired(c =>
                            {
                                c.Items[1].Text = @"Number of Devices : 0";
                            });
                        }
                    }
                }
            }   //End While
            if (this._userDeviceList.userDeviceDevices == null)
            {
                this._userDeviceList.userDeviceDevices = new Device[] { };

            }
            if (this._userDeviceList.userDeviceDevices == null)
            {
                this._userDeviceList.userDeviceDevices = new Device[] { };
            }
            else if (this._userDeviceList.userDeviceDevices.Count() > 0)
            {
                this._userDeviceList.userDeviceDevices = new Device[] { };
            }
            // Remove all threads
            this.RemoveAllDevicesThread();
            this.statusStripPrimary.InvokeIfRequired(c =>
            {
                c.Items[1].Text = @"Number of Devices : 0";
            });
        }

        /// <summary>
        /// It run every some minutes and
        /// get updated list of device assigned to this user
        /// </summary>
        /// <returns>
        /// Void
        /// Update Device List attribute
        /// </returns>
        private void GetDeviceList()
        {
            // Lock until get all device list
            lock (this._userDeviceListLocker)
            {
                // User is logged in
                if (this._isLogin)
                {
                    if (this._userDeviceList.userDeviceDevices == null)
                    {
                        this._userDeviceList.userDeviceDevices = new Device[] { };
                    }
                    if (this._userDeviceList.userDeviceDevices == null)
                    {
                        this._userDeviceList.userDeviceDevices = new Device[] { };
                    }
                    else if (this._userDeviceList.userDeviceDevices.Count() > 0)
                    {
                        this._userDeviceList.userDeviceDevices = new Device[] { };
                    }

                    try
                    {
                        if (this._isLogin)
                        {
                            // Send request for login web service to server
                            this._userDeviceList = this._sectionnerObject.GetUserDeviceList(this._deviceListObject);

                            if (this._isLogin)
                            {
                                foreach (Device deviceObject in this._userDeviceList.userDeviceDevices)
                                {
                                    if (this._isLogin)
                                    {
                                        this._toolsObject.DeviceInfoFromBase64(deviceObject);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        this._userDeviceList.userDeviceError.errorMessage = "Could not get user device list";
                        this._userDeviceList.userDeviceError.errorNumber = 5000;

                        if (this._userDeviceList.userDeviceDevices == null)
                        {
                            this._userDeviceList.userDeviceDevices = new Device[] { };
                        }
                        if (this._userDeviceList.userDeviceDevices == null)
                        {
                            this._userDeviceList.userDeviceDevices = new Device[] { };
                        }
                        else if (this._userDeviceList.userDeviceDevices.Count() > 0)
                        {
                            this._userDeviceList.userDeviceDevices = new Device[] { };
                        }
                    }
                }
            }
        }

        private void GetAllDeviceList()
        {
            // User is logged in
            if (this._isLogin)
            {
                // Init to
                this._userDeviceList.userDeviceError = new ErrorCode();

                if (this._userDeviceList.userDeviceDevices == null)
                {
                    this._userDeviceList.userDeviceDevices = new Device[] { };
                }
                if (this._userDeviceList.userDeviceDevices == null)
                {
                    this._userDeviceList.userDeviceDevices = new Device[] { };
                }
                else if (this._userDeviceList.userDeviceDevices.Count() > 0)
                {
                    this._userDeviceList.userDeviceDevices = new Device[] { };
                }
                this._allDeviceStatus.requestUser = this._userInfo;

                try
                {
                    if (this._isLogin)
                    {
                        // Send request for login web service to server
                        this._userDeviceList = this._sectionnerObject.GetAllDeviceStatus(this._allDeviceStatus);
                        if (this._userDeviceList.userDeviceDevices != null)
                        {
                            foreach (Device deviceObject in this._userDeviceList.userDeviceDevices)
                            {
                                if (this._isLogin)
                                {
                                    this._toolsObject.DeviceInfoFromBase64(deviceObject);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    if (this._userDeviceList.userDeviceError == null)
                    {
                        this._userDeviceList.userDeviceError = new ErrorCode();
                    }
                    this._userDeviceList.userDeviceError.errorMessage = "Could not get user device list";
                    this._userDeviceList.userDeviceError.errorNumber = 5000;

                    if (this._userDeviceList.userDeviceDevices == null)
                    {
                        this._userDeviceList.userDeviceDevices = new Device[] { };
                    }
                    if (this._userDeviceList.userDeviceDevices == null)
                    {
                        this._userDeviceList.userDeviceDevices = new Device[] { };
                    }
                    else if (this._userDeviceList.userDeviceDevices.Count() > 0)
                    {
                        this._userDeviceList.userDeviceDevices = new Device[] { };
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputData"></param>
        /// <param name="BackColor"></param>
        /// <param name="ForColor"></param>
        /// <returns></returns>
        private void SetServerStatusBar(string InputData, Color BackColor, Color ForColor)
        {
            this.statusStripPrimary.InvokeIfRequired(c =>
            {
                c.Items[0].Text = InputData;
            });
            this.statusStripPrimary.InvokeIfRequired(c =>
            {
                c.Items[0].BackColor = BackColor;
            });
            this.statusStripPrimary.InvokeIfRequired(c =>
            {
                c.Items[0].ForeColor = ForColor;
            });
        }

        /// <summary>
        /// Remove all threads
        /// </summary>
        /// <returns></returns>
        private void RemoveAllDevicesThread()
        {
            // Clear All objects
            this.pnlIcons.InvokeIfRequired(c =>
            {
                c.Controls.Clear();
            });
            this.tvDevices.InvokeIfRequired(c =>
            {
                c.Nodes.Clear();
            });
            this.dgvStatus.InvokeIfRequired(c =>
            {
                c.Rows.Clear();
            });
            this._pictureBoxList.Clear();
            this._userDeviceList.userDeviceError = new ErrorCode();
            this._userDeviceList.userDeviceDevices = new Device[] { };
            this._userDeviceList.userDeviceDevices = new Device[] { };

            this._userDeviceListBackUp.userDeviceError = new ErrorCode();
            this._userDeviceListBackUp.userDeviceDevices = new Device[] { };
            this._userDeviceListBackUp.userDeviceDevices = new Device[] { };

            this._locationH = 0;
            this._locationW = 0;
        }

        /// <summary>
        /// Get device id from thread list
        /// and search it in new devices list
        /// and remove device if it not exist more
        /// </summary>
        /// <returns></returns>
        private void RemoveDevicesThread()
        {
            if (this._isLogin)
            {
                bool itExist = false;
                // Check if device not exist in new list
                // Remove objects in GUI
                foreach (Device deviceBackup in this._userDeviceListBackUp.userDeviceDevices)
                {
                    if (this._isLogin)
                    {
                        foreach (Device device in this._userDeviceList.userDeviceDevices)
                        {
                            if (this._isLogin)
                            {
                                if (device.deviceSerialNumber == deviceBackup.deviceSerialNumber)
                                {
                                    itExist = true;
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (itExist == false)
                        {
                            if (this._isLogin)
                            {
                                // Remove GUI for this Device
                                this.RemoveCityTreeView(deviceBackup);
                                this.RemoveDeviceRowStatus(deviceBackup);
                                this.RemoveDevicePicture(deviceBackup);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Get device id from new device list
        /// and search it in device thread list
        /// and add to list and generate its thread
        /// </summary>
        /// <returns></returns>
        private void AddDevicesThread()
        {
            if (this._isLogin)
            {
                Device DeviceBackUp = new Device();
                DeviceBackUp.deviceError = new ErrorCode();
                foreach (Device device in this._userDeviceList.userDeviceDevices)
                {
                    if (this._isLogin)
                    {
                        bool deviceTreadExist = false;
                        foreach (Device deviceBackUp in this._userDeviceListBackUp.userDeviceDevices)
                        {
                            if (this._isLogin)
                            {
                                if (device.deviceSerialNumber == deviceBackUp.deviceSerialNumber)
                                {
                                    deviceTreadExist = true;

                                    DeviceBackUp = MyClone.DeepClone(deviceBackUp);
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (this._isLogin)
                        {
                            // Device is new, thread should be created
                            if (deviceTreadExist == false)
                            {
                                this._pictureBoxList[device.deviceSerialNumber] = new DevicePicture();
                                // Add GUI objects
                                this.AddDevicePicture(device);
                            }
                            if (this._isLogin)
                            {
                                // Initialize data
                                this.InitiateDeviceStatus(device);
                                if (this._isLogin)
                                {
                                    // Add or refresh object like tree view node, Picture and status row
                                    this.RefreshGUI(device, DeviceBackUp);
                                    if (this._isLogin)
                                    {
                                        // If form status is open refresh data
                                        this.UpdateForms(device);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Refresh TreeView 
        /// Pictures
        /// Status Table
        /// </summary>
        /// <returns></returns>
        private void RefreshGUI(Device DeviceObject, Device DeviceObjectBackUp)
        {
            if (this._isLogin)
            {
                this.SuspendLayout();
                // Refresh TreeView
                // Add or modify tree view item if not exist
                if ((DeviceObject.deviceCity != DeviceObjectBackUp.deviceCity) ||
                                                    (DeviceObject.deviceLocation != DeviceObjectBackUp.deviceLocation) ||
                                                    (DeviceObject.deviceNikeName != DeviceObjectBackUp.deviceNikeName))
                {
                    // Add or modify tree view item if not exist
                    this.AddCityTreeView(DeviceObject, DeviceObjectBackUp);
                }
                if (this._isLogin)
                {

                    if (DeviceObject.deviceError != DeviceObjectBackUp.deviceError)
                    {
                        if (this._isLogin)
                        {
                            // Refresh picture
                            this.RefreshDevicePicture(DeviceObject);
                        }
                        if (this._isLogin)
                        {
                            // Add alarm status in data grid view if error exist
                            this.RefreshDeviceRowStatus(DeviceObject);
                        }
                    }
                }
                this.ResumeLayout();
            }
        }

        /// <summary>
        /// Update objects data in other child forms
        /// Like DeviceStatusForm
        /// </summary>
        /// <returns></returns>
        private void UpdateForms(Device DeviceInfoObject)
        {
            if (this._isLogin)
            {
                // Form is in show modal (open)
                if (this._formDeviceStatus.DeviceObject.deviceSerialNumber == DeviceInfoObject.deviceSerialNumber)
                {
                    if (this._isLogin)
                    {
                        this._formDeviceStatus.DeviceObject = MyClone.DeepClone(DeviceInfoObject);

                        if (this._isLogin)
                        {
                            this._formDeviceStatus.StartDeviceThread();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <param name="deviceInfoBackUp"></param>
        /// <returns></returns>
        private void AddCityTreeView(Device deviceInfo, Device deviceInfoBackUp)
        {
            if (this._isLogin)
            {
                if (deviceInfo.deviceCity != deviceInfoBackUp.deviceCity)
                {
                    if (this._isLogin)
                    {
                        this.RemoveCityTreeView(deviceInfoBackUp);
                        if (this._isLogin)
                        {
                            if (!this.tvDevices.Nodes.Find(deviceInfo.deviceCity, false).Any())
                            {
                                if (this._isLogin)
                                {
                                    // Device city node not exist
                                    this.tvDevices.InvokeIfRequired(c =>
                                    {
                                        c.Nodes.Add(deviceInfo.deviceCity, deviceInfo.deviceCity);
                                    });
                                    if (this._isLogin)
                                    {
                                        // Set tag for first level nodes to "City"
                                        this.tvDevices.InvokeIfRequired(c =>
                                        {
                                            c.Nodes[deviceInfo.deviceCity].Tag = "City";
                                        });

                                        if (this._isLogin)
                                        {
                                            // Device location node not exist
                                            this.tvDevices.InvokeIfRequired(
                                                c =>
                                                {
                                                    c.Nodes[deviceInfo.deviceCity].Nodes.Add(deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation, deviceInfo.deviceLocation);
                                                    // Set tag for second level nodes to "Location"
                                                    c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Tag = "Location";
                                                });
                                            if (this._isLogin)
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
                                    }
                                }
                            }
                            else if (!this.tvDevices.Nodes[deviceInfo.deviceCity].Nodes.Find(deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation, false).Any())
                            {
                                if (this._isLogin)
                                {
                                    // Device location node not exist
                                    this.tvDevices.InvokeIfRequired(
                                        c =>
                                        {
                                            c.Nodes[deviceInfo.deviceCity].Nodes.Add(deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation, deviceInfo.deviceLocation);
                                            // Set tag for second level nodes to "Location"
                                            c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Tag = "Location";
                                        });
                                    if (this._isLogin)
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
                            }
                            else if (!this.tvDevices.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes.Find(Convert.ToString(deviceInfo.deviceSerialNumber), false).Any())
                            {
                                if (this._isLogin)
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
                        }
                    }
                }
                else
                {
                    if (this._isLogin)
                    {
                        this.AddLocationTreeView(deviceInfo, deviceInfoBackUp);
                    }
                }
            }
        }

        private void AddLocationTreeView(Device deviceInfo, Device deviceInfoBackUp)
        {
            if (this._isLogin)
            {
                if (deviceInfo.deviceLocation != deviceInfoBackUp.deviceLocation)
                {
                    if (this._isLogin)
                    {
                        this.RemoveLocationTreeView(deviceInfoBackUp);
                        if (this._isLogin)
                        {
                            if (!this.tvDevices.Nodes[deviceInfo.deviceCity].Nodes.Find(deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation, false).Any())
                            {
                                if (this._isLogin)
                                {
                                    // Device location node not exist
                                    this.tvDevices.InvokeIfRequired(
                                        c =>
                                        {
                                            c.Nodes[deviceInfo.deviceCity].Nodes.Add(deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation,
                                                deviceInfo.deviceLocation);
                                            // Set tag for third level nodes to "Device"
                                            c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Tag = "Location";
                                        });
                                    if (this._isLogin)
                                    {
                                        // Device name node not exist
                                        this.tvDevices.InvokeIfRequired(
                                            c =>
                                            {
                                                c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes.Add(
                                                    Convert.ToString(deviceInfo.deviceSerialNumber), deviceInfo.deviceNikeName + "(" + deviceInfo.deviceSerialNumber + ")");
                                                // Set tag for third level nodes to "Device"
                                                c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes[Convert.ToString(deviceInfo.deviceSerialNumber)].Tag = "Device";
                                            });
                                    }
                                }
                            }
                            else if (!this.tvDevices.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes.Find(Convert.ToString(deviceInfo.deviceSerialNumber), false).Any())
                            {
                                if (this._isLogin)
                                {
                                    // Device name node not exist
                                    this.tvDevices.InvokeIfRequired(
                                        c =>
                                        {
                                            c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes.Add(
                                                Convert.ToString(deviceInfo.deviceSerialNumber), deviceInfo.deviceNikeName + "(" + deviceInfo.deviceSerialNumber + ")");
                                            // Set tag for third level nodes to "Device"
                                            c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes[Convert.ToString(deviceInfo.deviceSerialNumber)].Tag = "Device";
                                        });
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (this._isLogin)
                    {
                        this.AddDeviceTreeView(deviceInfo, deviceInfoBackUp);
                    }
                }
            }
        }

        private void AddDeviceTreeView(Device deviceInfo, Device deviceInfoBackUp)
        {
            if (this._isLogin)
            {
                if (deviceInfo.deviceNikeName != deviceInfoBackUp.deviceNikeName)
                {
                    if (this._isLogin)
                    {
                        this.RemoveDeviceTreeView(deviceInfoBackUp);
                        if (this._isLogin)
                        {
                            if (!this.tvDevices.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes.Find(Convert.ToString(deviceInfo.deviceSerialNumber), false).Any())
                            {
                                if (this._isLogin)
                                {
                                    // Device name node not exist
                                    this.tvDevices.InvokeIfRequired(
                                        c =>
                                        {
                                            c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes.Add(
                                                Convert.ToString(deviceInfo.deviceSerialNumber), deviceInfo.deviceNikeName + "(" + deviceInfo.deviceSerialNumber + ")");
                                            // Set tag for third level nodes to "Device"
                                            c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes[Convert.ToString(deviceInfo.deviceSerialNumber)].Tag = "Device";
                                        });
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RemoveCityTreeView(Device deviceInfo)
        {
            if (this._isLogin)
            {
                if (this.tvDevices.Nodes.Find(deviceInfo.deviceCity, false).Any())
                {
                    if (this._isLogin)
                    {
                        this.RemoveLocationTreeView(deviceInfo);
                        if (this._isLogin)
                        {
                            // Remove node if it nods is empty
                            if (this.tvDevices.Nodes[deviceInfo.deviceCity].Nodes.Count == 0)
                            {
                                this.tvDevices.InvokeIfRequired(c =>
                                {
                                    c.Nodes[deviceInfo.deviceCity].Remove();
                                });
                            }
                        }
                    }
                }
            }
        }

        private void RemoveLocationTreeView(Device deviceInfo)
        {
            if (this._isLogin)
            {
                if (this.tvDevices.Nodes[deviceInfo.deviceCity].Nodes.Find(deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation,
                            false).Any())
                {
                    if (this._isLogin)
                    {
                        this.RemoveDeviceTreeView(deviceInfo);
                        if (this._isLogin)
                        {
                            // Remove node if it nods is empty
                            if (this.tvDevices.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes
                                    .Count == 0)
                            {
                                if (this._isLogin)
                                {
                                    this.tvDevices.InvokeIfRequired(
                                        c =>
                                        {
                                            c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Remove();
                                        });
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RemoveDeviceTreeView(Device deviceInfo)
        {
            if (this._isLogin)
            {
                if (deviceInfo.deviceSerialNumber != 0)
                {
                    if (this._isLogin)
                    {
                        if (this.tvDevices.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes
                                .Find(Convert.ToString(deviceInfo.deviceSerialNumber), false).Any())
                        {
                            if (this._isLogin)
                            {
                                this.tvDevices.InvokeIfRequired(
                                    c =>
                                    {
                                        c.Nodes[deviceInfo.deviceCity].Nodes[deviceInfo.deviceCity + "_" + deviceInfo.deviceLocation].Nodes[
                                            Convert.ToString(deviceInfo.deviceSerialNumber)].Remove();
                                    });
                            }
                        }
                    }
                }
            }
        }

        private void AddDevicePicture(Device DeviceObject)
        {
            if (this._isLogin)
            {
                int indexKey = DeviceObject.deviceSerialNumber;
                this._pictureBoxList[indexKey].DevicePictureBox = new PictureBox();
                if (this._isLogin)
                {
                    // Set Location
                    this._pictureBoxList[indexKey].DevicePictureBox.Location = new System.Drawing.Point(this._locationW, this._locationH);
                    if (this._isLogin)
                    {
                        // Set Name to serial number (it is unique)
                        this._pictureBoxList[indexKey].DevicePictureBox.Name = Convert.ToString(DeviceObject.deviceSerialNumber);
                        this._pictureBoxList[indexKey].DevicePictureBox.Tag = DeviceObject.deviceModel;
                        if (this._isLogin)
                        {
                            // Set Size
                            this._pictureBoxList[indexKey].DevicePictureBox.Size = new System.Drawing.Size(Constants.Width, Constants.Height);
                            // Set icon and background color
                            this._pictureBoxList[indexKey].DevicePictureBox.Image = this.imgListDevice.Images["Device.png"];
                            this._pictureBoxList[indexKey].DevicePictureBox.BackColor = Color.Transparent;

                            if (this._isLogin)
                            {
                                this._pictureBoxList[indexKey].DeviceName = "(" + DeviceObject.deviceSerialNumber.ToString() + ")\n" + DeviceObject.deviceNikeName;
                                this._pictureBoxList[indexKey].DeviceModelName = DeviceObject.deviceModel;

                                this._pictureBoxList[indexKey].DevicePictureBox.Paint += new PaintEventHandler((sender, e) =>
                                {
                                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                                    e.Graphics.DrawString(this._pictureBoxList[indexKey].DeviceName, new Font("Tahoma", 8), Brushes.Black, 0, 0);
                                });

                                //Set the SizeMode to center the image
                                this._pictureBoxList[indexKey].DevicePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                                //this._pictureBoxList[indexKey].DevicePictureBox.AllowDrop = true;
                                this._pictureBoxList[indexKey].DevicePictureBox.ContextMenuStrip = this.msDevicePicture;
                                if (this._isLogin)
                                {
                                    this.pnlIcons.InvokeIfRequired((c) =>
                                    {
                                        c.Controls.Add(this._pictureBoxList[indexKey].DevicePictureBox);
                                    });
                                    this._pictureBoxList[indexKey].DeviceLevel = WebServiceLib.SettingLevel.Normal;
                                    this._pictureBoxList[indexKey].DevicePictureColor = Color.White;
                                }
                            }
                        }
                    }
                }

                this.GetLocation();
            }
        }

        private void RefreshDevicePicture(Device DeviceObject)
        {
            if (this._isLogin)
            {
                Color currentStatusColor = this.GetStatusColor(DeviceObject.deviceError.errorType);
                // PictureBox color changed to current device status
                if (currentStatusColor != this._pictureBoxList[DeviceObject.deviceSerialNumber].DevicePictureColor)
                {
                    if (this._isLogin)
                    {
                        if (this._isLogin)
                        {
                            this._pictureBoxList[DeviceObject.deviceSerialNumber].DevicePictureBox.Image = this._imageHandlerList[currentStatusColor].CurrentBitmap;
                        }
                    }
                    this._pictureBoxList[DeviceObject.deviceSerialNumber].DeviceLevel = DeviceObject.deviceError.errorType;
                    this._pictureBoxList[DeviceObject.deviceSerialNumber].DevicePictureColor = currentStatusColor;
                }

                // Device name changed update PictureBox name
                if (this._pictureBoxList[DeviceObject.deviceSerialNumber].DeviceName != "(" + DeviceObject.deviceSerialNumber.ToString() + ")\n" + DeviceObject.deviceNikeName)
                {
                    this._pictureBoxList[DeviceObject.deviceSerialNumber].DeviceName = "(" + DeviceObject.deviceSerialNumber.ToString() + ")\n" + DeviceObject.deviceNikeName;
                    this._pictureBoxList[DeviceObject.deviceSerialNumber].DevicePictureBox.InvokeIfRequired((c) =>
                    {
                        c.Invalidate();
                    });
                }
            }
        }

        private void RemoveDevicePicture(Device DeviceObject)
        {
            if (this._isLogin)
            {
                try
                {
                    // Remove from control
                    this.pnlIcons.InvokeIfRequired((c) =>
                    {
                        c.Controls.Remove(this._pictureBoxList[DeviceObject.deviceSerialNumber].DevicePictureBox);
                    });
                    if (this._isLogin)
                    {
                        this._pictureBoxList.Remove(DeviceObject.deviceSerialNumber);
                    }
                }
                catch
                {
                    if (this._isLogin)
                    {
                        this._pictureBoxList.Remove(DeviceObject.deviceSerialNumber);
                    }
                }
            }
        }

        private void RefreshDeviceRowStatus(Device DeviceObject)
        {
            if (this._isLogin)
            {
                bool exist = false;
                // Check status row exist for this device or no
                int rowIndex = 0;
                foreach (DataGridViewRow row in this.dgvStatus.Rows)
                {
                    if (this._isLogin)
                    {
                        if (row.Cells["deviceId"].Value.ToString() == Convert.ToString(DeviceObject.deviceSerialNumber))
                        {
                            // Row exist
                            // Just update its row
                            rowIndex = row.Index;
                            exist = true;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                // Some Error Occurred

                if (this._isLogin)
                {
                    if (DeviceObject.deviceError.errorMessage != "")
                    {
                        // Status row do not exist for this device so create it
                        if (!exist)
                        {
                            if (this._isLogin)
                            {
                                this.dgvStatus.InvokeIfRequired(c =>
                                {
                                    c.Rows.Add();
                                });
                                rowIndex = this.dgvStatus.Rows.Count - 1;
                                // Initialize row cells
                                this.dgvStatus.InvokeIfRequired(c =>
                                    {
                                        c.Rows[rowIndex].Cells["deviceId"].Value = "";
                                        c.Rows[rowIndex].Cells["DeviceName"].Value = "";
                                        c.Rows[rowIndex].Cells["ErrorText"].Value = "";
                                        c.Rows[rowIndex].Cells["DateTime"].Value = "";
                                    });
                            }
                        }
                        if (this._isLogin)
                        {
                            if (this.dgvStatus.Rows[rowIndex].Cells["ErrorText"].Value.ToString() != DeviceObject.deviceError.errorMessage)
                            {
                                if (this._isLogin)
                                {
                                    DateTime t = System.DateTime.Now;
                                    this.dgvStatus.InvokeIfRequired(
                                        c =>
                                        {
                                            c.Rows[rowIndex].Cells["deviceId"].Value = Convert.ToString(DeviceObject.deviceSerialNumber);
                                            c.Rows[rowIndex].Cells["deviceId"].Tag = DeviceObject.deviceModel;
                                            c.Rows[rowIndex].Cells["DeviceName"].Value = Convert.ToString(DeviceObject.deviceNikeName);
                                            c.Rows[rowIndex].Cells["ErrorText"].Value = @"";
                                            c.Rows[rowIndex].Cells["ErrorText"].Value = DeviceObject.deviceError.errorMessage;
                                            c.Rows[rowIndex].Cells["DateTime"].Value = t.ToString("tt hh:mm:ss yyyy-dd-mm");
                                        });
                                }
                            }

                            if (this._isLogin)
                            {
                                Color newColor = this.GetStatusColor(DeviceObject.deviceError.errorType);
                                if (this.dgvStatus.Rows[rowIndex].DefaultCellStyle.BackColor != newColor)
                                {
                                    if (this._isLogin)
                                    {
                                        if (DeviceObject.deviceError.errorType != WebServiceLib.SettingLevel.Normal)
                                        {
                                            if (this._isLogin)
                                            {
                                                this.dgvStatus.InvokeIfRequired(c =>
                                                    {
                                                        c.Rows[rowIndex].DefaultCellStyle.BackColor = newColor;
                                                    });
                                            }
                                        }
                                        else
                                        {
                                            if (this._isLogin)
                                            {
                                                this.dgvStatus.InvokeIfRequired(c =>
                                                {
                                                    c.Rows[rowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Green;
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (exist)
                        {
                            if (this._isLogin)
                            {
                                // Delete row
                                this.dgvStatus.InvokeIfRequired(c =>
                                {
                                    c.Rows.RemoveAt(rowIndex);
                                });
                            }
                        }
                    }
                }
            }
        }

        private void RemoveDeviceRowStatus(Device DeviceObject)
        {
            if (this._isLogin)
            {
                bool exist = false;
                // Check status row exist for this device or no
                int rowIndex = 0;
                foreach (DataGridViewRow row in this.dgvStatus.Rows)
                {
                    if (this._isLogin)
                    {
                        if (row.Cells["deviceId"].Value.ToString() == Convert.ToString(DeviceObject.deviceSerialNumber))
                        {
                            // Row exist
                            // Just update its row
                            rowIndex = row.Index;
                            exist = true;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (exist)
                {
                    if (this._isLogin)
                    {
                        // Delete row
                        this.dgvStatus.InvokeIfRequired(c =>
                        {
                            c.Rows.RemoveAt(rowIndex);
                        });
                    }
                }
            }
        }

        public int AddUpdateDeletSearchDeviceInList(Device DeviceInfo, int CommandType)
        {
            int result = -1;
            lock (this._userDeviceListLocker)
            {
                switch (CommandType)
                {
                    case 1: // Add
                        {
                            for (int i = 0; i < this.UserDeviceList.userDeviceDevices.Count(); i++)
                            {
                                if (this.UserDeviceList.userDeviceDevices[i].deviceSerialNumber == DeviceInfo.deviceSerialNumber)
                                {
                                    this.UserDeviceList.userDeviceDevices[i] = MyClone.DeepClone(DeviceInfo);
                                    result = i;
                                    break;
                                }
                            }

                            if (result == -1)
                            {
                                //this.UserDeviceList.userDeviceDevices.Add(DeviceInfo);
                                result = 1;
                            }
                            break;
                        }
                    case 2: // Update
                        {
                            for (int i = 0; i < this.UserDeviceList.userDeviceDevices.Count(); i++)
                            {
                                if (this.UserDeviceList.userDeviceDevices[i].deviceSerialNumber == DeviceInfo.deviceSerialNumber)
                                {
                                    this.UserDeviceList.userDeviceDevices[i] = MyClone.DeepClone(DeviceInfo);
                                    result = i;
                                    break;
                                }
                            }
                            break;
                        }
                    case 3: // Delete
                        {
                            for (int i = 0; i < this.UserDeviceList.userDeviceDevices.Count(); i++)
                            {
                                if (this.UserDeviceList.userDeviceDevices[i].deviceSerialNumber == DeviceInfo.deviceSerialNumber)
                                {
                                    //this.UserDeviceList.userDeviceDevices.RemoveAt(i);
                                    result = i;
                                    break;
                                }
                            }
                            break;
                        }
                    case 4: // Search
                        {
                            result = 0;
                            for (int i = 0; i < this.UserDeviceList.userDeviceDevices.Count(); i++)
                            {
                                if (this.UserDeviceList.userDeviceDevices[i].deviceSerialNumber == DeviceInfo.deviceSerialNumber)
                                {
                                    DeviceInfo = MyClone.DeepClone(this.UserDeviceList.userDeviceDevices[i]);
                                    result = i;
                                    break;
                                }
                            }
                            break;
                        }
                }
            }
            return result;
        }

        private void GetLocation()
        {
            if (this.pnlIcons.Size.Width > this._locationW + Constants.Width + 4)
            {
                this._locationW += Constants.Width + 4;
            }
            else
            {
                this._locationW = 4;
                this._locationH += Constants.Height + 4;
            }
        }

        /// <summary>
        /// Load settings from Settings XML file
        /// </summary>
        /// <returns></returns>
        private void LoadSettings()
        {
            if (this._isLogin)
            {
                try
                {
                    this._settingObject = Settings.LoadFromFile(Constants.SettingFileName);
                }
                catch (SystemException ex)
                {
                    // Generate default to disable settings
                    FormClientSettings frmSettings = new FormClientSettings();
                    frmSettings.GenerateDefaultSettings();
                    // Get last settings
                    this._settingObject = frmSettings.SettingsObject;
                    string strMessage = ex.Message + "\nAll settings go to default and Disabled\nWould you like to set settings now? ";
                    DialogResult result = MessageBox.Show(this, strMessage, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        // Open setting window and set new settings
                        this.tsmiSetting_Click(this, null);
                    }
                }
            }
        }

        private void pnlTreeView_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlLogs_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tsmiLogout_Click(object sender, EventArgs e)
        {
            // User is logged out
            // All threads terminated
            this._isLogin = false;

            // Show login form
            this.DoUserLogin();
        }

        private void tsmiSetting_Click(object sender, EventArgs e)
        {
            // Open setting window
            FormClientSettings frmSettings = new FormClientSettings();
            frmSettings.ShowDialog();
            if (frmSettings.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Get last settings
                this._settingObject = frmSettings.SettingsObject;
            }
        }

        private TreeNode m_OldSelectNode;
        private void tvDevices_MouseUp(object sender, MouseEventArgs e)
        {
            // Show menu only if the right mouse button is clicked.
            if (e.Button == MouseButtons.Right)
            {

                // Point where the mouse is clicked.
                Point p = new Point(e.X, e.Y);

                // Get the node that the user has clicked.
                TreeNode node = this.tvDevices.GetNodeAt(p);
                if (node != null)
                {

                    // Select the node the user has clicked.
                    // The node appears selected until the menu is displayed on the screen.
                    m_OldSelectNode = this.tvDevices.SelectedNode;
                    this.tvDevices.SelectedNode = node;
                    this._treeNode = node;

                    // Find the appropriate ContextMenu depending on the selected node.
                    switch (Convert.ToString(node.Tag))
                    {
                        case "City":
                            this.msTVCity.Show(this.tvDevices, p);
                            break;
                        case "Location":
                            this.msTVLocation.Show(this.tvDevices, p);
                            break;
                        case "Device":
                            this.msTVDevice.Show(this.tvDevices, p);
                            break;
                    }

                    // Highlight the selected node.
                    this.tvDevices.SelectedNode = m_OldSelectNode;
                    m_OldSelectNode = null;
                }
            }
        }

        private void tsmDeviceStatus_Click(object sender, EventArgs e)
        {
            // Share parents attributes
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            ContextMenuStrip cms = (ContextMenuStrip)mi.Owner;
            PictureBox pictureBoxSender = (PictureBox)cms.SourceControl;

            // Generate and show device status windows form
            this.GenerateDeviceStatusWindowForm(Convert.ToInt32(((PictureBox)pictureBoxSender).Name), _pictureBoxList[Convert.ToInt32(((PictureBox)pictureBoxSender).Name)].DeviceModelName);
        }

        private void GenerateDeviceStatusWindowForm(int DeviceSerialNumber, DeviceModel Model)
        {
            this._formDeviceStatus = new FormDeviceStatus();

            this._formDeviceStatus.UserInfo = this._userInfo;
            this._formDeviceStatus.FormMain = this;
            this._formDeviceStatus.DeviceObject.deviceSerialNumber = DeviceSerialNumber;
            this._formDeviceStatus.DeviceObject.deviceModel = Model;
            this._formDeviceStatus.pbDevicePicture.Image = this.imgListDevice.Images["Device.png"];

            if (this._deviceLastImages.ContainsKey(this._formDeviceStatus.DeviceObject.deviceSerialNumber))
            {
                if (this._deviceLastImages[this._formDeviceStatus.DeviceObject.deviceSerialNumber] == null)
                {
                    this._formDeviceStatus.pbLastPicture.Image = this.imgListDevice.Images["Device.png"];
                }
                else
                {
                    this._formDeviceStatus.pbLastPicture.Image = this._deviceLastImages[this._formDeviceStatus.DeviceObject.deviceSerialNumber];
                }
            }
            else
            {
                this._formDeviceStatus.pbLastPicture.Image = this.imgListDevice.Images["Device.png"];
            }

            this._formDeviceStatus.ShowDialog();

            // Destruct data
            this._formDeviceStatus.DeviceObject.deviceSerialNumber = -1;
        }

        public void GenerateDeviceSettingWindowForm(int DeviceSerialNumber)
        {
            this._formDeviceSetting = new FormDeviceSetting();

            this._formDeviceSetting.UserInfo = this._userInfo;
            this._formDeviceSetting.DeviceObject.deviceSerialNumber = DeviceSerialNumber;
            this._formDeviceSetting.pbDevicePicture.Image = this.imgListDevice.Images["Device.png"];
            this._formDeviceSetting.SectionnerObject = this._sectionnerObject;
            this._formDeviceSetting.FormMain = this;

            this._formDeviceSetting.DeviceObject = this.GetDeviceInformations(this._formDeviceSetting.DeviceObject.deviceSerialNumber);

            this._formDeviceSetting.ShowDialog();

            // Destruct data
            this._formDeviceSetting.DeviceObject.deviceSerialNumber = -1;
        }

        private Device GetDeviceInformations(int DeviceSerialNumber)
        {
            Device DeviceObject = new Device();
            // Lock user device list when search in it
            lock (this._userDeviceListLocker)
            {
                for (int i = 0; i < this.UserDeviceList.userDeviceDevices.Count(); i++)
                {
                    if (this.UserDeviceList.userDeviceDevices[i].deviceSerialNumber == DeviceSerialNumber)
                    {
                        DeviceObject = MyClone.DeepClone(this.UserDeviceList.userDeviceDevices[i]);
                        return DeviceObject;
                    }
                }
            }
            return DeviceObject;
        }

        private void tsmShowDeviceStatus_Click(object sender, EventArgs e)
        {
            this.GenerateDeviceStatusWindowForm(Convert.ToInt32(this._treeNode.Name.ToString()), _pictureBoxList[Convert.ToInt32(this._treeNode.Name.ToString())].DeviceModelName);
        }

        private void tsmiLogin_Click(object sender, EventArgs e)
        {
            FormLogin loginObject = new FormLogin();

            loginObject.ShowDialog();
        }

        public Color GetStatusColor(WebServiceLib.SettingLevel DeviceErrorType)
        {
            if (DeviceErrorType != WebServiceLib.SettingLevel.Normal)
            {
                return System.Drawing.Color.FromName(this._settingObject.LevelColors.Find(x => x.LevelName == (SettingsLib.SettingLevel)DeviceErrorType).ColorName.ToString());
            }
            else
            {
                return System.Drawing.Color.Green;
            }
        }

        private void InitiateAllDeviceStatus(Device[] UserDeviceObject)
        {
            foreach (Device DeviceObject in UserDeviceObject)
            {
                this.InitiateDeviceStatus(DeviceObject);
            }
        }

        public void InitiateDeviceStatus(Device DeviceObject)
        {
            /*if(this._isLogin)
			{
				if(DeviceObject.deviceError.errorMessage == "")
				{
					// Analyze all sensors data
					// (include min and max and thresholds)
					// and generate status string

					// For every sensors
					if(DeviceObject.deviceSensors.Count() > 0)
					{
						if(this._isLogin)
						{
							// Error index
							int counter = 1;
							// Sensor error index
							int senCounter = 1;

							bool commaOk = false;
							bool commaOkSensor = false;

							foreach(Sensor sensor in DeviceObject.deviceSensors)
							{
								commaOkSensor = false;
								if(this._isLogin)
								{
									if(sensor.sensorError == null)
									{
										sensor.sensorError = new ErrorCode();
									}
									else
									{
										sensor.sensorError.errorMessage = "";
									}
									// Is a sensor with min max and threshold data
									//  Is multi sensor
									if(sensor.sensorType == SensorType.Multi)
									{
										if(this._isLogin)
										{
											SettingSensors result = null;
											// Find setting of errors levels
											// In settings
											try
											{
												result = this._settingObject.SensorsSetting.Find(x => x.SensorName == (SettingSensor)System.Enum.Parse(typeof(SettingSensor), sensor.sensorName.ToString()));
											}
											catch
											{
												result = null;
											}

											if(this._isLogin)
											{
												// Value is lower than minimum or upper than maximum
												if(sensor.sensorValue < sensor.sensorMinimumValue + sensor.sensorMinimumThreshold)
												{
													if(this._isLogin)
													{
														senCounter = 1;
														if(sensor.sensorValue < sensor.sensorMinimumValue)
														{
															if(this._isLogin)
															{
																if(commaOk == false)
																{
																	commaOk = true;
																}
																else
																{
																	DeviceObject.deviceError.errorMessage += ", ";
																}
																if(commaOkSensor == false)
																{
																	commaOkSensor = true;
																}
																else
																{
																	sensor.sensorError.errorMessage += ", ";
																}
																// Set error string
																sensor.sensorError.errorMessage += senCounter.ToString() + "_" + sensor.sensorName + " Minimum";

																DeviceObject.deviceError.errorMessage += counter.ToString() + "_" + sensor.sensorName + " Minimum";
																counter++;
																senCounter++;

																// Set level status value
																if(result != null)
																{
																	if(this._isLogin)
																	{
																		if(sensor.sensorError.errorType < (WebServiceLib.SettingLevel)result.MinimumLevel)
																		{
																			sensor.sensorError.errorType = (WebServiceLib.SettingLevel)result.MinimumLevel;
																		}
																		if(this._isLogin)
																		{
																			// Set level with higher priority
																			if(DeviceObject.deviceError.errorType < (WebServiceLib.SettingLevel)result.MinimumLevel)
																			{
																				DeviceObject.deviceError.errorType = (WebServiceLib.SettingLevel)result.MinimumLevel;
																			}
																		}
																	}
																}
																else
																{
																	if(this._isLogin)
																	{
																		DeviceObject.deviceError.errorType = WebServiceLib.SettingLevel.Green;
																	}
																}
															}
														}
														else
														{
															if(this._isLogin)
															{
																if(commaOk == false)
																{
																	commaOk = true;
																}
																else
																{
																	DeviceObject.deviceError.errorMessage += ", ";
																}
																if(commaOkSensor == false)
																{
																	commaOkSensor = true;
																}
																else
																{
																	sensor.sensorError.errorMessage += ", ";
																}
																// value is between minimum and minimum threshold (warning area)
																// Set error string
																sensor.sensorError.errorMessage += senCounter.ToString() + "_" + sensor.sensorName + " Minimum threshold";
																DeviceObject.deviceError.errorMessage += counter.ToString() + "_" + sensor.sensorName + " Minimum threshold";
																counter++;
																senCounter++;
																// Set level status value
																if(result != null)
																{
																	if(this._isLogin)
																	{
																		if(sensor.sensorError.errorType < (WebServiceLib.SettingLevel)result.MinimumThresholdLevel)
																		{
																			sensor.sensorError.errorType = (WebServiceLib.SettingLevel)result.MinimumThresholdLevel;
																		}
																		if(this._isLogin)
																		{
																			// Set level with higher priority
																			if(DeviceObject.deviceError.errorType < (WebServiceLib.SettingLevel)result.MinimumThresholdLevel)
																			{
																				DeviceObject.deviceError.errorType = (WebServiceLib.SettingLevel)result.MinimumThresholdLevel;
																			}
																		}
																	}
																}
																else
																{
																	if(this._isLogin)
																	{
																		DeviceObject.deviceError.errorType = WebServiceLib.SettingLevel.Green;
																	}
																}
															}
														}
													}
												}
												else if(sensor.sensorValue > sensor.sensorMaximumValue - sensor.sensorMaximumThreshold)
												{
													if(this._isLogin)
													{
														if(sensor.sensorValue > sensor.sensorMaximumValue)
														{
															if(commaOk == false)
															{
																commaOk = true;
															}
															else
															{
																DeviceObject.deviceError.errorMessage += ", ";
															}
															if(commaOkSensor == false)
															{
																commaOkSensor = true;
															}
															else
															{
																sensor.sensorError.errorMessage += ", ";
															}
															// Set error string
															sensor.sensorError.errorMessage += senCounter.ToString() + "_" + sensor.sensorName + " Maximum";
															DeviceObject.deviceError.errorMessage += counter.ToString() + "_" + sensor.sensorName + " Maximum";
															counter++;
															senCounter++;
															// Set level status value
															if(result != null)
															{
																if(this._isLogin)
																{
																	if(sensor.sensorError.errorType < (WebServiceLib.SettingLevel)result.MaximumLevel)
																	{
																		sensor.sensorError.errorType = (WebServiceLib.SettingLevel)result.MaximumLevel;
																	}
																	if(this._isLogin)
																	{
																		// Set level with higher priority
																		if(DeviceObject.deviceError.errorType < (WebServiceLib.SettingLevel)result.MaximumLevel)
																		{
																			DeviceObject.deviceError.errorType = (WebServiceLib.SettingLevel)result.MaximumLevel;
																		}
																	}
																}
															}
															else
															{
																if(this._isLogin)
																{
																	DeviceObject.deviceError.errorType = WebServiceLib.SettingLevel.Green;
																}
															}
														}
														else
														{
															if(this._isLogin)
															{
																if(commaOk == false)
																{
																	commaOk = true;
																}
																else
																{
																	DeviceObject.deviceError.errorMessage += ", ";
																}
																if(commaOkSensor == false)
																{
																	commaOkSensor = true;
																}
																else
																{
																	sensor.sensorError.errorMessage += ", ";
																}
																// value is between minimum and minimum threshold (warning area)
																// Set error string
																sensor.sensorError.errorMessage += senCounter.ToString() + "_" + sensor.sensorName + " Maximum threshold";
																DeviceObject.deviceError.errorMessage += counter.ToString() + "_" + sensor.sensorName + " Maximum threshold";
																counter++;
																senCounter++;
																// Set level status value
																if(result != null)
																{
																	if(this._isLogin)
																	{
																		if(sensor.sensorError.errorType < (WebServiceLib.SettingLevel)result.MaximumThresholdLevel)
																		{
																			sensor.sensorError.errorType = (WebServiceLib.SettingLevel)result.MaximumThresholdLevel;
																		}
																		if(this._isLogin)
																		{
																			// Set level with higher priority
																			if(DeviceObject.deviceError.errorType < (WebServiceLib.SettingLevel)result.MaximumThresholdLevel)
																			{
																				DeviceObject.deviceError.errorType = (WebServiceLib.SettingLevel)result.MaximumThresholdLevel;
																			}
																		}
																	}
																}
																else
																{
																	if(this._isLogin)
																	{
																		DeviceObject.deviceError.errorType = WebServiceLib.SettingLevel.Green;
																	}
																}
															}
														}
													}
												}
											}
										}
									}
									else
									{
										if(this._isLogin)
										{
											// It is mono sensor
											if(sensor.sensorValue == 1)
											{
												if(commaOk == false)
												{
													commaOk = true;
												}
												else
												{
													DeviceObject.deviceError.errorMessage += ", ";
												}
												if(commaOkSensor == false)
												{
													commaOkSensor = true;
												}
												else
												{
													sensor.sensorError.errorMessage += ", ";
												}
												// Set error string
												sensor.sensorError.errorMessage += senCounter.ToString() + "_" + sensor.sensorName + " Error";
												DeviceObject.deviceError.errorMessage += counter.ToString() + "_" + sensor.sensorName + " Error";
												counter++;
												senCounter++;
												// Set level status value
												if(this._isLogin)
												{
													SettingSensorsMono result = this._settingObject.SensorsSettingMono.Find(x => x.SensorName == (SettingSensorMono)System.Enum.Parse(typeof(SettingSensorMono), sensor.sensorName.ToString()));
													if(result != null)
													{
														if(this._isLogin)
														{
															if(sensor.sensorError.errorType < (WebServiceLib.SettingLevel)result.LevelName)
															{
																sensor.sensorError.errorType = (WebServiceLib.SettingLevel)result.LevelName;
															}
															if(this._isLogin)
															{
																// Set level with higher priority
																if(DeviceObject.deviceError.errorType < (WebServiceLib.SettingLevel)result.LevelName)
																{
																	DeviceObject.deviceError.errorType = (WebServiceLib.SettingLevel)result.LevelName;
																}
															}
														}
													}
													else
													{
														if(this._isLogin)
														{
															DeviceObject.deviceError.errorType = WebServiceLib.SettingLevel.Green;
														}
													}
												}
											}
										}
									}
								}
								else
								{
									break;
								}
							}
						}
					}
				}
				else
				{
					if(this._isLogin)
					{
						DeviceObject.deviceError.errorType = WebServiceLib.SettingLevel.Green;
					}
				}
			}*/
        }

        private void tsmiDeviceStatus_dgv_Click(object sender, EventArgs e)
        {
            if (this._mouseLocation != null)
            {
                if (this._mouseLocation.RowIndex > -1)
                {
                    this.GenerateDeviceStatusWindowForm(Convert.ToInt32(this.dgvStatus.Rows[this._mouseLocation.RowIndex].Cells["deviceId"].Value.ToString()), _pictureBoxList[Convert.ToInt32(this.dgvStatus.Rows[this._mouseLocation.RowIndex].Cells["deviceId"].Value.ToString())].DeviceModelName);
                }
            }
        }

        /// <summary>
        /// Get row index that clicked or right clicked
        /// for get device data like serial number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private void dgvStatus_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            this._mouseLocation = e;
        }

        private void tsmiSetting_dgv_Click(object sender, EventArgs e)
        {
            this.tsmiSetting_Click(sender, e);
        }

        private void tsmChangeDeviceSetting_Click(object sender, EventArgs e)
        {
            this.GenerateDeviceSettingWindowForm(Convert.ToInt32(this._treeNode.Name.ToString()));
        }

        private void tsmiDeviceSetting_dgv_Click(object sender, EventArgs e)
        {
            if (this._mouseLocation != null)
            {
                if (this._mouseLocation.RowIndex > -1)
                {
                    this.GenerateDeviceSettingWindowForm(Convert.ToInt32(this.dgvStatus.Rows[this._mouseLocation.RowIndex].Cells["deviceId"].Value.ToString()));
                }
            }
        }

        private void tsmDeviceSetting_P_Click(object sender, EventArgs e)
        {
            // Share parents attributes
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            ContextMenuStrip cms = (ContextMenuStrip)mi.Owner;
            PictureBox pictureBoxSender = (PictureBox)cms.SourceControl;

            // Generate and show device status windows form
            this.GenerateDeviceSettingWindowForm(Convert.ToInt32(((PictureBox)pictureBoxSender).Name));
        }

        private void tsmChangeCityName_Click(object sender, EventArgs e)
        {
            FormEditCity frmEditCityName = new FormEditCity();
            frmEditCityName.lblCurrentCityName.Text = this._treeNode.Name;
            frmEditCityName.SectionnerObject = this._sectionnerObject;

            frmEditCityName.UserInfo = this._userInfo;

            this.GetCityNames();

            if (this._citiesList != null)
            {
                if (this._citiesList.allCityLocatoins != null)
                {
                    if (this._citiesList.allCityLocatoins.Count() > 0)
                    {
                        Base64 b64 = new Base64();
                        foreach (CityLocation cityName in this._citiesList.allCityLocatoins)
                        {
                            if (cityName.cityLocationCityName != b64.Base64Encoding(this._treeNode.Name, Encoding.UTF8))
                            {
                                frmEditCityName.cbCityList.Items.Add(b64.Base64Decoding(cityName.cityLocationCityName, Encoding.UTF8));
                            }
                        }
                        if (frmEditCityName.cbCityList.Items.Count > 0)
                        {
                            frmEditCityName.cbCityList.SelectedIndex = 0;
                        }
                    }
                }
            }

            frmEditCityName.ShowDialog();
        }

        public void GetCityNames()
        {
            this._cities.requestUser = this._userInfo;

            try
            {
                this._citiesList = this._sectionnerObject.GetCityAndLocations(this._cities);
            }
            catch
            {

            }
        }

        private void tsmChangeLocationName_Click(object sender, EventArgs e)
        {
            FormEditLocation formEditLocation = new FormEditLocation();
            char[] separator = { '_' };
            string[] cityLocation = this._treeNode.Name.ToString().Split(separator);

            formEditLocation.lblCurrentCityName.Text = cityLocation[0];
            formEditLocation.lblCurrentLocationName.Text = cityLocation[1];
            formEditLocation.SectionnerObject = this._sectionnerObject;

            formEditLocation.UserInfo = this._userInfo;

            this.GetCityNames();

            if (this._citiesList != null)
            {
                if (this._citiesList.allCityLocatoins != null)
                {
                    if (this._citiesList.allCityLocatoins.Count() > 0)
                    {
                        Base64 b64 = new Base64();
                        foreach (CityLocation cityName in this._citiesList.allCityLocatoins)
                        {
                            if (cityName.cityLocationCityName != b64.Base64Encoding(cityLocation[0], Encoding.UTF8))
                            {
                                formEditLocation.cbCityList.Items.Add(b64.Base64Decoding(cityName.cityLocationCityName, Encoding.UTF8));
                                if (cityName.cityLocationLocationsName != null)
                                {
                                    if (cityName.cityLocationLocationsName.Count() > 0)
                                    {
                                        foreach (string locationName in cityName.cityLocationLocationsName)
                                        {
                                            if (locationName != b64.Base64Encoding(cityLocation[1], Encoding.UTF8))
                                            {
                                                formEditLocation.cbLocationList.Items.Add(b64.Base64Decoding(locationName, Encoding.UTF8));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (formEditLocation.cbCityList.Items.Count > 0)
                        {
                            formEditLocation.cbCityList.SelectedIndex = 0;
                        }
                        if (formEditLocation.cbLocationList.Items.Count > 0)
                        {
                            formEditLocation.cbLocationList.SelectedIndex = 0;
                        }
                    }
                }
            }

            formEditLocation.ShowDialog();
        }

        private void tsmChangeDeviceName_Click(object sender, EventArgs e)
        {
            FormEditDevice formEditDevice = new FormEditDevice();
            formEditDevice.SectionnerObject = this._sectionnerObject;

            formEditDevice.UserInfo = this._userInfo;

            Device DeviceObject = new Device();
            DeviceObject.deviceSerialNumber = Convert.ToInt32(this._treeNode.Name.ToString());

            DeviceObject = this.GetDeviceInformations(DeviceObject.deviceSerialNumber);

            formEditDevice.lblCurrentCityName.Text = DeviceObject.deviceCity;
            formEditDevice.lblCurrentLocationName.Text = DeviceObject.deviceLocation;
            formEditDevice.lblCurrentDeviceName.Text = DeviceObject.deviceNikeName;
            formEditDevice.lblCurrentDeviceID.Text = DeviceObject.deviceSerialNumber.ToString();

            this.GetCityNames();

            if (this._citiesList != null)
            {
                if (this._citiesList.allCityLocatoins != null)
                {
                    if (this._citiesList.allCityLocatoins.Count() > 0)
                    {
                        Base64 b64 = new Base64();
                        foreach (CityLocation cityName in this._citiesList.allCityLocatoins)
                        {
                            if (cityName.cityLocationCityName != b64.Base64Encoding(DeviceObject.deviceCity, Encoding.UTF8))
                            {
                                formEditDevice.cbCityList.Items.Add(b64.Base64Decoding(cityName.cityLocationCityName, Encoding.UTF8));
                                if (cityName.cityLocationLocationsName != null)
                                {
                                    if (cityName.cityLocationLocationsName.Count() > 0)
                                    {
                                        foreach (string locationName in cityName.cityLocationLocationsName)
                                        {
                                            if (locationName != b64.Base64Encoding(DeviceObject.deviceLocation, Encoding.UTF8))
                                            {
                                                formEditDevice.cbLocationList.Items.Add(b64.Base64Decoding(locationName, Encoding.UTF8));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (formEditDevice.cbCityList.Items.Count > 0)
                        {
                            formEditDevice.cbCityList.SelectedIndex = 0;
                        }
                        if (formEditDevice.cbLocationList.Items.Count > 0)
                        {
                            formEditDevice.cbLocationList.SelectedIndex = 0;
                        }
                    }
                }
            }

            formEditDevice.ShowDialog();
        }

        private void tsmiUsers_Manager_Click(object sender, EventArgs e)
        {
            FormUsers formUsers = new FormUsers();

            formUsers.SectionnerObject = this._sectionnerObject;
            formUsers.UserInfo = this._userInfo;

            formUsers.ShowDialog();
        }

        private void menuStripPrimary_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tsmiReports_Tools_Click(object sender, EventArgs e)
        {
            FormReport frmReport = new FormReport();

            frmReport.SectionnerObject = this._sectionnerObject;
            frmReport.UserInfo = this._userInfo;

            frmReport.ShowDialog();
        }
    }
}
