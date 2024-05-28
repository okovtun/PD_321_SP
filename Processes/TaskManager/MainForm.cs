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
		//Process[] a_processes;
		Dictionary<int, Process> d_processes;
		int ramFactor = 1024;
		public MainForm()
		{
			InitializeComponent();
			listViewProcesses.Columns.Add("PID");
			listViewProcesses.Columns.Add("Name");
			listViewProcesses.Columns.Add("WorkingSet");
			listViewProcesses.Columns.Add("PrivateSet");
			processes = Process.GetProcesses().OfType<Process>().ToList();
			LoadProcesses();
			RemoveClosedProcesses();

			AllocConsole();
			Console.WriteLine("Debug");
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

			Dictionary<int, Process> d_processes = Process.GetProcesses().ToDictionary(key => key.Id, process => process);
			this.d_processes = d_processes;
			for (int i = 0; i < d_processes.Count; i++)
			{
				KeyValuePair<int, Process> pair = d_processes.ElementAt(i);
				listViewProcesses.Items.Add(pair.Key.ToString());
				listViewProcesses.Items[i].SubItems.Add(pair.Value.ProcessName);
				listViewProcesses.Items[i].SubItems.Add((pair.Value.WorkingSet64/ramFactor).ToString());
				listViewProcesses.Items[i].SubItems.Add((pair.Value.PrivateMemorySize64/ramFactor).ToString());
			}
		}
		void UpdateListView()
		{
			Dictionary<int, Process> d_processes = Process.GetProcesses().ToDictionary(key => key.Id, process => process);
			if (this.d_processes.SequenceEqual(d_processes)) return;
			//if (processes.SequenceEqual(Process.GetProcesses().OfType<Process>().ToList())) return;
			//this.processes = Process.GetProcesses().OfType<Process>().ToList();
			this.d_processes = d_processes;

			listViewProcesses.BeginUpdate();
			RemoveClosedProcesses();
			AddNewProcesses();
			UpdateExistingProcesses();
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
				Console.Write(Convert.ToInt32(listViewProcesses.Items[i].Text) + "\t");
				if (!d_processes.ContainsKey(Convert.ToInt32(listViewProcesses.Items[i].Text)))
				{
					listViewProcesses.Items.RemoveAt(i);
				}
			}
		}
		void AddNewProcesses()
		{
			//AllocConsole();
			//Console.WriteLine();
			//Console.Clear();
			for (int i = 0; i < d_processes.Count; i++)
			{
				KeyValuePair<int, Process> pair = d_processes.ElementAt(i);
				if (listViewProcesses.FindItemWithText(pair.Key.ToString()) == null)
				{
					ListViewItem item = new ListViewItem(pair.Key.ToString());
					item.SubItems.Add(pair.Value.ProcessName);
					item.SubItems.Add((pair.Value.WorkingSet64/ramFactor).ToString());
					item.SubItems.Add((pair.Value.PrivateMemorySize64/ramFactor).ToString());

					listViewProcesses.Items.Add(item);
					//Console.Write(pair.Key + "\t");
				}
			}

			//foreach (ListViewItem i in listViewProcesses.Items)
			//{
			//	Console.Write(i.Text);
			//}
		}
		void UpdateExistingProcesses()
		{
			for (int i = 0; i < listViewProcesses.Items.Count; i++)
			{
				//Console.Write($"{listViewProcesses.Items[i].Text}\t");
				int PID = Convert.ToInt32(listViewProcesses.Items[i].Text);
				long workingSet = d_processes[PID].WorkingSet64/ramFactor;
				long privateSet = d_processes[PID].PrivateMemorySize64/ramFactor;
				listViewProcesses.Items[i].SubItems[2].Text = workingSet.ToString();
				listViewProcesses.Items[i].SubItems[3].Text = privateSet.ToString();
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
