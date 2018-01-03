using System;
using System.Runtime.InteropServices;

namespace ZClicker
{
	public enum ZHOTKEYS
	{
		START = 1337 + 0,
		STOP = 1337 + 1,
		RUN = 1337 + 2,
		EXIT = 1337 + 3,
		SPEED_UP = 1337 + 4,
		SPEED_DOWN = 1337 + 5,
		REPEAT = 1337 + 6,
	}

	public static class ZHotkeyManager
	{
		#region [ WIN-LIB IMPORTS ]

		// Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8 (calculate the sum of them)
		[ DllImport( "user32.dll" ) ]
		public static extern bool RegisterHotKey( IntPtr hWnd, int id, int fsModifiers, int vlc );

		[ DllImport( "user32.dll" ) ]
		public static extern bool UnregisterHotKey( IntPtr hWnd, int id );

		#endregion

		#region [ WIN CONSTS ]

		public const int _WM_HOTKEY = 0x0312,
			KMOD_ALT = 1 << 0,
			KMOD_CTRL = 1 << 1,
			KMOD_SHIFT = 1 << 2,
			KMOD_WIN = 1 << 3;

		#endregion
	}
}