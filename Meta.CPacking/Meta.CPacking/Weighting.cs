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
        public Dictionary<int, double> Ratio = new Dictionary<int, double>();
        public double WtoVRatio = 0;

        public Weighting (Instance inst)
        {
            Instance = inst;
        }

        public void VolumeRatio ()
        {
            for(int i=0; i < Instance.NumBoxes; i++)
            {
                WtoVRatio += (double)Instance.Boxes[i, 4] / (Instance.Boxes[i, 1] * Instance.Boxes[i, 2] * Instance.Boxes[i, 3]);
                WtoVRatio /= Instance.NumBoxes;
            }
            
            for(int i = 0; i < Instance.NumBoxes; i++)
            {
                Ratio.Add(i + 1, (double)Instance.Boxes[i, 5] / (Instance.Boxes[i, 1] * Instance.Boxes[i, 2] * Instance.Boxes[i, 3])
                    +);
            }
            Ratio = Ratio.OrderByDescending(box => box.Value).ToDictionary(box => box.Key, box => box.Value);
        }
    }
}
