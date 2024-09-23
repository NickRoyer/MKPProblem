using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knapsack.Tests;


namespace Knapsack.Models
{
    public class KnapsackSolution
    {
        public List<KSItem> Solution { get; set; } = new List<KSItem>();

        public List<KSItem> OriginalItemList { get; set; } = new List<KSItem>();

        public KSItem Result { get; set; } = new KSItem();

        public int MaxWeight { get; set; }
        public int? MaxVolume { get; set; }

        public KnapsackSolution(int maxWeight, int? maxVolume, List<KSItem> itemlist) {
            MaxWeight = maxWeight; 
            MaxVolume = maxVolume;
            OriginalItemList = itemlist;
        }

        public KnapsackSolution(KnapsackTestManager testManager) {  
            MaxWeight = testManager.MaxWeight; 
            MaxVolume = testManager.MaxVolume;
            OriginalItemList = testManager.ItemList.ToList();
       }

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
                if (testWeight <= MaxWeight &&
                   (MaxVolume == null || (testVolume <= MaxVolume)))
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
