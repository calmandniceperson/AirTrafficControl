namespace AirTrafficControl
{
    public class AirPlane
    {
        public string FlightNum {get;}

        public double StartSpeed {get;}
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
        public CartesianCoord CartPosition {get;set;}

        public AirPlane(string flightNum, double startSpeed, PolarCoord pos)
        {
            this.FlightNum = flightNum;
            this.StartSpeed = startSpeed;
            this.flightSpeed = startSpeed;
            this.Position = pos;
            CartPosition = CConverter.convertToCartesian(this.Position);
        }
    }
}
