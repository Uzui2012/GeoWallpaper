using Microsoft.Win32;
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
		
		Pen pen = new Pen(Color.Black, 1);
		Graphics g = null;

		public Form1()
		{
			InitializeComponent();
			
			this.maxDist = Convert.ToInt16(numericUpDown1.Value);
		}

		private void drawLines()
		{
			Point[] points = GeneratePoints();
			/*
			String debugMsg = "0:";
			for (int i = 0; i < points.Length; i++)
			{
				debugMsg = debugMsg + points[i] + "\n" + (i + 1);
			}
			MessageBox.Show(debugMsg);
			*/
			for (int i = 0; i < points.Length; i++)
			{
				Point[] nPoints = findNearest5Points(points[i], points);
				for (int j = 0; j < 5; j++)
				{
					String debugMsg = "";
					for (int k = 0; k < nPoints.Length; k++)
					{
						debugMsg = debugMsg + nPoints[k] + "\n" + (k + 1);
					}
					MessageBox.Show(debugMsg);
					g.DrawLine(pen, points[i].X, points[i].Y, nPoints[j].X, nPoints[j].Y);
				}
			}
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			canvasPanel.Refresh();
		}

		private Point[] GeneratePoints()
		{
			Random rand = new Random();
			Point[] points = new Point[20];
			for(int i = 0; i < points.Length; i++){
				points[i].X = rand.Next(1, 101) * 5;
				points[i].Y = rand.Next(1, 101) * 5;
			}
			return points;
		}

		private Point[] findNearest5Points(Point p, Point[] points){
			Point[] results = new Point[5];
			results[0] = points[0];
			results[1] = points[1];
			results[2] = points[2];
			results[3] = points[3];
			results[4] = points[4];
			/*
			String debugMsg = "";
			for (int i = 0; i < results.Length; i++)
			{
				debugMsg = debugMsg + results[i] + "\n" + (i + 1);
			}
			MessageBox.Show(debugMsg);			 
			*/
			
			for (int i = 0; i < points.Length; i++){
				for (int j = 0; j < 5; j++){
					if(p != points[i]){
						if(calcDistance(p, points[i]) < calcDistance(p, results[j])){
							results[j] = points[i];
						}
					}
				}
			}

			return results;
		}
		private double calcDistance(Point p1, Point p2)
		{
			int dx = p2.X - p1.X;
			int dy = p2.Y - p1.Y;
			return Math.Sqrt(Convert.ToDouble(dx*dx + dy*dy));
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			maxDist = Convert.ToInt16(numericUpDown1.Value);
		}

		private void canvasPanel_Paint(object sender, PaintEventArgs e)
		{
			g = canvasPanel.CreateGraphics();
			drawLines();
		}
	}

}

