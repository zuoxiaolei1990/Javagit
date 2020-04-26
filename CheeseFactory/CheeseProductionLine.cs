using System;
using System.Collections.Generic;
using System.Text;

namespace CheeseSystem
{
    public class CheeseProductionLine : IProductionLine
    {
        public Production Produce()
        {
            return new Cheese();
        }
    }
}
