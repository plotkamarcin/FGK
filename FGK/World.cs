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

        public World(Color background)
        {
            this.BackgroundColor = background;
            this.objects = new List<GeometricObject>();
        }

        public void Add(GeometricObject obj)
        {
            objects.Add(obj);
        }

        public HitInfo TraceRay(Ray ray)
        {
            HitInfo result = new HitInfo();
            double minimalDistance = Ray.Huge; // najbliższe trafienie
            double hitDistance = 0; // zmienna pomocnicza, ostatnia odległość

            foreach (var obj in objects)
            {
                if (obj.HitTest(ray, ref hitDistance) &&
                    hitDistance < minimalDistance) // jeśli najbliższe trafienie
                {
                    minimalDistance = hitDistance; // nowa najmniejsza odległość
                    result.HitObject = true; // trafiono obiekt
                    result.Color = obj.Color; // zapisz kolor trafionego obiektu
                }
            }

            return result;
        }

        public Color BackgroundColor { get; private set; }
    }
}
