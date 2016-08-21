namespace AirTrafficControl
{
    // Represents a plane's location with distance (km) and angle (degrees)
    public class PolarCoord
    {
        public double Distance {get;set;}
        public double Angle {get;set;}

        public PolarCoord(double distance, double angle)
        {
            this.Distance = distance;
            this.Angle = angle;
        }
    }
}
