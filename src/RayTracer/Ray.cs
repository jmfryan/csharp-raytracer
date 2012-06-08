using System.Windows.Media.Media3D;

namespace RayTracer
{
    public struct Ray
    {
        public Vector3D Start { get; private set; }
        public Vector3D Direction { get; private set; }

        public Ray(Vector3D start, Vector3D direction) : this()
        {
            Start = start;
            direction.Normalize();
            Direction = direction;
        }

        public static Ray FromStartAndEndPoints(Vector3D start, Vector3D end)
        {
            return new Ray(start, Vector3D.Subtract(end, start));
        }
    }
}