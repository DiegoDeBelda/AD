using System;
using System.Collections.Generic;
using Gtk;
using System.Data;
using Particulo;
using System.Collections;

public partial class MainWindow: Gtk.Window
{	

	public MainWindow() : base (Gtk.WindowType.Toplevel) {
		Build ();
	}

	public MainWindow (string consulta): this ()
	{

		//creamos el objeto de la consulta con la query 
		entrySelect.Text = consulta;

		QueryResult queryresult = PersisterHelper.Get (consulta);
		//creamos una variable auxiliar la cual usara el metodo visto en PersisterHelper.cs
		String[] columNames = queryresult.ColumNames;

		//creamos el objeto que "dibujara" las celdas de cada fila y columna
		CellRendererText cellrenderertext = new CellRendererText ();


		for (int i=0; i<queryresult.ColumNames.Length; i++) {
			int column = i;
			//marcamos el nombre de las columna en el TreeView
			TreeView.AppendColumn (columNames [i], cellrenderertext,
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
		TreeView.Model = lista;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}

