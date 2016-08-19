namespace AirTrafficControl
{
    public class AirPlane
    {
        public string FlightNum {get;}

        private double flightSpeed;
        public double FlightSpeed
        {
            get
            {
                return flightSpeed;
            }
            set
            {
                if (value < 200)
                {
                    value = 200;
                }
                flightSpeed = value;
            }
        }
        public PolarCoord Position {get;}

        public AirPlane(string flightNum, double flightSpeed, PolarCoord pos)
        {
            this.FlightNum = flightNum;
            this.FlightSpeed = flightSpeed;
            this.Position = pos;
        }
    }
}
