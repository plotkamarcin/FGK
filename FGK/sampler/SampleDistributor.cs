using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    public abstract class SampleDistributor
    {
       public abstract Vector2 MapSample(Vector2 sample);
    }
}
