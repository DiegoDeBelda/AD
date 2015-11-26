using System;
using SerpisAD;
using System.Collections;
using Gtk;
using System.Data;
 
namespace Particulo
{
	public class ArticuloPersister
	{
		public ArticuloPersister ()
		{
		}
		public static object id=null;
		public static object categoria=null;
		public static object nombre=null;
		public static decimal precio=0;

		public static Articulo load(object id){
			//pasar por parametro un id
			//crear un objeto articulo y devolverlo con la lectura de datos
			Articulo articulo = new Articulo();

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

			//asignamiento de la lectura al objeto
			articulo.Id = id;
			articulo.Nombre = nombre;
			articulo.Categoria = categoria;
			articulo.Precio = precio;
			//cerrar lectura de datos
			datareader.Close ();
			//devolucion
			return articulo;
		}

		public static void Insert(Articulo onjecto){
			//en la clase articulo hay un objeto el cual recoge el return de load(id)
			//este metodo tiene como parametro dicho objeto
			IDbCommand dbcommand = APP.Instance.Dbconection.CreateCommand ();
			dbcommand.CommandText = "insert into articulo (nombre, ategoria, precio)" +
				"values (@nombre, @categoria, @precio)";


			DbCommandHelper.AddParameter (dbcommand, "nombre", onjecto.Nombre);
			DbCommandHelper.AddParameter (dbcommand, "categoria",onjecto.Categoria);
			DbCommandHelper.AddParameter (dbcommand, "precio", Convert.ToDecimal (onjecto.Precio));
			dbcommand.ExecuteNonQuery ();


		}
		public static void update(Articulo onjecto){
			//en la clase articulo hay un objeto el cual recoge el return de load(id)
			//este metodo tiene como parametro dicho objeto
			IDbCommand dbcommand = APP.Instance.Dbconection.CreateCommand ();
			dbcommand.CommandText = "update articulo set nombre=@nombre, categoria=@categoria, precio=@precio where id=@id";
			id = onjecto.Id;
			nombre = onjecto.Nombre;
			categoria = onjecto.Categoria;
			precio = Convert.ToDecimal (onjecto.Precio);

			DbCommandHelper.AddParameter (dbcommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbcommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbcommand, "precio", precio);
			DbCommandHelper.AddParameter (dbcommand, "id", id);
			dbcommand.ExecuteNonQuery ();


		}

		private static void addParameter(IDbCommand dbcommand,string name, object value){
			IDbDataParameter dbdataparameter = dbcommand.CreateParameter ();
			dbdataparameter.ParameterName = name;
			dbdataparameter.Value = value;
			dbcommand.Parameters.Add (dbdataparameter);
		}

	}
}

