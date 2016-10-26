using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    class PhongMaterial : Material
    {
        ColorRgb materialColor;
        double diffuseCoeff;
        double specular;
        double specularExponent;
        public PhongMaterial(ColorRgb materialColor,
        double diffuse,
        double specular,
        double specularExponent)
        {
            this.materialColor = materialColor;
            this.diffuseCoeff = diffuse;
            this.specular = specular;
            this.specularExponent = specularExponent;
        }
        public override ColorRgb Radiance(PointLight light, HitInfo hit)
        {
            Vector3 inDirection = (light.Position - hit.HitPoint).Normalized;
            double diffuseFactor = inDirection.Dot(hit.Normal);
            if (diffuseFactor < 0) { return ColorRgb.Black; }
            ColorRgb result = light.Color * materialColor * diffuseFactor * diffuseCoeff;
            double phongFactor = PhongFactor(inDirection, hit.Normal, -hit.Ray.Direction);
            if (phongFactor != 0)
            { result += materialColor * specular * phongFactor; }
            return result;
        }
        double PhongFactor(Vector3 inDirection, Vector3 normal, Vector3 toCameraDirection)
        {
            Vector3 reflected = Vector3.Reflect(inDirection, normal);
            double cosAngle = reflected.Dot(toCameraDirection);
            if (cosAngle <= 0) { return 0; }
            return Math.Pow(cosAngle, specularExponent);
        }
    }
}
