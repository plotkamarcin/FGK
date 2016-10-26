using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    abstract class Material
    {
        public abstract ColorRgb Radiance(PointLight light, HitInfo hit);
    }
}
