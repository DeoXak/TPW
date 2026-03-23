using Logic;

namespace LogicTests
{
    [TestClass]
    public sealed class LogicTests
    {
        [TestMethod]
        public void AddBall_ShouldIncreaseBallCount()
        {
            LogicAPI logic = LogicAPI.CreateLayer();
            int initialCount = logic.GetBalls().Count;

            logic.AddBall(10, 10, 5);

            int finalCount = logic.GetBalls().Count;
            Assert.AreEqual(initialCount + 1, finalCount, "Liczba kulek powinna wzrosnąć o 1.");
        }

        [TestMethod]
        public void BallPosition_ShouldBeCorrect()
        {
            LogicAPI logic = LogicAPI.CreateLayer();
            double testX = 100;
            double testY = 150;

            logic.AddBall(testX, testY, 5);
            var ball = logic.GetBalls().Last();

            Assert.AreEqual(testX, ball.X);
            Assert.AreEqual(testY, ball.Y);
        }
    }
}
