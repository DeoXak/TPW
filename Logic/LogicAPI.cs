using Data;

namespace Logic
{
    public abstract class LogicAPI
    {
        public abstract IReadOnlyList<Ball> GetBalls();
        public abstract bool AddBall(double x, double y, double radius, double velx, double vely);
        public abstract void UpdatePosition(double height, double width);
        public abstract void StartSimulation(double height, double width);
        public abstract void StopSimulation();
        public abstract void Clear();

        public static LogicAPI CreateLayer() => new BallRepository();
    }

    internal class BallRepository : LogicAPI
    {
        private readonly List<Ball> _balls = new();
        private readonly object _lock = new object();
        private CancellationTokenSource? _cts;
        private double _height, _width;

        public override IReadOnlyList<Ball> GetBalls()
        {
            lock (_lock)
                return _balls.ToList();
        }

        public override bool AddBall(double x, double y, double radius, double velx, double vely)
        {
            lock (_lock)
            {
                foreach (var b in _balls)
                {
                    double dist = Math.Sqrt(Math.Pow(x - b.X, 2) + Math.Pow(y - b.Y, 2));
                    if (dist <= radius + b.Radius)
                        return false;
                }
                _balls.Add(new Ball(x, y, radius, velx, vely));
                return true;
            }
        }

        public override void Clear()
        {
            lock (_lock)
                _balls.Clear();
        }

        public override void UpdatePosition(double height, double width)
        {
            lock (_lock)
            {
                _height = height;
                _width = width;
                UpdatePositions();
                ResolveCollisions();
            }
        }

        public override void StartSimulation(double height, double width)
        {
            _height = height;
            _width = width;
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    lock (_lock)
                    {
                        UpdatePositions();
                        ResolveCollisions();
                    }
                    Thread.Sleep(16);
                }
            }, token);
        }

        public override void StopSimulation()
        {
            _cts?.Cancel();
        }

        private void UpdatePositions()
        {
            foreach (var b in _balls)
            {
                b.X += b.VelX;
                b.Y += b.VelY;

                if (b.X < 0)
                {
                    b.X = 0;
                    b.VelX = Math.Abs(b.VelX);
                }
                else if (b.X + b.Radius * 2 > _width)
                {
                    b.X = _width - b.Radius * 2;
                    b.VelX = -Math.Abs(b.VelX);
                }

                if (b.Y < 0)
                {
                    b.Y = 0;
                    b.VelY = Math.Abs(b.VelY);
                }
                else if (b.Y + b.Radius * 2 > _height)
                {
                    b.Y = _height - b.Radius * 2;
                    b.VelY = -Math.Abs(b.VelY);
                }
            }
        }

        private void ResolveCollisions()
        {
            for (int i = 0; i < _balls.Count; i++)
            {
                for (int j = i + 1; j < _balls.Count; j++)
                {
                    var a = _balls[i];
                    var b = _balls[j];

                    double ax = a.X + a.Radius;
                    double ay = a.Y + a.Radius;
                    double bx = b.X + b.Radius;
                    double by = b.Y + b.Radius;

                    double dx = bx - ax;
                    double dy = by - ay;
                    double dist = Math.Sqrt(dx * dx + dy * dy);
                    double minDist = a.Radius + b.Radius;

                    if (dist < minDist && dist > 0)
                    {
                        double nx = dx / dist;
                        double ny = dy / dist;

                        double overlap = (minDist - dist) / 2.0;
                        a.X -= nx * overlap;
                        a.Y -= ny * overlap;
                        b.X += nx * overlap;
                        b.Y += ny * overlap;

                        double dvx = a.VelX - b.VelX;
                        double dvy = a.VelY - b.VelY;
                        double dot = dvx * nx + dvy * ny;

                        if (dot > 0)
                        {
                            a.VelX -= dot * nx;
                            a.VelY -= dot * ny;
                            b.VelX += dot * nx;
                            b.VelY += dot * ny;
                        }
                    }
                }
            }
        }
    }
}