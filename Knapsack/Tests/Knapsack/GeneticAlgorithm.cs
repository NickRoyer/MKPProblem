using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Knapsack.Models;

namespace Knapsack.Tests
{
    //public class GeneticAlgorithm : KnapSackTest

    // Genetic Algorithm test inheriting from MKPTest
    public class GeneticAlgorithmTest : KnapSackTest
    {
        const int POPULATION_SIZE = 50;
        const int GENERATIONS = 100;
        const double MUTATION_RATE = 0.1;
        const int TOURNAMENT_SIZE = 5;

        public int BestFitness { get; private set; }

        protected override void ProcessTest()
        {
            DetermineOptimalSolution(TM.ItemList);
        }

        public void DetermineOptimalSolution(List<KSItem> items)
        {
            var population = InitializePopulation(items);
            KnapsackSolution bestSolution = null;
            BestFitness = 0;
            var random = new Random();

            for (int generation = 0; generation < GENERATIONS; generation++)
            {
                var newPopulation = new List<KnapsackSolution>();

                for (int i = 0; i < POPULATION_SIZE; i++)
                {
                    // Selection
                    var parent1 = Selection(population, random);
                    var parent2 = Selection(population, random);

                    // Crossover
                    var child = Crossover(parent1, parent2, TM, random);

                    // Mutation
                    child = Mutate(child, random);

                    newPopulation.Add(child);
                }

                // Update population
                population = newPopulation;

                // Check for best solution
                foreach (var solution in population)
                {
                    int currentFitness = Fitness(solution);
                    if (currentFitness > BestFitness)
                    {
                        BestFitness = currentFitness;
                        bestSolution = solution;
                    }
                }

                if (bestSolution != null) {
                    OptimalSolution = bestSolution;
                }
                // Optional: Print generation info
                // Console.WriteLine($"Generation {generation + 1}: Best Fitness = {BestFitness}");
            }
        }

        // Initialize population
        List<KnapsackSolution> InitializePopulation(List<KSItem> itemList)
        {
            var population = new List<KnapsackSolution>();
            var random = new Random();

            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                var solution = new KnapsackSolution(TM);
                var items = itemList.OrderBy(x => random.Next()).ToList();
                foreach (var item in items)
                {
                    solution.TryAddItem(item);
                }
                population.Add(solution);
            }
            return population;
        }

        // Fitness function
        int Fitness(KnapsackSolution solution)
        {
            return solution.Result.Value;
        }

        // Selection (Tournament Selection)
        KnapsackSolution Selection(List<KnapsackSolution> population, Random random)
        {
            var tournament = new List<KnapsackSolution>();
            for (int i = 0; i < TOURNAMENT_SIZE; i++)
            {
                tournament.Add(population[random.Next(POPULATION_SIZE)]);
            }
            var fittest = tournament.OrderByDescending(c => Fitness(c)).First();
            return fittest;
        }

        // Crossover (Single Point Crossover)
        KnapsackSolution Crossover(KnapsackSolution parent1, KnapsackSolution parent2, KnapsackTestManager tm, Random random)
        {
            var child = new KnapsackSolution(tm);
            var crossoverPoint = random.Next(tm.ItemList.Count);

            var items = tm.ItemList;

            for (int i = 0; i < crossoverPoint; i++)
            {
                if (parent1.Solution.Contains(items[i]))
                {
                    child.TryAddItem(items[i]);
                }
            }
            for (int i = crossoverPoint; i < items.Count; i++)
            {
                if (parent2.Solution.Contains(items[i]))
                {
                    child.TryAddItem(items[i]);
                }
            }
            return child;
        }

        // Mutation
        KnapsackSolution Mutate(KnapsackSolution solution, Random random)
        {
            var mutatedSolution = new KnapsackSolution(TM);
            var items = solution.OriginalItemList;
            foreach (var item in items)
            {
                if (random.NextDouble() < MUTATION_RATE)
                {
                    // Decide to add or remove the item
                    if (solution.Solution.Contains(item))
                    {
                        // Remove item
                        solution.Solution.Remove(item);
                        solution.Result.Value -= item.Value;
                        solution.Result.Weight -= item.Weight;
                        solution.Result.Volume -= item.Volume;
                    }
                    else
                    {
                        // Try to add item
                        solution.TryAddItem(item);
                    }
                }
            }
            return solution;
        }
    }


}
