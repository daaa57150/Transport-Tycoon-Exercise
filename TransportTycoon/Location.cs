using System.Collections.Generic;

namespace TransportTycoon
{
    public class Location
    {
        public string Name { get; set; }

        private Queue<Container> containers;

        public Location(string name)
        {
            this.Name = name;
            this.containers = new Queue<Container>();
        }

        public Container? TakeContainer()
        {
            if (this.containers.Count == 0)
            {
                return null;
            }
            return containers.Dequeue();
        }

        public void PutContainer(Container container)
        {
            containers.Enqueue(container);
        }
    }
}
