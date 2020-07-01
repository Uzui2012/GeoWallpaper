using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GeometricWallpaper
{
	public partial class Form1 : Form
	{
		private int numPoints;
		private Pen pen = new Pen(Color.Black, 1);
		private Graphics g = null;

		public Form1()
		{
			InitializeComponent();
			this.numPoints = Convert.ToInt16(numericUpDown1.Value);
		}

		private void drawLines()
		{
			Point[] points = generatePoints();
			Triangle seedTriangle = new Triangle(generateTrianglePoints(points));

			Point circumCentre = calcCircumCentre(seedTriangle[0], seedTriangle[1], seedTriangle[2]);
			
			g.DrawPolygon(pen, seedTriangle.toPoints());

			List<Point> orderedListOfPoints = reorderPoints(points, circumCentre);
			orderedListOfPoints.Remove(seedTriangle[0]);
			orderedListOfPoints.Remove(seedTriangle[1]);
			orderedListOfPoints.Remove(seedTriangle[2]);

			List<Point> hull = seedTriangle.toPoints().ToList();
			Point X = calcCircumCentre(seedTriangle[0], seedTriangle[1], seedTriangle[2]);
			hull = hull.ToArray().OrderBy(x => Math.Atan2(x.X - X.X, x.Y - X.Y)).ToArray().ToList();

			foreach (Point p in orderedListOfPoints)
			{
				//MessageBox.Show("");
				//calculate which points of this convex hull are facets to current p
				List<Point> facets = new List<Point>();				

				for (int i = 0; i < hull.Count - 1; i++)
				{
					if (isLeft(hull.ElementAt(i), hull.ElementAt(i + 1), p) != isLeft(hull.ElementAt(i), hull.ElementAt(i + 1), calcHullAvg(hull)))
					{
						
						facets.Add(hull.ElementAt(i));
						facets.Add(hull.ElementAt(i+1));
					}
				}

				if (isLeft(hull.ElementAt(hull.Count - 1), hull.ElementAt(0), p) != isLeft(hull.ElementAt(hull.Count - 1), hull.ElementAt(0), calcHullAvg(hull)))
				{
					facets.Add(hull.ElementAt(hull.Count - 1));
					facets.Add(hull.ElementAt(0));
				}

				//facets = facets.Distinct().ToList();

				//draw to those such points
				foreach (Point f in facets)
				{
					g.DrawLine(pen, p.X, p.Y, f.X, f.Y);
				}

				//calculate next current convex hull
				hull = getConvexHull(hull, p);
				//pen.Color = Color.Red;
				//g.DrawPolygon(pen, hull.ToArray());
				//pen.Color = Color.Black;
				//hull = hull.ToArray().OrderBy(x => Math.Atan2(x.X - X.X, x.Y - X.Y)).ToArray().ToList();
			}

		}

		private bool isLeft(Point a, Point b, Point c)
		{
			return ((b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X)) > 0;
		}

		private Point calcHullAvg(List<Point> hull)
		{
			double avgX = 0, avgY = 0;
			foreach (Point p in hull)
			{
				avgX = avgX + p.X;
				avgY = avgY + p.Y;
			}
			avgX = avgX / hull.Count;
			avgY = avgY / hull.Count;
			Point result = new Point((int)avgX, (int)avgY);
			//pen.Color = Color.Blue;
			//g.DrawLine(pen, result.X, result.Y, result.X + 3, result.Y);
			//pen.Color = Color.Black;
			return result;
		}

		private List<Point> getConvexHull(List<Point> hull, Point p)
		{
			hull.Add(p);			
			hull = hull.ToArray().OrderBy(x => x.X).ToArray().ToList();
			List<Point> U = new List<Point>();
			List<Point> L = new List<Point>();

			for(int i = 0; i < hull.Count; i++)
			{
				while(L.Count >= 2 && cross(L.ElementAt(L.Count - 2), L.ElementAt(L.Count - 1), hull.ElementAt(i)) <=0)//stuff
				{
					L.RemoveAt(L.Count-1);
				}
				L.Add(hull.ElementAt(i));
			}
			for (int i = hull.Count-1; i >= 0; i--)
			{
				while (U.Count >= 2 && cross(U.ElementAt(U.Count - 2), U.ElementAt(U.Count - 1), hull.ElementAt(i)) <= 0)//stuff
				{
					U.RemoveAt(U.Count - 1);
				}
				U.Add(hull.ElementAt(i));
			}
			L.RemoveAt(L.Count - 1);
			U.RemoveAt(U.Count - 1);
			L.AddRange(U);
			return L;
		}

		private Double cross(Point O, Point A, Point B)
		{
			return (A.X - O.X) * (long)(B.Y - O.Y) - (A.Y - O.Y) * (long)(B.X - O.X);
		}

		private List<Point> reorderPoints(Point[] points, Point circumCentre)
		{
			List<Point> listOfPoints = new List<Point>();
			for (int i = 0; i < points.Length; i++)
			{
				for (int j = 0; j < listOfPoints.Count; j++)
				{
					if (calcDistance(points[i], circumCentre) < calcDistance(listOfPoints[j], circumCentre))
					{
						listOfPoints.Insert(j, points[i]);
						goto exitLoop;
					}
				}
				exitLoop:;
				if (!listOfPoints.Contains(points[i]))
				{
					listOfPoints.Add(points[i]);
				}
			}
			/*
			String sPoints = "";
			foreach (Point p in listOfPoints)
			{
				sPoints = sPoints + p.X + ", " + p.Y + ", Dist: " + calcDistance(circumCentre, p) + "\n";
			}
			MessageBox.Show("Sorted Points:\n" + sPoints);*/
			return listOfPoints;
		}

		private Point[] generateTrianglePoints(Point[] points)
		{
			Point x_0 = points[0];
			Point x_i = points[1];
			Point x_j = points[2];
			int x_iIndex = 1;
			for (int i = 1; i < points.Length; i++)
			{
				if (calcDistance(x_0, points[i]) < calcDistance(x_0, x_i))
				{
					x_i = points[i];
					x_iIndex = i;
				}
			}
			if (x_iIndex != 1)
			{
				//MessageBox.Show("x_iIndex != 1");
				x_j = points[1];
			}

			for (int j = 1; j < points.Length; j++)
			{
				if (x_iIndex != j)
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

		private Point calcCircumCentre(Point a, Point b, Point c)
		{
			var A = b.X - a.X;
			var B = b.Y - a.Y;
			var C = c.X - a.X;
			var D = c.Y - a.Y;
			var E = A * (a.X + b.X) + B * (a.Y + b.Y);
			var F = C * (a.X + c.X) + D * (a.Y + c.Y);
			var G = 2 * (A * (c.Y - b.Y) - B * (c.X - b.X));
			double minx;
			double miny;
			double dx;
			double dy;

			double radius;
			Point circumCenter = new Point();

			if (Math.Abs(G) < 0.00001)
			{
				minx = Math.Min(a.X, Math.Min(b.X, c.X));
				miny = Math.Min(a.Y, Math.Min(b.Y, c.Y));
				dx = (Math.Max(a.X, Math.Max(b.X, c.X)) - minx) * 0.5;
				dy = (Math.Max(a.Y, Math.Max(b.Y, c.Y)) - miny) * 0.5;

				circumCenter.X = Convert.ToInt16(minx + dx);
				circumCenter.Y = Convert.ToInt16(miny + dy);
				radius = Math.Sqrt(dx * dx + dy * dy);
			}

			else
			{
				circumCenter.X = (D * E - B * F) / G;
				circumCenter.Y = (A * F - C * E) / G;
				dx = circumCenter.X - a.X;
				dy = circumCenter.Y - a.Y;
				radius = Math.Sqrt(dx * dx + dy * dy);

			}

			/*
			circumCenter.X = ((p1.X * p1.X + p1.Y * p1.Y) * (p2.Y - p3.Y) + (p2.X * p2.X + p2.Y * p2.Y) * (p3.Y - p1.Y) + (p3.X * p3.X + p3.Y * p3.Y) * (p1.Y - p2.Y))
								/
								(2 * (p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)));

			circumCenter.Y = ((p1.X * p1.X + p1.Y * p1.Y) * (p3.X - p2.X) + (p2.X * p2.X + p2.Y * p2.Y) * (p1.X - p3.X) + (p2.X * p3.X + p3.Y * p3.Y) * (p2.X - p1.X))
								/
								(2 * (p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)));

			double radius = calcDistance(circumCenter, p1); */
			/*Point[] triangle = { a, b, c };

			pen.Color = Color.Red;
			//g.DrawPolygon(pen, triangle);
			pen.Color = Color.Blue;
			//Rectangle rect = new Rectangle(Convert.ToInt16(circumCenter.X - radius), Convert.ToInt16(circumCenter.Y - radius), Convert.ToInt16(radius * 2), Convert.ToInt16(radius * 2));

			PointF rectOrigin = new PointF((float)(circumCenter.X - radius), (float)(circumCenter.Y - radius));
			RectangleF rect = new RectangleF(rectOrigin, new SizeF((float)radius * 2F, (float)radius * 2F));
			Point rp1 = new Point(Convert.ToInt16(rect.X), Convert.ToInt16(rect.Y));
			Point rp3 = new Point(Convert.ToInt16(rect.X), Convert.ToInt16(rect.Y + rect.Height));
			Point rp2 = new Point(Convert.ToInt16(rect.X + rect.Width), Convert.ToInt16(rect.Y + rect.Height));
			Point rp4 = new Point(Convert.ToInt16(rect.X + rect.Width), Convert.ToInt16(rect.Y));
			Point[] rparray = { rp1, rp4, rp2, rp3 };
			g.DrawPolygon(pen, rparray);
			g.DrawEllipse(pen, rect);

			pen.Color = Color.Black;*/
			//MessageBox.Show("X: " + circumCenter.X + ", Y: " + circumCenter.Y + "\np1:" + p1.ToString() + "\np2:" + p2.ToString() + "\np3:" + p3.ToString() + "\nRad: " + radius);

			return circumCenter;
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			canvasPanel.Refresh();
		}

		private Point[] generatePoints()
		{
			Random rand = new Random();
			Point[] points = new Point[numPoints];
			List<int> randomXSet = new List<int>();
			int numX = 0;
			List<int> randomYSet = new List<int>();
			int numY = 0;
			int count = 1;
			for (int i = 0; i < points.Length; i++)
			{
				numX = rand.Next(1, 101);
				numY = rand.Next(1, 101);
				while (randomXSet.Contains(numX))
				{
					numX = rand.Next(1, 101);
					count++;
				}
				while (randomYSet.Contains(numY))
				{
					numY = rand.Next(1, 101);
					count++;
				}
				randomXSet.Add(numX);
				randomYSet.Add(numY);
				points[i].X = numX *8;
				points[i].Y = numY *5;
				g.DrawLine(pen, points[i].X, points[i].Y, points[i].X + 1, points[i].Y + 1);
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
			numPoints = Convert.ToInt16(numericUpDown1.Value);
		}

		private void canvasPanel_Paint(object sender, PaintEventArgs e)
		{
			g = canvasPanel.CreateGraphics();
			drawLines();
		}
	}

}

