using System;
using Gtk;
using SerpisAD;
using MySql.Data.MySqlClient;


namespace Pcategoria
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			APP.Instance.Dbconection= new MySqlConnection (
				"Database=dbprueba;Data Source=localhost;User id=root; Password=sistemas");

			APP.Instance.Dbconection.Open();
			String consulta="";
			//while (consulta==null) {
			System.Console.Write ("introduce la consulta: ");

			consulta = Console.ReadLine ();
			//	}

			Application.Init ();
			MainWindow win = new MainWindow (consulta);
			win.Show ();
			Application.Run ();
			APP.Instance.Dbconection.Close();
		}
	}
}
