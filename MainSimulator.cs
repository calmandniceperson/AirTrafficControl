// Michael Koeppl
// AirTrafficControl
// August 2016

using System;
using System.Collections.Generic;
using System.Threading;

namespace AirTrafficControl
{
    public class MainSimulator
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Welcome to AirTrafficControl");

            AirTrafficControl atc = new AirTrafficControl();
            generatePlanes(atc);

            do {
                // repeat steps for moving planes
                Console.WriteLine("Status after Step ---------------------");
                for (int i = 0; i < atc.getPlanes().Count; i++)
                {
                    var plane = atc.getPlane(i);
                    if (plane.Position.Distance > 3)
                    {
                        step(plane);
                        if (planeWithinRangeOfAnother(plane, atc.getPlanes()))
                        {
                            putPlaneOnHold(plane, atc.getPlanes());
                        }
                    }
                    else if (plane.Position.Distance <= 3)
                    {
                        // Main radar handles planes within 3km
                        // Not our job
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Airplane " + plane.FlightNum + 
                                " to land [speed="+
                                Math.Round(plane.FlightSpeed)+"km/h, Distance="+
                                Math.Round(plane.Position.Distance, 1)+"km " +
                                ", Angle=" + plane.Position.Angle + "deg ]");
                        Console.ResetColor();
                        atc.removePlane(plane);
                    }
                }
                Thread.Sleep(Convert.ToInt32(AtcoConstants.
                            TIMEINTERVALSECONDS*100));
            } while (atc.getPlanes().Count > 0);
            Console.WriteLine("No further Airplane");
        }

        private static bool planeWithinRangeOfAnother(AirPlane plane,
                List<AirPlane> planes)
        {
            var planeCartPos =
                CConverter.convertToCartesian(plane.Position);
            foreach (var p in planes)
            {
                if (plane.FlightNum == p.FlightNum) { continue; }
                var compPlaneCartPos =
                    CConverter.convertToCartesian(p.Position);
                var xDistance = planeCartPos.X - compPlaneCartPos.X;
                var yDistance = planeCartPos.Y - compPlaneCartPos.Y;
                var pointDistance =
                    Math.Sqrt(Math.Pow(xDistance, 2)+Math.Pow(yDistance, 2));
                if (pointDistance <= 3)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Plane " + plane.FlightNum +
                                    " was within range of plane " +
                                    p.FlightNum + "; Distance=" +
                                    pointDistance);
                    Console.ResetColor();
                    return true;
                }
            }
            return false;
        }

        private static void putPlaneOnHold(AirPlane plane,
                List<AirPlane> planes)
        {
            plane.Position.Distance = 100.0;
            var foundCorridor = false;
            do
            {
                plane.Position.Angle += 3;
                if (!planeWithinRangeOfAnother(plane, planes))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Plane " + plane.FlightNum +
                            " has been put in corridor " +
                            plane.Position.Angle + " at " +
                            plane.Position.Distance + "km distance.");
                    Console.ResetColor();
                    break;
                }
            } while (!foundCorridor);
        }

        private static void step(AirPlane plane)
        {
            var distanceMoved = ((plane.FlightSpeed*10)/3.6)/1000;
            plane.Position.Distance -= distanceMoved;
            plane.FlightSpeed -= distanceMoved * (plane.StartSpeed/100);
            Console.WriteLine("Airplane " + plane.FlightNum +
                    " [speed="+(int)plane.FlightSpeed+
                    "km/h, Distance="+Math.Round(plane.Position.Distance, 1)+
                    "km, Angle=" + plane.Position.Angle + "deg ]");
        }

        private static void generatePlanes(AirTrafficControl atc)
        {
            Random rnd = new Random();
            int numOfPlanes = rnd.Next(5, 31);
            for (int i = 0; i < numOfPlanes; i++)
            {
                var rndDistance = rnd.Next(3, 101);
                var rndAngle    = rnd.Next(0, 361);
                var rndSpeed    = rnd.Next(200, 1101);

                var newPlane = new AirPlane(("A"+(i+1)), rndSpeed,
                        new PolarCoord(rndDistance, rndAngle));
                atc.addPlane(newPlane);
            }
        }
    }
}
