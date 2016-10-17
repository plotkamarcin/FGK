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
            RenderedImagePreview r = new RenderedImagePreview(bmp);
            r.Visible = true;
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
                    if (info.HitObject) { color = info.Color; }
                    else { color = world.BackgroundColor; }
                    //bmp.SetPixel(x, y, color);
                    bmp.SetPixel(x, y, AdaptiveAliasing(camera,world,pictureCoordinates,5));
                    
                }
                //bmp.Save("temp" + x + ".png");
                r.pictureBox1.Image = bmp;
                r.pictureBox1.Refresh();
            }
            Console.WriteLine("Rendering zakonczony.");
            return bmp;
        }

        private Color AdaptiveAliasing(Camera c, World w, Vector2 pictureCoord,int samplesDepth)
        {
            Vector2 localCoord = pictureCoord;
            Ray ray0 = c.GetRayTo(pictureCoord);

            localCoord.X += 0.001;localCoord.Y += 0.001;
            Ray rayA = c.GetRayTo(localCoord);

            localCoord.X -= 0.001; localCoord.Y += 0.001;
            Ray rayB = c.GetRayTo(localCoord);

            localCoord.X += 0.001; localCoord.Y -= 0.001;
            Ray rayC = c.GetRayTo(localCoord);

            localCoord.X -= 0.001; localCoord.Y -= 0.001;
            Ray rayD = c.GetRayTo(localCoord);
            HitInfo info0 = w.TraceRay(ray0);
            HitInfo infoA = w.TraceRay(rayA);
            HitInfo infoB = w.TraceRay(rayB);
            HitInfo infoC = w.TraceRay(rayC);
            HitInfo infoD = w.TraceRay(rayD);
            // ustawienie odpowiedniego koloru w obrazie wynikowym
            Color color0;
            Color colorA;
            Color colorB;
            Color colorC;
            Color colorD;
            if (info0.HitObject ) { color0 = info0.Color; }
            else { color0 = w.BackgroundColor; }
            if (infoA.HitObject) { colorA = infoA.Color; }
            else { colorA = w.BackgroundColor; }
            if (infoB.HitObject) { colorB = infoB.Color; }
            else { colorB = w.BackgroundColor; }
            if (infoC.HitObject) { colorC = infoC.Color; }
            else { colorC = w.BackgroundColor; }
            if (infoD.HitObject) { colorD = infoD.Color; }
            else { colorD = w.BackgroundColor; }

            if (colorA.Equals(colorB) && colorB.Equals(colorC) && colorC.Equals(colorD) && colorD.Equals(colorA))
            {
                return color0;
            }
            else
                return Color.FromArgb((colorA.R + colorB.R + colorC.R + colorD.R) / 4, (colorA.R + colorB.R + colorC.R + colorD.R) / 4, (colorA.R + colorB.R + colorC.R + colorD.R) / 4);
        }
    }
}
