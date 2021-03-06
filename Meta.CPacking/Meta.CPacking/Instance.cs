﻿using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Meta.CPacking
{
    class Instance
    {
        public int NumContainers;
        public int[,] Containers;
        public int NumBoxes;
        public int[,] Boxes;

        public Instance (string path)
        {
            Initialization(path);
        }

        private void Initialization(string InPath)
        {
            var Temp = File.ReadLines(InPath)
                .Select(line => line.Split(' '))
                .ToArray();

            NumContainers = int.Parse(Temp[0][1]);

            Containers = new int [NumContainers,5];
            for (int i = 1; i <= NumContainers; i++)
            {
                Containers[i-1, 0] = i;
                for (int j = 0; j < 4; j++)
                {
                    Containers[i-1,j+1] = int.Parse(Temp[i][j]);
                }
            }

            NumBoxes = int.Parse(Temp[NumContainers + 1][1]);

            Boxes = new int[NumBoxes,6];
            for (int i = NumContainers + 2; i < NumContainers + NumBoxes + 2; i++)
            {
                Boxes[i - NumContainers - 2, 0] = i - NumContainers - 1;
                for (int j = 0; j < 5; j++)
                {
                    Boxes[i - NumContainers - 2, j + 1] = int.Parse(Temp[i][j]);
                }
            }


        }
    }
}
