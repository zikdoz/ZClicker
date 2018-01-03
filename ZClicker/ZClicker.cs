using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZClicker
{
	public static class ZPointExtension
	{
		public static Point addOffset( this Point point, Point offset ) =>
			new Point( point.X + offset.X, point.Y + offset.Y );

		public static Point addOffset( this Point point, int offset_x, int offset_y ) =>
			new Point( point.X + offset_x, point.Y + offset_y );
	}

	public enum ZMOUSE_STATE
	{
		UP = -1,
		NONE = 0,
		DOWN = 1
	}

	public struct ZMOUSE_DATA
	{
		//TODO: add delta_time: time since last mouse activity
		public MouseButtons _button;

		public ZMOUSE_STATE _state;
		public Point _location;

		public ZMOUSE_DATA( MouseButtons button, ZMOUSE_STATE state, Point location )
		{
			_button = button;
			_state = state;
			_location = location;
		}
	}

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

		public static void useMouse( ZMOUSE_DATA data )
		{
			Cursor.Position = data._location;

			if ( data._state != ZMOUSE_STATE.NONE )
			{
				var mouse_input = new INPUT { _type = 0 };
				mouse_input._data._mouse._flags = ( uint ) ( ( ( data._button == MouseButtons.Left ) ? _MOUSE_LEFT_DOWN : _MOUSE_RIGHT_DOWN ) << ( ( data._state == ZMOUSE_STATE.UP ) ? 1 : 0 ) );

				SendInput( 1, new[] { mouse_input }, Marshal.SizeOf( typeof( INPUT ) ) );
			}
		}
	}
}