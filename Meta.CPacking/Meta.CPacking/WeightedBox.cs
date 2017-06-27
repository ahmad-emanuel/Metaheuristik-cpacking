using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meta.CPacking
{
    class WeightedBox
    {
        public double Weight;
        public int[] Orientation = new int[3];

        public WeightedBox() { }

        public WeightedBox(double weight, int[] orientation)
        {
            Weight = weight;
            Orientation = orientation;
        }
    }
}
