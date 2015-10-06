using System;
using System.Collections.Generic;
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
		mysqlcomand.CommandText = "select * from articulo";
		MySqlDataReader mysqldatareader = mysqlcomand.ExecuteReader ();



//		TreeView.AppendColumn ("id", new CellRendererText(), "text", 0);
//		TreeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);

		String[] ColumNames = getColumnames (mysqldatareader);
		for (int i=0; i<ColumNames.Length; i++) {
			TreeView.AppendColumn (ColumNames [i], new CellRendererText (), "text", i);
		}

		//modelo:

//		ListStore listStore = new ListStore(typeof(String), typeof(String));
		Type[] types = getTypes (mysqldatareader.FieldCount);
		ListStore listStore = new ListStore(types);

	
		while (mysqldatareader.Read()) {
			string[] values = getValues(mysqldatareader);
			listStore.AppendValues (values);
		}

		//cerrar la conexion
		mysqldatareader.Close ();
		a.Close();


		return listStore;

	}

	private String[] getColumnames (MySqlDataReader mysqldatareader){
		List<string> columnames = new List<string>();
		int count = mysqldatareader.FieldCount;
		for (int i=0; i<count; i++) {
			columnames.Add (mysqldatareader.GetName (i));
		}
		return columnames.ToArray ();
	
	}

	private Type[] getTypes(int count){
		List<Type> types = new List<Type> ();
		for (int i=0; i<count; i++) {
			types.Add (typeof(string));
		
		}
		return types.ToArray ();
	}

	private string[] getValues(MySqlDataReader mysqldatareader){
		int count = mysqldatareader.FieldCount;
		List<string> values = new List<string> ();
		for (int i=0; i<count; i++) {
			values.Add (mysqldatareader [i].ToString ());
		}
		return values.ToArray ();
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
