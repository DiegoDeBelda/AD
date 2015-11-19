using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace SerpisAD
{
	public class APP
	{
		private APP(){}

		private static APP instance = new APP();
		public static APP Instance {
			get { return instance;}
		}

		private IDbConnection dbconection;
		public IDbConnection Dbconection {
			get { 
				return dbconection;
//				if (dbconection == null) {
//					dbconection = new MySqlConnection (
//						"Database=dbprueba;Data Source=localhost;User id=root; Password=sistemas");
//					dbconection.Open ();
//				}

			}
			set{ dbconection = value;}
		}





	}
}

