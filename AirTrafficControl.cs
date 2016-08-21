using System.Collections.Generic;

namespace AirTrafficControl
{
    public class AirTrafficControl
    {
        // List of planes currently tracked
        // If a plane reaches the 3km threshold, it is removed from this list
        private List<AirPlane> planes = new List<AirPlane>();

        public void addPlane(AirPlane a)
        {
            this.planes.Add(a);
        }

        public void removePlane(AirPlane plane)
        {
            this.planes.RemoveAt(this.planes.IndexOf(plane));
        }

        public List<AirPlane> getPlanes()
        {
            return this.planes;
        }

        public AirPlane getPlane(int index)
        {
            return this.planes[index];
        }
    }
}
