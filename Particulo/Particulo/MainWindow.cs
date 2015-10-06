using System;
using Gtk;
using MySql.Data.MySqlClient;

public partial class MainWindow: Gtk.Window
{	
	public MySqlConnection EstablecerConexion(){

		Console.WriteLine ("Main Windows ctor.");

		//crear conexion---------------------------------------------------------
		MySqlConnection mysqlconection = new MySqlConnection("Database=dbprueba;Data Source=localhost;User id=root; Password=sistemas");
		mysqlconection.Open ();
		return mysqlconection;
	}

	public ListStore rellenado(MySqlConnection a){

		//sentencias--------------------------------------------------------------
		MySqlCommand mysqlcomand =a.CreateCommand ();
		mysqlcomand.CommandText = "select id, nombre from articulo";
		MySqlDataReader mysqldatareader = mysqlcomand.ExecuteReader ();



		TreeView.AppendColumn ("id", new CellRendererText(), "text", 0);
		TreeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);

		//modelo:

		ListStore listStore = new ListStore(typeof(String), typeof(String));

		String[] values = new String[2];
		while (mysqldatareader.Read()) {
			values [0] =  mysqldatareader [0].ToString();
			values [1] = mysqldatareader [1].ToString();
			listStore.AppendValues (values);

		}
		//cerrar la conexion
		mysqldatareader.Close ();
		a.Close();


		return listStore;

	}



	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
			//TO DO rellenar List Store
		TreeView.Model = rellenado(EstablecerConexion());


	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
