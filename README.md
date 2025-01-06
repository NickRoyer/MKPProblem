# Multidimensional Knapsack Problem

This is a test project that verifies the validity of algorithms being utilized to find the optimal solution to a single dimension knapsack problem or a simple multidimensional knapsack problem.

# Knapsack Problem

The traditional problem is a knapsack is being filled with items and you are trying to maximize the value of items being placed in the knapsack. Note: for this test there is only one copy of each item.

The single dimension in this test project is weight, i.e. maximize value without exceeding the maximum weight.

The second (optional) dimension in this test is volume, i.e. maximize value without exceeding either maximum weight OR maximum volume.


# Algorithms

N => the number of items,

K => the maximum number of items allowed in the knapsack

## Brute Force Permutations

Attempt each permutation when placing items in the knapsack 123 != 321 != 132 != 321, etc. (order matters)

N! == the number of possible solutions if there is no bound on the number of items allowed to be placed in the knapsack 

N!/((N-K)! == the number of possible solutions where the knapsack is limited to K items 

NOTE: the knapsack problem does not have a constraint where order matters so this is included for informational purposes.

## Brute Force Combination

Attempt each combination of items 1,2,3,4 == none,1,2,3,4,12,13,14,23,24,34,123,124,134,234, etc. (order does not matter)

2^N == The number of possible solutions if no bound on the number of items

N! / (K! * (N -K)!) == The number of solutions if the maximum number of items is limited.

## Dynamic Programming, Bottom Up Approach

Build an array of int[,,] = int[N,W,V] to store the calculated value when adding that item to the mix

Loop through the items, the possible weights, and the possible volumes

Base case is all 0 (no items which means no value)

If the item being evaluated can be added to the knapsack (constraints not invalid)

set the Value array(n, curWeight, curVolume) => max(of adding this value or the max of not adding this value)

otherwise set the value array to the previous state.

Decide to include or not according to the value and weight (and volume) of the items. 

After the items have been evaluated, the value of ValueArray[ItemCount,MaximumWeight, (optional MaximumVolume)] shows the maximum value we can get into the knapsack.

W => The maximum Weight

V => The maximum Value

O(N * W * V) == The processing time in Big O

### Constraints

Weight and Volume have to be positive Integers (so decimal values may need to be scaled to bring them into range). 

N * W * V == the amount of memory is required to utilize this technique.

# Sample Processing Times

## Scaling Weight and Value Impact on Processing Time

When scaling the potential values to get a more accurate result there is a significant impact on which technique works best.


### Scale 1/100

N => 20 (potential items)

MaxWeight => 40 Lbs => 4000 (to scale from 30.05 lbs to 3005 (scale == 1/100))

MaxVolume => 30 Liters => 3000 

No Bound on Maximum number of items

Brute Force Combinations: 1,048,576

Dynamic: 240,000,000 (plus an array of 240 million ints in memory)

BEST: Brute force combination

### Scale 1/10

N => 20 (potential items)

MaxWeight => 40 Lbs => 400 (to scale from 30.5 lbs to 305 ((LIMIT scale == 1/10))

MaxVolume => 30 Liters => 300  

No Bound on Maximum number of items

Brute Force Combinations: 1,048,576 (Same no changes)

Dynamic: 2,400,000 (plus an array of 2.4 million ints in memory)

BEST: Brute force combination 

### Scale 1

N => 20 (potential items)

MaxWeight => 40 Lbs => 40 (no scaling)

MaxVolume => 30 Liters => 30  

No Bound on Maximum number of items

Brute Force Combinations: 1,048,576 (Same no changes)

Dynamic: 24,000 (plus an array of 24 thousand ints in memory)

BEST: Dynamic 

## Varying the Number of Values:

### N => 20

Using MaxWeight => 400, MaxVolume => 300, No bound on number of items

Brute Force: 1,048,576 

Dynamic: 2,400,000

### N => 100

Using MaxWeight => 400, MaxVolume => 300, No bound on number of items

Brute Force: 1,267,650,600,228,229,401,496,703,205,376

Dynamic: 12,000,000

## Algorithms that produce near optimal solutions 

## Genetic Algorithm

The Genetic Algorithm (GA) is a heuristic optimization technique inspired by the process of natural selection in biological evolution. It is particularly effective for solving complex optimization problems like the **Multidimensional Knapsack Problem (MDKP)**, where traditional exact algorithms become computationally infeasible due to the problem's NP-hard nature.

### Overview

In the context of the knapsack problem, the GA aims to find a near-optimal solution by evolving a population of candidate solutions over several generations. Each candidate solution, called a **chromosome**, represents a possible selection of items to include in the knapsack(s). The algorithm applies genetic operators such as selection, crossover, and mutation to evolve the population toward better solutions.

### Genetic Algorithm Steps

1. **Initialization**:
   - Generate an initial population of chromosomes randomly. Each chromosome is a binary string or list where each gene represents whether an item is included (`1`) or not (`0`).

2. **Fitness Evaluation**:
   - Calculate the fitness of each chromosome based on a fitness function. In the knapsack problem, the fitness function typically evaluates the total value of the included items while penalizing solutions that violate weight and volume constraints.

3. **Selection**:
   - Select parent chromosomes from the current population based on their fitness. Fitter chromosomes have a higher chance of being selected for reproduction. Common selection methods include **roulette wheel selection**, **tournament selection**, and **rank selection**.

4. **Crossover (Recombination)**:
   - Create offspring by combining parts of two parent chromosomes. Common crossover methods include **single-point crossover**, **two-point crossover**, and **uniform crossover**. The crossover operator allows the algorithm to explore new regions of the solution space.

5. **Mutation**:
   - Introduce random changes to individual genes in a chromosome with a small probability. Mutation helps maintain genetic diversity within the population and prevents premature convergence to local optima.

6. **Replacement**:
   - Form a new population by replacing some or all of the old population with the offspring. Strategies include **generational replacement** (replacing the entire population) or **steady-state replacement** (replacing only a few individuals).

7. **Termination**:
   - Repeat the evaluation, selection, crossover, and mutation steps for a specified number of generations or until a termination condition is met (e.g., no improvement in fitness over several generations).

### Constraints Handling

- The GA must ensure that the weight and volume constraints of the knapsack are not violated. This can be achieved by:

  - **Penalty Functions**: Penalize the fitness of chromosomes that exceed the constraints.
  - **Repair Functions**: Modify infeasible chromosomes to make them feasible by removing items until constraints are satisfied.
  - **Constraint-aware Initialization and Operators**: Design the initialization, crossover, and mutation operators to produce feasible solutions directly.

### Advantages

- **Scalability**: GAs are suitable for large problem instances where exact algorithms are impractical.
- **Flexibility**: They can easily handle additional constraints and multiple objectives.
- **Global Search Capability**: GAs perform a global search and are less likely to get trapped in local optima compared to greedy algorithms.

### Limitations

- **No Guarantee of Optimality**: GAs provide approximate solutions and do not guarantee finding the optimal solution.
- **Computational Time**: They can be computationally intensive, especially for very large populations or many generations.
- **Parameter Sensitivity**: Performance depends on the choice of parameters like population size, mutation rate, and crossover rate.

### Sample Processing Times

Processing times for GAs can vary significantly based on the problem size and parameters. Below are hypothetical examples:

- **Small Instance (N = 20 items)**
  - **Population Size**: 50
  - **Generations**: 100
  - **Processing Time**: Approximately 1-2 seconds

- **Medium Instance (N = 100 items)**
  - **Population Size**: 100
  - **Generations**: 200
  - **Processing Time**: Approximately 10-20 seconds

- **Large Instance (N = 1000 items)**
  - **Population Size**: 200
  - **Generations**: 500
  - **Processing Time**: Several minutes to hours

### Comparison with Other Algorithms

- **Brute Force**: GAs are significantly faster than brute force methods, which are computationally infeasible for large \( N \).
- **Dynamic Programming**: While dynamic programming provides exact solutions, it becomes impractical for large instances due to exponential time and space complexity.
- **FPTAS**: The Fully Polynomial-Time Approximation Scheme offers a theoretical guarantee on the approximation ratio but may still be computationally intensive for large instances with small \( \epsilon \).

### Implementation Tips

- **Parameter Tuning**: Experiment with different values for population size, mutation rate, and crossover rate to find the best performance.
- **Elitism**: Preserve a number of the best individuals from each generation to ensure that the best solutions are carried forward.
- **Diversity Maintenance**: Ensure sufficient genetic diversity to avoid premature convergence by adjusting mutation rates or using diversity-preserving selection methods.

### Conclusion

The Genetic Algorithm is a powerful heuristic for approximating solutions to the multidimensional knapsack problem, especially when dealing with large datasets where exact methods are impractical. By simulating the process of natural evolution, GAs can explore a vast search space and provide high-quality solutions within reasonable time frames.

### References

- **Books**
  - *Genetic Algorithms in Search, Optimization, and Machine Learning* by David E. Goldberg
  - *An Introduction to Genetic Algorithms* by Melanie Mitchell

- **Articles**
  - *A Genetic Algorithm for the Multidimensional Knapsack Problem* by P. C. Chu and J. E. Beasley

- **Online Resources**
  - [Genetic Algorithm - Wikipedia](https://en.wikipedia.org/wiki/Genetic_algorithm)

### Credits

- **Genetic Algorithm Implementation**: Inspired by common GA frameworks and tailored for the multidimensional knapsack problem.
- **Constraint Handling Techniques**: Based on strategies discussed in optimization literature for handling constraints in genetic algorithms.




# Summary 

Finding the optimal solution to this problem is considered NP-Hard. 
Except where the problem is trivial it is very time and resource intensive. 
Therefore in non trivial problems it is valuable to consider approximation algorithms that attempt to determine an approximate optimal solution with better performance heuristics. 

This wiki entry is very helpful on the subject.
  - [Knapsack Problem - Wikipedia](https://en.wikipedia.org/wiki/Knapsack_problem)


Credits: 

Brute Force Permutations: https://www.geeksforgeeks.org/write-a-c-program-to-print-all-permutations-of-a-given-string/

Dynamic Programming Bottom Up (single dimension): https://github.com/ayzahmt/Knapsack-Problem

Dynamic Programming Bottom Up vs Top Down (Tabulation vs Memoization) https://www.geeksforgeeks.org/tabulation-vs-memoization/






