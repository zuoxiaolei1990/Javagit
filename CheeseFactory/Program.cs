using System;
using System.Threading;

namespace CheeseSystem
{
    public class Program
    {
               
        static void Main(string[] args)
        {
            //主线程，生产
            Truck truck = new Truck();
            CheeseFactory cheeseFactory = new CheeseFactory();
            ColdStorage coldStorage = ColdStorage.GetInstance();
            coldStorage.AddTransportEvent(truck.TransportCheese);

            while (cheeseFactory.cheeseCount < CheeseFactory.MaxCountOfCheeseFactory)
            {
                cheeseFactory.ProduceProtuction();
            }    

            //线程1，运输
            ThreadStart threadStart = new ThreadStart(truck.TransportCheese);
            Thread threadTransportCheese = new Thread(threadStart);
            threadTransportCheese.Start();
                        
        }
    }
}
