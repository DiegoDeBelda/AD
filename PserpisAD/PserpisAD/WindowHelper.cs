using System;
using Gtk;

namespace SerpisAD
{
	public class WindowHelper
	{
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
	}
}

