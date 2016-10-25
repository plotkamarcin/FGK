using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    class Mesh
    {
        public List<Triangle> triangles { get;  set; }

        public Mesh()
        {
            triangles = new List<Triangle>();
        }


    }
}
