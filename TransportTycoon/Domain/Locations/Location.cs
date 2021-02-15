using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportTycoon
{
    // Base for all locations
    public abstract class Location
    {
        public string Name { get; set; }
        // protected List<Container> Containers;

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
        
        // protected abstract void PrepareVehicles();

        public Location(string name)
        {
            this.Name = name;
            this.ContainersInTransit = new Queue<Container>();
            this.ContainersAtDestination = new List<Container>();
        }

        public void AddRoute(string destinationName, Location nextStop, int duration)
        {
            this.routing.Add(destinationName, new Route(nextStop, duration));
        }

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

        public void PutVehicle(Vehicle vehicle)
        {
            this.Vehicles.Add(vehicle);

            vehicle.From = this;
            vehicle.To = null;
        }

        protected Route RouteTo(string destinationName)
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
                var route = RouteTo(container.DestinationName);
                var vehicle = PopVehicleTo(route);
                
                if(vehicle is null) return; // FiFo mode: the head cannot be handled

                Console.WriteLine($"{Name} loading container for {container.DestinationName} onto {vehicle.Name}, next destination: {route.Destination.Name}, it will take {route.Duration} hours");
                vehicle.Container = container;
                vehicle.Depart(this, route.Destination, route.Duration);
                this.ContainersInTransit.Dequeue();
            }
        }

        public void SendUselessVehiclesHome()
        {
            var uselessVehicles = Vehicles.Where(vehicle => !vehicle.IsHome && !vehicle.Hascontainer);
            foreach(var vehicle in uselessVehicles)
            {
                var route = RouteTo(vehicle.HomeName);
                vehicle.Depart(this, route.Destination, route.Duration);
                Console.WriteLine($"{vehicle.Name} going back home ({vehicle.HomeName}), next destination: {route.Destination.Name}, it will take {route.Duration} hours");
            }
        }
       

        public bool IsDestinationForContainer(Container container)
        {
            return container.DestinationName == this.Name;
        }

        // public bool HasContainersInTransit => Containers.Any(container => !IsDestinationForContainer(container));

        // can be extended to see if the route is practicable by the vehicle
        private Vehicle? PopVehicleTo(Route route)
        {
            if(HasVehicles)
            {
                var vehicle = Vehicles.ElementAt(0);
                Vehicles.RemoveAt(0);
                return vehicle;
            }
            return null;
        }

    }
}
