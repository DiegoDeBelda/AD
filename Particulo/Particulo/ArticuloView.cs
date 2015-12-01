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
			articulo = new Articulo();
			init ();
			save = ArticuloPersister.Insert;
		}

		public ArticuloView(object id) : base(WindowType.Toplevel){

			articulo = ArticuloPersister.load(id);
			init ();
			save = ArticuloPersister.update;
		}



		public void init(){
			this.Build ();
			articulo.Nombre=entry1.Text;
			QueryResult query = PersisterHelper.Get ("Select * from categoria");
			ComboBoxHelper.Fill (combobox1, query, articulo.Categoria);
			spinbutton1.Value = Convert.ToDouble (articulo.Precio);

			saveAction.Activated += delegate { 
				articulo.Nombre = entry1.Text;
				articulo.Categoria=ComboBoxHelper.GetId(combobox1);
				articulo.Precio=Convert.ToDecimal(spinbutton1.ValueAsInt);
				save (articulo); 
			};

		}


	}
}

