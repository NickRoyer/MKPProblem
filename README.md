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

# Summary 

Finding the optimal solution to this problem is considered NP-Hard. 
Except where the problem is trivial it is very time and resource intensive. 
Therefore in non trivial problems it is valuable to consider approximation algorithms that attempt to determine an approximate optimal solution with better performance heuristics. 

This wiki entry is very helpful on the subject.

https://en.wikipedia.org/wiki/Knapsack_problem

Credits: 

Brute Force Permutations: https://www.geeksforgeeks.org/write-a-c-program-to-print-all-permutations-of-a-given-string/

Dynamic Programming Bottom Up (single dimension): https://github.com/ayzahmt/Knapsack-Problem

Dynamic Programming Bottom Up vs Top Down (Tabulation vs Memoization) https://www.geeksforgeeks.org/tabulation-vs-memoization/






