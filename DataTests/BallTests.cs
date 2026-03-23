using Data;
namespace DataTests
{
    [TestClass]
    public sealed class BallTests
    {
        [TestMethod]
        public void CreatesBallWithCorrectValues()
        {
            double x = 5.0;
            double y = 10.0;
            double radius = 2.5;

            Ball ball = new Ball(x, y, radius);

            Assert.AreEqual(x, ball.X);
            Assert.AreEqual(y, ball.Y);
            Assert.AreEqual(radius, ball.Radius);
        }
    }
}
