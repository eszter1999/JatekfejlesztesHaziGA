using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1.GeneticAlgotithm
{
    class Population
    {
        private List<Individual> population;
        const int chromosomeLength = 50;
        public bool FitnessReady;

        public Population(int populationSize)
        {
            FitnessReady = false;
            population = new List<Individual>();
            for (int ind = 0; ind < populationSize; ind++)
            {
                population.Add(new Individual(chromosomeLength));
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
        public int getChomosomeLength() { return chromosomeLength; }
        public Individual getIndividual(int offset) { return population[offset]; }
        public Individual getFittest(int offset)
        {
            population.Sort();
            return population[offset];
        }
        public void setIndividual(int offset, Individual individual) { this.population[offset] = individual; }


    }
}
