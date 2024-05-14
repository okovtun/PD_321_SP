using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProcessesList
{
	public partial class MainForm : Form
	{
		List<Process> proc_list;
		public MainForm()
		{
			InitializeComponent();
			proc_list = new List<Process>();
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			proc_list.Add(new Process());
			proc_list.Last().StartInfo = new ProcessStartInfo(comboBox1.Text);
			proc_list.Last().Start();
			string item = proc_list.Last().ProcessName;
			item += $"(PID:{proc_list.Last().Id})";
			comboBox1.Items.Add(item);
		}

		
	}
}
