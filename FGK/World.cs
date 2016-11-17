using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FGK
{
   public class World
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

        public bool AnyObstacleBetween(Vector3 pointA, Vector3 pointB)
        {
            // odległość od cieniowanego punktu do światła
            Vector3 vectorAB = pointB - pointA;
            double distAB = vectorAB.Length;
            double currDistance = Ray.Huge;
            // promień (półprosta) z cieniowanego punktu w kierunku światła
            Ray ray = new Ray(pointA, vectorAB);
            Vector3 ignored = default(Vector3);
            foreach (var obj in objects)
            {
                // jeśli jakiś obiekt jest na drodze promienia oraz trafienie
                // nastąpiło bliżej niż odległość punktu do światła,
                // obiekt jest w cieniu
                if (obj.HitTest(ray, ref currDistance, ref ignored) && currDistance < distAB)
                { return true; }
            }
            // obiekt nie jest w cieniu
            return false;
        }

        public ColorRgb BackgroundColor { get; private set; }
        public List<GeometricObject> Objects { get { return objects; } }
        public List<PointLight> Lights { get { return lights; } }
    }
}
