using System;

namespace TransportTycoon
{
    public enum RouteType
    {
        Road, Sea
    }

    public class Route
    {
        public Location Destination { get; private set; }
        public int Duration { get; private set; }
        public RouteType Type { get; private set; }

        public Route(Location destination, int duration, RouteType type)
        {
            this.Destination = destination;
            this.Duration = duration;
            this.Type = type;
        }
    }
}
