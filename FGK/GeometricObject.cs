using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    abstract class GeometricObject
    {
        public Color Color { get; set; }
        public abstract bool HitTest(Ray ray, ref double distance);
    }
}
