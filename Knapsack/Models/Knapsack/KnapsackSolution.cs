using System;
using System.Collections.Generic;
using System.Text;
using Knapsack.Tests;


namespace Knapsack.Models
{
    public class KnapsackSolution
    {
        public List<KSItem> Solution { get; set; } = new List<KSItem>();
        public KSItem Result { get; set; } = new KSItem();

        public virtual KnapsackTestManager TM { get; private set; } = null;

        public KnapsackSolution(KnapsackTestManager tm) { TM = tm; }

        public bool TryAddItem(KSItem curItem)
        {
            if (CanAddItem(curItem))
            {                                
                AddToResults(curItem);
                return true;
            }

            return false;
        }

        private bool CanAddItem(KSItem curItem)
        {
            if (curItem != null)
            {
                int testWeight = curItem.Weight + Result.Weight;
                int testVolume = curItem.Volume + Result.Volume;

                //Verify adding the next item does NOT violate a constraint
                //then add it to the current solution
                if (testWeight <= TM.MaxWeight &&
                   (TM.MaxVolume == null || (testVolume <= TM.MaxVolume)))
                    return true;
            }

            return false;
        }

        private void AddToResults(KSItem curItem)
        {
            if (curItem != null)
            {
                Solution.Add(curItem);
                Result.Value += curItem.Value;
                Result.Weight += curItem.Weight;
                Result.Volume += curItem.Volume;
            }
        }

    }
}
