using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportTycoon
{
    public class World
    {
        private Location factory;
        private Location warehouseA;
        private Location warehouseB;
        private Location port;

        private int ContainerToDeliever;
        public World(IEnumerable<string> containerDestinations)
        {
            ContainerToDeliever = containerDestinations.Count();
            factory = new Location("Factory");
            warehouseA = new Location("A");
            warehouseB = new Location("B");
            port = new Location("Port");

            var factoryToWarehouseB = new Route(factory, warehouseB, 5);
            var factoryToPort = new Route(factory, port, 1);
            var portToWarehouseA = new Route(port, warehouseA, 4);

            var truck = new Vehicle();
            var truck2 = new Vehicle();
            var boat = new Vehicle();

            factory.PutContainer(new Container(warehouseA));



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
        public bool DelieveryIsDone()
        {

            return warehouseA.ContainerCount + warehouseB.ContainerCount == ;
        }
        public void Deliver()
        {
            this.Print();
            while (true)
            {

            }
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
