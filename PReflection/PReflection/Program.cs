using System;
using Gtk;
using System.Reflection;

namespace PReflection
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			object i = 33;
			Type type1 = i.GetType ();
			showType (type1);

			object s = "hola";
			Type type2 = s.GetType ();
			showType (type2);

			Type typeX = typeof(string);
			showType (typeX);


			Type typeFoo = typeof(Foo);
			showType (typeFoo);

			Type type0 = typeof(object);
			showType (type0);

			Articulo articulo = new Articulo ();
			showType (articulo.getType ());

		}
		private static void showType(Type type){
			Console.WriteLine ("type.Name={0} type.FullName={1} type.BaseType.Name={2}", type.Name, type.FullName, type.BaseType);
			PropertyInfo[] propertyInfos= type.GetProperties();
			foreach (PropertyInfo propertyInfo in propertyInfos)
				Console.WriteLine ("propertyInfo.Name={0}", propertyInfo.Name);
		}

		public class Foo{
			private object id;
			public object Id{
				get { return id;}
				set {id = value;}
			}


			private string name;
			public string Name {
				get{ return name;}
				set{ name = value;}
			}
		}
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
}
