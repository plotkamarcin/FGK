﻿using System;
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
        static double eps = 0.01;

        [STAThread]
        static void Main(string[] args)
        {
            const int SampleCt = 32;
            Bitmap texture = new Bitmap("texture_cone.png");
            Bitmap textureLena = new Bitmap("lena.jpg");
            Bitmap textureWorld = new Bitmap("world.jpg");
            Color sampled = texture.GetPixel(0, 0);

            Material redMat = new PhongMaterial(Color.Red, 1.0, 0, 50);
            Material greenMat = new PhongMaterial(Color.Green, 1.0, 0, 50);
            Material whiteMat =  new PhongMaterial(Color.White, 1.0, 0, 50);
            Material blueMat = new PhongMaterial(Color.Blue, 0.7, 8, 50);
            Material grayMat = new PhongMaterial(Color.Gray, 0.7, 8, 50);
            Material lenaMat = new PhongTexturedMaterial(Color.White, 1, 0.8, 1, 3, ref textureLena);
            Material worldMat = new PhongTexturedMaterial(Color.White, 1, 0.8, 1, 3, ref textureWorld);
            Material reflectiveMat = new Reflective(Color.White, 0.4, 1, 300, 0.6);
            Material transparentMat = new Transparent(Color.White, 0.1, 0, 0, 0.3, 0.9, 0.9);

            Sphere s = new Sphere(new Vector3(0, 0, 0), 10.0, redMat);
            Ray r1 = new Ray(new Vector3(0, 0, -20), new Vector3(0, 0, 20));
            Ray r2 = new Ray(new Vector3(0, 0, -20), new Vector3(0, 10, 0));
            Ray r3 = new Ray(new Vector3(-10, 10, 0), new Vector3(1, 0, 0));

            Plane plane = new Plane(new Vector3(0, 0, 0), new Vector3(0, 1, 1), redMat);

            r1.CheckHit(s);
            r2.CheckHit(s);
            r3.CheckHit(s);
            r2.CheckHit(plane);
            World world = new World(Color.PowderBlue);

            // world.Add(new Sphere(new Vector3(0, 2, 2), 1, transparentMat));
            //world.Add(new Sphere(new Vector3(2, 2, 6), 1, reflectiveMat));
            //world.Add(new Sphere(new Vector3(2, 2, 0), 2, transparentMat));
            //world.Add(new Plane(new Vector3(2, -2, 2), new Vector3(0, 1, 0), lenaMat));
            world.Add(new Plane(new Vector3(-3, 0, 0), new Vector3(1, 0, 0), redMat));
            world.Add(new Plane(new Vector3(3, 0, 0), new Vector3(-1, 0, 0), greenMat));
           world.Add(new Plane(new Vector3(5, -2, 0), new Vector3(0, 1, 0), whiteMat));
            world.Add(new Plane(new Vector3(5, 2, 0), new Vector3(0, -1, 0), whiteMat));
            world.Add(new Plane(new Vector3(0, 2, 6), new Vector3(0, 0, -1), whiteMat));
            world.Add(new Plane(new Vector3(0, 2, -8), new Vector3(0, 0, 1), whiteMat));
            world.Add(new Sphere(new Vector3(1.5, -1, 3), 0.75, transparentMat));
            world.Add(new Sphere(new Vector3(0, -1, 3), 0.75, blueMat));
            world.Add(new Sphere(new Vector3(-1.5, -1, 3), 0.75, reflectiveMat));


            Sampler areaLightSampler = new Sampler(new Jittered(SampleCt,0),new SquareDistribution(),SampleCt, 32); // ilość zestawów sampli


            world.AddLight(new Light(new Vector3(0, 1, 2),Color.White, areaLightSampler,1)); // promien

           // world.AddLight(new Light(new Vector3(0, 1, 2), Color.White));
            Obj parser = new Obj();
            parser.LoadObj("cone.obj");
            Mesh externalMesh = new Mesh();

            Random rnd = new Random();
            for (int i = 0; i < parser.FaceList.Count; i++)
            {
                Vector3 p1 = new Vector3(parser.VertexList[parser.FaceList[i].VertexIndexList[0] - 1].X, parser.VertexList[parser.FaceList[i].VertexIndexList[0] - 1].Y, parser.VertexList[parser.FaceList[i].VertexIndexList[0] - 1].Z);
                Vector3 p2 = new Vector3(parser.VertexList[parser.FaceList[i].VertexIndexList[1] - 1].X, parser.VertexList[parser.FaceList[i].VertexIndexList[1] - 1].Y, parser.VertexList[parser.FaceList[i].VertexIndexList[1] - 1].Z);
                Vector3 p3 = new Vector3(parser.VertexList[parser.FaceList[i].VertexIndexList[2] - 1].X, parser.VertexList[parser.FaceList[i].VertexIndexList[2] - 1].Y, parser.VertexList[parser.FaceList[i].VertexIndexList[2] - 1].Z);
                externalMesh.triangles.Add(new Triangle(p1, p2, p3, new PhongTexturedMaterial(new ColorRgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)), 1, 0.8, 1, 1, ref texture)));

                externalMesh.triangles[i].SetVertexNormals(new Vector3(parser.NormalsList[parser.FaceList[i].NormalsVertexIndexList[0] - 1].X, parser.NormalsList[parser.FaceList[i].NormalsVertexIndexList[0] - 1].Y, parser.NormalsList[parser.FaceList[i].NormalsVertexIndexList[0] - 1].Z),
                    new Vector3(parser.NormalsList[parser.FaceList[i].NormalsVertexIndexList[1] - 1].X, parser.NormalsList[parser.FaceList[i].NormalsVertexIndexList[1] - 1].Y, parser.NormalsList[parser.FaceList[i].NormalsVertexIndexList[1] - 1].Z),
                    new Vector3(parser.NormalsList[parser.FaceList[i].NormalsVertexIndexList[2] - 1].X, parser.NormalsList[parser.FaceList[i].NormalsVertexIndexList[2] - 1].Y, parser.NormalsList[parser.FaceList[i].NormalsVertexIndexList[2] - 1].Z));

                externalMesh.triangles[i].SetTextureCoords(new Vector2(parser.TextureList[parser.FaceList[i].TextureVertexIndexList[0] - 1].X, parser.TextureList[parser.FaceList[i].TextureVertexIndexList[0] - 1].Y),
                    new Vector2(parser.TextureList[parser.FaceList[i].TextureVertexIndexList[1] - 1].X, parser.TextureList[parser.FaceList[i].TextureVertexIndexList[1] - 1].Y),
                    new Vector2(parser.TextureList[parser.FaceList[i].TextureVertexIndexList[2] - 1].X, parser.TextureList[parser.FaceList[i].TextureVertexIndexList[2] - 1].Y));

            }
            foreach (Triangle t in externalMesh.triangles)
            {
                //t.TranslateTriangle(2, 2, 1);
                //t.ScaleTriangle(0.75);
                //world.Add(t);
            }

            //world.Add(new Triangle(new Vector3(0.0, -2.0, 2.0), new Vector3(2.0, -6.0, 6.0), new Vector3(-2.0, -3.0, 6.0), new PhongMaterial(new ColorRgb(255,0,0),0.8,1,30)));
            // world.Add(new Plane(new Vector3(0, -2, 0), new Vector3(0, 1, 0), redMat));
            Camera camera = new Orthogonal(new Vector3(0, 0, 0), 0, new Vector2(5, 5));
            Camera perspectiveCam = new Perspective(new Vector3(0, 0, -8), new Vector3(0, 0, 0), new Vector3(0, -1, 0), 2);
            Raytracer tracer = new Raytracer(10);

            Bitmap image = tracer.Raytrace(world, perspectiveCam, new Size(1024, 1024),areaLightSampler);
            image.Save("raytraced.png");
            Console.ReadKey();
        }
    }
}
