using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    public class Reflective : Material
    {
        PhongMaterial direct; // do bezpośredniego oświetlenia
        double reflectivity;
        ColorRgb reflectionColor;
        public Reflective(ColorRgb materialColor,
        double diffuse,
        double specular,
        double exponent,
        double reflectivity)
        {
            this.direct = new PhongMaterial(materialColor, diffuse, specular, exponent);
            this.reflectivity = reflectivity;
            this.reflectionColor = materialColor;
        }

        public override ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            Vector3 toCameraDirection = -hit.Ray.Direction;
            ColorRgb radiance = direct.Shade(tracer, hit);
            Vector3 reflectionDirection = Vector3.Reflect(toCameraDirection, hit.Normal);
            Ray reflectedRay = new Ray(hit.HitPoint, reflectionDirection);
            radiance += tracer.ShadeRay(hit.World, reflectedRay, hit.Depth) *
            reflectionColor *
            reflectivity;
            return radiance;
        }

        public override ColorRgb Radiance(Light light, HitInfo hit)
        {
            throw new NotImplementedException();
        }
    }
}
