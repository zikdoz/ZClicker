namespace ZClicker
{
	partial class form_main
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button_clear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button_clear
			// 
			this.button_clear.Font = new System.Drawing.Font("Dina ttf 10px", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button_clear.Location = new System.Drawing.Point(662, 12);
			this.button_clear.Name = "button_clear";
			this.button_clear.Size = new System.Drawing.Size(110, 34);
			this.button_clear.TabIndex = 0;
			this.button_clear.Text = "Clear";
			this.button_clear.UseVisualStyleBackColor = true;
			// 
			// form_main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 559);
			this.Controls.Add(this.button_clear);
			this.Name = "form_main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Main";
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.form_main_MouseClick);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button_clear;
	}
}

