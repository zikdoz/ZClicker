using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;

namespace ZClicker
{
	public class ZMouseHook
	{
		#region [ WIN STRUCTURES ]

		[ StructLayout( LayoutKind.Sequential ) ]
		private class POINT
		{
			public readonly int _x, _y;

			public POINT( int x, int y )
			{
				_x = x;
				_y = y;
			}
		}

		[ StructLayout( LayoutKind.Sequential ) ]
		private class MouseHookStruct
		{
			public POINT _point;

			public int _handle_window, _hit_test_value, _extra_info;
		}

		[ StructLayout( LayoutKind.Sequential ) ]
		private class MouseLLHookStruct
		{
			public POINT _point;

			public int _data, _flags, _time, _extra_info;
		}

		#endregion

		#region [ WIN-LIB IMPORTS ]

		[ DllImport( "user32.dll", CharSet = CharSet.Auto,
			CallingConvention = CallingConvention.StdCall, SetLastError = true ) ]
		private static extern int SetWindowsHookEx(
			int id_hook,
			HookProc lpfn,
			IntPtr h_mod,
			int dw_thread_id );

		[ DllImport( "user32.dll", CharSet = CharSet.Auto,
			CallingConvention = CallingConvention.StdCall, SetLastError = true ) ]
		private static extern int UnhookWindowsHookEx( int id_hook );

		[ DllImport( "user32.dll", CharSet = CharSet.Auto,
			CallingConvention = CallingConvention.StdCall ) ]
		private static extern int CallNextHookEx(
			int id_hook,
			int n_code,
			int w_param,
			IntPtr l_param );

		private delegate int HookProc( int n_code, int w_param, IntPtr l_param );

		#endregion

		#region [ WIN CONTS ]

		// Windows NT/2000/XP: Installs a hook procedure that monitors low-level mouse input events.
		private const int _WH_MOUSE_LL = 14;

		// Installs a hook procedure that monitors mouse messages. For more information, see the MouseProc hook procedure. 
		private const int _WH_MOUSE = 7;

		// The WM_MOUSEMOVE message is posted to a window when the cursor moves. 
		private const int _WM_MOUSEMOVE = 0x200;

		// The WM_LBUTTONDOWN message is posted when the user presses the left mouse button 
		private const int _WM_LBUTTONDOWN = 0x201;

		// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button
		private const int _WM_RBUTTONDOWN = 0x204;

		// The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button 
		private const int _WM_MBUTTONDOWN = 0x207;

		// The WM_LBUTTONUP message is posted when the user releases the left mouse button 
		private const int _WM_LBUTTONUP = 0x202;

		// The WM_RBUTTONUP message is posted when the user releases the right mouse button 
		private const int _WM_RBUTTONUP = 0x205;

		// The WM_MBUTTONUP message is posted when the user releases the middle mouse button 
		private const int _WM_MBUTTONUP = 0x208;

		// The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button 
		private const int _WM_LBUTTONDBLCLK = 0x203;

		// The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button 
		private const int _WM_RBUTTONDBLCLK = 0x206;

		// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button 
		private const int _WM_MBUTTONDBLCLK = 0x209;

		// The WM_MOUSEWHEEL message is posted when the user presses the mouse wheel. 
		private const int _WM_MOUSEWHEEL = 0x020A;

		#endregion

		#region [ DATA ]

		public event MouseEventHandler OnMouseActivity;

		private int _h_mouse_hook;

		private static HookProc _MouseHookProcedure;

		#endregion

		#region [ CLASS ]

		public ZMouseHook()
		{
			start();
		}

		public ZMouseHook( bool install_hook )
		{
			start( install_hook );
		}

		~ZMouseHook()
		{
			stop( true, false );
		}

		#endregion

		public void start( bool install_hook = true )
		{
			if ( _h_mouse_hook == 0 && install_hook )
			{
				_MouseHookProcedure = mouseHookProc;

				_h_mouse_hook = SetWindowsHookEx(
					_WH_MOUSE_LL,
					_MouseHookProcedure,
					Marshal.GetHINSTANCE(
						Assembly.GetExecutingAssembly().GetModules()[ 0 ] ),
					0 );

				if ( _h_mouse_hook == 0 )
				{
					stop( true, false );

					throw new Win32Exception( Marshal.GetLastWin32Error() );
				}
			}
		}

		public void stop( bool uninstall_hook = true, bool throw_exceptions = true )
		{
			if ( _h_mouse_hook != 0 && uninstall_hook )
			{
				var ret_mouse = UnhookWindowsHookEx( _h_mouse_hook );

				_h_mouse_hook = 0;

				if ( ret_mouse == 0 && throw_exceptions )
					throw new Win32Exception( Marshal.GetLastWin32Error() );
			}
		}

		private int mouseHookProc( int code, int button_param, IntPtr param )
		{
			if ( ( code >= 0 ) && ( OnMouseActivity != null ) )
			{
				var mouse_hook_struct = ( MouseLLHookStruct ) Marshal.PtrToStructure( param, typeof( MouseLLHookStruct ) );

				var button = MouseButtons.None;
				short mouse_delta = 0;

				switch ( button_param )
				{
					case _WM_LBUTTONDOWN:

						button = MouseButtons.Left;
						break;
					case _WM_RBUTTONDOWN:

						button = MouseButtons.Right;
						break;
					case _WM_MOUSEWHEEL:

						mouse_delta = ( short ) ( ( mouse_hook_struct._data >> 16 ) & 0xffff );
						break;

					default:
						break;
				}

				var click_count = 0;
				if ( button != MouseButtons.None )
					click_count = ( ( button_param == _WM_LBUTTONDBLCLK || button_param == _WM_RBUTTONDBLCLK ) ? 2 : 1 );

				var e = new MouseEventArgs(
					button,
					click_count,
					mouse_hook_struct._point._x,
					mouse_hook_struct._point._y,
					mouse_delta );

				OnMouseActivity( this, e );
			}

			return CallNextHookEx( _h_mouse_hook, code, button_param, param );
		}
	}
}