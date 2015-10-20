using System;
using System.Collections.Generic;
using Gtk;
using System.Data;
using SerpisAD;
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
		TreeViewHelper.Fill (TreeView, queryresult);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}

