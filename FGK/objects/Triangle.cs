using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FGK
{
    class Triangle : GeometricObject
    {
        Vector3 p1;
        Vector3 p2;
        Vector3 p3;
        Color color;

        public Triangle(Vector3 p1, Vector3 p2, Vector3 p3, Color c)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.Color = c;
        }

        public override bool HitTest(Ray ray, ref double distance)
        {
            Vector3 A = p2 - p1;
            Vector3 B = p3 - p1;
            Vector3 C = Vector3.Cross(A, B);
            Vector3 N=C.Normalized;
            double area2 = N.Length;

            double rayDirection = N.Dot(ray.Direction);
            if (Math.Abs(rayDirection)<0.000001){
                return false;// they are parallel so they don't intersect !
            }

            double D = N.Dot(A);
            double t = -(N.Dot(ray.Origin) + D) / N.Dot(ray.Direction);

            if (t < 0)
            {
                return false; // the triangle is behind 
            }
            Vector3 Phit = ray.Origin + (ray.Direction*t);

            Vector3 perpendicular;
            Vector3 edge0 = p2 - p1;
            Vector3 vp0 = Phit - p1;
            perpendicular = Vector3.Cross(edge0, vp0);
            if (N.Dot(perpendicular) < 0)
            {
                return false; // P is on the right side 
            }
            Vector3 edge1 = p3 - p2;
            Vector3 vp1 = Phit - p2;
            perpendicular = Vector3.Cross(edge1, vp1);
            if (N.Dot(perpendicular) < 0)
            {
                return false; // P is on the right side 
            }
            Vector3 edge2 = p1 - p3;
            Vector3 vp2 = Phit - p3;
            perpendicular = Vector3.Cross(edge2, vp2);
            if (N.Dot(perpendicular) < 0)
            {
                return false; // P is on the right side 
            }
            return true;
        }
        public void translateTriangle(double x, double y, double z)
        {
            this.p1.x += x;
            this.p1.y += y;
            this.p1.z += z;

            this.p2.x += x;
            this.p2.y += y;
            this.p2.z += z;

            this.p3.x += x;
            this.p3.y += y;
            this.p3.z += z;


        }
    }
}
