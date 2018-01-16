using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: tooltips to show hotkeys of each control: record / stop / run, speed up/down, repeat
// TODO: maybe, allow user to set font/size/etc
// TODO: maybe, allow user edit hotkeys
namespace ZClicker
{
	public partial class form_clicker : Form
	{
		#region [ DATA ]

		private ZMouseHook _mouse_activity_hook;
		private List< ZMOUSE_DATA > _zmouse_data;
		private Stopwatch _stopwatch_delta_time;

		private readonly PrivateFontCollection _font_collection = new PrivateFontCollection();

		#endregion

		#region [ CONTROLS VARS ]

		private int _speed_mod = 150;
		private bool _is_la2_fixed = true, _is_playing, _repeat_play = true;

		#endregion

		public form_clicker()
		{
			InitializeComponent();

			Shown += ( sender, args ) => init();
		}

		private void init()
		{
			#region [ EMBED FONTS ]

			embedFont( Properties.Resources.dina10px );

			#endregion

			var embed_font_family = _font_collection.Families[ 0 ];
			if ( embed_font_family.IsStyleAvailable( FontStyle.Regular ) )
				updateControlsFont( new Font( embed_font_family, 20, FontStyle.Regular, GraphicsUnit.Point ) );

			#region [ REGISTER GLOBAL HOTKEYS ]

			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.START, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.Q );
			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.STOP, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.A );
			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.RUN, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.Z );
			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.EXIT, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.X );

			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.SPEED_UP, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.Up );
			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.SPEED_DOWN, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.Down );
			ZHotkeyManager.RegisterHotKey( Handle, ( int ) ZHOTKEYS.REPEAT, ZHotkeyManager.KMOD_SHIFT, ( int ) Keys.R );

			#endregion

			#region [ CONTROLS VARS ]

			numericUD_speed.Value = _speed_mod;

			checkbox_l2_drag_fix.Checked = _is_la2_fixed;
			checkbox_repeat.Checked = _repeat_play;

			numericUD_speed.ValueChanged += ( sender, args ) => { _speed_mod = ( int ) numericUD_speed.Value; };

			checkbox_l2_drag_fix.CheckedChanged += ( sender, args ) => { checkboxSetVar( sender as CheckBox, out _is_la2_fixed ); };
			checkbox_repeat.CheckedChanged += ( sender, args ) => { checkboxSetVar( sender as CheckBox, out _repeat_play ); };

			#endregion

			#region [ RECORD ]

			_mouse_activity_hook = new ZMouseHook( false );
			_mouse_activity_hook.OnMouseActivity += ( sender, args ) =>
			{
				if ( args.Clicks != 0 )
				{
					if ( !_stopwatch_delta_time.IsRunning )
						_stopwatch_delta_time.Start();

					_zmouse_data.Add( new ZMOUSE_DATA( args.Button, ( ZMOUSE_STATE ) args.Clicks, Cursor.Position, ( int ) _stopwatch_delta_time.ElapsedMilliseconds ) );

					_stopwatch_delta_time.Restart();
				}
			};

			#endregion

			#region [ PLAY ]

			background_worker.DoWork += async ( sender, args ) =>
			{
				do
				{
					for ( int i = 0, end = _zmouse_data.Count; !( args.Cancel = background_worker.CancellationPending ) && ( i < end ); ++i )
					{
						Task current_job;

						if ( _is_la2_fixed && ( _zmouse_data[ i ]._state == ZMOUSE_STATE.UP ) )
						{
							var temp = new ZMOUSE_DATA( MouseButtons.None, ZMOUSE_STATE.NONE, _zmouse_data[ i - 1 ]._location );

							current_job = ZClicker.delayedUse( temp );
							await current_job;
						}

//						Console.WriteLine( $@"[ {i} ]: {_zmouse_data[ i ].speedUp( _speed_mod )}" );

						current_job = ZClicker.delayedUse( _zmouse_data[ i ].speedUp( _speed_mod ) );
						await current_job;

						if ( _is_la2_fixed && ( _zmouse_data[ i ]._state == ZMOUSE_STATE.UP ) )
						{
							var temp = new ZMOUSE_DATA( MouseButtons.None, ZMOUSE_STATE.NONE, _zmouse_data[ i ]._location );

							current_job = ZClicker.delayedUse( temp );
							await current_job;
						}
					}
				} while ( _repeat_play && !args.Cancel );

				Invoke( new Action( () =>
				{
					button_run.Enabled = button_record.Enabled = true;
					button_stop.Enabled = _is_playing = false;
				} ) );
			};

			#endregion

			#region [ BUTTONS CLICK ]

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
				if ( _is_playing )
					background_worker.CancelAsync();

				_stopwatch_delta_time?.Stop();
				_mouse_activity_hook?.stop();

				button_record.Enabled = button_run.Enabled = true;
				button_stop.Enabled = false;
			};

			button_run.Click += ( sender, args ) =>
			{
				button_stop.Enabled = _is_playing = true;
				button_run.Enabled = button_record.Enabled = false;

				background_worker?.RunWorkerAsync();
			};

			#endregion
		}

		#region [ HANDLE GLOBAL HOTKEYS ]

		protected override void WndProc( ref Message message )
		{
			base.WndProc( ref message );

			switch ( message.Msg )
			{
				case ZHotkeyManager._WM_HOTKEY:

					switch ( ( ZHOTKEYS ) message.WParam.ToInt32() )
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

							if ( _is_playing )
								background_worker.CancelAsync();

							Environment.Exit( 0 );
							break;

						case ZHOTKEYS.SPEED_UP:

							numericUD_speed.Value = Math.Min( numericUD_speed.Maximum, numericUD_speed.Value + numericUD_speed.Increment );
							break;

						case ZHOTKEYS.SPEED_DOWN:

							numericUD_speed.Value = Math.Max( numericUD_speed.Minimum, numericUD_speed.Value - numericUD_speed.Increment );
							break;

						case ZHOTKEYS.REPEAT:

							checkbox_repeat.Checked = !checkbox_repeat.Checked;
							break;
					}
					break;
			}
		}

		#endregion

		private void embedFont( byte[] font )
		{
			var font_length = font.Length;
			var buffer = font;

			var data = Marshal.AllocCoTaskMem( font_length );
			Marshal.Copy( buffer, 0, data, font_length );

			_font_collection.AddMemoryFont( data, font_length );
			Marshal.FreeCoTaskMem( data );
		}

		private void updateControlsFont( Font font ) =>
			button_run.Font = button_stop.Font = button_record.Font =
				label1.Font = label2.Font =
					checkbox_repeat.Font = checkbox_l2_drag_fix.Font =
						numericUD_speed.Font = font;

		private static void checkboxSetVar( CheckBox sender, out bool var ) =>
			var = sender.Checked;
	}
}