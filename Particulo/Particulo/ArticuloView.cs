using System;
using SerpisAD;
using System.Collections;
using Gtk;
using System.Data;
namespace Particulo
{
	public delegate void SaveDelegate(Articulo a);
	public partial class ArticuloView : Gtk.Window
	{

		public Articulo articulo;
		public static SaveDelegate save;




		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{ 
			init ();
			save = ArticuloPersister.Insert(articulo);
		}

		public ArticuloView(object id) : base(WindowType.Toplevel){

			articulo = ArticuloPersister.load(id);
			init ();
			save = ArticuloPersister.update(articulo);
		}



		public void init(){
			this.Build ();
			articulo.Nombre=entry1.Text;
			QueryResult query = PersisterHelper.Get ("Select * from categoria");
			ComboBoxHelper.Fill (combobox1, query, articulo.Categoria);
			spinbutton1.Value = Convert.ToDouble (articulo.Precio);

			saveAction.Activated += delegate { save (articulo); };

		}


	}
}

