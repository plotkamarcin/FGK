using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FGK
{
    class World
    {
        List<GeometricObject> objects;
        List<PointLight> lights;

        public World(Color background)
        {
            this.BackgroundColor = background;
            this.objects = new List<GeometricObject>();
            this.lights = new List<PointLight>();
        }

        public void Add(GeometricObject obj)
        {
            objects.Add(obj);
        }
        public void AddLight(PointLight light)
        {
            lights.Add(light);
        }
        public HitInfo TraceRay(Ray ray)
        {
            HitInfo result = new HitInfo();
            Vector3 normal = new Vector3(0,0,0);
            double minimalDistance = Ray.Huge; // najbliższe trafienie
            double hitDistance = 0; // zmienna pomocnicza, ostatnia odległość
            double lastDistance = 0; // zmienna pomocnicza, ostatnia odległość

            foreach (var obj in objects)
            {
                if (obj.HitTest(ray, ref lastDistance, ref normal) && lastDistance < minimalDistance) // jeśli najbliższe trafienie
                {
                    minimalDistance = lastDistance; // nowa najmniejsza odległość
                    result.HitObject = obj; // nowy trafiony obiekt
                    result.Normal = normal; // normalna trafienia
                }
            }

            if (result.HitObject != null) // jeśli trafiliśmy cokolwiek
            {
                result.HitPoint = ray.Origin + ray.Direction * minimalDistance;
                result.Ray = ray;
                result.World = this;
            }
            return result;
        }

        public ColorRgb BackgroundColor { get; private set; }
        public List<GeometricObject> Objects { get { return objects; } }
        public List<PointLight> Lights { get { return lights; } }
    }
}
