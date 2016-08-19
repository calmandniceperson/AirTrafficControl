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
            //atc.addPlane(new AirPlane("A112", AtcoConstants.MINSPEED,
            //            new PolarCoord(300.0, 20));

            do {
                // repeat steps for moving planes
                for (int i = 0; i < atc.getPlanes().Count; i++)
                {
                    var plane = atc.getPlane(i);
                    if(plane.Position.Distance <= 100 &&
                            plane.Position.Distance > 3)
                    {
                        Console.WriteLine("Airplane [speed="+plane.FlightSpeed+
                                "km/h, Distance="+
                                Math.Round(plane.Position.Distance, 1)+"km ]");
                        plane.Position.Distance -= 
                            (plane.FlightSpeed/3600)*AtcoConstants.
                            TIMEINTERVALSECONDS;
                        Console.WriteLine("Status after Step ----------------");
                    }
                    else if(plane.Position.Distance <= 3)
                    {
                        Console.WriteLine("Airplane to land [speed="+
                                plane.FlightSpeed+"km/h, Distance="+
                                Math.Round(plane.Position.Distance, 1)+"km ]");
                        atc.removePlane(plane);
                    }
                }
                Thread.Sleep(Convert.ToInt32(AtcoConstants.
                            TIMEINTERVALSECONDS*100));
            } while(atc.getPlanes().Count > 0);
            Console.WriteLine("No further Airplane");
        }
    }
}
