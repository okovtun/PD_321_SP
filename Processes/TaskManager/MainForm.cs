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

namespace TaskManager
{
	public partial class MainForm : Form
	{
		List<Process> processes;
		public MainForm()
		{
			InitializeComponent();
			listViewProcesses.Columns.Add("Name");
			listViewProcesses.Columns.Add("PID");
			processes = Process.GetProcesses().OfType<Process>().ToList();
			UpdateListView();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (!processes.SequenceEqual(Process.GetProcesses().OfType<Process>().ToList()))
				UpdateListView();

			this.processes = Process.GetProcesses().OfType<Process>().ToList();
			statusStrip.Items["toolStripStatusLabelProcesses"].Text = $"Total processes: {processes.Count}, displayed processes {listViewProcesses.Items.Count}";
		}
		void UpdateListView()
		{
			//this.processes = Process.GetProcesses().OfType<Process>().ToList();
			//listViewProcesses.Items.
			//listViewProcesses.DataBindings.Add("Items", this.processes, "processes");
			//listViewProcesses.BeginUpdate();
			//listViewProcesses.Items.Clear();
			//for (int i = 0; i < processes.Count; i++)
			//{
			//	listViewProcesses.Items.Add(processes[i].ProcessName);
			//	listViewProcesses.Items[i].SubItems.Add(processes[i].Id.ToString());
			//}
			//listViewProcesses.EndUpdate();
		}
	}
}
