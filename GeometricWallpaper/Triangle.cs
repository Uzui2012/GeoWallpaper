using System.Drawing;

namespace GeometricWallpaper
{
	class Triangle
	{
		private Point[] points;

		public Triangle(Point p1, Point p2, Point p3)
		{
			this.points = new Point[]{ p1, p2, p3};
		}

		public Triangle(Point[] points)
		{
			this.points = new Point[] { points[0], points[1], points[2] };
		}

		public void draw(Graphics g, Pen pen)
		{
			g.DrawPolygon(pen, points);
		}

		public Point getPointAtIndex(int index)
		{
			return points[index];
		}

		public Point[] toPoints()
		{
			return points;
		}

		public Point this[int index]
		{
			get
			{
				return points[index];
			}

			set
			{
				points[index] = value;
			}
		}
	}
}
