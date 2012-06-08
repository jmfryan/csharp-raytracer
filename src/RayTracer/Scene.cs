using System.Collections;
using System.Collections.Generic;

namespace RayTracer
{
    public class Scene
    {
        public IList<Sphere> Objects;
        public IList<PointLight> Lights;

        public Scene()
        {
            Objects = new List<Sphere>();
            Lights = new List<PointLight>();
        }

        public void AddObject(Sphere s)
        {
            Objects.Add(s);
        }

        public void AddLightSource(PointLight pointLight)
        {
            Lights.Add(pointLight);
        }
    }
}