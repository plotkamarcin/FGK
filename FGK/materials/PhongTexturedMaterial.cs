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
        public PhongTexturedMaterial(ColorRgb materialColor, double diffuse, double specular,double specularExponent,ref Bitmap texture): base(materialColor, diffuse, specular, specularExponent)
        {
            this.texture = texture;
        }
        public override ColorRgb Radiance(PointLight light, HitInfo hit)
        {
           ColorRgb texelColor = texture.GetPixel((int)(hit.HitObject.TextureCoords.X * texture.Width), (int)(hit.HitObject.TextureCoords.Y * texture.Height));
            Vector3 inDirection = (light.Position - hit.HitPoint).Normalized;
            double diffuseFactor = inDirection.Dot(hit.Normal);
            if (diffuseFactor < 0) { return ColorRgb.Black; }
            ColorRgb result = light.Color * texelColor * diffuseFactor * diffuseCoeff;
            double phongFactor = PhongFactor(inDirection, hit.Normal, -hit.Ray.Direction);
            if (phongFactor != 0)
            { result += texelColor * specular * phongFactor; }
            return result;
        }
    }
}
