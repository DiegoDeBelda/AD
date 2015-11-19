using System;
using System.Data;

namespace SerpisAD
{
	public class DbCommandHelper
	{
	
		public static void AddParameter(IDbCommand dbCommand, string nombre, object value)
		{
			IDbDataParameter dbDataParameter=dbCommand.CreateParameter ();
			dbDataParameter.ParameterName = nombre;
			dbDataParameter.Value = value;
			dbCommand.Parameters.Add (dbDataParameter);
		}
	}
	}


