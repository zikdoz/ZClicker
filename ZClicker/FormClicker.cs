using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: embed font
namespace ZClicker
{
	public partial class form_clicker : Form
	{
		#region [ DATA ]

		private ZMouseHook _mouse_activity_hook;
		private List< ZMOUSE_DATA > _zmouse_data;
		private Stopwatch _stopwatch_delta_time;

		#endregion

		#region [ CONTROLS VARS ]

		private int _speed_mod;
		private bool _is_la2_fixed;

		#endregion

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

			#region [ CONTROLS VARS ]

			numericUD_speed.ValueChanged += ( sender, args ) => { _speed_mod = ( int ) numericUD_speed.Value; };
			checkbox_l2_drag_fix.CheckedChanged += ( sender, args ) => { _is_la2_fixed = checkbox_l2_drag_fix.Checked; };

			#endregion

			#region [ RECORD ]

			_mouse_activity_hook = new ZMouseHook( false );
			_mouse_activity_hook.OnMouseActivity += ( sender, args ) =>
			{
				if ( args.Clicks != 0 )
				{
					if ( !_stopwatch_delta_time.IsRunning )
						_stopwatch_delta_time.Start();

					_zmouse_data.Add( new ZMOUSE_DATA( args.Button, ( ZMOUSE_STATE ) args.Clicks, args.Location, ( int ) _stopwatch_delta_time.ElapsedMilliseconds ) );

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

						if ( _is_la2_fixed )
						{
							if ( _zmouse_data[ i ]._state == ZMOUSE_STATE.UP )
							{
								current_job = ZClicker.delayedUse( new ZMOUSE_DATA( MouseButtons.None, ZMOUSE_STATE.NONE, _zmouse_data[ i - 1 ]._location.addOffset( -1, -1 ) ).speedUp( _speed_mod ) );
								await current_job;
							}
						}

						current_job = ZClicker.delayedUse( _zmouse_data[ i ].speedUp( _speed_mod ) );
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
					Console.WriteLine( data.speedUp( _speed_mod ) );
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