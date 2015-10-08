using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Particulo
{
	public class APP
	{
		private APP(){
		
		
		}
		private IDbConnection dbconection;
		public IDbConnection Dbconection {
			get { 
				if (dbconection == null) {
					dbconection = new MySqlConnection ("Database=dbprueba;Data Source=localhost;User id=root; Password=sistemas");
					dbconection.Open ();
				}
				return dbconection;
			}
		}

		private static APP instance = new APP();
		public static APP Instance {
			get { return instance;}
		}



	}
}

