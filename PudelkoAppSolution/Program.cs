using PudelkoSolution;
using System;
using System.Collections.Generic;

namespace PudelkoAppSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pudelko> lists = new List<Pudelko>();
            lists.Add(new Pudelko(10, 20, 30, UnitOfMeasure.centimeter));
            lists.Add(new Pudelko());
            lists.Add(new Pudelko(5));

            Console.WriteLine("Lista przed sortowaniem:");
            foreach (Pudelko p in lists)
            {
                Console.WriteLine(p);
                Console.WriteLine(p.Capacity + " => Objętość");
                Console.WriteLine(p.Surface + " => Pole powierzchni całkowitej");
            }

            lists.Sort();
            Console.WriteLine("Lista po sortowaniu:");
            foreach (Pudelko p in lists)
            {
                Console.WriteLine(p);
                Console.WriteLine(p.Capacity + " => Objętość");
                Console.WriteLine(p.Surface + " => Pole powierzchni całkowitej");

            }


            Pudelko p1 = new Pudelko(25, 25, 25, UnitOfMeasure.centimeter);
            Console.WriteLine("Przeglądanie długosci boków za pomoca petli:");
            foreach (var x in p1)
            {
                Console.WriteLine(x);
            }


        }
    }
}
