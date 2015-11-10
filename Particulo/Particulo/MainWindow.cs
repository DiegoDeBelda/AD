using System;
using System.Collections.Generic;
using Gtk;
using System.Data;
using SerpisAD;
using System.Collections;
using Particulo;

public partial class MainWindow: Gtk.Window
{	
	public QueryResult query;


	public MainWindow() : base (Gtk.WindowType.Toplevel) {
		Build ();
	}

	public MainWindow (string consulta): this ()
	{

		//creamos el objeto de la consulta con la query 
		//entrySelect.Text = consulta;

		QueryResult queryresult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (TreeView, queryresult);

	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	protected void OnNewActionActivated (object sender, EventArgs e)
	{
		new ArticuloView ();
	}

	protected void onRefreshActivatedated (object sender, EventArgs e)
	{
		QueryResult queryresult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (TreeView, queryresult);
	}

}

