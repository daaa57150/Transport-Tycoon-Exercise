using System;

namespace TransportTycoon
{
    public class Truck: Vehicle
    {
        public Truck(string name, string homeName): base(name, homeName) { }

        public override bool CanUseRoute(Route route) => route.Type == RouteType.Road;
    }
}
