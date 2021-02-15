using System.Collections.Generic;

namespace TransportTycoon
{

    // Final destination
    public class Factory: Location
    {
        public Factory(): base("Factory") 
        {
            
        }

        public Factory(string name): base(name) 
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


