using System;

namespace TransportTycoon
{
    // Base for vehicles
    public class RouteNotFoundException: Exception
    {
        public RouteNotFoundException(string message): base(message)
        {
            
        }
    }
}