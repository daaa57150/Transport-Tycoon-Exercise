using System;

namespace TransportTycoon
{
    public class Route
    {
        public Location Destination { get; private set; }
        public int Duration { get; private set; }

        public Route(Location destination, int duration)
        {
            this.Destination = destination;
            this.Duration = duration;
        }
    }
}
