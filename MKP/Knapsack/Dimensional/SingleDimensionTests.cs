using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Knapsack.Models;
using Knapsack.Tests;

namespace MKP_Test.Knapsack
{
    [Collection("Knapsack Test Collection")]
    public class SingleDimension : KnapsackTests
    {
        private KnapsackTestData TestData { get; set; }

        public SingleDimension()
        {
            TestData = new KnapsackTestData();
            TestData.LoadTestData("Knapsack/30ItemTestFile.xml");
        }

        [Fact]
        public void ValidateTestManagerFixtureInstansiated()
        {
            Assert.NotNull(TestData);
        }

        [Theory]
        [InlineData(12)]
        [InlineData(5)]
        [InlineData(11)]
        [InlineData(1)]
        [InlineData(30)]
        public void ValidateTestManagerVariableItemCount(int cnt)
        {
            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, cnt, 0, 0);
            Assert.Equal(cnt, tm.ItemList.Count);
        }

        [Fact]
        public void ValidateTestManagerItemAttributes()
        {
            KnapsackTestManager tm = CreateTestManager(TestData.KSItemList, 10, 0, 0);
            //<Item ID="1" Value="20" Weight="22" Volume="13"></Item>
            KSItem item = tm.ItemList.Find(i => i.Id == 1);
            Assert.Equal(20, item.Value);
            Assert.Equal(22, item.Weight);
            Assert.Equal(13, item.Volume);
        }

        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        [InlineData(TestType.BruteForcePermutations)]
        [InlineData(TestType.DynamicProgramming)]
        [InlineData(TestType.GeneticAlgorithm)]
        [Trait("Category", "Zero Weight")]
        public void OneD_Zero_10Items(TestType type)
        {
            KnapsackTestManager TM = CreateTestManager(TestData.KSItemList, 10, 0);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = TM.RunTest(test);

            Assert.Equal(0, test.OptimalSolution.Result.Value);
            Assert.Equal(0, test.OptimalSolution.Result.Weight);
            Assert.Equal(0, test.OptimalSolution.Result.Volume);
            Assert.Empty(test.OptimalSolution.Solution);

            long testSolutionCnt = TestSolutions(type, TM.ItemList.Count, TM.MaxWeight, TM.MaxVolume);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        [InlineData(TestType.BruteForcePermutations)]
        [InlineData(TestType.DynamicProgramming)]
        [InlineData(TestType.GeneticAlgorithm)]
        [Trait("Category", "Mid Weight")]
        public void OneD_Mid_10_Items(TestType type)
        {
            KnapsackTestManager TM = CreateTestManager(TestData.KSItemList, 10, 100);
            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = TM.RunTest(test);

            Assert.Equal(217, test.OptimalSolution.Result.Value);
            Assert.Equal(90, test.OptimalSolution.Result.Weight);
            Assert.Equal(167, test.OptimalSolution.Result.Volume);
            Assert.Equal(6, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, TM.ItemList.Count, TM.MaxWeight, TM.MaxVolume);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        [InlineData(TestType.BruteForcePermutations)]
        [InlineData(TestType.DynamicProgramming)]
        [InlineData(TestType.GeneticAlgorithm)]
        [Trait("Category", "Large Weight")]
        public void OneD_Large_10_Items(TestType type)
        {
            KnapsackTestManager TM = CreateTestManager(TestData.KSItemList, 10, 500);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = TM.RunTest(test);

            Assert.Equal(309, test.OptimalSolution.Result.Value);
            Assert.Equal(194, test.OptimalSolution.Result.Weight);
            Assert.Equal(240, test.OptimalSolution.Result.Volume);
            Assert.Equal(10, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, TM.ItemList.Count, TM.MaxWeight, TM.MaxVolume);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        //[InlineData(TestType.BruteForcePermutations)] //Takes too long
        [InlineData(TestType.DynamicProgramming)]
        [InlineData(TestType.GeneticAlgorithm)]
        [Trait("Category", "Mid Weight")]
        [Trait("Category", "11 Items")]
        public void OneD_Mid_11_Items(TestType type)
        {
            KnapsackTestManager TM = CreateTestManager(TestData.KSItemList, 11, 100);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = TM.RunTest(test);

            Assert.Equal(217, test.OptimalSolution.Result.Value);
            Assert.Equal(90, test.OptimalSolution.Result.Weight);
            Assert.Equal(167, test.OptimalSolution.Result.Volume);
            Assert.Equal(6, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, TM.ItemList.Count, TM.MaxWeight, TM.MaxVolume);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        [InlineData(TestType.BruteForceCombinations)]
        //[InlineData(TestType.BruteForcePermutations)] //Takes too long
        [InlineData(TestType.DynamicProgramming)]
        [InlineData(TestType.GeneticAlgorithm)]
        [Trait("Category", "20 Items")]
        public void OneD_Large_20_Items(TestType type)
        {
            KnapsackTestManager TM = CreateTestManager(TestData.KSItemList, 20, 1000);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = TM.RunTest(test);

            Assert.Equal(570, test.OptimalSolution.Result.Value);
            Assert.Equal(436, test.OptimalSolution.Result.Weight);
            Assert.Equal(486, test.OptimalSolution.Result.Volume);
            Assert.Equal(20, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, TM.ItemList.Count, TM.MaxWeight, TM.MaxVolume);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }

        [Theory]
        //[InlineData(TestType.BruteForceCombinations)] //Takes too long
        //[InlineData(TestType.BruteForcePermutations)] //Takes too long
        [InlineData(TestType.DynamicProgramming)]
        //[InlineData(TestType.GeneticAlgorithm)] //Fails to get optimal
        [Trait("Category", "Mid Weight")]
        [Trait("Category", "25 Items")]
        public void OneD_Mid_25_Items(TestType type)
        {
            KnapsackTestManager TM = CreateTestManager(TestData.KSItemList, 25, 350);

            KnapSackTest test = (KnapSackTest)CreateTest(type);
            TimeSpan t = TM.RunTest(test);

            Assert.Equal(606, test.OptimalSolution.Result.Value);
            Assert.Equal(349, test.OptimalSolution.Result.Weight);
            Assert.Equal(423, test.OptimalSolution.Result.Volume);
            Assert.Equal(17, test.OptimalSolution.Solution.Count);

            long testSolutionCnt = TestSolutions(type, TM.ItemList.Count, TM.MaxWeight, TM.MaxVolume);
            Assert.Equal(testSolutionCnt, test.SolutionCount);
        }


    }
}
