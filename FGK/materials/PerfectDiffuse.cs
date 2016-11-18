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

        public override ColorRgb Radiance(PointLight light, HitInfo hit)
        {
            Vector3 inDirection = (light.Position - hit.HitPoint).Normalized;
            double diffuseFactor = inDirection.Dot(hit.Normal);
            if (diffuseFactor < 0) { return ColorRgb.Black; }
            return light.Color * materialColor * diffuseFactor;
        }
        public override ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            ColorRgb totalColor = ColorRgb.Black;
            foreach (var light in hit.World.Lights)
            {
                Vector3 inDirection = (light.Position - hit.HitPoint).Normalized;
                double diffuseFactor = inDirection.Dot(hit.Normal);
                if (diffuseFactor < 0) { continue; }
                if (hit.World.AnyObstacleBetween(hit.HitPoint, light.Position))
                { continue; }
                totalColor += light.Color * materialColor * diffuseFactor;
            }
            return totalColor;
        }
    }
}
