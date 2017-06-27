using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meta.CPacking
{
    class Weighting
    {
        private Instance Instance;
        public double[] Ratio;

        private double MeanOfRatio = 0;
        private double BiasingStrength = 0;

        public Weighting (Instance inst)
        {
            Instance = inst;
            Ratio = new double[Instance.NumBoxes];
            AbsoluteRatio();
        }

        private void AbsoluteRatio ()
        {
            // ratio of profit in relation to volume and weigh: Profit/ volume*weight
            // IDs of boxes are zero base
            for(int i = 0; i < Instance.NumBoxes; i++)
            {
                Ratio[i] = (double)Instance.Boxes[i, 5] / //profit
                             (Instance.Boxes[i, 1] * Instance.Boxes[i, 2] * Instance.Boxes[i, 3] * //Volume
                             Instance.Boxes[i,4]);    //weight
            }

            MeanOfRatio = Ratio.Average(ratio => ratio);
            double min = Ratio.Min(x => x);
            BiasingStrength = min / MeanOfRatio;
        }

        public WeightedBox[] InitilizeWeightingArray()
        {
            WeightedBox[] WeightingArray = new WeightedBox[Instance.NumBoxes];
            Random R = new Random();
            double Range = BiasingStrength * MeanOfRatio;

            for(int i=0; i< Instance.NumBoxes; i++)
            {
                WeightingArray[i] = new WeightedBox();
                WeightingArray[i].Weight = (R.NextDouble() * 2 * Range) - Range;
                int[] s = new int[3] { 1, 2, 3 };
                WeightingArray[i].Orientation = s.OrderBy(x => R.Next()).ToArray();
            }
            return WeightingArray;
        }
    }
}
