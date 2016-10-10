using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    class Program
    {
        static double eps=0.00001;

        static void Main(string[] args)
        {
            Sphere s = new Sphere(new Vector3(0, 0, 0), 10.0);
            Ray r1 = new Ray(new Vector3(0, 0, -20), new Vector3(0, 0, 20));
            Ray r2 = new Ray(new Vector3(0, 0, -20), new Vector3(0, 10, 0));
            Ray r3 = new Ray(new Vector3(-10, 10, 0), new Vector3(0, 1, 1));

            Plane plane = new Plane(new Vector3(0, 0, 0), new Vector3(0, 1, 1));

            r1.checkHit(s);
            r2.checkHit(s);
            r3.checkHit(s);
            r2.checkHit(plane);

            Console.ReadKey();
        }
    }
}
