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
			this.numericUD_speed = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.checkbox_l2_drag_fix = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkbox_repeat = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numericUD_speed)).BeginInit();
			this.SuspendLayout();
			// 
			// button_record
			// 
			this.button_record.Font = new System.Drawing.Font("Dina ttf 10px", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button_record.Location = new System.Drawing.Point(12, 12);
			this.button_record.Name = "button_record";
			this.button_record.Size = new System.Drawing.Size(261, 40);
			this.button_record.TabIndex = 0;
			this.button_record.Text = "Record";
			this.button_record.UseVisualStyleBackColor = true;
			// 
			// button_stop
			// 
			this.button_stop.Enabled = false;
			this.button_stop.Font = new System.Drawing.Font("Dina ttf 10px", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button_stop.Location = new System.Drawing.Point(12, 66);
			this.button_stop.Name = "button_stop";
			this.button_stop.Size = new System.Drawing.Size(261, 40);
			this.button_stop.TabIndex = 1;
			this.button_stop.Text = "Stop";
			this.button_stop.UseVisualStyleBackColor = true;
			// 
			// button_run
			// 
			this.button_run.Enabled = false;
			this.button_run.Font = new System.Drawing.Font("Dina ttf 10px", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button_run.Location = new System.Drawing.Point(12, 118);
			this.button_run.Name = "button_run";
			this.button_run.Size = new System.Drawing.Size(261, 40);
			this.button_run.TabIndex = 2;
			this.button_run.Text = "Run";
			this.button_run.UseVisualStyleBackColor = true;
			// 
			// background_worker
			// 
			this.background_worker.WorkerSupportsCancellation = true;
			// 
			// numericUD_speed
			// 
			this.numericUD_speed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUD_speed.Font = new System.Drawing.Font("Dina ttf 10px", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numericUD_speed.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numericUD_speed.Location = new System.Drawing.Point(440, 14);
			this.numericUD_speed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUD_speed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUD_speed.Name = "numericUD_speed";
			this.numericUD_speed.Size = new System.Drawing.Size(86, 36);
			this.numericUD_speed.TabIndex = 3;
			this.numericUD_speed.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Dina ttf 10px", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(331, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 30);
			this.label1.TabIndex = 4;
			this.label1.Text = "Speed:";
			// 
			// checkbox_l2_drag_fix
			// 
			this.checkbox_l2_drag_fix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkbox_l2_drag_fix.AutoSize = true;
			this.checkbox_l2_drag_fix.Font = new System.Drawing.Font("Dina ttf 10px", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkbox_l2_drag_fix.Location = new System.Drawing.Point(336, 70);
			this.checkbox_l2_drag_fix.Name = "checkbox_l2_drag_fix";
			this.checkbox_l2_drag_fix.Size = new System.Drawing.Size(212, 34);
			this.checkbox_l2_drag_fix.TabIndex = 4;
			this.checkbox_l2_drag_fix.Text = "La2 drag fix";
			this.checkbox_l2_drag_fix.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Dina ttf 10px", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(532, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(28, 30);
			this.label2.TabIndex = 4;
			this.label2.Text = "%";
			// 
			// checkbox_repeat
			// 
			this.checkbox_repeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkbox_repeat.AutoSize = true;
			this.checkbox_repeat.Checked = true;
			this.checkbox_repeat.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkbox_repeat.Font = new System.Drawing.Font("Dina ttf 10px", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkbox_repeat.Location = new System.Drawing.Point(336, 122);
			this.checkbox_repeat.Name = "checkbox_repeat";
			this.checkbox_repeat.Size = new System.Drawing.Size(122, 34);
			this.checkbox_repeat.TabIndex = 5;
			this.checkbox_repeat.Text = "Repeat";
			this.checkbox_repeat.UseVisualStyleBackColor = true;
			// 
			// form_clicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(572, 171);
			this.Controls.Add(this.checkbox_repeat);
			this.Controls.Add(this.checkbox_l2_drag_fix);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numericUD_speed);
			this.Controls.Add(this.button_run);
			this.Controls.Add(this.button_stop);
			this.Controls.Add(this.button_record);
			this.Name = "form_clicker";
			this.Text = "ZClicker";
			((System.ComponentModel.ISupportInitialize)(this.numericUD_speed)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button_record;
		private System.Windows.Forms.Button button_stop;
		private System.Windows.Forms.Button button_run;
		private System.ComponentModel.BackgroundWorker background_worker;
		private System.Windows.Forms.NumericUpDown numericUD_speed;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkbox_l2_drag_fix;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkbox_repeat;
	}
}