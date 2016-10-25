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
        double p1;
        double p2;
        double p3;
        Color color;

        public Triangle(Vector3 p, Color c)
        {
            this.p1 = p.x;
            this.p2 = p.y;
            this.p3 = p.z;
            this.color = c;
        }

        public override bool HitTest(Ray ray, ref double distance)
        {
            throw new NotImplementedException();
        }
    }
}
