using System;
using System.Collections;
using Gtk;
using SerpisAD;

namespace Particulo
{
	public class ComboBoxHelper
	{
		public static void Fill(ComboBox combobox, QueryResult queryresult){
			CellRendererText cellrenderertext = new CellRendererText ();
			combobox.PackStart (cellrenderertext, false);

			combobox.SetCellDataFunc (cellrenderertext, 
			                           delegate(CellLayout cell_layout, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
				IList row =(IList)tree_model.GetValue(iter, 0);
				cellrenderertext.Text=
					String.Format("{0} - {1}",row[1].ToString(), row[1]);
			});

			ListStore liststore = new ListStore (typeof(IList));
			IList first = new object[] { null, "<sin asignar>" };
			TreeIter treeiterfirst = liststore.AppendValues (first);
			foreach (IList row in queryresult.Rows)
				liststore.AppendValues (row);

			combobox.Model = liststore;
			//combobox.Active = 0;
			combobox.SetActiveIter (treeiterfirst);
	}
}

}