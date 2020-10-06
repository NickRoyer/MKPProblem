using System;
using System.Collections.Generic;
using System.Text;

namespace Knapsack.Tests
{
    public interface ITestManager
    {
        TimeSpan RunTest(MKPTest test);
    }
}
