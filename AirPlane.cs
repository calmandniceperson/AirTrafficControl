namespace AirTrafficControl
{
    public class AirPlane
    {
        public string flightNum {get;}
        public double flightSpeed {get;}
        public PolarCoord position {get;}

        public AirPlane(string flightNum, double flightSpeed, PolarCoord pos)
        {
            this.flightNum = flightNum;
            this.flightSpeed = flightSpeed;
            this.position = pos;
        }
    }
}
