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
        public Container container;

        public Packing(Instance inst)
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
        public bool Packer(int BoxID, int[] dimension, int ContainerID)
        {
            if (dimension.Length != 3 ||
               dimension[0] * dimension[1] * dimension[2] != instance.Boxes[BoxID, 1] * instance.Boxes[BoxID, 2] * instance.Boxes[BoxID, 3])
            {
                return false;
            }
            else if (instance.Boxes[BoxID, 4] + container.OccupiedWeight[ContainerID] > instance.Containers[ContainerID, 4])
            {
                return false;
            }
            else if (dimension[0] + container.ActionPoint[ContainerID, 0] <= instance.Containers[ContainerID, 1] &&
                     dimension[1] + container.ActionPoint[ContainerID, 1] <= instance.Containers[ContainerID, 2] &&
                     dimension[2] + container.ActionPoint[ContainerID, 2] <= instance.Containers[ContainerID, 3])
            {
                PackInSolution(BoxID, ContainerID, dimension);
                // update ActionPoint in relation to packing
                container.ActionPoint[ContainerID, 0] += dimension[0];
                
                // update occupied weight after packing
                container.OccupiedWeight[ContainerID] += instance.Boxes[BoxID, 4];
                return true;
            }
            else
            {
                Push(ContainerID);
                // temporary upadate Action point to a new BLOCK
                int MaxY = container.Solution[ContainerID].Where(row => row[2] == container.ActionPoint[ContainerID, 1]).Select(row => row[2] + row[5]).Max();

                var TempActionPoint = new int[3] { 0, MaxY, container.ActionPoint[ContainerID, 2] };

                //chech whethter fit box in temporary Action point
                if (dimension[0] + TempActionPoint[0] <= instance.Containers[ContainerID, 1] &&
                    dimension[1] + TempActionPoint[1] <= instance.Containers[ContainerID, 2] &&
                    dimension[2] + TempActionPoint[2] <= instance.Containers[ContainerID, 3])
                {
                    //if true pack box in temporary Action point
                    container.Solution[ContainerID].Add(new int[7] { BoxID, TempActionPoint[0], TempActionPoint[1], TempActionPoint[2], dimension[0], dimension[1], dimension[2] });

                    //update Action Point after packing with temporary Action point
                    container.ActionPoint[ContainerID, 0] = TempActionPoint[0] + dimension[0];
                    container.ActionPoint[ContainerID, 1] = TempActionPoint[1];
                    container.ActionPoint[ContainerID, 2] = TempActionPoint[2];

                    //update uccupied weight
                    container.OccupiedWeight[ContainerID] += instance.Boxes[BoxID, 4];
                    return true;
                }
                else
                {
                    //update Action point to a new Layer
                    int MaxZ = container.Solution[ContainerID].Where(row => row[3] == container.ActionPoint[ContainerID, 2]).Select(row => row[3] + row[6]).Max();
                    TempActionPoint = new int[3] { 0, 0, MaxZ };
                    //check wheter fit the box in new Action point
                    if (dimension[0] + TempActionPoint[0] <= instance.Containers[ContainerID, 1] &&
                        dimension[1] + TempActionPoint[1] <= instance.Containers[ContainerID, 2] &&
                        dimension[2] + TempActionPoint[2] <= instance.Containers[ContainerID, 3])
                    {
                        //if true pack box in temporary Action point
                        container.Solution[ContainerID].Add(new int[7] { BoxID, TempActionPoint[0], TempActionPoint[1], TempActionPoint[2], dimension[0], dimension[1], dimension[2] });

                        //update Action Point after packing by tem
                        container.ActionPoint[ContainerID, 0] = TempActionPoint[0] + dimension[0];
                        container.ActionPoint[ContainerID, 1] = TempActionPoint[1];
                        container.ActionPoint[ContainerID, 2] = TempActionPoint[2];

                        //update uccupied weight
                        container.OccupiedWeight[ContainerID] += instance.Boxes[BoxID, 4];
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
        }

        private void PackInSolution(int BoxID, int ContainerID, int[] dimension)
        {
            container.Solution[ContainerID].Add(new int[7] { BoxID, container.ActionPoint[ContainerID, 0], container.ActionPoint[ContainerID, 1], container.ActionPoint[ContainerID, 2], dimension[0], dimension[1], dimension[2] });
        }

        private void Push(int ContainerID)
        {

        }
    }
}
