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
        public Vector3 P1 { get; private set; }
        public Vector3 P2 { get; private set; }
        public Vector3 P3 { get; private set; }
        public Vector3 Normal;
        public Vector3 N1 { get; private set; }
        public Vector3 N2 { get; private set; }
        public Vector3 N3 { get; private set; }
        public Vector2 Vt1 { get; private set; }
        public Vector2 Vt2 { get; private set; }
        public Vector2 Vt3 { get; private set; }
        

        public Triangle(Vector3 p1, Vector3 p2, Vector3 p3, Material mat)
        {
            this.P1 = p1;
            this.P2 = p2;
            this.P3 = p3;
            Vector3 normal = Vector3.Cross(p2 - p1, p3 - p1);
            base.Material = mat;
        }

        public void SetVertexNormals(Vector3 n1, Vector3 n2, Vector3 n3)
        {
            this.N1 = n1;
            this.N2 = n2;
            this.N3 = n3;
        }

        public void SetTextureCoords(Vector2 vt1, Vector2 vt2, Vector2 vt3)
        {
            this.Vt1 = vt1;
            this.Vt2 = vt2;
            this.Vt3 = vt3;
        }

        public bool OtherHitTest(Ray ray, ref double distance, ref Vector3 normal)
        {
            Vector3 A = P2 - P1;
            Vector3 B = P3 - P1;
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
            Vector3 edge0 = P2 - P1;
            Vector3 vp0 = Phit - P1;
            perpendicular = Vector3.Cross(edge0, vp0);
            if (N.Dot(perpendicular) < 0)
            {
                return false; // P is on the right side 
            }
            Vector3 edge1 = P3 - P2;
            Vector3 vp1 = Phit - P2;
            perpendicular = Vector3.Cross(edge1, vp1);
            if (N.Dot(perpendicular) < 0)
            {
                return false; // P is on the right side 
            }
            Vector3 edge2 = P1 - P3;
            Vector3 vp2 = Phit - P3;
            perpendicular = Vector3.Cross(edge2, vp2);
            if (N.Dot(perpendicular) < 0)
            {
                return false; // P is on the right side 
            }
            normal = N;
            return true;
        }
        public void TranslateTriangle(double x, double y, double z)
        {
            this.P1.x += x;
            this.P1.y += y;
            this.P1.z += z;

            this.P2.x += x;
            this.P2.y += y;
            this.P2.z += z;

            this.P3.x += x;
            this.P3.y += y;
            this.P3.z += z;


        }

        public void ScaleTriangle(double factor)
        {
            this.P1.x *= factor;
            this.P1.y *= factor;
            this.P1.z *= factor;

            this.P2.x *= factor;
            this.P2.y *= factor;
            this.P2.z *= factor;

            this.P3.x *= factor;
            this.P3.y *= factor;
            this.P3.z *= factor;
        }

        public override bool HitTest(Ray ray, ref double distance, ref Vector3 outNormal)
        {

            //Moller - trumbone method
            double kEpsilon = 0.000001;
            Vector3 v0v1 = P2 - P1;
            Vector3 v0v2 = P3 - P1;
            Vector3 v0v3 = P2 - P3;
            Normal = Vector3.Cross(v0v2, v0v1).Normalized;


            Vector3 pvec = Vector3.Cross(ray.Direction, v0v2);
            double det = v0v1.Dot(pvec);

            double d = Normal.Dot(P1);
            double t = -(Normal.Dot(ray.Origin) + d) / Normal.Dot(ray.Direction);

            // if the determinant is negative the triangle is backfacing
            // if the determinant is close to 0, the ray misses the triangle
            if (det <= kEpsilon || det <= 0) return false;

            // ray and triangle are parallel if det is close to 0
            if (Math.Abs((det)) <= kEpsilon) return false;

            double invDet = 1 / det;
            Vector3 tvec = ray.Origin - P1;
            double u = tvec.Dot(pvec) * invDet;
            if (u <= 0 || u >= 1) return false;

            Vector3 qvec = Vector3.Cross(tvec, v0v1);
            double v = ray.Direction.Dot(qvec) * invDet;
            if (v <= 0 || u + v >= 1) return false;
            double tdist = v0v2.Dot(qvec) * invDet;
            distance = t;

            Vector3 N = (N1 * v + N2 * u + N3 * (1 - u - v)).Normalized;
            this.TextureCoords = (Vt1 * v + Vt2 * u + Vt3 * (1 - u - v));
            //this.TextureCoords = new Vector2(u, v);
            //v = α*va + β*vb + (1 - α - β)*vc
            //n = α * na + β * nb + (1 - α - β) * nc
            Normal = N;
            return true;
        }

    }
}
