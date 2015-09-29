using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace PMYSQL
{
	class MainClass
	{	//m mayucula en main obligatoriamente.
		public static void Main (string[] args)
		{

			MySqlConnection mysqlconection = new MySqlConnection (
				"Database=dbprueba;Data Source=localhost;User id=root; Password=sistemas");

			mysqlconection.Open ();


			MySqlCommand mysqlcommand = mysqlconection.CreateCommand ();
			mysqlcommand.CommandText = "select * from articulo";
//				"select a.categoria as articulocategoria, c.nombre as categorianombre, count(*)" +
//				"from articulo a " +
//				"left join categoria c on a.categoria= c.id " +
//				"group by articulocategoria, categorianombre";

			MySqlDataReader mysqldatareader = mysqlcommand.ExecuteReader ();
	

			//---------------------------------------------------------------
			updateDatabase (mysqlconection);
			showColumnNames (mysqldatareader);
			show (mysqldatareader);

			//---------------------------------------------------------------
			mysqldatareader.Close ();
			mysqlconection.Close ();



		}//metodoMain
		//-----------------------------------------------------------------------------------------------------------
		private static string[] getColumns(MySqlDataReader mysqldatareader){
			int count = mysqldatareader.FieldCount;
			List<string> columNames = new List<string> ();
			for (int i =0; i<count; i++) {
				columNames.Add (mysqldatareader.GetName(i));

			}
			return columNames.ToArray ();
		}
		//----------------------------------------------------------------------------------------------------------
		private static void updateDatabase(MySqlConnection mysqlconection){
			MySqlCommand mysqlcommand2 = mysqlconection.CreateCommand ();
			mysqlcommand2.CommandText = "delete categoria where id=5";
			//mysqlcommand2.ExecuteNonQuery ();
		}
		//-----------------------------------------------------------------------------------------------------------
		private static void showColumnNames(MySqlDataReader mysqldatareader){

			String columnName="";
			String[] array = new String[mysqldatareader.FieldCount];

			for (int i=0; i<mysqldatareader.FieldCount; i++) {

				columnName = mysqldatareader.GetName(i);
				array [i] = columnName;

				Console.WriteLine (columnName+" ");

			}
		}
		//-----------------------------------------------------------------------------------------------------------
		private static void show(MySqlDataReader mysqldatareader){


			Console.WriteLine ("show...");

			while (mysqldatareader.Read()) {
				showRow (mysqldatareader);
			}

				//Console.WriteLine ("id={0} | nombre={1} | categoria={2} | precio={3} |", mysqldatareader ["id"], mysqldatareader ["nombre"], mysqldatareader ["categoria"], mysqldatareader ["precio"]);
				//Console.Write ();
		}
		//-----------------------------------------------------------------------------------------------------------
	
		
	
	
	
	
	}//Main
}//Class
