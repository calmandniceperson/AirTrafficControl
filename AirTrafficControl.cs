using System.Collections.Generic;

namespace AirTrafficControl
{
    public class AirTrafficControl
    {
        private List<AirPlane> planes = new List<AirPlane>();

        public AirTrafficControl() {}

        public void addPlane(AirPlane a)
        {
            planes.Add(a);
        }

        public List<AirPlane> getPlanes()
        {
            return this.planes;
        }
    }
}
