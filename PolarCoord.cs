namespace AirTrafficControl
{
    public class PolarCoord
    {
        public double distance {get;set;}
        public double angle {get;}

        public PolarCoord(double distance, double angle)
        {
            this.distance = distance;
            this.angle = angle;
        }
    }
}
