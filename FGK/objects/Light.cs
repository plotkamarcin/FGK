using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    public class Light
    {
        Vector3 center;
        double radius;
        Sampler sampler;
        ColorRgb color;
        // Konstruktor udający światło punktowe
        public Light(Vector3 position, ColorRgb color)
        : this(position, color, null, 0) { }
        // pełny konstruktor
        public Light(Vector3 center, ColorRgb color, Sampler sampler, double radius)
        {
            this.center = center;
            this.radius = radius;
            this.sampler = sampler;
            this.color = color;
        }
        public Vector3 Sample()
        {
            if (radius == 0) { return center; } // symulowanie światła punktowego
            var sample = sampler.Single();
            return center + RemapSampleToUnitSphere(sample) * radius;
        }
        Vector3 RemapSampleToUnitSphere(Vector2 sample)
        {
            double z = 2 * sample.X - 1;
            double t = 2 * Math.PI * sample.Y;
            double r = Math.Sqrt(1 - z * z);
            return new Vector3(
            r * Math.Cos(t),
            r * Math.Sin(t),
            z);
        }
        public ColorRgb Color { get { return color; } }
    }
}
