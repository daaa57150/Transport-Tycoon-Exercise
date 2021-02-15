using System;

namespace TransportTycoon
{
    // Base for vehicles
    public abstract class Vehicle
    {
        public string Name { get; set; }
        public string HomeName { get; set; }

        public Container? Container { get; set; }

        public Location? From { get; set; }
        public Location? To { get; set; }
        
        public bool HasDestination => To != null;
        public bool Hascontainer => Container != null;
        public bool IsHome => From?.Name == HomeName;

        public int RemainingHoursForArrival { get;  set; }

        public Vehicle(string name, string homeName)
        {
            this.Name = name;
            this.HomeName = homeName;
        }

        public abstract bool CanUseRoute(Route route);
        

        public void Depart(Location current, Location destination, int duration)
        {
            this.From = current;
            this.To = destination;
            this.RemainingHoursForArrival = duration;
        }

        private void UnloadContainer()
        {
            // shut up compiler
            if(Container == null || To == null) return;

            // TODO: this is probably the role of the location to do the unloading
            var destination = To.IsDestinationForContainer(Container) ? $"its destination {Container.DestinationName}" : $"{To.Name}";
            Console.WriteLine($"{Name} unloading container to {destination}");

            To.PutContainer(this.Container);
            this.Container = null;
        }

        // move, and if harrives at destination, unload
        public void Move()
        {
            if(HasDestination)
            {
                RemainingHoursForArrival --;
                if(RemainingHoursForArrival == 0) 
                {
                    Console.WriteLine($"{Name} arriving at {To!.Name}");

                    // unload if has container
                    if(Hascontainer) 
                    {
                        this.UnloadContainer();
                    }

                    // arrived => no  more destination
                    // TODO: move into location (accoster/se garer/??)
                    this.From = this.To;
                    this.From.PutVehicle(this);
                    this.To = null;
                }
                else
                {
                    // TODO: (Home / Delivery)
                    var goal = Hascontainer ? $"delivering {Container!.DestinationName}" : "going home";
                    Console.WriteLine($"{Name} moving towards {To!.Name} ({goal}), {RemainingHoursForArrival} hours remaining");
                }
            }
        }

    }
}
