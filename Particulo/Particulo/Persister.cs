using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace SerpisAD
{
	//TODO resolver exception del update id???
	public class Persister
	{
		//esqueleto para la sentencia
		private const string INSERT_SQL ="insert into {0} ({1}) values ({2})";
		private const string UPDATE_SQL = "update {0} set ({1})";

		//metodo insert
		public static void Insert(object obj){
			IDbCommand dbcommand = APP.Instance.Dbconection.CreateCommand ();
			dbcommand.CommandText = getInsertSQL(obj.GetType());
			addParameters (dbcommand, obj);
			dbcommand.ExecuteNonQuery ();

		}
		//metodo update
		public static void Update(object obj){
			IDbCommand dbcommand = APP.Instance.Dbconection.CreateCommand ();
			dbcommand.CommandText = getUpdateSQL(obj.GetType());
			addParameters (dbcommand, obj);
			dbcommand.ExecuteNonQuery ();
		}
		//añadir los parametro para execute
		private static void addParameters(IDbCommand dbcommand, object obj){
			Type type= obj.GetType ();
			PropertyInfo[] propertyInfos= type.GetProperties();
			foreach (PropertyInfo propertyInfo in propertyInfos) {
				if (!propertyInfo.Name.Equals ("id")) {
					string name = propertyInfo.Name.ToLower();
					object value = propertyInfo.GetValue (obj, null);
					DbCommandHelper.AddParameter (dbcommand, name, value);
				}
			}

		}
		//creacion generica de la consulta para un insert
		private static string getInsertSQL(Type type){
				string tablename = type.Name.ToLower();
				string[] fieldnames = getFieldNames (type);
				string[] paramNames = getParamNames (fieldnames);
			return string.Format (INSERT_SQL, tablename, string.Join (",", fieldnames), string.Join (",", paramNames));
		}
		//creacion generica de la consulta para un update
		private static string getUpdateSQL(Type type){
			string tablename = type.Name.ToLower();
			string[] fieldnames = getFieldNames (type);
			string[] paramNames = getParamNames (fieldnames);
			return string.Format (UPDATE_SQL, tablename, string.Join (",", fieldnames), string.Join ("=", paramNames));
		}
		//recoleccion de los campos de una tabla en un array
		private static string[] getFieldNames(Type type){
			List<string> fieldNames = new List<string>();
			PropertyInfo[] propertyInfos= type.GetProperties();
			foreach (PropertyInfo propertyInfo in propertyInfos) {
				if (!propertyInfo.Name.Equals ("id"))
				fieldNames.Add(propertyInfo.Name.ToLower());

			}
			return fieldNames.ToArray();
		}
		//getFieldNames, añadiendo una @ para los values
		private static string[] getParamNames(string[] fieldNames){
			List<string> ParamNames = new List<string> ();
			foreach (string fieldName in fieldNames)
				ParamNames.Add("@" + fieldName);

		return ParamNames.ToArray();
		}
	}}

