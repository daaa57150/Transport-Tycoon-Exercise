using System.Collections.Generic;

namespace TransportTycoon
{

    // Final destination
    public class Factory: Location
    {
        public Factory(): base("Factory") 
        {
            
        }

        public void SetContainers(IEnumerable<Container> containers) 
        {
            foreach(var container in containers)
            {
                this.Containers.Add(container);
            }
        }

    }
}


