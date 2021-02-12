using System.Collections.Generic;
using System;

// https://github.com/Softwarepark/exercises/blob/master/transport-tycoon-1.md

namespace TransportTycoon
{
    class Program
    {
        static void Main(string[] args)
        {
            // var destinations = args[0];

            // var world = new World(destinations.Select(x => x.ToString()));
            var destinations = new List<string>()
            {
                "A"
            };
            var world = new World(destinations);
            world.Deliver();
            // Console.WriteLine("done");
        }
    }
}
