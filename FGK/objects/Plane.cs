using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace FGK
{
    class Plane:GeometricObject
    {
        /// <summary>Punkt przez który płaszczyzna przechodzi</summary>
        Vector3 point;
        /// <summary>Normalna do płaszczyzny</summary>
        public Vector3 normal;
        public Plane(Vector3 point, Vector3 normal, Material mat)
        {
            this.point = point;
            this.normal = normal;
            base.Material = mat;
        }
        public override bool HitTest(Ray ray, ref double distance, ref Vector3 outNormal)
        {
            double size = 10.0;
                double t = (point - ray.Origin).Dot(normal) / ray.Direction.Dot(normal);
                if (t > Ray.Epsilon)
                {
                    distance = t;
                    Vector3 hitPoint = (ray.Origin + ray.Direction * t);
                    outNormal = normal;

                if (Math.Abs(hitPoint.X - point.X) <= 10 && Math.Abs(hitPoint.Z - point.Z) <= 10)
                {
                    double v = ((hitPoint.X-point.X) - (-size)) / (size - (-size));
                    double u = ((hitPoint.Z-point.Z) - (-size)) / (size - (-size));
                    TextureCoords = new Vector2(v,1-u);
                }
                else
                {
                    TextureCoords = new Vector2(0, 0);
                }
                    return true;
                }
                return false;
        }

    }
}
