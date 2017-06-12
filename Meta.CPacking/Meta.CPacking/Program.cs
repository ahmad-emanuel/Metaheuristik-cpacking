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
            string Path = @"F:\Z Studiumsablauf Z\6. Semester\Metaheuristik\Projekt\Instances\instance_12_1000_4.txt";

            Instance expl = new Instance(Path);

            Console.WriteLine(" number of containers is: "+ expl.NumContainers);
            Console.WriteLine(" nuber of Boxes is: " + expl.NumBoxes);
            Console.ReadKey();
        }
    }
}
