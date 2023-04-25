namespace SectionnerClient
{
	partial class FormChart
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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.btnClose = new System.Windows.Forms.Button();
			this.chartReport = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.chartReport)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(63, 526);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(475, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "بستن";
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// chartReport
			// 
			this.chartReport.BorderSkin.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;
			this.chartReport.BorderSkin.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.BackwardDiagonal;
			this.chartReport.BorderSkin.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
			this.chartReport.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.FrameThin1;
			chartArea1.Name = "chaReport";
			this.chartReport.ChartAreas.Add(chartArea1);
			this.chartReport.Dock = System.Windows.Forms.DockStyle.Fill;
			legend1.Name = "lReport";
			this.chartReport.Legends.Add(legend1);
			this.chartReport.Location = new System.Drawing.Point(0, 0);
			this.chartReport.Name = "chartReport";
			this.chartReport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			series1.ChartArea = "chaReport";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			series1.Legend = "lReport";
			series1.Name = "sReport";
			series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
			series1.YValuesPerPoint = 6;
			series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
			this.chartReport.Series.Add(series1);
			this.chartReport.Size = new System.Drawing.Size(1211, 508);
			this.chartReport.TabIndex = 2;
			this.chartReport.Text = "نمودار";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.chartReport);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1211, 508);
			this.panel1.TabIndex = 3;
			// 
			// FormChart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(1235, 561);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnClose);
			this.MaximizeBox = false;
			this.Name = "FormChart";
			this.RightToLeftLayout = true;
			this.Text = "نمودار گزارش";
			this.Load += new System.EventHandler(this.FormChart_Load);
			((System.ComponentModel.ISupportInitialize)(this.chartReport)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.DataVisualization.Charting.Chart chartReport;
		private System.Windows.Forms.Panel panel1;
	}
}