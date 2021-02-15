using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportTycoon
{
    public class World
    {
        private IEnumerable<Container> Containers; // = new List<Container>();
        private IEnumerable<Vehicle> Vehicles; //  = new List<Vehicle>() { truck1, truck2, boat };
        private IEnumerable<Location> Locations; // = new List<Location>() { factory, warehouseA, warehouseB, port };


        public int CurrentTime { get; private set; } = 0; // in hours

        public World(IEnumerable<string> containerDestinations)
        {
            var truck1 = new Truck("Truck1", "Factory");
            var truck2 = new Truck("Truck2", "Factory");
            var boat = new Boat("Boat", "Port");

            // containerToDeliever = containerDestinations.Count();
            var factory = new Factory("Factory");
            var warehouseA = new Location("A");
            var warehouseB = new Location("B");
            var port = new Location("Port");

            Containers = ToContainers(containerDestinations);
            Vehicles = new List<Vehicle>() { truck1, truck2, boat };
            Locations = new List<Location>() { factory, warehouseA, warehouseB, port} ;

            // initial situation
            factory.PutVehicle(truck1);
            factory.PutVehicle(truck2);
            factory.SetContainers(Containers);
            port.PutVehicle(boat);

            // routing configurations
            // TODO: this is a bit too cryptic
            AddRoutingTwoWays(factory, port, 1, RouteType.Road, new List<string>{ "A" }, null);
            AddRoutingTwoWays(port, warehouseA, 4, RouteType.Sea, null, new List<string>{ "Factory" });
            AddRoutingTwoWays(factory, warehouseB, 5, RouteType.Road, null, null);
        }

        public IEnumerable<Container> ToContainers(IEnumerable<string> containerDestinations)
         => containerDestinations.Select(destination => new Container(destination));


        private void AddRoutingTwoWays(
            Location from, Location to, int duration, RouteType routeType, 
            // destinations we can target using the route from `From` to `To`
            List<string>? destinationsAfterTo,  
            // destinations we can target using the route from `To` to `From`
            List<string>? destinationsBeforeFrom)
        {
            // perspective of `From`
            AddRoutingOneWay(from, to, duration, routeType, destinationsAfterTo);

            // perspective of `To`
            AddRoutingOneWay(to, from, duration, routeType, destinationsBeforeFrom);
        }

        private void AddRoutingOneWay(Location from, Location to, int duration, RouteType routeType, List<string>? destinationsAfterTo)
        {
            from.AddRoute(to.Name, to, duration, routeType);
            destinationsAfterTo?.ForEach(destination => 
            {
                from.AddRoute(destination, to, duration, routeType);
            });
        }


        public bool DelieveryIsDone()
        {
            if(Vehicles.Any(vehicle => vehicle.Hascontainer)) return false;
            if(Locations.Any(location => location.HasContainersInTransit)) return false;

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
                    // TODO: location should be responsible for unloading too
                    location.LoadContainersOnVehicles();
                    location.SendUselessVehiclesHome();
                }

                // advance time
                foreach(var vehicle in Vehicles)
                {
                    vehicle.Move(); // also drops the container in the destination
                }
                CurrentTime ++;

                // failsafe stop
                if(CurrentTime >= 1000) 
                {
                    throw new Exception("You are bad and you should feel bad.");
                }

                Console.WriteLine(Environment.NewLine);
            }

            Console.WriteLine("----- ~~~~ DONE !!!! ~~~~ -----");
        }

    }


}
