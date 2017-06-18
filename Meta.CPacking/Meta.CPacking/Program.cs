using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Meta.CPacking
{
    class Program
    {
        static void Main(string[] args)
        {

            Dictionary<int, int[]> dict = new Dictionary<int, int[]>();
            int[] test = { 9, 12, 7, 25 };
            dict.Add(1,test);
            for (int k = 0; k< 3; k++)
                dict = dict.OrderBy(x => x.Value[k]).ToDictionary(x => x.Key , x => x.Value);








            string Path = @"F:\Z Studiumsablauf Z\6. Semester\Metaheuristik\Projekt\Instances\instance_2_10_1.txt";

            Instance Instance = new Instance(Path);

            Console.WriteLine(" number of containers is: "+ Instance.NumContainers);
            Console.WriteLine(" nuber of Boxes is: " + Instance.NumBoxes);

            Weighting weight = new Weighting(Instance);
            weight.VolumeRatio();

            foreach(KeyValuePair<int,double> box in weight.Ratio)
            {
                Console.WriteLine("box: {0} VolumeRatio: {1}", box.Key, box.Value);
            }
            for(int i=0; i< Instance.NumBoxes; i++)
            {
                Console.WriteLine();
                Console.Write("Box: ");
                for (int j=0; j < 6; j++)
                {
                    Console.Write(" {0} ", Instance.Boxes[i, j]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
