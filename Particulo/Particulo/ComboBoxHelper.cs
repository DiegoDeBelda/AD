using System;
using System.Collections;
using Gtk;
using SerpisAD;

namespace Particulo
{
	public class ComboBoxHelper
	{
		public static void Fill(ComboBox combobox, QueryResult queryresult, object id){
			CellRendererText cellrenderertext = new CellRendererText ();
			combobox.PackStart (cellrenderertext, false);

			combobox.SetCellDataFunc (cellrenderertext, 
			    delegate(CellLayout cell_layout, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
				IList row =(IList)tree_model.GetValue(iter, 0);
				cellrenderertext.Text=row[1].ToString();
			});

			ListStore liststore = new ListStore (typeof(IList));
			IList first = new object[] { null, "<sin asignar>" };
			TreeIter treeiterid = liststore.AppendValues (first);
			foreach (IList row in queryresult.Rows) {
				TreeIter treeiter= liststore.AppendValues (row);
				if (row[0].Equals(id))
					treeiterid = treeiter;
			}
			combobox.Model = liststore;
			//combobox.Active = 0;
			combobox.SetActiveIter (treeiterid);
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