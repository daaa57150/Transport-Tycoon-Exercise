using System;

namespace TransportTycoon
{
    public class Route
    {
        public Location UnloadingPoint { get; private set; }
        public TimeSpan TimeInHours { get; private set; }
        public Location LoadingPoint { get; private set; }

        public Route(Location loadingPoint, Location unloadingPoint, TimeSpan timeInHours)
        {
            this.LoadingPoint = loadingPoint;
            this.UnloadingPoint = unloadingPoint;
            this.TimeInHours = timeInHours;
        }
    }
}
