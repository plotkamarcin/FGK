using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    class PointLight
    {
        public PointLight(Vector3 position, ColorRgb color)
        {
            this.Position = position;
            this.Color = color;
        }
        public Vector3 Position { get; private set; }
        public ColorRgb Color { get; private set; }
    }
}

