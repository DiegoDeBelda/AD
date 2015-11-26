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
			showType (articulo.GetType ());

			articulo.Nombre = "nombre 33";
			articulo.Categoria = 2;
			articulo.Precio = 3.5;
			showObject (articulo);
			setValues (articulo, new object[] { 33L, "numero 33 modificado", decimal.Parse("33.33") });

		}
		private static void showType(Type type){
			Console.WriteLine ("type.Name={0} type.FullName={1} type.BaseType.Name={2}", type.Name, type.FullName, type.BaseType);
			PropertyInfo[] propertyInfos= type.GetProperties();
			foreach (PropertyInfo propertyInfo in propertyInfos)
				Console.WriteLine ("propertyInfo.Name={0}", propertyInfo.Name);
		}

		private static void showObject(object obj){
			Type type = obj.GetType ();
			PropertyInfo[] propertyInfos= type.GetProperties();
			foreach (PropertyInfo propertyInfo in propertyInfos){

				Console.WriteLine("{0}={1}",propertyInfo.Name, propertyInfo.GetValue (obj, null));
			}
		}

		private static void setValues(object obj, object[] values){
			int index = 0;
			Type type = obj.GetType ();
			PropertyInfo[] propertyInfos= type.GetProperties();
			foreach (PropertyInfo propertyInfo in propertyInfos) {
				propertyInfo.SetValue(obj, values[index], null);
				index++;
			}
		}


		//clase interna Foo
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
		//copia de PArticulo.Articulo
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
