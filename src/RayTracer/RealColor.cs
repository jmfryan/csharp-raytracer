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
            G = c.B / 255f;
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
    }
}