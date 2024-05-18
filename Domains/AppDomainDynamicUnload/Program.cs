using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace AppDomainDynamicUnload
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(Directory.GetCurrentDirectory());
			string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.DLL");
			foreach (string file in files)
			{
				Console.WriteLine(file);
			}
			//1) Создаем домен прилодения:
			AppDomain domain = AppDomain.CreateDomain("ConsoleDomain");
			//2) Загружаем DLL в этот домен:
			Assembly asm = domain.Load(AssemblyName.GetAssemblyName(files[0]));
			//3) Получаем модуль, из которого будем выполнять вызов:
			Module module = asm.GetModule("SampleLibrary.dll");
			//4) Получаем класс из DLL-библиотеки
			Type type = module.GetType("SampleLibrary.SampleClass");
			//5) Вытаскиваем из класса метод, который будем вызывать:
			MethodInfo method = type.GetMethod("Hello");
			//6) Вызываем метод:
			method.Invoke(null, null);

			AppDomain.Unload(domain);
		}
	}
}
