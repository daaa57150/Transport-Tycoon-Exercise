namespace TransportTycoon
{
    public class Vehicle
    {
        public Container? Container { get; set; }
        public Location Destination { get; set; }
        private int TimeBeforeArrival { get; set; }
    }
}
