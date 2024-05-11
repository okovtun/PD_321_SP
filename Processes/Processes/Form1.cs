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
using System.Threading;

namespace Processes
{
	public partial class MainForm : Form
	{
		//Process process;
		public MainForm()
		{
			InitializeComponent();
			richTextBox1.Text = "mspaint";
			richTextBox1.SelectAll();
			richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
			SetControls(true);
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			//process = new Process();
			process.StartInfo = new System.Diagnostics.ProcessStartInfo(richTextBox1.Text);
			process.Start();
			SetControls(false);
			//labelInfo.Text = SetLableProcessInfo();
		}
		private void process_Exited(object sender, EventArgs e)
		{
			SetControls(true);
			process.Close();
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			process.CloseMainWindow();
			process.Close();
			SetControls(true);
		}
		private void SetControls(bool enableControls)
		{
			richTextBox1.Enabled = enableControls;
			buttonStart.Enabled = enableControls;
			buttonStop.Enabled = !enableControls;
			timer1.Enabled = !enableControls;
			labelInfo.Text = "Process exited";
		}
		private string SetLableProcessInfo()
		{
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
			labelInfo.Text = SetLableProcessInfo();
		}
		private int GetCpuTime()
		{
			int time;
			TimeSpan userTime = process.UserProcessorTime + process.PrivilegedProcessorTime;
			System
			//TimeSpan systemTime = 
			return time;
		}
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr handle);
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr handle);
	}
}
