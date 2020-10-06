using System;
using System.Collections.Generic;
using System.Text;
using Knapsack.Tests;
using System.Xml.Linq;
using Knapsack.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using System.Linq;

namespace MKP_Test.Knapsack
{
    public class KnapsackTestData
    {
        public List<KSItem> KSItemList { get; private set; }  = new List<KSItem>();

        public void LoadTestData(string fileName)
        {
            XDocument testDataDoc = XDocument.Load("./" + fileName);

            //XML Structure
            //Items -> Item
            var allElements = testDataDoc.Elements();
            KSItemList.Clear();

            foreach (var itemElement in allElements.Elements())
            {               
                KSItem i = new KSItem
                {
                    Id = Convert.ToInt32(itemElement.Attribute("Id").Value),
                    Value = Convert.ToInt32(itemElement.Attribute("Value").Value),
                    Weight = Convert.ToInt32(itemElement.Attribute("Weight").Value),
                    Volume = Convert.ToInt32(itemElement.Attribute("Volume").Value)
                };

                KSItemList.Add(i);
            }
        }
    }
}
