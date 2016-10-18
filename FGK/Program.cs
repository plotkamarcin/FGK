using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace FGK
{
    class Program
    {
        static double eps=0.00001;
        
        [STAThread]
        static void Main(string[] args)
        {
                         
            Sphere s = new Sphere(new Vector3(0, 0, 0), 10.0,System.Drawing.Color.Aqua);
            Ray r1 = new Ray(new Vector3(0, 0, -20), new Vector3(0, 0, 20));
            Ray r2 = new Ray(new Vector3(0, 0, -20), new Vector3(0, 10, 0));
            Ray r3 = new Ray(new Vector3(-10, 10, 0), new Vector3(1, 0, 0));

            Plane plane = new Plane(new Vector3(0, 0, 0), new Vector3(0, 1, 1),Color.Gainsboro);

            r1.checkHit(s);
            r2.checkHit(s);
            r3.checkHit(s);
            r2.checkHit(plane);
            World world = new World(Color.PowderBlue);

            world.Add(new Sphere(new Vector3(0, 0, 4), 2, Color.Blue));
            world.Add(new Sphere(new Vector3(2, 0, 5), 1, Color.Red));

            world.Add(new Plane(new Vector3(-5, -5, 6), new Vector3(0, 0, 1), Color.FromArgb(0,0,0)));
            world.Add(new Plane(new Vector3(-5, -3.333, 6), new Vector3(0, 0, 1), Color.FromArgb(51, 0, 0)));
            world.Add(new Plane(new Vector3(-5, -1.666, 6), new Vector3(0, 0, 1), Color.FromArgb(102, 0, 0)));
            world.Add(new Plane(new Vector3(-5, -0.0002, 6), new Vector3(0, 0, 1), Color.FromArgb(153, 0, 0)));
            world.Add(new Plane(new Vector3(-5, 1.666, 6), new Vector3(0, 0, 1), Color.FromArgb(204, 0, 0)));
            world.Add(new Plane(new Vector3(-5, 3.333, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 0, 0)));

            world.Add(new Plane(new Vector3(-3.333, -5, 6), new Vector3(0, 0, 1), Color.FromArgb(0, 0, 0)));
            world.Add(new Plane(new Vector3(-3.333, -3.333, 6), new Vector3(0, 0, 1), Color.FromArgb(0, 51, 0)));
            world.Add(new Plane(new Vector3(-3.333, -1.666, 6), new Vector3(0, 0, 1), Color.FromArgb(0, 102, 0)));
            world.Add(new Plane(new Vector3(-3.333, -0.0002, 6), new Vector3(0, 0, 1), Color.FromArgb(0, 153, 0)));
            world.Add(new Plane(new Vector3(-3.333, 1.666, 6), new Vector3(0, 0, 1), Color.FromArgb(0, 204, 0)));
            world.Add(new Plane(new Vector3(-3.333, 3.333, 6), new Vector3(0, 0, 1), Color.FromArgb(0, 255, 0)));

            world.Add(new Plane(new Vector3(-1.666, -5, 6), new Vector3(0, 0, 1), Color.FromArgb(0, 0, 0)));
            world.Add(new Plane(new Vector3(-1.666, -3.333, 6), new Vector3(0, 0, 1), Color.FromArgb(0,0 , 51)));
            world.Add(new Plane(new Vector3(-1.666, -1.666, 6), new Vector3(0, 0, 1), Color.FromArgb(0, 0, 102)));
            world.Add(new Plane(new Vector3(-1.666, -0.0002, 6), new Vector3(0, 0, 1), Color.FromArgb(0, 0, 156)));
            world.Add(new Plane(new Vector3(-1.666, 1.666, 6), new Vector3(0, 0, 1), Color.FromArgb(0, 0, 204)));
            world.Add(new Plane(new Vector3(-1.666, 3.333, 6), new Vector3(0, 0, 1), Color.FromArgb(0, 0, 255)));

            world.Add(new Plane(new Vector3(-0.0002, -5, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 0, 0)));
            world.Add(new Plane(new Vector3(-0.0002, -3.333, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 0, 51)));
            world.Add(new Plane(new Vector3(-0.0002, -1.666, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 0, 102)));
            world.Add(new Plane(new Vector3(-0.0002, -0.0002, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 0, 156)));
            world.Add(new Plane(new Vector3(-0.0002, 1.666, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 0, 204)));
            world.Add(new Plane(new Vector3(-0.0002, 3.333, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 0, 255)));

            world.Add(new Plane(new Vector3(1.666, -5, 6), new Vector3(0, 0, 1), Color.FromArgb(0,255, 0)));
            world.Add(new Plane(new Vector3(1.666, -3.333, 6), new Vector3(0, 0, 1), Color.FromArgb(0,255, 51)));
            world.Add(new Plane(new Vector3(1.666, -1.666, 6), new Vector3(0, 0, 1), Color.FromArgb(0,255, 102)));
            world.Add(new Plane(new Vector3(1.666, -0.0002, 6), new Vector3(0, 0, 1), Color.FromArgb(0,255, 156)));
            world.Add(new Plane(new Vector3(1.666, 1.666, 6), new Vector3(0, 0, 1), Color.FromArgb(0,255, 204)));
            world.Add(new Plane(new Vector3(1.666, 3.333, 6), new Vector3(0, 0, 1), Color.FromArgb(0,255, 255)));

            world.Add(new Plane(new Vector3(3.333, -5, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 255, 0)));
            world.Add(new Plane(new Vector3(3.333, -3.333, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 255, 51)));
            world.Add(new Plane(new Vector3(3.333, -1.666, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 255, 102)));
            world.Add(new Plane(new Vector3(3.333, -0.0002, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 255, 156)));
            world.Add(new Plane(new Vector3(3.333, 1.666, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 255, 204)));
            world.Add(new Plane(new Vector3(3.333, 3.333, 6), new Vector3(0, 0, 1), Color.FromArgb(255, 255, 255)));

            Camera camera = new Orthogonal(new Vector3(0, 0, -5), 0, new Vector2(5, 5));
            Camera perspectiveCam = new Perspective(new Vector3(0, 1, -8),new Vector3(0, 0, 0),new Vector3(0, -1, 0),1);
            Raytracer tracer = new Raytracer();

            Bitmap image = tracer.Raytrace(world, camera, new Size(1024, 1024));
            image.Save("raytraced.png");
            Console.ReadKey();
        }
    }
}
