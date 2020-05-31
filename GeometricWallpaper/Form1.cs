﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricWallpaper
{
	public partial class Form1 : Form
	{
		private int maxDist;
		public Form1()
		{
			InitializeComponent();
			this.maxDist = Convert.ToInt16(numericUpDown1.Value);
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			this.Invalidate();
		}

		private int[][] GeneratePoints()
		{
			Random rand = new Random();
			int[][] points = new int[1000][];
			for(int i = 0; i < points.Length; i++){
				points[i] = new int[2];
				points[i][0] = rand.Next(1, 101) * 5;
				points[i][1] = rand.Next(1, 101) * 5;
			}
			return points;
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Pen pen = new Pen(Color.Black, 1);
			g.DrawLine(pen, 5, 5, 5, 500);
			g.DrawLine(pen, 5, 5, 995, 5);
			g.DrawLine(pen, 995, 5, 995, 500);
			g.DrawLine(pen, 5, 500, 995, 500);
			int[][] points = GeneratePoints();
			
			for(int i = 0; i < points.Length; i++){
				for(int j = 0; j < points.Length; j++){
					int[] check = { points[i][0], points[i][1], 0 };
					if(calcDistance(points[i], points[j]) < maxDist){
						g.DrawLine(pen, points[i][0], points[i][1], points[j][0], points[j][1]);
					}
				}
				pen.Color = Color.Red;
				g.DrawLine(pen, points[i][0], points[i][1], points[i][0]+1, points[i][1]+1);
				pen.Color = Color.Black;
			}
		}
		private double calcDistance(int[] p1, int[] p2)
		{
			int dx = p2[0] - p1[0];
			int dy = p2[1] - p1[1];
			return Math.Sqrt(Convert.ToDouble(dx*dx + dy*dy));
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.Paint += Form1_Paint;
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			maxDist = Convert.ToInt16(numericUpDown1.Value);
		}
	}



	public sealed class Wallpaper
	{
		Wallpaper() { }

		const int SPI_SETDESKWALLPAPER = 20;
		const int SPIF_UPDATEINIFILE = 0x01;
		const int SPIF_SENDWININICHANGE = 0x02;

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

		public enum Style : int
		{
			Tiled,
			Centered,
			Stretched
		}

		public static void Set(Uri uri, Style style)
		{
			System.IO.Stream s = new System.Net.WebClient().OpenRead(uri.ToString());

			System.Drawing.Image img = System.Drawing.Image.FromStream(s);
			string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
			img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

			RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
			if (style == Style.Stretched)
			{
				key.SetValue(@"WallpaperStyle", 2.ToString());
				key.SetValue(@"TileWallpaper", 0.ToString());
			}

			if (style == Style.Centered)
			{
				key.SetValue(@"WallpaperStyle", 1.ToString());
				key.SetValue(@"TileWallpaper", 0.ToString());
			}

			if (style == Style.Tiled)
			{
				key.SetValue(@"WallpaperStyle", 1.ToString());
				key.SetValue(@"TileWallpaper", 1.ToString());
			}

			SystemParametersInfo(SPI_SETDESKWALLPAPER,
				0,
				tempPath,
				SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
		}
	}
}

