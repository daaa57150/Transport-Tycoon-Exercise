using System;
using System.Collections.Generic;

namespace TransportTycoon
{
    public class Location
    {
        public string Name { get; set; }

        private List<Container> containers;

        public Location(string name)
        {
            this.Name = name;
            this.containers = new List<Container>();
        }

        public Container? LoadContainer()
        {
            if (this.containers.Count == 0)
            {
                return null;
            }
        }

        public void UnloadContainer(Container container)
        {

        }
    }

    public class Container
    {
        private readonly Location destination;

        public Container(Location destination)
        {
            this.destination = destination;
        }
    }

    public class Route
    {
        public Location UnloadingPoint { get; private set; }
        public TimeSpan TimeInHours { get; private set; }
        public Location LoadingPoint { get; private set; }

        public Route(Location loadingPoint, Location unloadingPoint, TimeSpan timeInHours)
        {
            this.LoadingPoint = loadingPoint;
            this.UnloadingPoint = unloadingPoint;
            this.TimeInHours= timeInHours;
        }
    }
}
