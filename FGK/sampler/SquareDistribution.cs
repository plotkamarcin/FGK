using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    class SquareDistribution:SampleDistributor
    {
        public override Vector2 MapSample(Vector2 sample)
        { return sample; }
    }
}
