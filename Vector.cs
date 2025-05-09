using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector_Meshes
{
    public struct Vector
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string? ToString()
        {
            return $"{X}, {Y}";
        }

        public static Vector Zero => new Vector(0, 0);

        public static float Dot(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static float Distance(Vector a, Vector b)
        {
            var relative = a - b;
            return MathF.Sqrt(Dot(relative, relative));
        }

        public static implicit operator Point(Vector v)
        {
            return new Point((int)v.X, (int)v.Y);
        }

        public static implicit operator PointF(Vector v)
        {
            return new PointF(v.X, v.Y);
        }

        public static implicit operator Size(Vector v)
        {
            return new Size((int)v.X, (int)v.Y);
        }

        public static implicit operator SizeF(Vector v)
        {
            return new SizeF(v.X, v.Y);
        }

        public static implicit operator Vector(Point p)
        {
            return new Vector(p.X, p.Y);
        }

        public static implicit operator Vector(PointF p)
        {
            return new Vector(p.X, p.Y);
        }

        public static implicit operator Vector(Size s)
        {
            return new Vector(s.Width, s.Height);
        }

        public static implicit operator Vector(SizeF s)
        {
            return new Vector(s.Width, s.Height);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator -(Vector a)
        {
            return new Vector(-a.X, -a.Y);
        }

        public static Vector operator *(Vector a, float b)
        {
            return new Vector(a.X * b, a.Y * b);
        }

        public static Vector operator /(Vector a, float b) {
            //if (b == 0)
            //    throw new DivideByZeroException();
            return new Vector(a.X / b, a.Y / b);
        }
        public static Vector operator *(float a, Vector b)
        {
            return new Vector(a * b.X, a * b.Y);
        }

        public static Vector operator /(float a, Vector b)
        {
            //if (b.X == 0 || b.Y == 0)
            //    throw new DivideByZeroException();
            return new Vector(a / b.X, a / b.Y);
        }
    }
}
