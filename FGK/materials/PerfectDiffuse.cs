using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    class PerfectDiffuse:Material
    {
        ColorRgb materialColor;
        public PerfectDiffuse(ColorRgb materialColor)
        {
            this.materialColor = materialColor;
        }

        public override ColorRgb Radiance(Light light, HitInfo hit)
        {
            Vector3 lightPos = light.Sample();
            Vector3 inDirection = (lightPos - hit.HitPoint).Normalized;
            double diffuseFactor = inDirection.Dot(hit.Normal);
            if (diffuseFactor < 0) { return ColorRgb.Black; }
            return light.Color * materialColor * diffuseFactor;
        }
        public override ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            ColorRgb totalColor = this.materialColor;
            foreach (var light in hit.World.Lights)
            {
                Vector3 lightPos = light.Sample();
                Vector3 inDirection = (lightPos - hit.HitPoint).Normalized;
                double diffuseFactor = inDirection.Dot(hit.Normal);
                if (diffuseFactor < 0) { continue; }
                if (hit.World.AnyObstacleBetween(hit.HitPoint, lightPos))
                { continue; }
            }
            return totalColor;
        }
    }
}
