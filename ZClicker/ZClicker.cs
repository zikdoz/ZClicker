using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: maybe handle mouse wheel?
namespace ZClicker
{
	#region [ HELPRERS ]

	public static class ZExtensions
	{
		public static Point addOffset( this Point point, Point offset ) =>
			new Point( point.X + offset.X, point.Y + offset.Y );

		public static Point addOffset( this Point point, int offset_x, int offset_y ) =>
			new Point( point.X + offset_x, point.Y + offset_y );

		public static ZMOUSE_DATA speedUp( this ZMOUSE_DATA data, int speed )
		{
			data.deltaTime = ( int ) ( data.deltaTime / ( speed / 100.0 ) );

			return data;
		}
	}

	#endregion

	#region [ STRUCT / ENUMS ]

	public enum ZMOUSE_STATE
	{
		UP = -1,
		NONE = 0,
		DOWN = 1
	}

	public struct ZMOUSE_DATA
	{
		private int _delta_time;

		public int deltaTime
		{
			get => _delta_time;
			set => _delta_time = Math.Max( _MIN_DELTA_TIME, value );
		}

		public readonly MouseButtons _button;

		public readonly ZMOUSE_STATE _state;

		public Point _location;

		private const int _MIN_DELTA_TIME = 50; // in ms

		public ZMOUSE_DATA( MouseButtons button, ZMOUSE_STATE state, Point location, int delta_time = _MIN_DELTA_TIME )
		{
			_button = button;
			_state = state;
			_location = location;
			_delta_time = Math.Max( _MIN_DELTA_TIME, delta_time );
		}

		public override string ToString() =>
			$@"Button = [ {_button} ] | State = [ {_state} ] | Delta_time = [ {_delta_time} ] | Location = [ {_location.X}, {_location.Y} ]";
	}

	#endregion

	public static class ZClicker
	{
		#region [ WIN STRUCTURES ]

#pragma warning disable 649
		private struct INPUT
		{
			public uint _type;
			public MOUSEKEYBDHARDWAREINPUT _data;
		}

		[ StructLayout( LayoutKind.Explicit ) ]
		private struct MOUSEKEYBDHARDWAREINPUT
		{
			[ FieldOffset( 0 ) ] public MOUSEINPUT _mouse;
		}

		private struct MOUSEINPUT
		{
			public int _x;
			public int _y;
			public uint _mouse_data;
			public uint _flags;
			public uint _time;
			public IntPtr _extra_info;
		}

#pragma warning restore 649

		#endregion

		#region [ WIN-LIB IMPORTS ]

		[ DllImport( "user32.dll" ) ]
		private static extern bool ClientToScreen( IntPtr hWnd, ref Point lpPoint );

		[ DllImport( "user32.dll" ) ]
		private static extern uint SendInput( uint nInputs, [ MarshalAs( UnmanagedType.LPArray ), In ] INPUT[] pInputs, int cbSize );

		#endregion

		#region [ WIN CONSTS ]

		private const int
			_MOUSE_LEFT_DOWN = 1 << 1,
			_MOUSE_LEFT_UP = 1 << 2,
			_MOUSE_RIGHT_DOWN = 1 << 3,
			_MOUSE_RIGHT_UP = 1 << 4;

		#endregion

		private static void useMouse( ZMOUSE_DATA data )
		{
			Cursor.Position = data._location;

			if ( data._state != ZMOUSE_STATE.NONE )
			{
				var mouse_input = new INPUT { _type = 0 };
				mouse_input._data._mouse._flags = ( uint ) ( ( ( data._button == MouseButtons.Left ) ? _MOUSE_LEFT_DOWN : _MOUSE_RIGHT_DOWN ) << ( ( data._state == ZMOUSE_STATE.UP ) ? 1 : 0 ) );

				SendInput( 1, new[] { mouse_input }, Marshal.SizeOf( typeof( INPUT ) ) );
			}
		}

		public static Task delayedUse( ZMOUSE_DATA data, int delay_ms = -1 ) =>
			Task.Run( async () =>
			{
				await Task.Delay( ( ( delay_ms == -1 ) ? data.deltaTime : delay_ms ) );

				useMouse( data );
			} );
	}
}