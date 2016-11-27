using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    class Jittered:SampleGenerator
    {
        Random r;
        public Jittered(int sampleCt, int seed)
        {
            this.r = new Random(seed);
        }
        public override Vector2[] Sample(int count)
        {
            int sampleRow = (int)Math.Sqrt(count);
            Vector2[] result = new Vector2[sampleRow * sampleRow];
            for (int x = 0; x < sampleRow; x++)
                for (int y = 0; y < sampleRow; y++)
                {
                    double fracX = (x + r.NextDouble()) / sampleRow;
                    double fracY = (y + r.NextDouble()) / sampleRow;
                    result[x * sampleRow + y] = new Vector2(fracX, fracY);
                }
            return result;
        }
    }
}
