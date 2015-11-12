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
			object id = GetId(TreeView);
			delete(id);
		};
	}

	public static bool ConfirmDelete(Window window){
		MessageDialog messageDialog = new MessageDialog (
			window,
			DialogFlags.DestroyWithParent,
			MessageType.Question,
			ButtonsType.YesNo,
			"Estas seguro de eliminar esta fila?");
		ResponseType response = (ResponseType)messageDialog.Run ();
		messageDialog.Destroy ();
		return response == ResponseType.Yes;
	}

	private void delete (object id){

		if (ConfirmDelete(this)) {
			QueryResult queryEliminar = PersisterHelper.Get ("delete from articulo where id=" + id);
			Console.WriteLine ("linea borrada");
		}
	
	}

	private void fillTreeView(){
		QueryResult queryresult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (TreeView, queryresult);
	}
	public  object GetId(TreeView treeView){
		TreeIter treeiter;
		TreeView.Selection.GetSelected(out treeiter);
		IList row = (IList) TreeView.Model.GetValue(treeiter, 0);
		Console.WriteLine("click en delete id={0}", row[0]);
		return row [0];
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



}

