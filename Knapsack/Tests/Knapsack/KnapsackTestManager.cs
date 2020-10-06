using System;
using System.Collections.Generic;
using System.Text;
using Knapsack.Models;

namespace Knapsack.Tests
{
    public class KnapsackTestManager : ITestManager
    {
        public List<KSItem> ItemList { get; set; }

        public int MaxWeight { get; set; }
        public int? MaxVolume { get; set; } = null; // null == ignore the volume dimension

        public TimeSpan RunTest(MKPTest test)
        {
            test.Run(this);
            return test.TestTime;
        }

    }
}
