using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GAF;
using GAF.Extensions;

namespace Meta.CPacking
{
    class Program
    {
        static string Path = @"F:\Z Studiumsablauf Z\6. Semester\Metaheuristik\Projekt\Instances\instance_2_10_1.txt";

        static void Main(string[] args)
        {
            Instance Instance = new Instance(Path);
            Weighting weighter = new Weighting(Instance);

            // empty population --- Population size = boxes*containers --- Chromosome size = boxes
            int PopulationSize = Instance.NumBoxes * Instance.NumContainers;
            int ChromosomeSize = Instance.NumBoxes;
            var Population = new Population();

            for(var p = 0; p < PopulationSize; p++)
            {
                var Chromosome = new Chromosome();

                WeightedBox[] WeightingArray = new WeightedBox[ChromosomeSize];
                WeightingArray = weighter.InitilizeWeightingArray();

                foreach(var box in WeightingArray)
                {
                    Chromosome.Genes.Add(new Gene(box));
                }
                Chromosome.Genes.ShuffleFast();

                double test = EvaluateFitness(Chromosome);

                Population.Solutions.Add(Chromosome);
            }


            Console.ReadKey();
        }

        public static double EvaluateFitness(Chromosome chromosome)
        {
            Instance instance = new Instance(Path);
            Weighting weighter = new Weighting(instance);

            Dictionary<int, WeightedBox> InputToPack = new Dictionary<int, WeightedBox>();


            int i = 0;
            foreach(Gene Gene in chromosome.Genes)
            {
                //save Gene in WeightedBox instance
                var box = new WeightedBox();
                box = (WeightedBox)Gene.ObjectValue;

                //update Weight and orientaton with absolute Ratio and dimension
                box.Weight += weighter.Ratio[i];

                int[] Dimension = new int[3];
                for(int j = 0; j < 3; j++)
                {
                    Dimension[j] = instance.Boxes[i, j + 1];
                }
                box.Orientation = ConvertDimension(box.Orientation, Dimension);

                //Add to dictionary
                InputToPack.Add(i, box);
                i++;
            }

            InputToPack = InputToPack.OrderByDescending(x => x.Value.Weight).ToDictionary(x => x.Key, x => x.Value);

            return 0;
        }

        public static int[] ConvertDimension(int[] Key, int[] Value)
        {
            Array.Sort(Value);
            int min = Value[0];
            int mid = Value[1];
            int max = Value[2];

            for (int i = 0; i < 3; i++)
            {
                switch (Key[i])
                {
                    case 1:
                        Value[i] = min;
                        break;
                    case 2:
                        Value[i] = mid;
                        break;
                    case 3:
                        Value[i] = max;
                        break;
                }
            }
            return Value;
        }
    }
}
