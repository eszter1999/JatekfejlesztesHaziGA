using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GeneticAlgotithm
{
    class Population
    {
        private List<Individual> population;

        public static int Fitness { get; set; } = -1;

        public Population(int populationSize, int chomosomeLength)
        {
            for (int ind = 0; ind < populationSize; ind++)
            {
                population.Add(new Individual(chomosomeLength));
            }
        }

        public void shuffle()
        {
            Random rnd = new Random();
            int n = population.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Individual temp = population[k];
                population[k] = population[n];
                population[n] = temp;
            }
        }

        //get-set
        public int size() { return population.Count; }
        public List<Individual> getPopulation() { return population; }
        public Individual getIndividual(int offset) { return population[offset]; }
        public Individual getFitesst(int offset)
        {
            population.Sort();
            return population[offset];
        }
        public void setIndividual(int offset, Individual individual) { this.population[offset] = individual; }


    }
}
