using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportTycoon
{
    // Base for all locations
    public abstract class Location
    {
        public string Name { get; set; }
        protected List<Container> Containers;
        public int ContainerCount => Containers.Count;
        public bool HasContainers => this.Containers.Count > 0;
        public bool HasVehicles => this.Vehicles.Count > 0;


        // TODO: should be its own class
        // destination name <=> next location
        protected Dictionary<string, Route> routing = new Dictionary<string, Route>();

        protected List<Vehicle> Vehicles = new List<Vehicle>();
        
        // protected abstract void PrepareVehicles();

        public Location(string name)
        {
            this.Name = name;
            this.Containers = new List<Container>();
        }

        public void AddRoute(string destinationName, Location nextStop, int duration)
        {
            this.routing.Add(destinationName, new Route(nextStop, duration));
        }

        public void PutContainer(Container container)
        {
            Containers.Add(container);
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

        public void LoadContainersOnVehicles()
        {
            // reverse, as we may remove while iterating
            for (int i = Containers.Count - 1; i >= 0; i--)
            {
                var container = Containers[i];
                
                if(IsDestinationForContainer(container)) continue;

                var route = RouteTo(container.DestinationName);
                var vehicle = PopVehicleTo(route);
                if(vehicle != null)
                {
                    Console.WriteLine($"{Name} loading container for {container.DestinationName} onto {vehicle.Name}, next destination: {route.Destination.Name}, it will take {route.Duration} hours");
                    vehicle.Container = container;
                    vehicle.Depart(this, route.Destination, route.Duration);
                    this.Containers.RemoveAt(i);
                }
            }
        }

        public bool IsDestinationForContainer(Container container)
        {
            return container.DestinationName == this.Name;
        }

        public bool HasContainersInTransit => Containers.Any(container => !IsDestinationForContainer(container));

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
