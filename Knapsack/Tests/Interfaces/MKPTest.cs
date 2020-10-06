using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Knapsack.Models;

namespace Knapsack.Tests
{
    public abstract class MKPTest
    {
        protected Stopwatch SW { get; set; } = new Stopwatch();

        public TimeSpan TestTime { get { return SW.Elapsed; } }

        public void Run(ITestManager tm)
        {
            if (tm == null)
                throw new ArgumentException("Test Manager is not initialized");

            SetTestManager(tm);

            SW.Reset();
            
            if (ValidateTestInputs())
            {
                PreTestSetup();

                SW.Start();
                ProcessTest();
                SW.Stop();

                PostTestCleanup();
            }
        }

        //Required: Process method
        protected abstract void ProcessTest();
        //Required: Init
        protected abstract void SetTestManager(ITestManager tm);

        //Optional Setup / Cleanup for testing
        protected virtual void PreTestSetup() { }
        protected virtual void PostTestCleanup() { }

        //Optional Validation of test inputs
        protected virtual bool ValidateTestInputs()
        {
            return true;
        }
    }
}
