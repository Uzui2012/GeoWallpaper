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
		private Pen pen = new Pen(Color.Black, 1);
		private Graphics g = null;

		public Form1()
		{
			InitializeComponent();

			this.maxDist = Convert.ToInt16(numericUpDown1.Value);
		}

		private void drawLines()
		{
			Point[] points = generatePoints();
			Point[] seedTriangle = generateTrianglePoints(points);
			g.DrawPolygon(pen, seedTriangle);
		}

		private Point[] generateTrianglePoints(Point[] points)
		{
			Point x_0 = points[0];
			Point x_i = points[1];
			Point x_j = points[1];
			int curi = 1;
			for (int i = 1; i < points.Length; i++)
			{
				if (calcDistance(x_0, points[i]) < calcDistance(x_0, x_i))
				{
					x_i = points[i];
					curi = i;
				}
			}
			for (int j = 1; j < points.Length; j++)
			{
				if (curi != j)
				{
					if (calcDistance(x_0, calcCircumCentre(x_0, x_i, points[j])) < calcDistance(x_0, calcCircumCentre(x_0, x_i, x_j)))
					{
						x_j = points[j];
					}
				}
			}
			Point[] result = { x_0, x_i, x_j };
			return result;
		}

		private Point calcCircumCentre(Point p1, Point p2, Point p3)
		{
			Point circumCenter = new Point();
			circumCenter.X = ((p1.X * p1.X + p1.Y * p1.Y) * (p2.Y - p3.Y) + (p2.X * p2.X + p2.Y * p2.Y) * (p3.Y - p1.Y) + (p2.X * p3.X + p3.Y * p3.Y) * (p1.Y - p2.Y)) / (2 * (p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)));
			circumCenter.Y = ((p1.X * p1.X + p1.Y * p1.Y) * (p3.Y - p2.Y) + (p2.X * p2.X + p2.Y * p2.Y) * (p1.Y - p3.Y) + (p2.X * p3.X + p3.Y * p3.Y) * (p2.Y - p1.Y)) / (2 * (p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)));
			Rectangle rect = new Rectangle(Convert.ToInt16(circumCenter.X - calcDistance(p1, circumCenter)), Convert.ToInt16(circumCenter.Y-calcDistance(p1, circumCenter)), 2* Convert.ToInt16(calcDistance(p1, circumCenter)), 2*Convert.ToInt16(calcDistance(p1, circumCenter)));
			g.DrawEllipse(pen, rect);
			return circumCenter;
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			canvasPanel.Refresh();
		}

		private Point[] generatePoints()
		{
			Random rand = new Random();
			Point[] points = new Point[20];
			List<int> randomXSet = new List<int>();
			int numX = 0;
			List<int> randomYSet = new List<int>();
			int numY = 0;
			for (int i = 0; i < points.Length; i++)
			{
				numX = rand.Next(1, 101);
				numY = rand.Next(1, 101);
				while (randomXSet.Contains(numX))
				{
					numX = rand.Next(1, 101);
				}
				while (randomYSet.Contains(numY))
				{
					numY = rand.Next(1, 101);
				}
				randomXSet.Add(numX);
				randomYSet.Add(numY);
				points[i].X = numX;
				points[i].Y = numY;
				g.DrawLine(pen, points[i].X, points[i].Y, points[i].X+1, points[i].Y+1);
			}
			return points;
		}

		private double calcDistance(Point p1, Point p2)
		{
			int dx = p2.X - p1.X;
			int dy = p2.Y - p1.Y;
			return Math.Sqrt(Convert.ToDouble(dx * dx + dy * dy));
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

