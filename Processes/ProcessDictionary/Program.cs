using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProcessDictionary
{
	class Program
	{
		static void Main(string[] args)
		{
			Process[] a_processes = Process.GetProcesses();
			//https://www.c-sharpcorner.com/UploadFile/219d4d/how-to-convert-an-array-into-a-generic-type-in-C-Sharp/
			Dictionary<int, Process> d_processes = a_processes.ToDictionary(key => key.Id, process => process);
			//Dictionary<int, Process> processes = Process.GetProcesses().ToDictionary();
			Console.WriteLine(d_processes.GetType());
			//d_processes.
			for (int i = 0; i < d_processes.Count; i++)
			{
				KeyValuePair<int, Process> process = d_processes.ElementAt(i);
				Console.WriteLine($"{process.Key}\t{process.Value}");
			}
		}
	}
}
