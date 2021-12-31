using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Project1.GeneticAlgotithm
{
    class Individual : IEquatable<Individual>, IComparable<Individual>
    {
        private List<Directions> chromosome;
        public int Fitness { get; set; } = -1;

        public Individual(int chromosomeLength)
        {
            for (int gene = 0; gene < chromosomeLength; gene++)
                chromosome.Add(Directions.STAY);
            
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
                    case Directions.STAY:
                        temp = "Stay; ";
                        break;
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

        public override bool Equals(object obj)
        {
             if (obj == null)
                return false;
             Individual objAsPart = obj as Individual;
             if (objAsPart == null) 
                return false;
             else 
                return Equals(objAsPart);
        }

        public bool Equals(Individual other)
        {
            if(other == null)
                return false;
            return (this.Fitness.Equals(other.Fitness));
        }

        public int CompareTo(Individual other)
        {
            if (other == null)
                return 1;

            else
                return this.Fitness.CompareTo(other.Fitness);
        }
    }
}
