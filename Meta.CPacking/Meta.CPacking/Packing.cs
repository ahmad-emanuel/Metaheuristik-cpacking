using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meta.CPacking
{
    class Packing
    {
        Instance instance;
        Container container;

        public Packing (Instance inst)
        {
            instance = inst;
            container = new Container(inst);
        }

        /// <summary>
        /// IDs are Zero Base
        /// </summary>
        /// <param name="BoxID"></param>
        /// <param name="ContainerID"></param>
        /// <returns></returns> whether the Box is packed or not
        public Boolean Packer(int BoxID, int[] dimension, int ContainerID)
        {
            if(dimension.Length != 3 ||
               dimension[0]*dimension[1]*dimension[2] != instance.Boxes[BoxID,1]* instance.Boxes[BoxID, 2]* instance.Boxes[BoxID, 3])
            {
                return false;
            }
            else if (instance.Boxes[BoxID, 4] + container.OccupiedWeight[ContainerID] > instance.Containers[ContainerID, 4])
            {
                return false;
            }
            else if (WhetherFit(BoxID, ContainerID))
            {
                PackInSolution(BoxID, ContainerID, dimension);
                // update ActionPoint in relation to packing
                container.ActionPoint[ContainerID, 0] += dimension[0];
                container.ActionPoint[ContainerID, 0] += dimension[1];
                container.ActionPoint[ContainerID, 0] += dimension[2];
                // update occupied weight after packing
                container.OccupiedWeight[ContainerID] += instance.Boxes[BoxID, 4];
                return true;
            }
            else
            {
                Push(ContainerID);
                // upadate Action point to a new BLOCK

            }
        }

        private Boolean WhetherFit(int BoxID, int ContainerID)
        {
            if (instance.Boxes[BoxID, 1] + container.ActionPoint[ContainerID, 0] <= instance.Containers[ContainerID, 1] &&
                instance.Boxes[BoxID, 2] + container.ActionPoint[ContainerID, 1] <= instance.Containers[ContainerID, 2] &&
                instance.Boxes[BoxID, 3] + container.ActionPoint[ContainerID, 2] <= instance.Containers[ContainerID, 3])
            {
                return true;
            }
            else return false;
        }

        private void PackInSolution (int BoxID, int ContainerID , int[] dimension)
        {
            container.Solution[ContainerID].Add(new int[7] { BoxID, container.ActionPoint[ContainerID, 0], container.ActionPoint[ContainerID, 1], container.ActionPoint[ContainerID, 2], dimension[0], dimension[1], dimension[2] });
        }

        private void Push (int ContainerID)
        {

        }

    }
}
