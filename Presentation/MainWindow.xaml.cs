using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic;
using System.Windows.Threading;
using System.Security.RightsManagement;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LogicAPI _logic = LogicAPI.CreateLayer();
        private Random _random = new Random();
        DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(16);
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _logic.UpdatePosition(BilliardTable.ActualHeight, BilliardTable.ActualWidth);
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
                Ellipse ellipse = new Ellipse
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