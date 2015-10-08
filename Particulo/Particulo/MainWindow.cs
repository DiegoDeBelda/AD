using System;
using System.Collections.Generic;
using Gtk;
using MySql.Data.MySqlClient;
using System.Data;

public partial class MainWindow: Gtk.Window
{	
	public IDbConnection EstablecerConexion(){

		Console.WriteLine ("Main Windows ctor.");

		//crear conexion---------------------------------------------------------
		IDbConnection dbconection = new MySqlConnection("Database=dbprueba;Data Source=localhost;User id=root; Password=sistemas");
		dbconection.Open ();
		return dbconection;
	}



	public ListStore rellenado(IDbConnection a){

		//sentencias--------------------------------------------------------------
		IDbCommand dbcomand =a.CreateCommand ();
		dbcomand.CommandText = "select * from articulo";
		IDataReader datareader = dbcomand.ExecuteReader ();



//		TreeView.AppendColumn ("id", new CellRendererText(), "text", 0);
//		TreeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);

		String[] ColumNames = getColumnames (datareader);
		for (int i=0; i<ColumNames.Length; i++) {
			TreeView.AppendColumn (ColumNames [i], new CellRendererText (), "text", i);
		}

		//modelo:

//		ListStore listStore = new ListStore(typeof(String), typeof(String));
		Type[] types = getTypes (datareader.FieldCount);
		ListStore listStore = new ListStore(types);

	
		while (datareader.Read()) {
			string[] values = getValues(datareader);
			listStore.AppendValues (values);
		}

		//cerrar la conexion
		datareader.Close ();
		a.Close();


		return listStore;

	}

	private String[] getColumnames (IDataReader datareader){
		List<string> columnames = new List<string>();
		int count = datareader.FieldCount;
		for (int i=0; i<count; i++) {
			columnames.Add (datareader.GetName (i));
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

	private string[] getValues(IDataReader datareader){
		int count = datareader.FieldCount;
		List<string> values = new List<string> ();
		for (int i=0; i<count; i++) {
			values.Add (datareader [i].ToString ());
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
