namespace AirTrafficControl
{
    // Represents a plane's location with X and Y coordinates
    public class CartesianCoord
    {
        public double X {get;}
        public double Y {get;}

        public CartesianCoord(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
