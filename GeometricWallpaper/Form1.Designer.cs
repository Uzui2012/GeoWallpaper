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
			this.pBox = new System.Windows.Forms.PictureBox();
			this.numTriangles = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.pBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTriangles)).BeginInit();
			this.SuspendLayout();
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(748, 779);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(372, 233);
			this.btnNew.TabIndex = 0;
			this.btnNew.Text = "Generate New Image";
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// pBox
			// 
			this.pBox.Location = new System.Drawing.Point(300, 25);
			this.pBox.Name = "pBox";
			this.pBox.Size = new System.Drawing.Size(1280, 720);
			this.pBox.TabIndex = 1;
			this.pBox.TabStop = false;
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
			this.Controls.Add(this.pBox);
			this.Controls.Add(this.btnNew);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.pBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTriangles)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.PictureBox pBox;
		private System.Windows.Forms.NumericUpDown numTriangles;
	}
}

