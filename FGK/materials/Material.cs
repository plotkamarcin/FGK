using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    public abstract class Material
    {

            public abstract ColorRgb Shade(Raytracer tracer, HitInfo hit);
            public abstract ColorRgb Radiance(PointLight light, HitInfo hit);

    }
}
