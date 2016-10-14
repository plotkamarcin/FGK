using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FGK
{
    class Sphere:GeometricObject
    {
        Vector3 center;
        double radius;
        Color color;
        public Sphere(Vector3 center, double radius, Color color)
        {
            this.center = center;
            this.radius = radius;
            base.Color = color;
        }
        public override bool HitTest(Ray ray, ref double minDistance)
        {
            double t;
            Vector3 distance = ray.Origin - center;
            double a = ray.Direction.LengthSq;
            double b = (distance * 2).Dot(ray.Direction);
            double c = distance.LengthSq - radius * radius;
            double disc = b * b - 4 * a * c;
            if (disc < 0) { return false; }
            double discSq = Math.Sqrt(disc);
            double denom = 2 * a;
            t = (-b - discSq) / denom;
            if (t < 0)
            { t = (-b + discSq) / denom; }
            if (t < 0)
            { return false; }
            minDistance = t;
            return true;
        }

    }
}
