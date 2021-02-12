namespace TransportTycoon
{
    public class Vehicule
    {
        public Container? container { get; set; }
        public Location destination { get; set; }
        private int timeBeforeArrival { get; set; }
    }
}
