using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ZClicker
{
	public partial class form_clicker : Form
	{
		private ZMouseHook _mouse_activity_hook;
		private List< ZMOUSE_DATA > _zmouse_data;

		public form_clicker()
		{
			InitializeComponent();

			Shown += ( sender, args ) => init();
		}

		private void init()
		{
			#region [ REGISTER HOTKEYS ]

			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.START, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.Q );
			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.STOP, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.A );
			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.RUN, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.Z );
			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.EXIT, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.X );

			#endregion

			_mouse_activity_hook = new ZMouseHook( false );
			_mouse_activity_hook.OnMouseActivity += ( sender, args ) =>
			{
				if ( args.Clicks != 0 )
					_zmouse_data.Add( new ZMOUSE_DATA( args.Button, ( ZMOUSE_STATE ) args.Clicks, args.Location ) );
			};

			background_worker.DoWork += ( sender, args ) =>
			{
				for ( int i = 0, end = _zmouse_data.Count; i < end; ++i )
				{
					if ( !( args.Cancel = background_worker.CancellationPending ) )
					{
						// TODO: enable fix optionally
//						// TODO: la2 fix goes below
//						if ( _zmouse_data[ i ]._state == ZMOUSE_STATE.UP )
//							ZClicker.useMouse( new ZMOUSE_DATA( MouseButtons.None, ZMOUSE_STATE.NONE, _zmouse_data[ i - 1 ]._location.addOffset( -1, -1 ) ) );
//						// TODO: such fix

						// TODO: delay between actions - option
						Thread.Sleep( 50 );
						ZClicker.useMouse( _zmouse_data[ i ] );
					}
				}
			};
			background_worker.RunWorkerCompleted += ( sender, args ) => { button_run.Enabled = true; };

			button_record.Click += ( sender, args ) =>
			{
				_zmouse_data = new List< ZMOUSE_DATA >();
				_mouse_activity_hook.start();

				button_stop.Enabled = true;
				button_record.Enabled = button_run.Enabled = false;
			};

			button_stop.Click += ( sender, args ) =>
			{
				_mouse_activity_hook.stop();

				button_record.Enabled = button_run.Enabled = true;
				button_stop.Enabled = false;
			};

			button_run.Click += ( sender, args ) =>
			{
				background_worker.RunWorkerAsync();

				button_run.Enabled = false;
			};
		}

		protected override void WndProc( ref Message m )
		{
			if ( m.Msg == ZHotkeyManager._WM_HOTKEY )
			{
				switch ( ( ZHOTKEYS ) m.WParam.ToInt32() )
				{
					case ZHOTKEYS.START:

						button_record.PerformClick();
						break;

					case ZHOTKEYS.STOP:

						button_stop.PerformClick();
						break;

					case ZHOTKEYS.RUN:

						button_run.PerformClick();
						break;

					case ZHOTKEYS.EXIT:

						if ( background_worker.IsBusy )
							background_worker.CancelAsync();

						Environment.Exit( 0 );
						break;
				}
			}

			base.WndProc( ref m );
		}
	}
}