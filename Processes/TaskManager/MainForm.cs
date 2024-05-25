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
using System.Runtime.InteropServices;

namespace TaskManager
{
	public partial class MainForm : Form
	{
		List<Process> processes;
		Process[] a_processes;
		Dictionary<int, Process> d_processes;
		public MainForm()
		{
			InitializeComponent();
			listViewProcesses.Columns.Add("PID");
			listViewProcesses.Columns.Add("Name");
			processes = Process.GetProcesses().OfType<Process>().ToList();
			LoadProcesses();
			RemoveClosedProcesses();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (!processes.SequenceEqual(Process.GetProcesses().OfType<Process>().ToList()))
				UpdateListView();

			this.processes = Process.GetProcesses().OfType<Process>().ToList();
			statusStrip.Items["toolStripStatusLabelProcesses"].Text = $"Total processes: {processes.Count}, displayed processes {listViewProcesses.Items.Count}";
		}
		void LoadProcesses()
		{
			listViewProcesses.Items.Clear();
			//processes = Process.GetProcesses().ToList();
			//for (int i = 0; i < processes.Count; i++)
			//{
			//	listViewProcesses.Items.Add(processes[i].ProcessName);
			//	listViewProcesses.Items[i].SubItems.Add(processes[i].Id.ToString());
			//}

			Dictionary<int, Process> d_processes = Process.GetProcesses().ToDictionary(key => key.Id, process => process);
			this.d_processes = d_processes;
			for (int i = 0; i < d_processes.Count; i++)
			{
				KeyValuePair<int, Process> pair = d_processes.ElementAt(i);
				listViewProcesses.Items.Add(pair.Key.ToString());
				listViewProcesses.Items[i].SubItems.Add(pair.Value.ProcessName);
			}
		}
		void UpdateListView()
		{
			if (processes.SequenceEqual(Process.GetProcesses().OfType<Process>().ToList()))
				return;
			this.processes = Process.GetProcesses().OfType<Process>().ToList();
			Dictionary<int, Process> d_processes = Process.GetProcesses().ToDictionary(key => key.Id, process => process);
			this.d_processes = d_processes;
			//listViewProcesses.Items.ContainsKey
			//listViewProcesses.Items.
			//listViewProcesses.DataBindings.Add("Items", this.processes, "processes");
			listViewProcesses.BeginUpdate();
			//listViewProcesses.Items.Clear();

			//for (int i = 0; i < processes.Count; i++)
			//{
			//	listViewProcesses.Items[i].Text = (processes[i].ProcessName);
			//	listViewProcesses.Items[i].SubItems[1].Text = (processes[i].Id.ToString());
			//}
			RemoveClosedProcesses();
			AddNewProcesses();

			listViewProcesses.EndUpdate();
			//Dictionary
		}
		void RemoveClosedProcesses()
		{
			//AllocConsole();
			for (int i = 0; i < listViewProcesses.Items.Count; i++)
			{
				//Console.Clear();
				string item_name = listViewProcesses.Items[i].Name;
				Console.WriteLine(item_name);
				//Console.Write(Convert.ToInt32(listViewProcesses.Items[i].Text) + "\t");
				if (!d_processes.ContainsKey(Convert.ToInt32(listViewProcesses.Items[i].Text)))
				{
					listViewProcesses.Items.RemoveAt(i);
				}
			}
		}
		void AddNewProcesses()
		{
			//AllocConsole();
			//Console.Clear();
			for (int i = 0; i < d_processes.Count; i++)
			{
				KeyValuePair<int, Process> pair = d_processes.ElementAt(i);

				if (listViewProcesses.Items.ContainsKey(pair.Key.ToString()))
				{
					//ListViewItem item = new ListViewItem(pair.Key.ToString());
					//item.SubItems.Add(pair.Value.ProcessName);

					//listViewProcesses.Items.Add(item);
					//Console.Write(pair.Key + "\t");
				}
			}
		}
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool AllocConsole();

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool FreeConsole();
	}
}
