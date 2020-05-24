namespace GeometricWallpaper
{
	partial class Form1
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
			this.btnNew = new System.Windows.Forms.Button();
			this.numTriangles = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numTriangles)).BeginInit();
			this.SuspendLayout();
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(566, 800);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(266, 134);
			this.btnNew.TabIndex = 0;
			this.btnNew.Text = "Generate New Image";
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// numTriangles
			// 
			this.numTriangles.Location = new System.Drawing.Point(1713, 186);
			this.numTriangles.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.numTriangles.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numTriangles.Name = "numTriangles";
			this.numTriangles.Size = new System.Drawing.Size(104, 26);
			this.numTriangles.TabIndex = 2;
			this.numTriangles.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1898, 1024);
			this.Controls.Add(this.numTriangles);
			this.Controls.Add(this.btnNew);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximumSize = new System.Drawing.Size(1920, 1080);
			this.MinimumSize = new System.Drawing.Size(1920, 1080);
			this.Name = "Form1";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
			((System.ComponentModel.ISupportInitialize)(this.numTriangles)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.NumericUpDown numTriangles;
	}
}

