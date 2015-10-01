using System;
using Gtk;
using MySql.Data.MySqlClient;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Console.WriteLine ("Main Windows ctor.");
		//crear conexion---------------------------------------------------------
		MySqlConnection mysqlconection = new MySqlConnection(
			"Database=dbprueba;Data Source=localhost;User id=root; Password=sistemas");
		//------------------------------------------------------------------------
		mysqlconection.Open ();
		//sentencias--------------------------------------------------------------
		MySqlCommand mysqlcomand = mysqlconection.CreateCommand ();
		mysqlcomand.CommandText = "select id, nombre from articulo";

		MySqlDataReader mysqldatareader = mysqlcomand.ExecuteReader ();

//		while (mysqldatareader.Read()) {
//			Console.WriteLine ("id={0} nombre={1}", mysqldatareader [0], mysqldatareader [1]);
//		
//		}


		TreeView.AppendColumn ("id", new CellRendererText(), "text", 0);
		TreeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);

		//modelo:

		ListStore listStore = new ListStore(typeof(String), typeof(String));
//		listStore.AppendValues ("1", "nombre del primero");
//		listStore.AppendValues ("2", "nombre del segundo");

		String[] values = new String[2];
		while (mysqldatareader.Read()) {
			values [0] =  mysqldatareader [0].ToString();
			values [1] = mysqldatareader [1].ToString();
			listStore.AppendValues (values);
		
		}
		//TO DO rellenar List Store
		TreeView.Model = listStore;

		//cerrar conexion
		mysqldatareader.Close ();
		mysqlconection.Close ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
