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
using System.Security.Principal;

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
			proc_list.Last().EnableRaisingEvents = true;
			//proc_list.Last().Exited += new EventHandler(this.process_Exited);
			proc_list.Last().Exited += proc_close_handler;
			string item = proc_list.Last().ProcessName;
			item += $"(PID:{proc_list.Last().Id})";
			comboBox1.Items.Add(item);
		}

		private string SetLableProcessInfo(int i)
		{
			Process process = proc_list[i];
			process.Refresh();
			//if (!process.Responding) return "No active process";
			string info = "Process info:\n";
			info += $"ProcName:\t{process.ProcessName}\n";
			info += $"PID:\t{process.Id.ToString()}\n";
			info += $"Name:\t{process.ProcessName}\n";
			info += $"Priority:\t{process.PriorityClass} ({process.BasePriority})\n";
			info += $"Handle count:\t{process.HandleCount}\n";
			info += $"CPU time:\t{process.PrivilegedProcessorTime}\n";
			info += $"SessionID:\t{process.SessionId}\n";
			info += $"Filename:\t{process.MainModule.FileName}\n";

			IntPtr handle = IntPtr.Zero;
			OpenProcessToken(process.Handle, 8, out handle);
			WindowsIdentity wi = new WindowsIdentity(handle);

			info += $"Username:\t{wi.Name}\n";
			info += $"WorkDir:\t{process.StartInfo.WorkingDirectory}\n";
			info += $"Start time:\t{process.StartTime}\n";
			info += $"Start time:\t{process.Threads.Count}\n";

			info += $"\nCPU:\n";
			info += $"Total CPU time:\t{process.TotalProcessorTime.ToString()}\n";
			info += $"User CPU time:\t{process.UserProcessorTime}\n";
			info += $"Privilaged CPU time:\t{process.PrivilegedProcessorTime}\n";

			//info += $"CPU usage:\t{}\n";

			info += $"\nMemory:\n";
			info += $"Working set:{process.WorkingSet64.ToString("N")}\n";
			info += $"Virtual memory:{process.VirtualMemorySize64.ToString("N")}\n";
			info += $"Private memory:{(process.PrivateMemorySize64 / 1024).ToString("N")}\n";
			return info;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			//labelInfo.Text = sender.GetType().ToString();
			if (comboBox1.SelectedIndex >= 0)
				labelInfo.Text = SetLableProcessInfo(comboBox1.SelectedIndex);
		}
		private void process_Exited(object sender, EventArgs e)
		{
			int pid = ((Process)sender).Id;
			for (int i = 0; i < proc_list.Count; i++)
			{
				if (pid == proc_list[i].Id)
				//if(((Process)sender) == proc_list[i])
				{
					if (i == comboBox1.SelectedIndex) labelInfo.Text = "Process closed";
					proc_list[i].Close();
					proc_list.RemoveAt(i);
					comboBox1.Items.RemoveAt(i);
					if (comboBox1.Items.Count == 0) comboBox1.Text = "No processes started";
				}
			}
		}
		private void Form1_Closing(object sender, FormClosingEventArgs e)
		{
			foreach (Process i in proc_list)
			{
				i.CloseMainWindow();
				i.Close();
			}
		}
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr handle);
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr handle);
	}
}
