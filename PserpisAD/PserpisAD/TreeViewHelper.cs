using System;
using System.Collections;
using Gtk;


namespace SerpisAD
{
	public class TreeViewHelper
	{
		public static void Fill (TreeView treeView, QueryResult queryresult)
		{

			removeAllColumns (treeView);
			//creamos una variable auxiliar la cual usara el metodo visto en PersisterHelper.cs
			String[] columNames = queryresult.ColumNames;

			//creamos el objeto que "dibujara" las celdas de cada fila y columna
			CellRendererText cellrenderertext = new CellRendererText ();


			for (int i=0; i<queryresult.ColumNames.Length; i++) {
				int column = i;
				//marcamos el nombre de las columna en el TreeView
				treeView.AppendColumn (columNames [i], cellrenderertext,
				                       delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model,TreeIter iter) {
					IList row = (IList)tree_model.GetValue(iter, 0);
					cellrenderertext.Text = row[column].ToString();
				});

			}
			//tal como las columnas creamos una variable auxiliar para meter las filas
			ListStore lista = new ListStore (typeof(IList));
			//usando un foreach para ahorrar codigo en vez de for o while
			foreach (IList row in queryresult.Rows) {
				lista.AppendValues (row);
			}
			//por ultimo agregamos toda la informacion restante 
			treeView.Model = lista;



		}
		private static void removeAllColumns(TreeView treeView){
			TreeViewColumn[] treeviewcolumns = treeView.Columns;
			foreach (TreeViewColumn treeviewcolumn in treeviewcolumns)
				treeView.RemoveColumn (treeviewcolumn);
		}
		public static object GetId(TreeView treeView){
			TreeIter treeiter;
			if (!treeView.Selection.GetSelected (out treeiter)) {
				return null;
			}
			IList row = (IList) treeView.Model.GetValue(treeiter, 0);
			return row [0];

		}
		public static bool isSelected(TreeView treeview){
			TreeIter treeIter;
			return treeview.Selection.GetSelected (out treeIter);
		}
	}
}

