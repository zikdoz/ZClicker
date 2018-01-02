using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ZClicker
{
	public partial class form_clicker : Form
	{
		private ZMouseHook _mouse_activity_hook;
		private List< Point > _clicked_points;

		public form_clicker()
		{
			InitializeComponent();

			Shown += ( sender, args ) => init();
		}

		private void init()
		{
			_mouse_activity_hook = new ZMouseHook( false );
			_mouse_activity_hook.OnMouseActivity += ( sender, args ) =>
			{
				if ( args.Clicks > 0 )
					_clicked_points.Add( args.Location );
			};

			button_record.Click += ( sender, args ) =>
			{
				_clicked_points = new List< Point >();
				_mouse_activity_hook.start();

				button_stop.Enabled = true;
				button_record.Enabled = button_run.Enabled = false;
			};

			button_stop.Click += ( sender, args ) =>
			{
				_mouse_activity_hook.stop();

				button_record.Enabled = button_run.Enabled = true;
				button_stop.Enabled = false;

				for ( int i = 0, end = _clicked_points.Count; i < end; ++i )
					Console.WriteLine( $@"#{i} @ x = {_clicked_points[ i ].X} |  y = {_clicked_points[ i ].Y}" );
			};

			button_run.Click += ( sender, args ) =>
			{
				foreach ( var point in _clicked_points )
					ZClicker.clickHere( point );
			};
		}
	}
}