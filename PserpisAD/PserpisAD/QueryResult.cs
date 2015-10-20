using System;
using System.Collections;
using System.Collections.Generic;
using Gtk;

namespace SerpisAD
{
	public class QueryResult
	{


		private string[] columnNames;

		public string[] ColumNames {
			get{ return columnNames;}
			set{ columnNames = value;}
		}

		private IEnumerable<IList> rows;
		public IEnumerable<IList> Rows {
			get{ return rows;}
			set{ rows = value;}
		}



	}
}

