using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CheeseSystem
{
    public class ColdStorage
    {
        public delegate void StartProduceHandler(bool isStartProduce);
        private event StartProduceHandler StartProduceEvent;

        public delegate void StartTransportHandler();
        private event StartTransportHandler TransportEvent;

        private static int MaxCountofStorge = 1000;
        private List<Cheese> cheeses = new List<Cheese>();

        private volatile static ColdStorage ColdStorageRepository;

        private ColdStorage()
        {

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ColdStorage GetInstance()
        {
            if (ColdStorageRepository == null)
            {
                ColdStorageRepository = new ColdStorage();
            }
            return ColdStorageRepository;
        }

        public void AddTransportEvent(StartTransportHandler method)
        {
            TransportEvent += method;
        }

        public void AddStartProduceEvent(StartProduceHandler method)
        {
            StartProduceEvent += method;
        }

        public void RemoveStartProduceEvent(StartProduceHandler method)
        {
            StartProduceEvent -= method;
        }

        public void RemoveTransportEvent(StartTransportHandler method)
        {
            TransportEvent -= method;
        }

        public void AddCheese(Cheese cheese)
        {
            lock(cheeses)
            {
                if (cheeses.Count < MaxCountofStorge)
                {
                    cheeses.Add(cheese);
                    if (cheeses.Count == MaxCountofStorge)
                    {
                        BroadcastStartProduceEvents(false);
                    }
                    if (cheeses.Count < MaxCountofStorge)
                    {
                        BroadcastStartProduceEvents(true);
                    }
                    
                }
                else
                {
                    BroadcastStartProduceEvents(false);
                }

                if (cheeses.Count >= Truck.MaxCountOfTruck)
                {
                    BroadcastTransportEvents();
                }

            }
        }

        public void TranportCheeses()
        {
            lock (cheeses)
            {
                if (cheeses.Count >= Truck.MaxCountOfTruck)
                {
                    cheeses.RemoveRange(0, Truck.MaxCountOfTruck);
                }

            }
        }

        private void BroadcastTransportEvents()
        {
            if (TransportEvent != null)
            {
                TransportEvent();
            }
        }

        private void BroadcastStartProduceEvents(bool state)
        {
            if (StartProduceEvent != null)
            {
                StartProduceEvent(state);
            }
        }        

    }
}
