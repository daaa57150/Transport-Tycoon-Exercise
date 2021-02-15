using System.Collections.Generic;
using System.Linq;

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
                this.PutContainer(container);
            }
        }

    }
}


