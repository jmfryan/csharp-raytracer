using System;
using System.Windows.Media.Media3D;

namespace RayTracer
{
    public class Sphere
    {
        public float Radius { get; private set; }
        public Material Material { get; private set; }
        public Vector3D Position { get; private set; }

        public Sphere(float radius, float x, float y, float z, Material m1)
        {
            Radius = radius;
            Material = m1;
            Position = new Vector3D(x, y, z);
        }

        public double? Intersect(Ray ray)
        {
            var dist = ray.Start - Position;
            
            var a = Vector3D.DotProduct(ray.Direction, ray.Direction);
            var b = 2*Vector3D.DotProduct(dist, ray.Direction);
            var c = Vector3D.DotProduct(dist, dist) - (Radius * Radius);

            var discriminant = b*b - 4*a*c;

            if(discriminant < 0)
                return null;

            var t1 = (-b + Math.Sqrt(discriminant))/2*a;
            var t2 = (-b - Math.Sqrt(discriminant))/2*a;

            if (t1 < 0.5 && t2 < 0.5)
                return null;

            if (t1 < 0.5)
                t1 = Double.MaxValue;

            if (t2 < 0.5)
                t2 = Double.MaxValue;

            return Math.Min(t1, t2);
        }
    }
}