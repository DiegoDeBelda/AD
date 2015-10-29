using System;
using SerpisAD;
using System.Collections;
using Gtk;
namespace Particulo
{
	public partial class ArticuloView : Gtk.Window
	{
		public ArticuloView () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			entry1.Text = "nuevo";
			spinbutton1.Value = 1.5;

			QueryResult query = PersisterHelper.Get ("Select * from categoria");
			CellRendererText cellrenderertext = new CellRendererText ();
			combobox1.PackStart (cellrenderertext, false);

			combobox1.SetCellDataFunc (cellrenderertext, 
			delegate(CellLayout cell_layout, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
				IList row =(IList)tree_model.GetValue(iter, 0);
				cellrenderertext.Text=
					String.Format("{0} - {1}",row[1].ToString(), row[1]);
			});

			ListStore liststore = new ListStore (typeof(IList));
			foreach (IList row in query.Rows)
				liststore.AppendValues (row);

			combobox1.Model = liststore;

		}
	}
}

