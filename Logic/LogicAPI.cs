using Data;

namespace Logic
{
    public abstract class LogicAPI
    {
        public abstract List<Ball> GetBalls();
        public abstract bool AddBall(double x, double y, double radius);

        public static LogicAPI CreateLayer()
        {
            return new BallRepository();
        }
    }

    internal class BallRepository : LogicAPI
    {
        private readonly List<Ball> _balls = new List<Ball>();

        public override List<Ball> GetBalls() => _balls;

        public override bool AddBall(double x, double y, double radius)
        {
            foreach (var b in _balls)
            {
                double distance = Math.Sqrt(Math.Pow(x - b.X, 2) + Math.Pow(y - b.Y, 2));

                if (distance < (radius + b.Radius))
                {
                    return false;
                }
            }

            _balls.Add(new Ball(x, y, radius));
            return true;
        }
    }
}
