using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RayTracer.Tests
{
    [TestFixture]
    public class RayTracing_Tests
    {
        [Test]
        public void Rendering_empty_scene_should_be_correct_size_and_background_color()
        {
            var t = new Tracer(new Scene());
            t.Background = Color.FromArgb(255, 0, 0);

            var result = t.Render();

            for(var y = 0; y < t.Height; y++)
                for (int x = 0; x < t.Width; x++)
                    Assert.AreEqual(t.Background, result.GetPixel(x, y));
        }

        [Test]
        public void Render_test_scene()
        {
            var s = new Scene();

            var m1 = new Material { Diffuse = new RealColor(Color.Yellow), ReflectionCoefficient = 0.5 };
            var m2 = new Material { Diffuse = new RealColor(Color.Cyan), ReflectionCoefficient = 0.5 };
            var m3 = new Material { Diffuse = new RealColor(Color.Magenta), ReflectionCoefficient = 0.5 };

            s.AddObject(new Sphere(100, 233, 290, 0, m1));
            s.AddObject(new Sphere(100, 407, 290, 0, m2));
            s.AddObject(new Sphere(100, 320, 140, 0, m3));
            s.AddObject(new Sphere(30, 160, 240, -50, m3));

            s.AddLightSource(new PointLight(new RealColor(1, 1, 1), 0, 240, -100));
            s.AddLightSource(new PointLight(new RealColor(0.6f, 0.6f, 0.6f), 640, 240, -10000));

            var t = new Tracer(s);
            //t.Background = Color.Gray;

            var image = t.Render();

            string imageLocation = @"C:\render.png";
            if (File.Exists(imageLocation))
                File.Delete(imageLocation);

            image.Save(imageLocation, ImageFormat.Png);
        }
    }
}
