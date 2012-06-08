using System.Windows.Media.Media3D;

namespace RayTracer
{
    public class PointLight
    {
        public RealColor Color { get; private set; }
        public Vector3D Position { get; private set; }

        public PointLight(RealColor color, float x, float y, float z)
        {
            Color = color;
            Position = new Vector3D(x, y, z);
        }
    }
}