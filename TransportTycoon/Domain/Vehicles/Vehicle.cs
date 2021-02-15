using System;

namespace TransportTycoon
{
    // Base for vehicles
    public class Vehicle
    {
        public string Name { get; set; }

        public Container? Container { get; set; }

        public Location? From { get; set; }
        public Location? To { get; set; }
        
        public bool HasDestination => To != null;
        public bool Hascontainer => Container != null;

        public int RemainingHoursForArrival { get;  set; }

        public Vehicle(string name)
        {
            this.Name = name;
        }

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
                    this.From = this.To;
                    this.To = null;
                }
                else
                {
                    Console.WriteLine($"{Name} moving towards {To!.Name}, {RemainingHoursForArrival} hours remaining");
                }
            }
        }

        // public void Tick()
        // {
        //     if(HasDestination)
        //     {
        //         RemainingHoursForArrival --;
        //         if(RemainingHoursForArrival == 0) 
        //         {
        //             To.PutContainer(this.Container);
                    
        //         }
        //     }
        // }
    }
}
