using System;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Media3D;

namespace RayTracer
{
    public class Tracer
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Background { get; set; }
        public Scene Scene { get; set; }

        public Tracer(Scene s)
        {
            Width = 640;
            Height = 480;
            Background = Color.Black;
            Scene = s;
        }

        public Bitmap Render()
        {
            var bm = new Bitmap(Width, Height);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    bm.SetPixel(x, y, GetColorForViewPortPixel(x, y).ToColor());        
                }
            }

            return bm;
        }

        private RealColor GetColorForViewPortPixel(int x, int y)
        {
            var ray = GetRayForViewportPixel(x, y);
            return GetColorForRay(ray) ?? new RealColor(Background);
        }

        private RealColor? GetColorForRay(Ray viewRay, int iteration = 1)
        {
            var materialColor = new RealColor();

            var intersection = Scene.Objects
                .Select(o => new {Obj = o, Dist = o.Intersect(viewRay)})
                .Where(o => o.Dist != null)
                .OrderBy(f => f.Dist)
                .FirstOrDefault();

            if (intersection != null)
            {
                var start = viewRay.Start + (intersection.Dist.Value*viewRay.Direction);
                var normal = start - intersection.Obj.Position;
                normal.Normalize();

                foreach (var light in Scene.Lights)
                {
                    var lightRay = Ray.FromStartAndEndPoints(start, light.Position);

                    if (Scene.Objects.Any(o => o.Intersect(lightRay) != null))
                        continue;

                    if (Vector3D.DotProduct(normal, lightRay.Direction) < 0)
                        continue;

                    var lambert = Vector3D.DotProduct(lightRay.Direction, normal);

                    materialColor += lambert * light.Color * intersection.Obj.Material.Diffuse;

                }

                if (iteration > 10 || intersection.Obj.Material.ReflectionCoefficient <= 0)
                    return materialColor;

                var reflectionAngle = 2.0f * (Vector3D.DotProduct(viewRay.Direction, normal));
                var reflectionRay = new Ray(start, viewRay.Direction - reflectionAngle * normal);

                var reflectedColor = GetColorForRay(reflectionRay, iteration + 1);

                if (reflectedColor == null)
                    return materialColor;

                return materialColor +
                       reflectedColor*intersection.Obj.Material.ReflectionCoefficient;
            }

            //return null;
            return new RealColor(Background);
        }

        private Ray GetRayForViewportPixel(int x, int y)
        {
            return new Ray(new Vector3D(x, y, -1000), new Vector3D(0, 0, 1));
        }
    }
}
