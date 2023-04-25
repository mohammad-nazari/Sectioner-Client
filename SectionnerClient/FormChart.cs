using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SectionnerClient
{
	public partial class FormChart : Form
	{
		private DataGridView _dgv;
		public System.Windows.Forms.DataGridView Dgv
		{
			get
			{
				return _dgv;
			}
			set
			{
				_dgv = value;
			}
		}
		public FormChart()
		{
			InitializeComponent();
		}

		private void FormChart_Load(object sender, EventArgs e)
		{
			foreach(DataGridViewRow row in this.Dgv.Rows)
			{
				this.chartReport.Series[0].Points.AddXY(Convert.ToDateTime(row.Cells["DateTime"].Value), Convert.ToDouble(row.Cells["Error"].Value));
			}
		}
	}
}
