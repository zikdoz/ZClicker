using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZClicker
{
	public enum ZMOUSE_STATE
	{
		UP = -1,
		NONE = 0,
		DOWN = 1
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
			_MOUSE_LEFT_DOWN = 0x0002,
			_MOUSE_LEFT_UP = 0x0004,
			_MOUSE_RIGHT_DOWN = 0x0008,
			_MOUSE_RIGHT_UP = 0x0010;

		#endregion

		// TODO: pass mouse button and its state
		public static void clickAtLocation( Point location, MouseButtons button )
		{
			Cursor.Position = location;

			var input_mouse_down = new INPUT { _type = 0 };
			input_mouse_down._data._mouse._flags = _MOUSE_LEFT_DOWN;

			var input_mouse_up = new INPUT { _type = 0 };
			input_mouse_up._data._mouse._flags = _MOUSE_LEFT_UP;

			var inputs = new[] { input_mouse_down, input_mouse_up };
			SendInput( ( uint ) inputs.Length, inputs, Marshal.SizeOf( typeof( INPUT ) ) );
		}
	}
}