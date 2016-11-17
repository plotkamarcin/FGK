using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    public class Ray
    {
        public const double Epsilon = 0.00001;
        public const double Huge = double.MaxValue;
        Vector3 origin;
        Vector3 direction;
        public Ray(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction.Normalized;
        }
        public Vector3 Origin { get { return origin; } }
        public Vector3 Direction { get { return direction; } }
        public Vector3 ShowPoint(double distance)
        {
            return Origin + Direction * distance;
        }
        public void CheckHit(GeometricObject g)
        {
            double distance = 0;
            Vector3 nil = new Vector3(0, 0, 0);
            if (g.HitTest(this, ref distance,ref nil))
            {
                Console.WriteLine(" przeciecie w " + this.ShowPoint(distance));
            }
            else
            {
                Console.WriteLine(" brak przeciecia");
            }
        }
    }
}
