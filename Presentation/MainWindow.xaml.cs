using Logic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Presentation
{
    public partial class MainWindow : Window
    {
        private readonly LogicAPI _logic = LogicAPI.CreateLayer();
        private readonly Random _random = new();
        private readonly DispatcherTimer _renderTimer;

        public MainWindow()
        {
            InitializeComponent();
            _renderTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16)
            };
            _renderTimer.Tick += (_, _) => Redraw();
        }

        private void AddBall_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IloscKulek.Text, out int count)) return;

            for (int i = 0; i < count; i++)
            {
                double radius = 10;
                double x = _random.NextDouble() * (BilliardTable.ActualWidth - 2 * radius);
                double y = _random.NextDouble() * (BilliardTable.ActualHeight - 2 * radius);
                double velx = _random.NextDouble() * 6 - 3;
                double vely = _random.NextDouble() * 6 - 3;
                _logic.AddBall(x, y, radius, velx, vely);
            }
            Redraw();
        }

        private void MoveBall_Click(object sender, RoutedEventArgs e)
        {
            _logic.StartSimulation(BilliardTable.ActualHeight, BilliardTable.ActualWidth);
            _renderTimer.Start();
        }

        private void StopBall_Click(object sender, RoutedEventArgs e)
        {
            _logic.StopSimulation();
            _renderTimer.Stop();
        }

        private void DeleteBall_Click(object sender, RoutedEventArgs e)
        {
            _logic.Clear();
            Redraw();
        }

        private void AddBall_Click(object sender, RoutedEventArgs e)
        {
            int ilosc = int.Parse(IloscKulek.Text);
            for (int i = 0; i < ilosc; i++)
            {
                double radius = 10;

                double x = _random.NextDouble() * (BilliardTable.ActualWidth - 2 * radius);
                double y = _random.NextDouble() * (BilliardTable.ActualHeight - 2 * radius);
                double velx = _random.NextDouble() * 10 - 5;
                double vely = _random.NextDouble() * 10 - 5;

                _logic.AddBall(x, y, radius, velx, vely);

                Redraw();
            }
        }
        private void MoveBall_Click(object sender, RoutedEventArgs e)
        {

            _timer.Start();
        }
        private void StopBall_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }
        private void DeleteBall_Click(object sender, RoutedEventArgs e)
        {
            int ilosc = int.Parse(IloscKulek.Text);
            for (int i = 0; i < ilosc; i++)
            {
                _logic.GetBalls().RemoveAt(i);
                Redraw();
            }
        }
        private void Redraw()
        {
            BilliardTable.Children.Clear();
            foreach (var ball in _logic.GetBalls())
            {
                var ellipse = new Ellipse
                {
                    Width = ball.Radius * 2,
                    Height = ball.Radius * 2,
                    Fill = Brushes.White,
                };
                Canvas.SetLeft(ellipse, ball.X);
                Canvas.SetTop(ellipse, ball.Y);
                BilliardTable.Children.Add(ellipse);
            }
        }
    }
}