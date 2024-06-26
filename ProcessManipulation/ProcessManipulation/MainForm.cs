﻿using System;
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
using System.IO;
using System.Reflection;
using System.Management;

namespace ProcessManipulation
{
	public partial class MainForm : Form
	{
		List<Process> processes = new List<Process>();
		int counter = 0;
		delegate void ProcessDelegate(Process process);
		string directory;
		public MainForm()
		{
			InitializeComponent();
			directory = Application.StartupPath;
			LoadAvailableAssemblies();
		}
		private void process_Exited(object sender, EventArgs e)
		{
			Process process = sender as Process;
			listBoxStartedAssemblies.Items.Remove(process.ProcessName);
			listBoxAvailableAsseblies.Items.Add(process.ProcessName);
			//MoveItem(listBoxAvailableAsseblies, listBoxStartedAssemblies, process.ProcessName);

			processes.Remove(process);
			counter--;
			for (int i = 0; i < processes.Count; i++)
				SetChildWindowText(process.MainWindowHandle, $"Child process {i + 1}");
		}
		private void process_OutputDataReceived(object sender, EventArgs e)
		{
			Process process = sender as Process;
			SendMessage(process.MainWindowHandle, WM_SETTEXT, (System.IntPtr)0, $"Child process #{processes.Count}");
		}
		void LoadAvailableAssemblies()
		{
			listBoxAvailableAsseblies.Items.Clear();
			//string except = new FileInfo(Application.ExecutablePath).Name.Split('.').First();
			//string[] files = Directory.GetFiles(Application.StartupPath, "*.exe");
			string[] files = Directory.GetFiles(directory, "*.exe");
			for (int i = 0; i < files.Length; i++)
			{
				listBoxAvailableAsseblies.Items.Add(files[i].Split('\\').Last().Split('.').First());
			}
			if (directory == Application.StartupPath)
			{
				string except = new FileInfo(Application.ExecutablePath).Name.Split('.').First();
				listBoxAvailableAsseblies.Items.Remove(except); 
			}
		}
		void RunProcess(string assemblyName)
		{
			Process process = Process.Start(assemblyName);
			processes.Add(process);
			if (Process.GetCurrentProcess().Id == GetParentProcessId(process.Id))
			{
				//MessageBox.Show($"{process.ProcessName} является дочерним процессом");
			}
			process.EnableRaisingEvents = true;
			process.Exited += process_Exited;
			//process.OutputDataReceived += process_OutputDataReceived;
			SendMessage(process.MainWindowHandle, WM_SETTEXT, (System.IntPtr)0, $"Child process #{processes.Count}");
			//SetChildWindowText(process.MainWindowHandle, $"Child process #{processes.Count}");
			listBoxStartedAssemblies.Items.Add(process.ProcessName);
			listBoxAvailableAsseblies.Items.Remove(process.ProcessName);
			//MoveItem(listBoxStartedAssemblies, listBoxAvailableAsseblies);
		}
		void ExecuteOnProcessByName(string name, ProcessDelegate func)
		{
			Process[] processes = Process.GetProcessesByName(name);
			foreach (Process proc in processes)
			{
				if (Process.GetCurrentProcess().Id == GetParentProcessId(proc.Id)) func(proc);
			}
		}
		int GetParentProcessId(int id)
		{
			int parentID;
			using (ManagementObject obj = new ManagementObject("win32_process.handle=" + id.ToString()))
			{
				obj.Get();
				parentID = Convert.ToInt32(obj["ParentProcessId"]);
			}
			return parentID;
		}
		void SetChildWindowText(IntPtr handle, string text)
		{
			SendMessage(handle, WM_SETTEXT, (System.IntPtr)0, text);
		}
		/// <summary>
		/// API function:
		/// </summary>
		const uint WM_SETTEXT = 0x0C;
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hwnd, uint uMsg, IntPtr wParam,
			[MarshalAs(UnmanagedType.LPStr)]string lParam);

		private void buttonStart_Click(object sender, EventArgs e)
		{
			if (listBoxAvailableAsseblies.SelectedItem != null)
				RunProcess(listBoxAvailableAsseblies.SelectedItem.ToString());
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach (Process proc in processes)
			{
				proc.CloseMainWindow();
				proc.Close();
			}
		}
		void Kill(Process process)
		{
			process.Kill();
		}
		private void buttonStop_Click(object sender, EventArgs e)
		{
			if (listBoxStartedAssemblies.SelectedItem != null)
			{
				string selected = listBoxStartedAssemblies.SelectedItem.ToString();
				ExecuteOnProcessByName(selected, Kill);
				listBoxStartedAssemblies.Items.Remove(listBoxStartedAssemblies.SelectedItem);
			}
		}
		private void MoveItem(ListBox dst, ListBox src)
		{
			if (src.SelectedItem != null)
			{
				dst.Items.Add(src.SelectedItem);
				src.Items.Remove(src.SelectedItem);
			}
		}
		private void MoveItem(ListBox dst, ListBox src, string name)
		{
			src.Items.Remove(name);
			dst.Items.Add(name);
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.RootFolder = Environment.SpecialFolder.MyComputer;
			dialog.SelectedPath = directory;
			dialog.ShowDialog();
			directory = dialog.SelectedPath;
			//MessageBox.Show(this, directory, "Selected folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
			LoadAvailableAssemblies();
		}
	}
}
