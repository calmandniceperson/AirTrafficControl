// Michael Koeppl
// AirTrafficControl
// August 2016

using System;
using System.Threading;

namespace AirTrafficControl
{
    public class MainSimulator
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to AirTrafficControl");

            AirTrafficControl atc = new AirTrafficControl();

            atc.addPlane(new AirPlane("A111", 800,
                        new PolarCoord(AtcoConstants.STARTDISTANCE, 90)));
            atc.addPlane(new AirPlane("A112", 700,
                        new PolarCoord(99.0, 90)));

            do {
                // repeat steps for moving planes
                for (int i = 0; i < atc.getPlanes().Count; i++)
                {
                    var plane = atc.getPlane(i);
                    if(plane.Position.Distance <= 100 &&
                            plane.Position.Distance > 3)
                    {
                        step(plane);
                    }
                    else if(plane.Position.Distance <= 3)
                    {
                        Console.WriteLine("Airplane to land [speed="+
                                Math.Round(plane.FlightSpeed)+"km/h, Distance="+
                                Math.Round(plane.Position.Distance, 1)+"km ]");
                        atc.removePlane(plane);
                    }
                }
                Thread.Sleep(Convert.ToInt32(AtcoConstants.
                            TIMEINTERVALSECONDS*100));
            } while(atc.getPlanes().Count > 0);
            Console.WriteLine("No further Airplane");
        }

        private static void step(AirPlane plane)
        {
            var distanceMoved = ((plane.FlightSpeed*10)/3.6)/1000;
            plane.Position.Distance -= distanceMoved;
            plane.FlightSpeed -= distanceMoved * (plane.StartSpeed/100);
            Console.WriteLine("Status after Step ---------------------");
            Console.WriteLine("Airplane [speed="+(int)plane.FlightSpeed+
                    "km/h, Distance="+Math.Round(plane.Position.Distance, 1)+
                    "km ]");
        }
    }
}
