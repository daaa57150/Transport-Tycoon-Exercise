namespace TransportTycoon
{
    public class Vehicle
    {
        public Container? Container { get; set; }
        public Location Destination { get; set; }
        public int TimeBeforeArrival { get;  set; }
    }
}
