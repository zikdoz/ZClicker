using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZClicker
{
	public partial class form_clicker : Form
	{
		private ZMouseHook _mouse_activity_hook;
		private List< ZMOUSE_DATA > _zmouse_data;
		private Stopwatch _stopwatch_delta_time;

		public form_clicker()
		{
			InitializeComponent();

			Shown += ( sender, args ) => init();
		}

		private void init()
		{
			#region [ REGISTER GLOBAL HOTKEYS ]

			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.START, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.Q );
			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.STOP, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.A );
			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.RUN, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.Z );
			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.EXIT, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.X );

			#endregion

			#region [ RECORD ]

			_mouse_activity_hook = new ZMouseHook( false );
			_mouse_activity_hook.OnMouseActivity += ( sender, args ) =>
			{
				if ( args.Clicks != 0 )
				{
					if ( !_stopwatch_delta_time.IsRunning )
						_stopwatch_delta_time.Start();

					_zmouse_data.Add( new ZMOUSE_DATA( args.Button, ( ZMOUSE_STATE ) args.Clicks, args.Location, Math.Max( ( int ) _stopwatch_delta_time.ElapsedMilliseconds, ZMOUSE_DATA._MIN_DELTA_TIME ) ) );

					_stopwatch_delta_time.Restart();
				}
			};

			#endregion

			#region [ (TEST) PLAY ]

			background_worker.DoWork += async ( sender, args ) =>
			{
				for ( int i = 0, end = _zmouse_data.Count; i < end; ++i )
				{
					if ( !( args.Cancel = background_worker.CancellationPending ) )
					{
						Task current_job;

//						// TODO: la2 fix - enable optionally
						if ( _zmouse_data[ i ]._state == ZMOUSE_STATE.UP )
						{
							current_job = ZClicker.delayedUse( new ZMOUSE_DATA( MouseButtons.None, ZMOUSE_STATE.NONE, _zmouse_data[ i - 1 ]._location.addOffset( -1, -1 ) ) );
							await current_job;
						}

						// TODO: record delay between actions & customize it
						current_job = ZClicker.delayedUse( _zmouse_data[ i ] );
						await current_job;
					}
				}
			};
			background_worker.RunWorkerCompleted += ( sender, args ) => { button_run.Enabled = true; };

			#endregion

			button_record.Click += ( sender, args ) =>
			{
				_stopwatch_delta_time = new Stopwatch();
				_zmouse_data = new List< ZMOUSE_DATA >();
				_mouse_activity_hook?.start();

				button_stop.Enabled = true;
				button_record.Enabled = button_run.Enabled = false;
			};

			button_stop.Click += ( sender, args ) =>
			{
				_stopwatch_delta_time?.Stop();
				_mouse_activity_hook?.stop();

				button_record.Enabled = button_run.Enabled = true;
				button_stop.Enabled = false;
			};

			button_run.Click += ( sender, args ) =>
			{
				background_worker?.RunWorkerAsync();

#if DEBUG
				foreach ( var data in _zmouse_data )
					Console.WriteLine( data );
#endif

				button_run.Enabled = false;
			};
		}

		#region [ HANDLE GLOBAL HOTKEYS ]

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

		#endregion
	}
}