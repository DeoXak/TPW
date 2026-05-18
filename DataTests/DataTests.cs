using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void Ball_Constructor_SetsPropertiesCorrectly()
        {
            var ball = new Ball(10, 20, 5, 1.5, -2.0);

            Assert.AreEqual(10, ball.X);
            Assert.AreEqual(20, ball.Y);
            Assert.AreEqual(5, ball.Radius);
            Assert.AreEqual(1.5, ball.VelX);
            Assert.AreEqual(-2.0, ball.VelY);
        }

        [TestMethod]
        public void Ball_Properties_CanBeModified()
        {
            var ball = new Ball(0, 0, 5, 0, 0);

            ball.X = 99;
            ball.Y = 88;
            ball.VelX = 3.5;
            ball.VelY = -1.5;

            Assert.AreEqual(99, ball.X);
            Assert.AreEqual(88, ball.Y);
            Assert.AreEqual(3.5, ball.VelX);
            Assert.AreEqual(-1.5, ball.VelY);
        }

        [TestMethod]
        public void Ball_Radius_CanBeModified()
        {
            var ball = new Ball(0, 0, 10, 0, 0);
            ball.Radius = 25;
            Assert.AreEqual(25, ball.Radius);
        }

        [TestMethod]
        public void Ball_NegativeVelocity_IsStored()
        {
            var ball = new Ball(0, 0, 5, -3.0, -7.0);
            Assert.AreEqual(-3.0, ball.VelX);
            Assert.AreEqual(-7.0, ball.VelY);
        }

        [TestMethod]
        public void Ball_ZeroValues_AreValid()
        {
            var ball = new Ball(0, 0, 0, 0, 0);
            Assert.AreEqual(0, ball.X);
            Assert.AreEqual(0, ball.Y);
            Assert.AreEqual(0, ball.Radius);
            Assert.AreEqual(0, ball.VelX);
            Assert.AreEqual(0, ball.VelY);
        }
    }
}