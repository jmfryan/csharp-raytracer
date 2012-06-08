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
                    bm.SetPixel(x, y, GetColorForViewPortPixel(x, y));        
                }
            }

            return bm;
        }

        private Color GetColorForViewPortPixel(int x, int y)
        {
            var ray = GetRayForViewportPixel(x, y);

            double r = 0, g = 0, b = 0;

            var intersection = Scene.Objects
                                         .Select(o => new { Obj = o, Dist = o.Intersect(ray)})
                                         .Where(o => o.Dist != null)
                                         .OrderBy(f => f.Dist)
                                         .FirstOrDefault();

            if (intersection != null)
            {
                var start = ray.Start + (intersection.Dist.Value*ray.Direction);
                var normal = start - intersection.Obj.Position;
                normal.Normalize();

                foreach (var light in Scene.Lights)
                {
                    var lightRay = Ray.FromStartAndEndPoints(start, light.Position);

                    if(Scene.Objects.Any(o => o.Intersect(lightRay) != null))
                        continue;

                    if (Vector3D.DotProduct(normal, lightRay.Direction) < 0)
                        continue;

                    var lambert = Vector3D.DotProduct(lightRay.Direction, normal);

                    r += lambert * light.Color.R * intersection.Obj.Material.Diffuse.R;
                    g += lambert * light.Color.G * intersection.Obj.Material.Diffuse.G;
                    b += lambert * light.Color.B * intersection.Obj.Material.Diffuse.B;
                }

                return new RealColor(r, g, b).ToColor();
            }

            return Background;
        }

        private Ray GetRayForViewportPixel(int x, int y)
        {
            return new Ray(new Vector3D(x, y, -1000), new Vector3D(0, 0, 1));
        }
    }
}
