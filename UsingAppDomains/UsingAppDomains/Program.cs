using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace UsingAppDomains
{
	static class Program
	{
		static AppDomain drawerDomain;
		static AppDomain textWindowDomain;

		static Assembly drawerASM;
		static Assembly textWindowASM;

		static Form DrawerWindowForm;
		static Form TextWindowForm;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		[LoaderOptimization(LoaderOptimization.MultiDomain)]
		static void Main()
		{
			Application.EnableVisualStyles();
			//Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new Form1());

			drawerDomain = AppDomain.CreateDomain("Drawer");
			textWindowDomain = AppDomain.CreateDomain("TextWindow");

			drawerASM = drawerDomain.Load(AssemblyName.GetAssemblyName("TextDrawer.exe"));
			textWindowASM = textWindowDomain.Load(AssemblyName.GetAssemblyName("TextWindow.exe"));

			DrawerWindowForm = Activator.CreateInstance(drawerASM.GetType("TextDrawer.MainForm")) as Form;
			TextWindowForm = Activator.CreateInstance(textWindowASM.GetType("TextWindow.MainForm"), 
				new object[] { drawerASM.GetModule("TextDrawer.exe"), DrawerWindowForm }) as Form;

			(new Thread(new ThreadStart(RunVisualizer))).Start();
			(new Thread(new ThreadStart(RunDrawer))).Start();

			drawerDomain.DomainUnload += new EventHandler(drawer_DomainUnload);
		}

		static void drawer_DomainUnload(object sender, EventArgs e)
		{
			MessageBox.Show($"Domain {(sender as AppDomain).FriendlyName} was unloaded.");
		}

		static void RunDrawer()
		{
			DrawerWindowForm.ShowDialog();
			AppDomain.Unload(drawerDomain);
		}
		static void RunVisualizer()
		{
			TextWindowForm.ShowDialog();
			Application.Exit();
		}
	}
}
