﻿using System;
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
        public override ColorRgb Radiance(Light light, HitInfo hit)
        {
            ColorRgb texelColor = texture.GetPixel((int)(hit.HitObject.TextureCoords.X * texture.Width), (int)(hit.HitObject.TextureCoords.Y * texture.Height));
            Vector3 lightPos = light.Sample();
            Vector3 inDirection = (lightPos - hit.HitPoint).Normalized*(-1.0);
            double diffuseFactor = inDirection.Dot(hit.Normal);
            if (diffuseFactor < 0) { return ColorRgb.Black; }           
            ColorRgb result = (light.Color * ambient + texelColor) * diffuseFactor * diffuseCoeff;
            double phongFactor = PhongFactor(inDirection, hit.Normal, -hit.Ray.Direction);
            if (phongFactor != 0)
            { result +=  texelColor*specular * phongFactor; }
            return result;
        }
        public override ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            ColorRgb totalColor = texture.GetPixel((int)(hit.HitObject.TextureCoords.X * texture.Width), (int)(hit.HitObject.TextureCoords.Y * texture.Height));
            foreach (var light in hit.World.Lights)
            {
                Vector3 lightPos = light.Sample();
                Vector3 inDirection = (lightPos - hit.HitPoint).Normalized;
                double diffuseFactor = inDirection.Dot(hit.Normal);
                if (diffuseFactor < 0) { continue; }
                if (hit.World.AnyObstacleBetween(hit.HitPoint, lightPos))
                { continue; }
                ColorRgb result = light.Color * materialColor * diffuseFactor * diffuseCoeff;
                double phongFactor = PhongFactor(inDirection, hit.Normal, -hit.Ray.Direction);
                if (phongFactor != 0)
                { result += materialColor * specular * phongFactor; }
                totalColor += result;
            }
            return totalColor;
        }
    }
}
