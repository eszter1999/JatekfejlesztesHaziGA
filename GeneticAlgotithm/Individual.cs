using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Project1.GeneticAlgotithm
{
    class Individual : IComparable
    {
        private List<Directions> chromosome;
        public double Fitness { get; set; } = 0;

        public Individual(int chromosomeLength)
        {
            chromosome = new List<Directions>();
            Random rnd = new Random();
            for (int gene = 0; gene < chromosomeLength; gene++) {
                int temp = rnd.Next(4);
                if (temp == 0)
                    chromosome.Add(Directions.UP);
                if (temp >1)
                    chromosome.Add(Directions.RIGHT);
                if (temp == 1)
                    chromosome.Add(Directions.LEFT);
            }

        }

        //get-set
        public int getChromosomeLength() { return chromosome.Count; }
        public Directions getGene(int offset) { return chromosome[offset]; }
        public void setGene(int offset, Directions dir) { chromosome[offset] = dir; }


        //segédfüggvények
        public string toString()
        {
            string output = "";
            foreach (var gene in chromosome)
            {
                string temp;
                switch (gene)
                {
                    case Directions.RIGHT:
                        temp = "Right; ";
                        break;
                    case Directions.LEFT:
                        temp = "Left; ";
                        break;
                    case Directions.UP:
                        temp = "Up; ";
                        break;
                    default:
                        temp = " !!ERROR!! ";
                        break;
                }
                output += temp;
            }
            return output;
        }

        public int CompareTo(object obj)
        {
            Individual orderToCompare = obj as Individual;
            if (orderToCompare.Fitness < Fitness)
            {
                return -1;
            }
            if (orderToCompare.Fitness > Fitness)
            {
                return 1;
            }
            return 0;
        }
    }
}
