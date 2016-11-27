using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    class PureRandom:SampleGenerator
    {
        Random r;
        public PureRandom(int seed)
        {
            this.r = new Random(seed);
        }
        public override Vector2[] Sample(int sampleCt)
        {
            Vector2[] samples = new Vector2[sampleCt];
            for (int i = 0; i < sampleCt; i++)
            {
                samples[i] = new Vector2(r.NextDouble(), r.NextDouble());
            }
            return samples;
        }
    }
}
