// Michael Koeppl
// AirTrafficControl
// August 2016

using System;

namespace AirTrafficControl
{
    public class MainSimulator
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to AirTrafficControl");

            AirTrafficControl atc = new AirTrafficControl();

            atc.addPlane(new AirPlane("A111"));
            atc.addPlane(new AirPlane("A112"));

            int index = 0;
            do {
                // repeat steps for moving planes
                foreach (AirPlane plane in atc.getPlanes())
                {
                    Console.WriteLine(plane.flightNum);
                }
                index = 10;
            } while(index < 10);
        }
    }
}
