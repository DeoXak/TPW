namespace Data
{
    public class Ball
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }

        public Ball(double x, double y, double radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }
    }
}
