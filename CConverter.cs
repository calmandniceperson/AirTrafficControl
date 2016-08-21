using System;

namespace AirTrafficControl
{
    public class CConverter
    {
        // Converts polar coordinates (distance and angle) to cartesian 
        // coordinates (x and y) and returns a CartesianCoord object containing
        // x and y.
        // "x= distance * cos( winkel ) und y= distance * sin ( winkel )"
        public static CartesianCoord convertToCartesian(PolarCoord coord)
        {
            double x = coord.Distance * Math.Cos(coord.Angle);
            double y = coord.Distance * Math.Sin(coord.Angle);
            return new CartesianCoord(x, y);
        }
    }
}
