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

            // Create new instance of AirTrafficControl which will manage
            // the planes.
            AirTrafficControl atc = new AirTrafficControl();
            // Generate a random number of planes with random speeds, distances
            // and angles
            generatePlanes(atc);

            // "Game loop"
            // Runs as long as there are still planes left
            do {
                // Print dividing line
                Console.WriteLine("Status after Step ---------------------");

                // Run through the list of planes currently tracked by the 
                // radar.
                for (int i = 0; i < atc.getPlanes().Count; i++)
                {
                    var plane = atc.getPlane(i);

                    // If the plane is farther away than 3km, we take care of
                    // moving it and slowing it down and checking whether it is
                    // too close to another plane.
                    if (plane.Position.Distance > 3)
                    {
                        // Decrease speed and distance and print the data
                        step(plane);
                        // Check, whether the plane is within range of any other
                        // plane in the list of currently tracked planes.
                        // If it is, we put it on hold by moving it to 100km
                        // distance in another corridor.
                        if (planeWithinRangeOfAnother(plane, atc.getPlanes()))
                        {
                            putPlaneOnHold(plane, atc.getPlanes());
                        }
                    }
                    // If the plane is within 3km or closer, the main radar
                    // takes over and we simply print a message saying that
                    // the plane is going to land and print speed and distance.
                    else if (plane.Position.Distance <= 3)
                    {
                        // Main radar handles planes within 3km
                        // Not our job, just print
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
                // Let the thread sleep for 1 second (10*100ms) in order to
                // simulate real data coming in
                Thread.Sleep(Convert.ToInt32(AtcoConstants.
                            TIMEINTERVALSECONDS*100));
            } while (atc.getPlanes().Count > 0);
            Console.WriteLine("No further Airplane");
        }

        // Checks whether a plane is too close to another
        private static bool planeWithinRangeOfAnother(AirPlane plane,
                List<AirPlane> planes)
        {
            // Get the plane's cartesian location data
            var planeCartPos =
                CConverter.convertToCartesian(plane.Position);
            // Run through all tracked planes
            foreach (var p in planes)
            {
                // We avoid comparing the plane with itself by comparing
                // the flight numbers
                if (plane.FlightNum == p.FlightNum) { continue; }
                // Get cartesian location data of the second plane
                var compPlaneCartPos =
                    CConverter.convertToCartesian(p.Position);
                // Get distance on x axis
                var xDistance = planeCartPos.X - compPlaneCartPos.X;
                // Get distance on y axis
                var yDistance = planeCartPos.Y - compPlaneCartPos.Y;
                // Calculate distance between the two coordinates utilising
                // Pythagorean theorem
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

        // Puts the given plane on hold
        private static void putPlaneOnHold(AirPlane plane,
                List<AirPlane> planes)
        {
            // Check whether the plane is within range of another.
            // If it is not, we can basically just stop right there because
            // the plane already is in the right spot.
            if (!planeWithinRangeOfAnother(plane, planes))
            {
                return;
            }
            // Set distance to 100km (as required)
            plane.Position.Distance = 100.0;
            // Move the plane to the next corridor by changing its angle by 3deg
            plane.Position.Angle += 3;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Plane " + plane.FlightNum +
                    " has been put in corridor " +
                    plane.Position.Angle + " at " +
                    plane.Position.Distance + "km distance.");
            Console.ResetColor();
            // Recursive call in order to repeat this procedure until a valid
            // corridor is found
            putPlaneOnHold(plane, planes);
        }

        // Decreases speed and distance for the given plane with every call
        private static void step(AirPlane plane)
        {
            // Calculate the distance moved in 1sec (1000)
            var distanceMoved =
                ((plane.FlightSpeed*AtcoConstants.TIMEINTERVALSECONDS)/3.6)/1000;
            // Set new distance
            plane.Position.Distance -= distanceMoved;
            // Decrease the flight speed linearly with the distance moved
            plane.FlightSpeed -= distanceMoved * (plane.StartSpeed/100);
            Console.WriteLine("Airplane " + plane.FlightNum +
                    " [speed="+(int)plane.FlightSpeed+
                    "km/h, Distance="+Math.Round(plane.Position.Distance, 1)+
                    "km, Angle=" + plane.Position.Angle + "deg ]");
        }

        // Generate random number of planes with...
        // ... random speed between 200km/h and 1000km/h
        // ... random distance between 3km and 100km
        // ... a random angle between 0 and 360deg
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
