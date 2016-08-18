namespace AirTrafficControl
{
    public class AtcoConstants
    {
        // Number of runs
        public const int NSTEPS = 110; 
        // Intervall of simulation step
        // (one calculation step = 10 sec)
        public const double TIMEINTERVALSECONDS = 10.0;
        // Minimum speed in km/h
        public const double MINSPEED = 200.0;
        // km range of radar
        public const double STARTDISTANCE = 100.0;
    }
}
