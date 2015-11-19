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

		refreshAction.Activated += delegate {
			fillTreeView();
		};

		deleteAction.Activated += delegate{
			object id = TreeViewHelper.GetId(TreeView);
			delete(id);
		};

		newAction.Activated += delegate {
			new ArticuloView();
		};
		editAction.Activated += delegate{
			object id = TreeViewHelper.GetId(TreeView);
			new ArticuloView(id);

		};
		TreeView.Selection.Changed += delegate {
			Console.WriteLine ("has seleccionado una fila");
			deleteAction.Sensitive=TreeViewHelper.GetId(TreeView)!=null;
		};
		deleteAction.Sensitive = false;
	}
		



	private void delete (object id){

		if (WindowHelper.ConfirmDelete(this)) {
			QueryResult queryEliminar = PersisterHelper.Get("delete from articulo where id=" + id);
			Console.WriteLine ("linea borrada");
		}
	
	}

	private void fillTreeView(){
		QueryResult queryresult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (TreeView, queryresult);
	}


	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}






}

