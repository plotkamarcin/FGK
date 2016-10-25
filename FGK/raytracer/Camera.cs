using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    abstract class Camera
    {
       public abstract Ray GetRayTo(Vector2 relativeLocation);
    }
}
