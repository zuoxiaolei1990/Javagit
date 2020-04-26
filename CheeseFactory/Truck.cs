using System;
using System.Collections.Generic;
using System.Text;

namespace CheeseSystem
{
    public class Truck
    {
        private ColdStorage coldStorage;
        public static int MaxCountOfTruck = 100;

        public Truck()
        {
            coldStorage = ColdStorage.GetInstance();
        }

        public void TransportCheese()
        {
            coldStorage.TranportCheeses();
        }
    }
}
