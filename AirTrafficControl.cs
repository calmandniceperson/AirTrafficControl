using System.Collections.Generic;

namespace AirTrafficControl
{
    public class AirTrafficControl
    {
        private List<AirPlane> planes = new List<AirPlane>();

        public AirTrafficControl() {}

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
