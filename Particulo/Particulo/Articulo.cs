using System;

namespace Particulo
{
	public class Articulo
	{
		public Articulo ()
		{
		}

		private object id;
		private object nombre;
		private object categoria;
		private decimal precio;

		public object Id{
			get{ return id;}
			set{ id = value;}
		}
		public object Nombre{
			get{ return nombre;}
			set{ nombre = value;}
		}
		public object Categoria{
			get{ return categoria;}
			set{ categoria = value;}
		}
		public decimal Precio{
			get{ return precio;}
			set{ precio = value;}
		}




}
}
