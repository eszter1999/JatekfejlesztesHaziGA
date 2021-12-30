using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.GeneticAlgotithm
{
    class GeneticAlgorithm
    {
        private int populationSize;
        private double mutationRate;
        private double crossoverRate;
        private int elitismCount;
        protected int tournamentSize;
        private const double min_fittness = 100;

        public GeneticAlgorithm(int populationSize, double mutationRate, double crossoverRate, int elitismCount,int tournamentSize)
        {
            this.populationSize = populationSize;
            this.mutationRate = mutationRate;
            this.crossoverRate = crossoverRate;
            this.elitismCount = elitismCount;
            this.tournamentSize = tournamentSize;
        }

        public Population initPopulaion()
        {
            return new Population(populationSize, populationSize);
            
        }

        //leállási feltételek
        public bool isTerminationConditionMet(int generationsCount, int maxGenerations)
        {
            return (generationsCount > maxGenerations);
        }
        public bool isTerminationConditionMet(Population population)
        {
            return population.getFitesst(0).Fitness == min_fittness;
        }

        //mutálás
        private Individual mutateRandom(Individual individual, double rate)
        {
            Random rnd = new Random();
            Individual rndIndividual = new Individual(populationSize);
            for (int geneIndex = 0; geneIndex < individual.getChromosomeLength(); geneIndex++)
            {
                if (rate > rnd.NextDouble())
                    individual.setGene(geneIndex, rndIndividual.getChromosome(geneIndex));
            }
            return individual;
        }


        }
    }
