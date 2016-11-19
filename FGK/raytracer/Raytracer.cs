﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FGK
{
    public class Raytracer
    {
        int maxDepth;
        public Raytracer(int maxDepth)
        {
            this.maxDepth = maxDepth;
        }
        public Bitmap Raytrace(World world, Camera camera, Size imageSize)
        {

            Bitmap bmp = new Bitmap(imageSize.Width, imageSize.Height);
            RenderedImagePreview r = new RenderedImagePreview(bmp)
            {
                Visible = true
            };
            for (int y = 0; y < imageSize.Height; y++)
            {
                for (int x = 0; x < imageSize.Width; x++)
                {
                    // przeskalowanie x i y do zakresu [-1; 1]
                    Vector2 pictureCoordinates = new Vector2(
                        (x / (double)imageSize.Width) * 2 - 1,
                        (y / (double)imageSize.Height) * 2 - 1);

                    // wysłanie promienia i sprawdzenie w co właściwie trafił
                    Ray ray = camera.GetRayTo(pictureCoordinates);
                    HitInfo info = world.TraceRay(ray);

                    // ustawienie odpowiedniego koloru w obrazie wynikowym
                    Color color;
                    //if (info.HitObject) { color = info.Color; }
                    // else { color = world.BackgroundColor; }
                    double searchArea = 0.001;
                    bmp.SetPixel(x, y, StripColor(ShadeRay(world, ray)));
                    Vector2 A = new Vector2(pictureCoordinates.X + searchArea, pictureCoordinates.Y + searchArea);
                    Vector2 B = new Vector2(pictureCoordinates.X - searchArea, pictureCoordinates.Y + searchArea);
                    Vector2 C = new Vector2(pictureCoordinates.X - searchArea, pictureCoordinates.Y - searchArea);
                    Vector2 D = new Vector2(pictureCoordinates.X + searchArea, pictureCoordinates.Y - searchArea);

                    //Color aliasedColor = AdaptiveAliasing(camera, world, A, B, C, D,color, 0);
                    //bmp.SetPixel(x, y, color);

                }
                //bmp.Save("temp" + x + ".png");
                r.pictureBox1.Image = bmp;
                r.pictureBox1.Refresh();
                //Console.WriteLine("{0:F2}", "Rendering... " + ((double)y / 1024.0) * 100 + "%");
            }
            Console.WriteLine("Rendering zakonczony.");
            return bmp;
        }

        public ColorRgb ShadeRay(World world, Ray ray)
        {
            HitInfo info = world.TraceRay(ray);
            if (info.HitObject == null) { return world.BackgroundColor; }
            ColorRgb finalColor = ColorRgb.Black;
            Material material = info.HitObject.Material;
            foreach (var light in world.Lights)
            {
                //if (world.AnyObstacleBetween(info.HitPoint, light.Position)) { continue; }
                finalColor += material.Radiance(light, info);
            }
            return finalColor;
        }

        public ColorRgb ShadeRay(World world, Ray ray, int currentDepth)
        {
            if (currentDepth > maxDepth) { return ColorRgb.Black; }
            HitInfo info = world.TraceRay(ray);
            info.Depth = currentDepth + 1;
            if (info.HitObject == null) { return world.BackgroundColor; }
            Material material = info.HitObject.Material;
            return material.Shade(this, info);
        }

        Color StripColor(ColorRgb colorInfo)
        {
            colorInfo.R = colorInfo.R < 0 ? 0 : colorInfo.R > 1 ? 1 : colorInfo.R;
            colorInfo.G = colorInfo.G < 0 ? 0 : colorInfo.G > 1 ? 1 : colorInfo.G;
            colorInfo.B = colorInfo.B < 0 ? 0 : colorInfo.B > 1 ? 1 : colorInfo.B;
            return Color.FromArgb((int)(colorInfo.R * 255),
            (int)(colorInfo.G * 255),
            (int)(colorInfo.B * 255));
        }

    }
}
