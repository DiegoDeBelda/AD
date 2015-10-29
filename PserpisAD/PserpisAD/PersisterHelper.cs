using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Gtk;

namespace SerpisAD
{
	public class PersisterHelper
	{
		//metodo para conseguir las columnas
		private static String[] getColumnames (IDataReader datareader){
			List<string> columnames = new List<string>();
			int count = datareader.FieldCount;
			for (int i=0; i<count; i++) {
				columnames.Add (datareader.GetName (i));
			}
			return columnames.ToArray ();

		}
		//metodo para conseguir las filas
		private static IList getRow(IDataReader datareader){
			int count = datareader.FieldCount;
			List<object> values = new List<object> ();
			for (int i=0; i<count; i++) {
				values.Add (datareader [i]);
			}
			return values;
		}

//		private Type[] getTypes(int count){
//			List<Type> types = new List<Type> ();
//			for (int i=0; i<count; i++) {
//				types.Add (typeof(string));
//
//			}
//			return types.ToArray ();
//		}
//


		public static QueryResult Get(string selectText){
			//obtenemos la conexion y los comandos de la misma
			IDbConnection dbconection = APP.Instance.Dbconection;
			IDbCommand dbcomand =dbconection.CreateCommand ();
			dbcomand.CommandText = selectText;
			//creamos un lector
			IDataReader datareader = dbcomand.ExecuteReader ();

			QueryResult queryresult = new QueryResult ();
			//almacenamos el resultado del metodo de las columnas en un array
			String[] ColumNames = getColumnames (datareader);
			//despues lo introducimos en el atributo del objeto correspondiente
			queryresult.ColumNames = ColumNames;

			//creamos una lista que almacenara las filas
			List<IList> rows = new List<IList> ();

			//leemos de la BD, la introducimos en la lista y la lista al atributo del objeto correspondiente
			while (datareader.Read()) {

				rows.Add(getRow (datareader));
			}
			queryresult.Rows = rows;





			//cerramos la conexion
			datareader.Close ();

			return queryresult;

		}
	}
}

