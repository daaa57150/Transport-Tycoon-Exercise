using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportTycoon
{
    // Base for all special locations
    public class Location
    {
        public string Name { get; set; }

        // TODO: some locations are FiFo (Factory), others are Random access (Port)
        protected Queue<Container> ContainersInTransit; 
        public int ContainersInTransitCount => ContainersInTransit.Count;
        public bool HasContainersInTransit => this.ContainersInTransit.Count > 0;


        protected List<Container> ContainersAtDestination;


        public bool HasVehicles => this.Vehicles.Count > 0;


        // TODO: should be its own class
        // destination name <=> next location
        protected Dictionary<string, Route> routing = new Dictionary<string, Route>();

        protected List<Vehicle> Vehicles = new List<Vehicle>();
        

        public Location(string name)
        {
            this.Name = name;
            this.ContainersInTransit = new Queue<Container>();
            this.ContainersAtDestination = new List<Container>();
        }

        // used during world configuration
        public void AddRoute(string destinationName, Location nextStop, int duration, RouteType type)
        {
            this.routing.Add(destinationName, new Route(nextStop, duration, type));
        }

        // "Unload" ?
        public void PutContainer(Container container)
        {
            if(container.DestinationName == this.Name) 
            {
                this.ContainersAtDestination.Add(container);
            }
            else 
            {
                this.ContainersInTransit.Enqueue(container);
            }
        }

        // "Park" ?
        public void PutVehicle(Vehicle vehicle)
        {
            this.Vehicles.Add(vehicle);

            vehicle.From = this;
            vehicle.To = null;
        }

        private Route FindRouteTo(string destinationName)
        {
            if(!this.routing.ContainsKey(destinationName)) 
            {
                throw new RouteNotFoundException("Did not find a route to " + destinationName + " in " + this.Name);
            }
            return this.routing[destinationName];
        }

        public void LoadContainersOnVehicles() // FiFo
        {
            while(HasContainersInTransit) // not a terminating case, we are not sure all containers can be handled 
            {
                var container = this.ContainersInTransit.First();
                var route = FindRouteTo(container.DestinationName);
                var vehicle = FindVehicleTo(route);
                
                if(vehicle is null) return; // FiFo mode: the head cannot be handled

                Console.WriteLine($"{Name} loading container for {container.DestinationName} onto {vehicle.Name}, next destination: {route.Destination.Name}, it will take {route.Duration} hours");
                LoadFirstContainerOnVehicle(vehicle);
                Depart(vehicle, route.Destination, route.Duration);
            }
        }

        // TODO: if not fifo, should be able to load any container
        private void LoadFirstContainerOnVehicle(Vehicle vehicle)
        {
            var container = this.ContainersInTransit.Dequeue();
            vehicle.Container = container;
        }

        public void SendUselessVehiclesHome()
        {
            var uselessVehicles = Vehicles
                .Where(vehicle => !vehicle.IsHome && !vehicle.Hascontainer)
                .ToList(); // needed to make a copy and not reference a subset
            foreach(var vehicle in uselessVehicles)
            {
                var route = FindRouteTo(vehicle.HomeName);
                Depart(vehicle, route.Destination, route.Duration);
                Console.WriteLine($"{vehicle.Name} going back home ({vehicle.HomeName}), next destination: {route.Destination.Name}, it will take {route.Duration} hours");
            }
        }

        private void Depart(Vehicle vehicle, Location destination, int duration)
        {
            vehicle.Depart(this, destination, duration);
            Vehicles.Remove(vehicle);
        }

        public bool IsDestinationForContainer(Container container)
            => container.DestinationName == this.Name;

        private Vehicle? FindVehicleTo(Route route)
            => Vehicles?.FirstOrDefault((vehicle => vehicle.CanUseRoute(route)));

    }
}
