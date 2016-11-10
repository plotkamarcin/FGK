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
        public Vector3 p1 { get; private set; }
        public Vector3 p2 { get; private set; }
        public Vector3 p3 { get; private set; }
        public Vector3 normal { get; private set; }
        public Vector3 n1 { get; private set; }
        public Vector3 n2 { get; private set; }
        public Vector3 n3 { get; private set; }

        public Triangle(Vector3 p1, Vector3 p2, Vector3 p3, Material mat)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            base.Material = mat;
        }

        public void setVertexNormals(Vector3 n1, Vector3 n2, Vector3 n3)
        {
            this.n1 = n1;
            this.n2 = n2;
            this.n3 = n3;
        }

        public bool otherHitTest(Ray ray, ref double distance, ref Vector3 normal)
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
            normal = N;
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

        public override bool HitTest(Ray ray, ref double distance, ref Vector3 outNormal)
        {
            //Moller-trumbone method
            double kEpsilon = 0.000001;
            Vector3 v0v1 = p2 - p1;
            Vector3 v0v2 = p3 - p1;
            Vector3 v0v3 = p2 - p3;
            normal = Vector3.Cross(v0v2, v0v1).Normalized;


            Vector3 pvec = Vector3.Cross(ray.Direction, v0v2);
            double det = v0v1.Dot(pvec);

            double d = normal.Dot(p1);
            double t = -(normal.Dot(ray.Origin) + d) / normal.Dot(ray.Direction);
            Vector3 hitPoint = ray.Origin + ray.Direction * t;
            Vector3 hitPointVector = hitPoint - p1;
            Vector3 hitPointNormal = Vector3.Cross( hitPointVector, ray.Direction);
            

             // if the determinant is negative the triangle is backfacing
            // if the determinant is close to 0, the ray misses the triangle
            if (det <= kEpsilon || det <=0) return false;

            // ray and triangle are parallel if det is close to 0
            if (Math.Abs((det)) <= kEpsilon) return false;

            double invDet = 1 / det;
           Vector3 tvec = ray.Origin - p1;
            double u = tvec.Dot(pvec) * invDet;
            if (u <= 0 || u >= 1) return false;

            Vector3 qvec = Vector3.Cross(tvec,v0v1);
            double v = ray.Direction.Dot(qvec) * invDet;
            if (v <= 0 || u + v >=1) return false;
            double tdist = v0v2.Dot(qvec) * invDet;
            distance = t;
                Vector3 N = (n1 * v + n2 * u + n3 * (1 - u - v)).Normalized;
            //v = α*va + β*vb + (1 - α - β)*vc
            //n = α * na + β * nb + (1 - α - β) * nc
            outNormal = N;
            return true;
        }
    }
}
