using System;

namespace AirTrafficControl
{
    public class CConverter
    {
        public static CartesianCoord convertToCartesian(PolarCoord coord)
        {
            double x = coord.distance * Math.Cos(coord.angle);
            double y = coord.distance * Math.Sin(coord.angle);
            return new CartesianCoord(x, y);
        }
    }
}
