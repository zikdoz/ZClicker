namespace ZClicker
{
	partial class form_clicker
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
			this.button_record = new System.Windows.Forms.Button();
			this.button_stop = new System.Windows.Forms.Button();
			this.button_run = new System.Windows.Forms.Button();
			this.background_worker = new System.ComponentModel.BackgroundWorker();
			this.SuspendLayout();
			// 
			// button_record
			// 
			this.button_record.Dock = System.Windows.Forms.DockStyle.Top;
			this.button_record.Font = new System.Drawing.Font("Dina ttf 10px", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button_record.Location = new System.Drawing.Point(0, 0);
			this.button_record.Name = "button_record";
			this.button_record.Size = new System.Drawing.Size(255, 40);
			this.button_record.TabIndex = 0;
			this.button_record.Text = "Record";
			this.button_record.UseVisualStyleBackColor = true;
			// 
			// button_stop
			// 
			this.button_stop.Dock = System.Windows.Forms.DockStyle.Top;
			this.button_stop.Enabled = false;
			this.button_stop.Font = new System.Drawing.Font("Dina ttf 10px", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button_stop.Location = new System.Drawing.Point(0, 40);
			this.button_stop.Name = "button_stop";
			this.button_stop.Size = new System.Drawing.Size(255, 42);
			this.button_stop.TabIndex = 1;
			this.button_stop.Text = "Stop";
			this.button_stop.UseVisualStyleBackColor = true;
			// 
			// button_run
			// 
			this.button_run.Dock = System.Windows.Forms.DockStyle.Top;
			this.button_run.Enabled = false;
			this.button_run.Font = new System.Drawing.Font("Dina ttf 10px", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button_run.Location = new System.Drawing.Point(0, 82);
			this.button_run.Name = "button_run";
			this.button_run.Size = new System.Drawing.Size(255, 40);
			this.button_run.TabIndex = 2;
			this.button_run.Text = "Run";
			this.button_run.UseVisualStyleBackColor = true;
			// 
			// background_worker
			// 
			this.background_worker.WorkerSupportsCancellation = true;
			// 
			// form_clicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(255, 123);
			this.Controls.Add(this.button_run);
			this.Controls.Add(this.button_stop);
			this.Controls.Add(this.button_record);
			this.Name = "form_clicker";
			this.Text = "Clicker";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button_record;
		private System.Windows.Forms.Button button_stop;
		private System.Windows.Forms.Button button_run;
		private System.ComponentModel.BackgroundWorker background_worker;
	}
}