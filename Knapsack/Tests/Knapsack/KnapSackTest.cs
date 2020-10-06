using System;
using System.Collections.Generic;
using System.Text;
using Knapsack.Models;

namespace Knapsack.Tests
{
    public abstract class KnapSackTest : MKPTest
    {
        private KnapsackTestManager _tm = null;
        public KnapsackTestManager TM { get { return _tm; }
            protected set {
                _tm = value;
                if (OptimalSolution == null)
                    OptimalSolution = new KnapsackSolution(_tm);
            }  }

        public long SolutionCount { get; protected set; } = 0;
        
        //Defualt to empty solution
        public KnapsackSolution OptimalSolution { get; set; } = null;

        protected override void SetTestManager(ITestManager tm)
        {
            if (tm is KnapsackTestManager)
                TM = (KnapsackTestManager)tm;
            else
                TM = null;            
        }
      
        //protected virtual void PreTestSetup() { }
        //protected virtual void PostTestCleanup() { }

        protected override bool ValidateTestInputs()
        {
            if (TM.ItemList.Count == 0)
                return false; 

            return true;
        }

    }
}
