using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    class Perspective : Camera
    {
        OrthonormalBasis onb;
        Vector3 origin;
        Vector3 lookAt;
        Vector3 up;
        double distance;
        public Perspective(Vector3 origin, Vector3 lookAt,Vector3 up, double distance)
        {
            this.onb = new OrthonormalBasis(origin, lookAt, up);
            this.origin = origin;
            this.distance = distance;
        }
        public override Ray GetRayTo(Vector2 relativeLocation)
        {
            return new Ray(origin, RayDirection(relativeLocation));
        }
        Vector3 RayDirection(Vector2 v)
        {
            return onb * new Vector3(v.X, v.Y, -distance);
        }
    }
}
