using Data;
namespace Logic
{
    public abstract class LogicAPI
    {
        public abstract List<Ball> GetBalls();
        public abstract bool AddBall(double x, double y, double radius, double velx, double vely);
        public abstract void UpdatePosition(double height, double width);
        public static LogicAPI CreateLayer()
        {
            return new BallRepository();
        }
    }

    internal class BallRepository : LogicAPI
    {
        private readonly List<Ball> _balls = new List<Ball>();

        public override List<Ball> GetBalls() => _balls;

        public override bool AddBall(double x, double y, double radius, double velx, double vely)
        {
            foreach (var b in _balls)
            {
                double distance = Math.Sqrt(Math.Pow(x - b.X, 2) + Math.Pow(y - b.Y, 2));

                if (distance < (radius + b.Radius))
                {
                    return false;
                }
            }

            _balls.Add(new Ball(x, y, radius, velx, vely));
            return true;
        }
        public override void UpdatePosition(double height, double width)
        {
            foreach (var b in _balls)
            {
                if (b.X <= 0)
                {
                    b.VelX = -b.VelX;
                    b.X = 0;
                }
                else if (b.X >= width - b.Radius * 2)
                {
                    b.VelX = -b.VelX;
                    b.X = width - b.Radius * 2;
                }
                else if (b.Y <= 0)
                {
                    b.VelY = -b.VelY;
                    b.Y = 0;
                }
                else if (b.Y >= height - b.Radius * 2)
                {
                    b.VelY = -b.VelY;
                    b.Y = height - b.Radius * 2;
                }
                b.X += b.VelX;
                b.Y += b.VelY;
            }

        }
    }
}
