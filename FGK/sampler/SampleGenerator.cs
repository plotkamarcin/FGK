using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    public abstract class SampleGenerator
    {
        public abstract Vector2[] Sample(int count);
    }
}
