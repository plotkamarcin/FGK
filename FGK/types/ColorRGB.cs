using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FGK
{
    public class ColorRgb
    {
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }
        public ColorRgb(double r, double g, double b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }
        public static implicit operator ColorRgb(Color color)
        { return new ColorRgb(color.R / 255.0, color.G / 255.0, color.B / 255.0); }
        public static ColorRgb operator +(ColorRgb col1, ColorRgb col2)
        { return new ColorRgb(col1.R + col2.R, col1.G + col2.G, col1.B + col2.B); }
        public static ColorRgb operator +(ColorRgb col1, double val)
        { return new ColorRgb(col1.R + val, col1.G + val, col1.B + val); }
        public static ColorRgb operator *(ColorRgb col1, double val)
        { return new ColorRgb(col1.R * val, col1.G * val, col1.B * val); }
        public static ColorRgb operator *(ColorRgb col1, ColorRgb col2)
        { return new ColorRgb(col1.R * col2.R, col1.G * col2.G, col1.B * col2.B); }
        public static ColorRgb operator /(ColorRgb col1, double val)
        { return col1 * (1 / val); }
        public static readonly ColorRgb White = new ColorRgb(1, 1, 1);
        public static readonly ColorRgb Black = new ColorRgb(0, 0, 0);
    }
}
