using System;
using System.Collections.Generic;
using System.Text;

namespace CheeseSystem
{
    public class MilkProductionLine : IProductionLine
    {
        public Production Produce()
        {
            return new Milk();
        }
    }
}
