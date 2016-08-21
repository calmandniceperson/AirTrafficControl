namespace AirTrafficControl
{
    public class AirPlane
    {
        // Flight number (e.g. A11)
        public string FlightNum {get;}

        // The speed at which the plane flew into range in km/h
        // flightSpeed is identical to StartSpeed in the beginning and is
        // gradually decreased later on
        public double StartSpeed {get;}

        // flightSpeed is the current speed of the plane in km/h. It gradually
        // decreases as the plane approaches the center.
        private double flightSpeed;
        public double FlightSpeed
        {
            get
            {
                return flightSpeed;
            }
            set
            {
                // The speed may not be less than 200km/h
                if (value < 200)
                {
                    value = 200;
                }
                flightSpeed = value;
            }
        }
        public PolarCoord Position {get;}

        public AirPlane(string flightNum, double startSpeed, PolarCoord pos)
        {
            this.FlightNum = flightNum;
            this.StartSpeed = startSpeed;
            this.flightSpeed = startSpeed;
            this.Position = pos;
        }
    }
}
