﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    class Vector3
    {
        public double x;
        public double y;
        public double z;
        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public double X
        { get { return x; } set { x = value; } }
        public double Y
        { get { return y; } set { y = value; } }
        public double Z
        { get { return z; } set { z = value; } }

        public static Vector3 operator +(Vector3 vecA, Vector3 vecB)
        {
            return new Vector3(vecA.X + vecB.X, vecA.Y + vecB.Y, vecA.Z + vecB.Z);
        }
        public static Vector3 operator -(Vector3 vecA, Vector3 vecB)
        {
            return new Vector3(vecA.X - vecB.X, vecA.Y - vecB.Y, vecA.Z - vecB.Z);
        }
        public static Vector3 operator *(Vector3 vec, double val)
        {
            return new Vector3(vec.X * val, vec.Y * val, vec.Z * val);
        }
        public static Vector3 operator /(Vector3 vec, double val)
        {
            return new Vector3(vec.X / val, vec.Y / val, vec.Z / val);
        }
        public double Dot(Vector3 vec)
        {
            return (this.X * vec.X + this.Y * vec.Y + this.Z * vec.Z);
        }
        public static Vector3 Cross(Vector3 vecA, Vector3 vecB)
        {
            return new Vector3(vecA.Y * vecB.Z - vecA.Z * vecB.Y,
            vecA.Z * vecB.X - vecA.X * vecB.Z,
            vecA.X * vecB.Y - vecA.Y * vecB.X);
        }
        public double Length
        { get { return Math.Sqrt(X * X + Y * Y + Z * Z); } }
        public double LengthSq
        { get { return X * X + Y * Y + Z * Z; } }
        public Vector3 Normalized
        { get { return this / this.Length; } }

    }

}

