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
        private int containerToDeliever;
        private Vehicle _truck1;
        private Vehicle _truck2;
        private Vehicle _boat;

        public World(IEnumerable<string> containerDestinations)
        {
            containerToDeliever = containerDestinations.Count();
            factory = new Location("Factory");
            warehouseA = new Location("A");
            warehouseB = new Location("B");
            port = new Location("Port");

            var factoryToWarehouseB = new Route(factory, warehouseB, 5);
            var factoryToPort = new Route(factory, port, 1);
            var portToWarehouseA = new Route(port, warehouseA, 4);

            _truck1 = new Vehicle();
            _truck2 = new Vehicle();
            _boat = new Vehicle();

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

        public int CurrentTime { get; set; } = 0;
        public bool DelieveryIsDone()
        {

            return warehouseA.ContainerCount + warehouseB.ContainerCount == containerToDeliever ;
        }
        public void Deliver()
        {
            this.Print();
            while (!DelieveryIsDone())
            {
                factory.LoadOnVehicle();


                CurrentTime++;
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
