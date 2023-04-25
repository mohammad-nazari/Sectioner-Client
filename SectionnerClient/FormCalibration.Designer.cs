namespace SectionnerClient
{
	partial class FormCalibration
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.btnClose = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.gbSensors = new System.Windows.Forms.GroupBox();
			this.dgvSensors = new System.Windows.Forms.DataGridView();
			this.btnSubmit = new System.Windows.Forms.Button();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SensorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SensorMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SensorMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SensorZero = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SensorSpan = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel1.SuspendLayout();
			this.gbSensors.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvSensors)).BeginInit();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(514, 294);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(119, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "بستن";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.gbSensors);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(624, 276);
			this.panel1.TabIndex = 3;
			// 
			// gbSensors
			// 
			this.gbSensors.Controls.Add(this.dgvSensors);
			this.gbSensors.Location = new System.Drawing.Point(3, 3);
			this.gbSensors.Name = "gbSensors";
			this.gbSensors.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.gbSensors.Size = new System.Drawing.Size(618, 270);
			this.gbSensors.TabIndex = 8;
			this.gbSensors.TabStop = false;
			this.gbSensors.Text = "سنسورها";
			// 
			// dgvSensors
			// 
			this.dgvSensors.AllowUserToAddRows = false;
			this.dgvSensors.AllowUserToDeleteRows = false;
			this.dgvSensors.AllowUserToResizeColumns = false;
			this.dgvSensors.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvSensors.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvSensors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvSensors.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.dgvSensors.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dgvSensors.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvSensors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvSensors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvSensors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SensorName,
            this.SensorMin,
            this.SensorMax,
            this.SensorZero,
            this.SensorSpan});
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvSensors.DefaultCellStyle = dataGridViewCellStyle7;
			this.dgvSensors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvSensors.Location = new System.Drawing.Point(3, 16);
			this.dgvSensors.Name = "dgvSensors";
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvSensors.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
			this.dgvSensors.RowHeadersVisible = false;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvSensors.RowsDefaultCellStyle = dataGridViewCellStyle9;
			this.dgvSensors.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dgvSensors.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvSensors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvSensors.Size = new System.Drawing.Size(612, 251);
			this.dgvSensors.TabIndex = 0;
			// 
			// btnSubmit
			// 
			this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnSubmit.Location = new System.Drawing.Point(18, 294);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(348, 23);
			this.btnSubmit.TabIndex = 4;
			this.btnSubmit.Text = "ثبت";
			this.btnSubmit.UseVisualStyleBackColor = true;
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn1.FillWeight = 40F;
			this.dataGridViewTextBoxColumn1.HeaderText = "نام سنسور";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn1.ToolTipText = "نام سنسور";
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle10.Format = "N0";
			dataGridViewCellStyle10.NullValue = null;
			this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle10;
			this.dataGridViewTextBoxColumn2.FillWeight = 15F;
			this.dataGridViewTextBoxColumn2.HeaderText = "کمینه";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn2.ToolTipText = "کمینه";
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle11.Format = "N0";
			this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle11;
			this.dataGridViewTextBoxColumn3.FillWeight = 15F;
			this.dataGridViewTextBoxColumn3.HeaderText = "بیشینه";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn3.ToolTipText = "بیشینه";
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle12.Format = "N0";
			this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle12;
			this.dataGridViewTextBoxColumn4.FillWeight = 15F;
			this.dataGridViewTextBoxColumn4.HeaderText = "مقدار معادل کمینه";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn4.ToolTipText = "مقدار معادل کمینه";
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle13.Format = "N0";
			this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle13;
			this.dataGridViewTextBoxColumn5.FillWeight = 15F;
			this.dataGridViewTextBoxColumn5.HeaderText = "مقدار معادل بیشینه";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn5.ToolTipText = "مقدار معادل بیشینه";
			// 
			// SensorName
			// 
			this.SensorName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.SensorName.FillWeight = 40F;
			this.SensorName.HeaderText = "نام سنسور";
			this.SensorName.Name = "SensorName";
			this.SensorName.ReadOnly = true;
			this.SensorName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.SensorName.ToolTipText = "نام سنسور";
			// 
			// SensorMin
			// 
			this.SensorMin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle3.Format = "N0";
			dataGridViewCellStyle3.NullValue = null;
			this.SensorMin.DefaultCellStyle = dataGridViewCellStyle3;
			this.SensorMin.FillWeight = 15F;
			this.SensorMin.HeaderText = "کمینه";
			this.SensorMin.Name = "SensorMin";
			this.SensorMin.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.SensorMin.ToolTipText = "کمینه";
			// 
			// SensorMax
			// 
			this.SensorMax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle4.Format = "N0";
			this.SensorMax.DefaultCellStyle = dataGridViewCellStyle4;
			this.SensorMax.FillWeight = 15F;
			this.SensorMax.HeaderText = "بیشینه";
			this.SensorMax.Name = "SensorMax";
			this.SensorMax.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.SensorMax.ToolTipText = "بیشینه";
			// 
			// SensorZero
			// 
			this.SensorZero.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle5.Format = "N0";
			this.SensorZero.DefaultCellStyle = dataGridViewCellStyle5;
			this.SensorZero.FillWeight = 15F;
			this.SensorZero.HeaderText = "مقدار معادل کمینه";
			this.SensorZero.Name = "SensorZero";
			this.SensorZero.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.SensorZero.ToolTipText = "مقدار معادل کمینه";
			// 
			// SensorSpan
			// 
			this.SensorSpan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle6.Format = "N0";
			this.SensorSpan.DefaultCellStyle = dataGridViewCellStyle6;
			this.SensorSpan.FillWeight = 15F;
			this.SensorSpan.HeaderText = "مقدار معادل بیشینه";
			this.SensorSpan.Name = "SensorSpan";
			this.SensorSpan.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.SensorSpan.ToolTipText = "مقدار معادل بیشینه";
			// 
			// FormCalibration
			// 
			this.AcceptButton = this.btnSubmit;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(648, 325);
			this.Controls.Add(this.btnSubmit);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnClose);
			this.MaximizeBox = false;
			this.Name = "FormCalibration";
			this.RightToLeftLayout = true;
			this.Text = "کالیبره کردن سنسورها";
			this.Load += new System.EventHandler(this.FormCalibration_Load);
			this.panel1.ResumeLayout(false);
			this.gbSensors.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvSensors)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox gbSensors;
		private System.Windows.Forms.Button btnSubmit;
		public System.Windows.Forms.DataGridView dgvSensors;
		private System.Windows.Forms.DataGridViewTextBoxColumn SensorName;
		private System.Windows.Forms.DataGridViewTextBoxColumn SensorMin;
		private System.Windows.Forms.DataGridViewTextBoxColumn SensorMax;
		private System.Windows.Forms.DataGridViewTextBoxColumn SensorZero;
		private System.Windows.Forms.DataGridViewTextBoxColumn SensorSpan;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
	}
}