namespace TransportTycoon
{
    public class Boat: Vehicle
    {
        public Boat(string name, string homeName): base(name, homeName) { }

        public override bool CanUseRoute(Route route) => route.Type == RouteType.Sea;
    }
}
