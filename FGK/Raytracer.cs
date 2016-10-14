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

            for (int x = 0; x < imageSize.Width; x++)
                for (int y = 0; y < imageSize.Height; y++)
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
                    bmp.SetPixel(x, y, color);
                }

            return bmp;
        }
    }
}
