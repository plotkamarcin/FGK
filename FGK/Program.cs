using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using ObjParser;
using ObjParser.Types;

namespace FGK
{
    class Program
    {
        static double eps = 0.00001;

        [STAThread]
        static void Main(string[] args)
        {

            Sphere s = new Sphere(new Vector3(0, 0, 0), 10.0, System.Drawing.Color.Aqua);
            Ray r1 = new Ray(new Vector3(0, 0, -20), new Vector3(0, 0, 20));
            Ray r2 = new Ray(new Vector3(0, 0, -20), new Vector3(0, 10, 0));
            Ray r3 = new Ray(new Vector3(-10, 10, 0), new Vector3(1, 0, 0));

            Plane plane = new Plane(new Vector3(0, 0, 0), new Vector3(0, 1, 1), Color.Gainsboro);

            r1.checkHit(s);
            r2.checkHit(s);
            r3.checkHit(s);
            r2.checkHit(plane);
            World world = new World(Color.PowderBlue);

            world.Add(new Sphere(new Vector3(0, 0, 4), 2, Color.Blue));
            world.Add(new Sphere(new Vector3(2, 0, 5), 1, Color.Red));

            Obj parser = new Obj();
            parser.LoadObj("cone.obj");
            Mesh cone = new Mesh();

            Random rnd = new Random();
            for (int i = 0; i < parser.FaceList.Count; i++)
            {
                Vector3 p1 = new Vector3(parser.VertexList[parser.FaceList[i].VertexIndexList[0] - 1].X, parser.VertexList[parser.FaceList[i].VertexIndexList[0] - 1].Y, parser.VertexList[parser.FaceList[i].VertexIndexList[0] - 1].Z);
                Vector3 p2 = new Vector3(parser.VertexList[parser.FaceList[i].VertexIndexList[1] - 1].X, parser.VertexList[parser.FaceList[i].VertexIndexList[1] - 1].Y, parser.VertexList[parser.FaceList[i].VertexIndexList[1] - 1].Z);
                Vector3 p3 = new Vector3(parser.VertexList[parser.FaceList[i].VertexIndexList[2] - 1].X, parser.VertexList[parser.FaceList[i].VertexIndexList[2] - 1].Y, parser.VertexList[parser.FaceList[i].VertexIndexList[2] - 1].Z);
                cone.triangles.Add(new Triangle(p1, p2, p3, Color.FromArgb(rnd.Next(0,256), rnd.Next(0, 256), rnd.Next(0, 256))));
            }
            foreach (Triangle t in cone.triangles)
            {
                t.translateTriangle(0, 0, 5);
                world.Add(t);
            }

            Camera camera = new Orthogonal(new Vector3(0, 0, -5), 0, new Vector2(5, 5));
            Camera perspectiveCam = new Perspective(new Vector3(0, 0, -8), new Vector3(0, 0, 0), new Vector3(0, -1, 0), 1);
            Raytracer tracer = new Raytracer();

            Bitmap image = tracer.Raytrace(world, perspectiveCam, new Size(1024, 1024));
            image.Save("raytraced.png");
            Console.ReadKey();
        }
    }
}
