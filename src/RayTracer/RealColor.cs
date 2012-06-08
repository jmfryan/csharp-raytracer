using System;
using System.Drawing;

namespace RayTracer
{
    public struct RealColor
    {
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }

        public RealColor(Color c) : this()
        {
            R = c.R / 255f;
            G = c.G / 255f;
            B = c.B / 255f;
        }

        public RealColor(double r, double g, double b) : this()
        {
            R = r;
            G = g;
            B = b;
        }

        public Color ToColor()
        {
            return Color.FromArgb((int)Math.Min(R*255d, 255d), (int)Math.Min(G*255d, 255d), (int)Math.Min(B*255d, 255d));
        }

        public static RealColor operator +(RealColor c1, RealColor c2)
        {
            return new RealColor(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B);
        }

        public static RealColor operator *(RealColor c, double d)
        {
            return new RealColor(c.R * d, c.G * d, c.B * d);
        }

        public static RealColor operator *(RealColor c1, RealColor c2)
        {
            return new RealColor(c1.R * c2.R, c1.G * c2.G, c1.B * c1.B);
        }

        public static RealColor operator *(double d, RealColor c)
        {
            return c*d;
        }
    }
}