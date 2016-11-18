using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FGK
{
    public class PhongTexturedMaterial : PhongMaterial
    {
        Bitmap texture;
        double ambient;
        public PhongTexturedMaterial(ColorRgb materialColor,double ambient, double diffuse, double specular,double specularExponent,ref Bitmap texture): base(materialColor, diffuse, specular, specularExponent)
        {
            this.texture = texture;
            this.ambient = ambient;
        }
        public override ColorRgb Radiance(PointLight light, HitInfo hit)
        {
            ColorRgb texelColor = texture.GetPixel((int)(hit.HitObject.TextureCoords.X * texture.Width), (int)(hit.HitObject.TextureCoords.Y * texture.Height));
            Vector3 inDirection =(hit.HitPoint-light.Position).Normalized*(-1.0);
            double diffuseFactor = inDirection.Dot(hit.Normal);
            if (diffuseFactor < 0) {
                return new ColorRgb(1, 0, 1);
            }
            ColorRgb result = (light.Color * ambient + texelColor) * diffuseFactor * diffuseCoeff;
            double phongFactor = PhongFactor(inDirection, hit.Normal, -hit.Ray.Direction);
            if (phongFactor != 0)
            { result +=  texelColor*specular * phongFactor; }
            return result;
        }
    }
}
