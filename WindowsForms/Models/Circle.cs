using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WindowsForms.Interfaces;
namespace WindowsForms
{
    public class Circle:IShape
    {
        public Point Center { get; set; }
        public Point PointOnCircleOutline { get; set; }
        public Color Color { get; set; }
        public int RgbaColor { get; set; }

        public Circle()
        {
            Center = new Point();
            PointOnCircleOutline = new Point();
            Color = Color.Gold;
        }
        public Circle(Point center, Point pointOnCircleOutline)
        {
            Center = center;
            PointOnCircleOutline = pointOnCircleOutline;
            Color = Color.Gold;
        }
        
        public float GetRadius()
        {
            return GetDistanseBetweenPoints(Center, PointOnCircleOutline);
        }


        public Point GetUpperLeftCorner()
        {
            return new Point(Center.X - Convert.ToInt32((GetRadius())), Center.Y - Convert.ToInt32((GetRadius())));
        }

        static public float GetDistanseBetweenPoints(Point a, Point b)
        {
            double tmp1 = Math.Pow((a.X - b.X), 2);
            double tmp2 = Math.Pow((a.Y - b.Y), 2);
            return float.Parse(Math.Sqrt(tmp1 + tmp2).ToString());
        }

        public void  Move(Point newCenter)
        {
            Point previouseCenter = this.Center;
            int xShifting = previouseCenter.X - newCenter.X;
            int yShifting = previouseCenter.Y - newCenter.Y;
            this.Center = newCenter;
            this.PointOnCircleOutline = new Point(this.PointOnCircleOutline.X - xShifting, this.PointOnCircleOutline.Y - yShifting);
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(this.Color), this.GetUpperLeftCorner().X, this.GetUpperLeftCorner().Y, this.GetRadius() * 2, this.GetRadius() * 2);
        }
    }
}
