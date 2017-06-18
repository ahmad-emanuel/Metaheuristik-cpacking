using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meta.CPacking
{
    class Container
    {
        public int[,] ActionPoint ;
        int CurrentContainer = 0;
        public int[] OccupiedWeight;
        public List<int[]>[] Solution; 

        public Container(Instance inst)
        {
            ActionPoint = new int[inst.NumContainers, 3];
            ActionPoint = null;
            OccupiedWeight = new int[inst.NumContainers];
            Solution = new List<int[]>[inst.NumContainers];
        }
    }
}