using System;
using System.Collections.Generic;

namespace TransportTycoon
{
    public class World
    {
        public World(IEnumerable<string> containerDestinations)
        {
            // 1 Factory
            // 1 Port
            // 2 Warehouses

            // 2 Trucks -> factory
            // 1 Ship -> port

            // factory -> port, 1h
            // port -> warehouse A , 4h
            // factory -> warehouse B, 5h
        }

        public TimeSpan CurrentTime { get; private set; } = TimeSpan.Zero;

        public void Deliver()
        {
            this.Print();

            // Loop while delivery is done
        }

        private void Print()
        {
            Console.WriteLine(this.CurrentTime);

            // Display

            Console.WriteLine(Environment.NewLine);
        }
    }
}
