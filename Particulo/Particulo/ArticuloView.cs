using System;
using SerpisAD;
using System.Collections;
using Gtk;
using System.Data;
namespace Particulo
{
	public delegate void SaveDelegate();
	public partial class ArticuloView : Gtk.Window
	{

		private object id=null;
		private object categoria=null;
		private string nombre="";
		private decimal precio=0;
		private SaveDelegate save;

		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{ 
			init ();
			save = insert;
		}

		public void init(){
			this.Build ();
			entry1.Text =nombre;
			QueryResult query = PersisterHelper.Get ("Select * from categoria");
			ComboBoxHelper.Fill (combobox1, query, categoria);
			spinbutton1.Value = Convert.ToDouble (precio);
			saveAction.Activated += delegate { save (); };

		}

		public ArticuloView(object id) : base(WindowType.Toplevel){
			this.id = id;
			load();
			init ();
			save = update;
		}

		private void load(){
		
			IDbCommand dbcommand = APP.Instance.Dbconection.CreateCommand();
			dbcommand.CommandText = "select * from articulo where id=@id";
			DbCommandHelper.AddParameter (dbcommand, "id", id);
			IDataReader datareader = dbcommand.ExecuteReader ();
			if (!datareader.Read ()) {
				string excepcion = "ha habido un problema en la lectura de datos";
				Console.Write( excepcion);
				datareader.Close ();
			}
			nombre = (string)datareader ["nombre"];
			categoria = (object)datareader ["categoria"];
			if (categoria is DBNull)
				categoria = null;
			precio = (decimal)datareader ["precio"];
			datareader.Close ();
		}
		//boton guardar
//		private void save(){
//			if (id == null)
//				insert ();
//			else
//				update();
//		}

		private void insert(){
			IDbCommand dbcommand = APP.Instance.Dbconection.CreateCommand ();
			dbcommand.CommandText = "insert into articulo (nombre, categoria, precio)" +
				"values (@nombre, @categoria, @precio)";

			nombre = entry1.Text;
			categoria = ComboBoxHelper.GetId(combobox1);
			precio = Convert.ToDecimal (spinbutton1.Value);

			DbCommandHelper.AddParameter (dbcommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbcommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbcommand, "precio", precio);
			dbcommand.ExecuteNonQuery ();
			Destroy ();
		
		}
		private void update(){
			IDbCommand dbcommand = APP.Instance.Dbconection.CreateCommand ();
			dbcommand.CommandText = "update articulo set nombre=@nombre, categoria=@categoria, precio=@precio where id=@id";
			nombre = entry1.Text;
			categoria = ComboBoxHelper.GetId(combobox1);
			precio = Convert.ToDecimal (spinbutton1.Value);

			DbCommandHelper.AddParameter (dbcommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbcommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbcommand, "precio", precio);
			DbCommandHelper.AddParameter (dbcommand, "id", id);
			dbcommand.ExecuteNonQuery ();
			Destroy ();

		}

		private static void addParameter(IDbCommand dbcommand,string name, object value){
			IDbDataParameter dbdataparameter = dbcommand.CreateParameter ();
			dbdataparameter.ParameterName = name;
			dbdataparameter.Value = value;
			dbcommand.Parameters.Add (dbdataparameter);
		}



	}
}

