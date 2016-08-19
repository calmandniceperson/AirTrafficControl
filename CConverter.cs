using System;

namespace AirTrafficControl
{
    public class CConverter
    {
        public static CartesianCoord convertToCartesian(PolarCoord coord)
        {
            double x = coord.Distance * Math.Cos(coord.Angle);
            double y = coord.Distance * Math.Sin(coord.Angle);
            return new CartesianCoord(x, y);
        }
    }
}
