using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportTycoon
{
    public class World
    {
        // private Factory factory;
        // private Destination warehouseA;
        // private Destination warehouseB;
        // private Port port;

        // private Vehicle truck1;
        // private Vehicle truck2;
        // private Vehicle boat;

        private IEnumerable<Container> Containers; // = new List<Container>();
        private IEnumerable<Vehicle> Vehicles; //  = new List<Vehicle>() { truck1, truck2, boat };
        private IEnumerable<Location> Locations; // = new List<Location>() { factory, warehouseA, warehouseB, port };
        // private IEnumerable<Location> FinalDestinations; //


        public int CurrentTime { get; private set; } = 0;

        public World(IEnumerable<string> containerDestinations)
        {
            var truck1 = new Vehicle("Truck1");
            var truck2 = new Vehicle("Truck2");
            var boat = new Vehicle("Boat");

            // containerToDeliever = containerDestinations.Count();
            var factory = new Factory();
            var warehouseA = new Destination("A");
            var warehouseB = new Destination("B");
            var port = new Port();

            Containers = ToContainers(containerDestinations);
            Vehicles = new List<Vehicle>() { truck1, truck2, boat };
            Locations = new List<Location>() { factory, warehouseA, warehouseB, port} ;

            // initial situation
            factory.PutVehicle(truck1);
            factory.PutVehicle(truck2);
            factory.SetContainers(Containers);
            port.PutVehicle(boat);

            // routing configurations
            factory.AddRoute("A", port, 1);
            port.AddRoute("Factory", factory, 1);
            port.AddRoute("A", warehouseA, 4);
            warehouseA.AddRoute("Factory", port, 4);

            factory.AddRoute("B", warehouseB, 5);
            warehouseB.AddRoute("Factory", factory, 5);


            // factory.PutContainer(new Container(warehouseA));



            // 1 Factory
            // 1 Port
            // 2 Warehouses

            // 2 Trucks -> factory
            // 1 Ship -> port

            // factory -> port, 1h
            // port -> warehouse A , 4h
            // factory -> warehouse B, 5h
        }

        public IEnumerable<Container> ToContainers(IEnumerable<string> containerDestinations)
         => containerDestinations.Select(destination => new Container(destination));



        public bool DelieveryIsDone()
        {
            if(Vehicles.Any(vehicle => vehicle.Hascontainer)) return false;
            if(Locations.Any(location => location.HasContainersInTransit)) return false;
            
            if(CurrentTime >= 100) 
            {
                throw new Exception("You are bad and you should feel bad.");
            }

            return true;
        }


        public void Deliver()
        {
            

            while (!DelieveryIsDone())
            {

                Console.WriteLine($"----- T: {this.CurrentTime} -----");

                // load all containers on vehicles and depart
                foreach(var location in Locations)
                {
                    location.LoadContainersOnVehicles();
                }

                // all unused trucks go back to factory

                // all unused boats go back to port


                // advance time
                foreach(var vehicle in Vehicles)
                {
                    vehicle.Move(); // also drops the container in the destination
                }
                CurrentTime ++;
                Console.WriteLine(Environment.NewLine);
            }

            Console.WriteLine("----- ~~~~ DONE !!!! ~~~~ -----");
        }

    }


}
