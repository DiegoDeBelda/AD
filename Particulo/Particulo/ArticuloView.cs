using System;
using SerpisAD;
using System.Collections;
using Gtk;
using System.Data;
namespace Particulo
{
	public partial class ArticuloView : Gtk.Window
	{

		public ArticuloView () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			entry1.Text = "nuevo";
			spinbutton1.Value = 1.5;

			QueryResult query = PersisterHelper.Get ("Select * from categoria");
			ComboBoxHelper.Fill (combobox1, query);
			//boton guardar
			saveAction.Activated += delegate {save();};

			Destroy ();





		}
		//boton guardar
		private void save(){
			IDbCommand dbcommand = APP.Instance.Dbconection.CreateCommand ();
			dbcommand.CommandText = "insert into articulo (nombre, categoria, precio)" +
				"values (@nombre, @categoria, @precio)";

			string nombre = entry1.Text;
			object categoria = GetId(combobox1);
			decimal precio = Convert.ToDecimal (spinbutton1.Value);

			addParameter (dbcommand, "nombre", nombre);
			addParameter (dbcommand, "categoria", categoria);
			addParameter (dbcommand, "precio", precio);
		
			dbcommand.ExecuteNonQuery ();
		}

		private static void addParameter(IDbCommand dbcommand,string name, object value){
			IDbDataParameter dbdataparameter = dbcommand.CreateParameter ();
			dbdataparameter.ParameterName = name;
			dbdataparameter.Value = value;
			dbcommand.Parameters.Add (dbdataparameter);
		}

		public static object GetId(ComboBox combobox){
			TreeIter treeiter;
			combobox.GetActiveIter (out treeiter);
			IList row = (IList)combobox.Model.GetValue (treeiter, 0);
			if (row == null)
				return null;
			return row [0];
		}

	}
}

