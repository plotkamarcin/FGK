using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FGK
{
    class Raytracer
    {

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
                    double searchArea=0.001;
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
                if (world.AnyObstacleBetween(info.HitPoint, light.Position)) { continue; }
                finalColor += material.Radiance(light, info);
            }
            return finalColor;
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

        //private Color AdaptiveAliasing(Camera c, World w, Vector2 A, Vector2 B, Vector2 C, Vector2 D, Color color, int samplesDepth)
        //{

            //Vector2 middlePoint = new Vector2((A.X + B.X) / 2, (C.Y + D.Y) /2);
            //Ray ray0 = c.GetRayTo(middlePoint);
            //Ray rayA = c.GetRayTo(A);
            //Ray rayB = c.GetRayTo(B);
            //Ray rayC = c.GetRayTo(C);
            //Ray rayD = c.GetRayTo(D);

            //HitInfo info0 = w.TraceRay(ray0);
            //HitInfo infoA = w.TraceRay(rayA);
            //HitInfo infoB = w.TraceRay(rayB);
            //HitInfo infoC = w.TraceRay(rayC);
            //HitInfo infoD = w.TraceRay(rayD);

            //Color color0;
            //Color colorA;
            //Color colorB;
            //Color colorC;
            //Color colorD;


            //if (info0.HitObject) { color0 = info0.Color; }
            //else { color0 = w.BackgroundColor; }
            //if (infoA.HitObject) { colorA = infoA.Color; }
            //else { colorA = w.BackgroundColor; }
            //if (infoB.HitObject) { colorB = infoB.Color; }
            //else { colorB = w.BackgroundColor; }
            //if (infoC.HitObject) { colorC = infoC.Color; }
            //else { colorC = w.BackgroundColor; }
            //if (infoD.HitObject) { colorD = infoD.Color; }
            //else { colorD = w.BackgroundColor; }
 

            //if (samplesDepth == 0 || color0.Equals(colorA) && color0.Equals(colorB) && color0.Equals(colorC) && color0.Equals(colorD))
            //{
            //    Color tmp1 = Color.FromArgb((colorA.R + color0.R) / 2, (colorA.G + color0.G) / 2, (colorA.B + color0.B) / 2);
            //    Color tmp2 = Color.FromArgb((colorB.R + color0.R) / 2, (colorB.G + color0.G) / 2, (colorB.B + color0.B) / 2);
            //    Color tmp3 = Color.FromArgb((colorC.R + color0.R) / 2, (colorC.G + color0.G) / 2, (colorC.B + color0.B) / 2);
            //    Color tmp4 = Color.FromArgb((colorD.R + color0.R) / 2, (colorD.G + color0.G) / 2, (colorD.B + color0.B) / 2);
            //    Color sum = Color.FromArgb((tmp1.R + tmp2.R + tmp3.R + tmp4.R) / 4, (tmp1.G + tmp2.G + tmp3.G + tmp4.G) / 4, (tmp1.B + tmp2.B + tmp3.B + tmp4.B) / 4);
            //    return sum;
            //}
            //else
            //{
            //    if (!(colorA.Equals(color.B) && colorA.Equals(colorD)))
            //    {
            //        Color tmp1 = Color.FromArgb((colorA.R + color0.R) / 2, (colorA.G + color0.G) / 2, (colorA.B + color0.B) / 2);
            //        Color tmp2 = Color.FromArgb((colorB.R + color0.R) / 2, (colorB.G + color0.G) / 2, (colorB.B + color0.B) / 2);
            //        Color tmp4 = Color.FromArgb((colorD.R + color0.R) / 2, (colorD.G + color0.G) / 2, (colorD.B + color0.B) / 2);
            //        Color sum = Color.FromArgb((tmp1.R + tmp2.R + color.R + tmp4.R) / 4, (tmp1.G + tmp2.G + color.G + tmp4.G) / 4, (tmp1.B + tmp2.B + color.B + tmp4.B) / 4);
            //        return AdaptiveAliasing(c, w, A, new Vector2((A.X + B.X) / 2, A.Y), middlePoint, new Vector2(A.X, (A.Y + D.Y) / 2), sum, samplesDepth - 1);
            //    }
            //    if (!(colorB.Equals(color.A) && colorB.Equals(colorC)))
            //    {
            //        Color tmp1 = Color.FromArgb((colorA.R + color0.R) / 2, (colorA.G + color0.G) / 2, (colorA.B + color0.B) / 2);
            //        Color tmp2 = Color.FromArgb((colorB.R + color0.R) / 2, (colorB.G + color0.G) / 2, (colorB.B + color0.B) / 2);
            //        Color tmp3 = Color.FromArgb((colorC.R + color0.R) / 2, (colorC.G + color0.G) / 2, (colorC.B + color0.B) / 2);
            //        Color sum = Color.FromArgb((tmp1.R + tmp2.R + tmp3.R + color.R) / 4, (tmp1.G + tmp2.G + tmp3.G + color.G) / 4, (tmp1.B + tmp2.B + tmp3.B + color.B) / 4);
            //        return  AdaptiveAliasing(c, w, new Vector2((A.X + B.X) / 2, A.Y), B, new Vector2(C.X, (C.Y + B.Y) / 2), middlePoint, sum, samplesDepth - 1);
            //    }
            //    if (!(colorC.Equals(colorB) && !colorB.Equals(colorD)))
            //    {
            //        Color tmp2 = Color.FromArgb((colorB.R + color0.R) / 2, (colorB.G + color0.G) / 2, (colorB.B + color0.B) / 2);
            //        Color tmp3 = Color.FromArgb((colorC.R + color0.R) / 2, (colorC.G + color0.G) / 2, (colorC.B + color0.B) / 2);
            //        Color tmp4 = Color.FromArgb((colorD.R + color0.R) / 2, (colorD.G + color0.G) / 2, (colorD.B + color0.B) / 2);
            //        Color sum = Color.FromArgb((color.R + tmp2.R + tmp3.R + tmp4.R) / 4, (color.G + tmp2.G + tmp3.G + tmp4.G) / 4, (color.B + tmp2.B + tmp3.B + tmp4.B) / 4);
            //       return  AdaptiveAliasing(c, w, middlePoint, new Vector2(B.X, (B.Y + C.Y) / 2), C, new Vector2((C.X + D.X) / 2, C.Y), sum, samplesDepth - 1);
                 
            //    }
            //    if (!(colorD.Equals(colorA) && !colorD.Equals(colorC)))
            //    {
            //        Color tmp1 = Color.FromArgb((colorA.R + color0.R) / 2, (colorA.G + color0.G) / 2, (colorA.B + color0.B) / 2);
            //        Color tmp3 = Color.FromArgb((colorC.R + color0.R) / 2, (colorC.G + color0.G) / 2, (colorC.B + color0.B) / 2);
            //        Color tmp4 = Color.FromArgb((colorD.R + color0.R) / 2, (colorD.G + color0.G) / 2, (colorD.B + color0.B) / 2);
            //        Color sum = Color.FromArgb((tmp1.R + color.R + tmp3.R + tmp4.R) / 4, (tmp1.G + color.G + tmp3.G + tmp4.G) / 4, (tmp1.B + color.B + tmp3.B + tmp4.B) / 4);
            //        return AdaptiveAliasing(c, w, new Vector2(D.X / 2, (A.Y + D.Y) / 2), middlePoint, new Vector2((C.X + D.X) / 2, D.Y), D, sum, samplesDepth - 1);
            //    }
            //    else
            //    {
            //        return Color.Black;
            //    }

            //}
        //}
    }
}
