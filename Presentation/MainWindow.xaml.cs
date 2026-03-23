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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LogicAPI _logic = LogicAPI.CreateLayer();
        private Random _random = new Random();

        private void AddBall_Click(object sender, RoutedEventArgs e)
        {
            double radius = 10;

            double x = _random.NextDouble() * (BilliardTable.ActualWidth - 2 * radius);
            double y = _random.NextDouble() * (BilliardTable.ActualHeight - 2 * radius);

            _logic.AddBall(x, y, radius);

            Redraw();
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
                    Fill = Brushes.White
                };
                Canvas.SetLeft(ellipse, ball.X);
                Canvas.SetTop(ellipse, ball.Y);
                BilliardTable.Children.Add(ellipse);
            }
        }
    }
}