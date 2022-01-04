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
        private const double min_fittness = 1000;

        public GeneticAlgorithm(int populationSize, double mutationRate, double crossoverRate, int elitismCount,int tournamentSize)
        {
            this.populationSize = populationSize;
            this.mutationRate = mutationRate;
            this.crossoverRate = crossoverRate;
            this.elitismCount = elitismCount;
            this.tournamentSize = tournamentSize;
        }
        public Population initPopulaion() { return new Population(populationSize); }


        public bool isTerminationConditionMet(int generationsCount, int maxGenerations)
        {
            return (generationsCount > maxGenerations);
        }
        public bool isTerminationConditionMet(Population population)
        {
            return population.getFittest(0).Fitness >= min_fittness;
        }


        //mutálás
        public Population mutateRandom(Population population)
        {
            foreach (var individual in population.getPopulation())
            {
                Random rnd = new Random();
                Individual rndIndividual = new Individual(population.getChomosomeLength());
                for (int geneIndex = 0; geneIndex < individual.getChromosomeLength(); geneIndex++)
                    if (mutationRate > rnd.NextDouble())
                        individual.setGene(geneIndex, rndIndividual.getGene(geneIndex));
            }
            return population;
        }
        // crossover
        public Population crossoverPopulation(Population population)
        {
            Population newPopulation = new Population(population.size());
            for (int populationIndex = 0; populationIndex < population.size(); populationIndex++)
            {
                Individual parent1 = population.getFittest(populationIndex);
                Random rnd = new Random();
                // Apply crossover to this individual?
                if (this.crossoverRate > rnd.NextDouble() && populationIndex >= this.elitismCount)
                {
                    Individual offspring = new Individual(parent1.getChromosomeLength());
                    // Find second parent
                    Individual parent2 = selectParent(population);
                    for (int geneIndex = 0; geneIndex < parent1.getChromosomeLength(); geneIndex++)
                    {
                        // Use half of parent1's genes and half of parent2's genes
                        if (0.5 > rnd.NextDouble())
                        {
                            offspring.setGene(geneIndex, parent1.getGene(geneIndex));
                        }
                        else
                        {
                            offspring.setGene(geneIndex, parent2.getGene(geneIndex));
                        }
                    }

                    // Add offspring to new population
                    newPopulation.setIndividual(populationIndex, offspring);
                }
                else
                {
                    // Add individual to new population without applying crossover
                    parent1.Fitness = 0;
                    newPopulation.setIndividual(populationIndex, parent1);
                }
            }

            return newPopulation;
        }
        public Individual selectParent(Population population)
        {
            // Create tournament
            Population tournament = new Population(this.tournamentSize);

            // Add random individuals to the tournament
            population.shuffle();
            for (int i = 0; i < this.tournamentSize; i++)
            {
                Individual tournamentIndividual = population.getIndividual(i);
                tournament.setIndividual(i, tournamentIndividual);
            }

            // Return the best
            return tournament.getFittest(0);
        }

        //fitness számolás
        public int calcFitness(Individual individual, Player p, Game1 g)
        {
            int fitness = 0, i = 0;
            bool won = false;
            while (fitness < individual.getChromosomeLength() && !won)
            {
                //g.movePlayer(p, individual.getGene(i));
                fitness = 
                i++;
            }

            return fitness;

            /*public double calcFitness(Individual individual, Timetable timetable) {

        // Create new timetable object to use -- cloned from an existing timetable
        Timetable threadTimetable = new Timetable(timetable);
        threadTimetable.createClasses(individual);

        // Calculate fitness
        int clashes = threadTimetable.calcClashes()[0];
        double fitness = 1000 - 100 * clashes;

        individual.setFitness(fitness);

        return fitness;*/
        }
        /*public void evalPopulation(Population population)
        {
            int populationFitness = 0;

            // Loop over population evaluating individuals and summing population
            // fitness
            foreach (var individual in population.getPopulation())
            {
                //populationFitness += this.calcFitness(individual);
            }
            population.Fitness = populationFitness;
        }*/


        
    }
    }

/**
 *
 *public class Main {
   static final int
            population_size = 290, 
elitism_count = 2,
            tournament_size = 5,
            max_generation = 1200;
static final double
        mutation_rate = 0.002,
        crossover_rate = 0.95;

public static void main(String[] args) throws IOException
{
    Timetable timetable = initializeTimetable();
    GeneticAlgorithm ga = new GeneticAlgorithm(population_size, mutation_rate, crossover_rate, elitism_count, tournament_size);
Population population = ga.initPopulation(timetable);
ga.evalPopulation(population, timetable);

// Keep track of current generation
int generation = 1;
while (!ga.isTerminationConditionMet(generation, max_generation)
        && !ga.isTerminationConditionMet(population))
{
    // Print fitness
    System.out.println("G" + generation + " Best fitness: " + population.getFittest(0).getFitness());

    // Apply crossover
    population = ga.crossoverPopulation(population);

    // Apply mutation
    population = ga.mutatePopulation(population, timetable);

    // Evaluate population
    ga.evalPopulation(population, timetable);

    // Increment the current generation
    generation++;
}*/