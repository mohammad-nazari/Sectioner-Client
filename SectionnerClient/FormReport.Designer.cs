namespace SectionnerClient
{
	partial class FormReport
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReport));
			this.pnlDevices = new System.Windows.Forms.Panel();
			this.tvDevices = new SectionnerClient.TriStateTreeView();
			this.dgvReports = new System.Windows.Forms.DataGridView();
			this.DeviceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DeviceCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DeviceLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Error = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pnlReports = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnShow = new System.Windows.Forms.Button();
			this.lblEndDateTime = new System.Windows.Forms.Label();
			this.lblStartDateTime = new System.Windows.Forms.Label();
			this.lblRecordsCount = new System.Windows.Forms.Label();
			this.dtnStartDateTime = new Atf.UI.DateTimeSelector();
			this.dtnEndDateTime = new Atf.UI.DateTimeSelector();
			this.cbSensor = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnReport = new System.Windows.Forms.Button();
			this.pnlDevices.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvReports)).BeginInit();
			this.pnlReports.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlDevices
			// 
			this.pnlDevices.Controls.Add(this.tvDevices);
			this.pnlDevices.Location = new System.Drawing.Point(1, 0);
			this.pnlDevices.Name = "pnlDevices";
			this.pnlDevices.Size = new System.Drawing.Size(230, 231);
			this.pnlDevices.TabIndex = 0;
			// 
			// tvDevices
			// 
			this.tvDevices.CheckBoxes = true;
			this.tvDevices.Location = new System.Drawing.Point(0, 0);
			this.tvDevices.Name = "tvDevices";
			this.tvDevices.Size = new System.Drawing.Size(230, 227);
			this.tvDevices.TabIndex = 0;
			// 
			// dgvReports
			// 
			this.dgvReports.AllowUserToAddRows = false;
			this.dgvReports.AllowUserToDeleteRows = false;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvReports.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
			this.dgvReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvReports.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.dgvReports.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvReports.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.dgvReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvReports.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeviceID,
            this.DeviceName,
            this.DeviceCity,
            this.DeviceLocation,
            this.Error,
            this.DateTime});
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvReports.DefaultCellStyle = dataGridViewCellStyle8;
			this.dgvReports.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvReports.Location = new System.Drawing.Point(0, 0);
			this.dgvReports.MultiSelect = false;
			this.dgvReports.Name = "dgvReports";
			this.dgvReports.ReadOnly = true;
			this.dgvReports.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvReports.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.dgvReports.RowHeadersVisible = false;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvReports.RowsDefaultCellStyle = dataGridViewCellStyle10;
			this.dgvReports.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dgvReports.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvReports.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvReports.Size = new System.Drawing.Size(905, 490);
			this.dgvReports.StandardTab = true;
			this.dgvReports.TabIndex = 1;
			// 
			// DeviceID
			// 
			this.DeviceID.FillWeight = 15F;
			this.DeviceID.HeaderText = "شماره پست";
			this.DeviceID.Name = "DeviceID";
			this.DeviceID.ReadOnly = true;
			this.DeviceID.ToolTipText = "شماره پست";
			// 
			// DeviceName
			// 
			this.DeviceName.FillWeight = 15F;
			this.DeviceName.HeaderText = "نام پست";
			this.DeviceName.Name = "DeviceName";
			this.DeviceName.ReadOnly = true;
			this.DeviceName.ToolTipText = "نام پست";
			// 
			// DeviceCity
			// 
			this.DeviceCity.FillWeight = 15F;
			this.DeviceCity.HeaderText = "شهر پست";
			this.DeviceCity.Name = "DeviceCity";
			this.DeviceCity.ReadOnly = true;
			this.DeviceCity.ToolTipText = "شهر پست";
			// 
			// DeviceLocation
			// 
			this.DeviceLocation.FillWeight = 15F;
			this.DeviceLocation.HeaderText = "ایستگاه پست";
			this.DeviceLocation.Name = "DeviceLocation";
			this.DeviceLocation.ReadOnly = true;
			this.DeviceLocation.ToolTipText = "ایستگاه پست";
			// 
			// Error
			// 
			this.Error.FillWeight = 25F;
			this.Error.HeaderText = "مقدار";
			this.Error.Name = "Error";
			this.Error.ReadOnly = true;
			this.Error.ToolTipText = "مفدار";
			// 
			// DateTime
			// 
			this.DateTime.FillWeight = 15F;
			this.DateTime.HeaderText = "تاریخ زمان";
			this.DateTime.Name = "DateTime";
			this.DateTime.ReadOnly = true;
			this.DateTime.ToolTipText = "تاریخ زمان";
			// 
			// pnlReports
			// 
			this.pnlReports.Controls.Add(this.dgvReports);
			this.pnlReports.Location = new System.Drawing.Point(237, 0);
			this.pnlReports.Name = "pnlReports";
			this.pnlReports.Size = new System.Drawing.Size(905, 490);
			this.pnlReports.TabIndex = 3;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(12, 476);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(216, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "بستن";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(12, 432);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(216, 23);
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "ذخیره";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnShow
			// 
			this.btnShow.Location = new System.Drawing.Point(12, 374);
			this.btnShow.Name = "btnShow";
			this.btnShow.Size = new System.Drawing.Size(216, 23);
			this.btnShow.TabIndex = 7;
			this.btnShow.Text = "مشاهده";
			this.btnShow.UseVisualStyleBackColor = true;
			this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
			// 
			// lblEndDateTime
			// 
			this.lblEndDateTime.AutoSize = true;
			this.lblEndDateTime.Location = new System.Drawing.Point(182, 355);
			this.lblEndDateTime.Name = "lblEndDateTime";
			this.lblEndDateTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.lblEndDateTime.Size = new System.Drawing.Size(49, 13);
			this.lblEndDateTime.TabIndex = 10;
			this.lblEndDateTime.Text = "تا تاریخ: ";
			// 
			// lblStartDateTime
			// 
			this.lblStartDateTime.AutoSize = true;
			this.lblStartDateTime.Location = new System.Drawing.Point(182, 328);
			this.lblStartDateTime.Name = "lblStartDateTime";
			this.lblStartDateTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.lblStartDateTime.Size = new System.Drawing.Size(49, 13);
			this.lblStartDateTime.TabIndex = 11;
			this.lblStartDateTime.Text = "از تاریخ: ";
			// 
			// lblRecordsCount
			// 
			this.lblRecordsCount.AutoSize = true;
			this.lblRecordsCount.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.lblRecordsCount.Location = new System.Drawing.Point(785, 486);
			this.lblRecordsCount.Name = "lblRecordsCount";
			this.lblRecordsCount.Size = new System.Drawing.Size(0, 13);
			this.lblRecordsCount.TabIndex = 12;
			// 
			// dtnStartDateTime
			// 
			this.dtnStartDateTime.CustomFormat = "yyyy-MM-dd HH:mm";
			this.dtnStartDateTime.Format = Atf.UI.DateTimeSelectorFormat.Custom;
			this.dtnStartDateTime.Location = new System.Drawing.Point(12, 320);
			this.dtnStartDateTime.Name = "dtnStartDateTime";
			this.dtnStartDateTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.dtnStartDateTime.Size = new System.Drawing.Size(164, 21);
			this.dtnStartDateTime.TabIndex = 13;
			this.dtnStartDateTime.UsePersianFormat = true;
			this.dtnStartDateTime.Value = new System.DateTime(2015, 4, 24, 17, 0, 0, 0);
			// 
			// dtnEndDateTime
			// 
			this.dtnEndDateTime.CustomFormat = "yyyy-MM-dd HH:mm";
			this.dtnEndDateTime.Format = Atf.UI.DateTimeSelectorFormat.Custom;
			this.dtnEndDateTime.Location = new System.Drawing.Point(12, 347);
			this.dtnEndDateTime.Name = "dtnEndDateTime";
			this.dtnEndDateTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.dtnEndDateTime.Size = new System.Drawing.Size(164, 21);
			this.dtnEndDateTime.TabIndex = 14;
			this.dtnEndDateTime.UsePersianFormat = true;
			this.dtnEndDateTime.Value = new System.DateTime(2015, 4, 24, 18, 0, 0, 0);
			// 
			// cbSensor
			// 
			this.cbSensor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSensor.FormattingEnabled = true;
			this.cbSensor.Items.AddRange(new object[] {
            "VR",
            "VS",
            "VT",
            "VR1",
            "VS1",
            "VT1",
            "VR4",
            "VS4",
            "VR2",
            "VS2",
            "VT2",
            "VR3",
            "VS3",
            "VT3",
            "VR5",
            "VS5",
            "IR",
            "IS",
            "IT",
            "IN",
            "IR1",
            "IS1",
            "IT1",
            "IN1",
            "IR2",
            "IS2",
            "IT2",
            "IN2",
            "IR3",
            "IS3",
            "IT3",
            "IN3"});
			this.cbSensor.Location = new System.Drawing.Point(12, 293);
			this.cbSensor.Name = "cbSensor";
			this.cbSensor.Size = new System.Drawing.Size(164, 21);
			this.cbSensor.TabIndex = 2;
			this.cbSensor.SelectedIndexChanged += new System.EventHandler(this.cbSensor_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(182, 301);
			this.label1.Name = "label1";
			this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 15;
			this.label1.Text = "سنسور:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 237);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(210, 50);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.FillWeight = 10F;
			this.dataGridViewTextBoxColumn1.HeaderText = "شماره پست";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.ToolTipText = "شماره پست";
			this.dataGridViewTextBoxColumn1.Width = 90;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.FillWeight = 10F;
			this.dataGridViewTextBoxColumn2.HeaderText = "نام پست";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.ToolTipText = "نام پست";
			this.dataGridViewTextBoxColumn2.Width = 90;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.FillWeight = 10F;
			this.dataGridViewTextBoxColumn3.HeaderText = "شهر پست";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.ToolTipText = "شهر پست";
			this.dataGridViewTextBoxColumn3.Width = 91;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.FillWeight = 10F;
			this.dataGridViewTextBoxColumn4.HeaderText = "ایستگاه پست";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.ToolTipText = "ایستگاه پست";
			this.dataGridViewTextBoxColumn4.Width = 90;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.FillWeight = 50F;
			this.dataGridViewTextBoxColumn5.HeaderText = "توضیح خطا";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.ToolTipText = "توضیح خطا";
			this.dataGridViewTextBoxColumn5.Width = 451;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.FillWeight = 10F;
			this.dataGridViewTextBoxColumn6.HeaderText = "تاریخ زمان";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn6.ToolTipText = "تاریخ زمان";
			this.dataGridViewTextBoxColumn6.Width = 90;
			// 
			// btnReport
			// 
			this.btnReport.Location = new System.Drawing.Point(12, 403);
			this.btnReport.Name = "btnReport";
			this.btnReport.Size = new System.Drawing.Size(216, 23);
			this.btnReport.TabIndex = 16;
			this.btnReport.Text = "نمودار";
			this.btnReport.UseVisualStyleBackColor = true;
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			// 
			// FormReport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(1143, 502);
			this.Controls.Add(this.btnReport);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbSensor);
			this.Controls.Add(this.dtnEndDateTime);
			this.Controls.Add(this.dtnStartDateTime);
			this.Controls.Add(this.lblRecordsCount);
			this.Controls.Add(this.lblStartDateTime);
			this.Controls.Add(this.lblEndDateTime);
			this.Controls.Add(this.btnShow);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.pnlReports);
			this.Controls.Add(this.pnlDevices);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "FormReport";
			this.Text = "گزارش گیری";
			this.Load += new System.EventHandler(this.FormReport_Load);
			this.pnlDevices.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvReports)).EndInit();
			this.pnlReports.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel pnlDevices;
		private TriStateTreeView tvDevices;
		private System.Windows.Forms.DataGridView dgvReports;
		private System.Windows.Forms.Panel pnlReports;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnShow;
		private System.Windows.Forms.Label lblEndDateTime;
		private System.Windows.Forms.Label lblStartDateTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.Label lblRecordsCount;
		private Atf.UI.DateTimeSelector dtnStartDateTime;
		private Atf.UI.DateTimeSelector dtnEndDateTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn DeviceID;
		private System.Windows.Forms.DataGridViewTextBoxColumn DeviceName;
		private System.Windows.Forms.DataGridViewTextBoxColumn DeviceCity;
		private System.Windows.Forms.DataGridViewTextBoxColumn DeviceLocation;
		private System.Windows.Forms.DataGridViewTextBoxColumn Error;
		private System.Windows.Forms.DataGridViewTextBoxColumn DateTime;
		private System.Windows.Forms.ComboBox cbSensor;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnReport;
	}
}