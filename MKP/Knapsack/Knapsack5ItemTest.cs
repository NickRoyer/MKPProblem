using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Knapsack.Models;
using Knapsack.Tests;

namespace MKP_Test.Knapsack
{
    [Collection("Knapsack Test Collection")]
    public class Knapsack5ItemTest : KnapsackTests
    {
        private KnapsackTestData TestData { get; set; } 

        public Knapsack5ItemTest()
        {
            TestData = new KnapsackTestData();
            TestData.LoadTestData("Knapsack/5ItemTestFile.xml");
        }

        [Fact]
        public void ValidateTestManagerFixtureInstansiated()
        {
            Assert.NotNull(TestData);
        }

        [Fact]
        public void ValidateTestManagerInstansiated()
        {
            Assert.NotNull(CreateTestManager(TestData.KSItemList,10, 0));
        }

        [Fact]
        public void ValidateTestManagerItemCount()
        {
            Assert.Equal(5, CreateTestManager(TestData.KSItemList,5,0).ItemList.Count);
        }

        [Fact]
        public void ValidateTestManagerItemAttributes()
        {
            //<Item Id="1" Value="1" Weight="2" Volume="5"></Item>
            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, 5, 100, 100);

            KSItem item = tm.ItemList.Find(i => i.Id == 1);
            Assert.Equal(1, item.Value);
            Assert.Equal(2, item.Weight);
            Assert.Equal(5, item.Volume);
        }

        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        [InlineData(TestType.BruteForcePermutations)]
        [InlineData(TestType.DynamicProgramming)]
        [InlineData(TestType.GeneticAlgorithm)]
        public void Run5ItemTestZeroWeightDynamic(TestType type)
        {
            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, 5, 0, 0);
            KnapSackTest test = (KnapSackTest)CreateTest(type);

            TimeSpan t = tm.RunTest(test);

            Assert.Equal(0, test.OptimalSolution.Result.Value);
            Assert.Equal(0, test.OptimalSolution.Result.Weight);
            Assert.Equal(0, test.OptimalSolution.Result.Volume);
            Assert.Empty(test.OptimalSolution.Solution);
        }

        [Theory]
        //[InlineData(TestType.BruteForceCombinations)]
        //[InlineData(TestType.BruteForcePermutations)]
        //[InlineData(TestType.DynamicProgramming)]
        [InlineData(TestType.GeneticAlgorithm)]
        public void Run5ItemTestLargeWeightCombinations(TestType type)
        {
            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, 5, 500);
            KnapSackTest test = (KnapSackTest)CreateTest(type);

            TimeSpan t = tm.RunTest(test);

            Assert.Equal(16, test.OptimalSolution.Result.Value);
            Assert.Equal(18, test.OptimalSolution.Result.Weight);
            Assert.Equal(17, test.OptimalSolution.Result.Volume);
        }

        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        [InlineData(TestType.BruteForcePermutations)]
        [InlineData(TestType.DynamicProgramming)]
        [InlineData(TestType.GeneticAlgorithm)]
        public void Run5ItemTestMidWeightDynamic(TestType type)
        {

            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, 5, 10);
            KnapSackTest test = (KnapSackTest)CreateTest(type);

            TimeSpan t = tm.RunTest(test);

            Assert.Equal(12, test.OptimalSolution.Result.Value);
            Assert.Equal(9, test.OptimalSolution.Result.Weight);
            Assert.Equal(9, test.OptimalSolution.Result.Volume);
            Assert.Equal(3, test.OptimalSolution.Solution.Count);
        }

    }
}
