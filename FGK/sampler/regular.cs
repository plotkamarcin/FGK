using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    public class Regular:SampleGenerator
    {
        public override Vector2[] Sample(int sampleCt)
        {
            int sampleRow = (int)Math.Sqrt(sampleCt);
            Vector2[] result = new Vector2[sampleRow * sampleRow];
            for (int x = 0; x < sampleRow; x++)
                for (int y = 0; y < sampleRow; y++)
                {
                    double fracX = (x + 0.5) / sampleRow;
                    double fracY = (y + 0.5) / sampleRow;
                    result[x * sampleRow + y] = new Vector2(fracX, fracY);
                }
            return result;
        }
    }
}
