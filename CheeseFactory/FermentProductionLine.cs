using System;
using System.Collections.Generic;
using System.Text;

namespace CheeseSystem
{
    public class FermentProductionLine : IProductionLine
    {
        public Production Produce()
        {
            return new Ferment();
        }

    }
}
