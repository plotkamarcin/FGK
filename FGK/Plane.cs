using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FGK
{
    class Plane:GeometricObject
    {
        /// <summary>Punkt przez który płaszczyzna przechodzi</summary>
        Vector3 point;
        /// <summary>Normalna do płaszczyzny</summary>
        Vector3 normal;
        public Plane(Vector3 point, Vector3 normal, Color color)
        {
            this.point = point;
            this.normal = normal;
            base.Color = color;
        }
        public override bool HitTest(Ray ray, ref double distance)
        {
            double t = (point - ray.Origin).Dot(normal) / ray.Direction.Dot(normal);
            if (t > Ray.Epsilon)
            {
                distance = t;
                return true;
            }
            return false;
        }
    }
}
