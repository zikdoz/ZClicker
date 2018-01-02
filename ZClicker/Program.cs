using System;
using System.Threading;
using System.Windows.Forms;

namespace ZClicker
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[ STAThread ]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );

			var thread = new Thread( () => runForm( new form_clicker() ) );
			thread.TrySetApartmentState( ApartmentState.STA );
			thread.Start();

			Application.Run( new form_main() );
		}

		private static void runForm( Form form_obj ) =>
			Application.Run( form_obj );
	}
}