using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZClicker
{
	public partial class form_main : Form
	{
		private Random _rng;

		public form_main()
		{
			InitializeComponent();

			Shown += ( sender, args ) => init();
		}

		private void init()
		{
			_rng = new Random();

			button_clear.Click += ( sender, args ) => { Invalidate(); };
		}

		private void form_main_MouseClick( object sender, MouseEventArgs e ) =>
			CreateGraphics().DrawRectangle(
				new Pen( Color.FromArgb(
						_rng.Next( 256 ),
						_rng.Next( 256 ),
						_rng.Next( 256 ) ),
					5f ), e.X, e.Y, 1, 1 );
	}
}