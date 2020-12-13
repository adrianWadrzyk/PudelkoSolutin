using PudelkoSolution;
using System;
using System.Collections.Generic;
using System.Text;

namespace PudelkoAppSolution
{
    public static class Kompresja
    {
        public static Pudelko Szczescian(this Pudelko p)
        {
            double a = Math.Pow(p.Capacity, (double)1 / 3);
            return new Pudelko(a, a, a, UnitOfMeasure.meter);
        }
    }
}
