using System;
using Gtk;

namespace Particulo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			String consulta="";
			//while (consulta==null) {
			System.Console.Write ("introduce la consulta");
			consulta = Console.ReadLine ();
			//	}

			Application.Init ();
			MainWindow win = new MainWindow (consulta);
			win.Show ();
			Application.Run ();
		}
	}
}
