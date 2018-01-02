using System;
using System.Runtime.InteropServices;

namespace ZClicker
{
	class ZHotkeyManager
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
			KMOD_ALT = 1,
			KMOD_CTRL = 2,
			KMOD_SHIFT = 4,
			KMOD_WIN = 8;

		#endregion

		#region [ HOTKEYS ID ]

		public const int
			_ZRECORD_START = 1337 + 0,
			_ZRECORD_STOP = 1337 + 1,
			_ZRECORD_RUN = 1337 + 2;

		#endregion
	}
}