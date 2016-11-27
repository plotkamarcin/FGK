using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    class DiskDistributor:SampleDistributor
    {
       public override Vector2 MapSample(Vector2 sample)
        {
            return new Vector2(sample.X * Math.Cos(sample.Y * Math.PI),sample.X * Math.Sin(sample.Y * Math.PI));
        }
    }
}
