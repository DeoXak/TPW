namespace Data
{
    public class Ball
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double VelX { get; set; }
        public double VelY { get; set; }
        public double Radius { get; set; }

        public Ball(double x, double y, double radius, double velx, double vely)
        {
            X = x;
            Y = y;
            VelX = velx;
            VelY = vely;
            Radius = radius;
        }
    }
}

