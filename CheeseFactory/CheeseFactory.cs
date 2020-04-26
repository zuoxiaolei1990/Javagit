using System;
using System.Collections.Generic;
using System.Text;

namespace CheeseSystem
{
    
    public class CheeseFactory
    {
        
        public int cheeseCount { get; private set; }
        private bool isStartProduceCheese = true;
        public static int MaxCountOfCheeseFactory = 100000;

        private CheeseProductionLine cheeseLine = new CheeseProductionLine();
        private FermentProductionLine fermentLine = new FermentProductionLine();
        private MilkProductionLine milkLine = new MilkProductionLine();

        private List<Milk> milks = new List<Milk>();
        private List<Ferment> ferments = new List<Ferment>();
        private ColdStorage coldStorage;

        public CheeseFactory()
        {
            coldStorage = ColdStorage.GetInstance();
            coldStorage.AddStartProduceEvent(ChangeProduceState);
        }

        public void ProduceProtuction()
        {
            if (isStartProduceCheese)
            {
                ProduceMilk();
                ProduceFerment();
                ProduceCheese();
            }
        }

        private void ChangeProduceState(bool isStart)
        {
            isStartProduceCheese = isStart;
        }

        private void ProduceCheese()
        {
            if (cheeseCount < MaxCountOfCheeseFactory && milks.Count >= 2 && ferments.Count >= 1)
            {
                milks.RemoveRange(0, 2);
                ferments.RemoveRange(0, 1);
                Cheese cheese = (Cheese)cheeseLine.Produce();
                coldStorage.AddCheese(cheese);
                cheeseCount++;
            }
        }

        private void ProduceMilk()
        {
            if (milks.Count < MaxCountOfCheeseFactory * 2)
            {
                Milk milk = (Milk)milkLine.Produce();
                milks.Add(milk);
                milks.Add(milk);
            }            
        }

        private void ProduceFerment()
        {
            if (ferments.Count < MaxCountOfCheeseFactory)
            {
                Ferment ferment = (Ferment)fermentLine.Produce();
                ferments.Add(ferment);
            }            
        }

    }
}
