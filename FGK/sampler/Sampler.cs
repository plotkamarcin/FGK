using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    public class Sampler
    {
        Random r;
        List<Vector2[]> sets;
        int sampleNdx;
        int setNdx;
        public Sampler(SampleGenerator sampler,
        SampleDistributor mapper,
        int sampleCt,
        int setCt)
        {
            this.sets = new List<Vector2[]>(setCt);
            this.r = new Random(0);
            this.SampleCount = sampleCt;
            for (int i = 0; i < setCt; i++)
            {
                var samples = sampler.Sample(sampleCt);
                var mappedSamples = samples.Select((x) => mapper.MapSample(x)).ToArray();
                sets.Add(mappedSamples);
            }
        }
        public Vector2 Single()
        {
            Vector2 sample = sets[setNdx][sampleNdx];
            sampleNdx++;
            if (sampleNdx >= sets[setNdx].Length)
            { sampleNdx = 0; setNdx = r.Next(sets.Count); }
            return sample;
            }
        public int SampleCount { get; private set; }
        public int SetCount { get { return sets.Count; } }
    }
}
