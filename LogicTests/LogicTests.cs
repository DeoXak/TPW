using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class LogicTests
    {
        private LogicAPI CreateLogic() => LogicAPI.CreateLayer();

        [TestMethod]
        public void AddBall_ValidPosition_ReturnsTrue()
        {
            var logic = CreateLogic();
            bool result = logic.AddBall(100, 100, 10, 1, 1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddBall_ValidPosition_BallAppearsInList()
        {
            var logic = CreateLogic();
            logic.AddBall(100, 100, 10, 1, 1);
            Assert.AreEqual(1, logic.GetBalls().Count);
        }

        [TestMethod]
        public void AddBall_MultipleBalls_AllAdded()
        {
            var logic = CreateLogic();
            logic.AddBall(50, 50, 10, 1, 1);
            logic.AddBall(200, 200, 10, 1, 1);
            logic.AddBall(400, 400, 10, 1, 1);
            Assert.AreEqual(3, logic.GetBalls().Count);
        }

        [TestMethod]
        public void AddBall_OverlappingPosition_ReturnsFalse()
        {
            var logic = CreateLogic();
            logic.AddBall(100, 100, 10, 0, 0);

            bool result = logic.AddBall(100, 100, 10, 0, 0);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddBall_OverlappingPosition_DoesNotAddBall()
        {
            var logic = CreateLogic();
            logic.AddBall(100, 100, 10, 0, 0);
            logic.AddBall(100, 100, 10, 0, 0);
            Assert.AreEqual(1, logic.GetBalls().Count);
        }

        [TestMethod]
        public void AddBall_JustTouching_IsRejected()
        {
            var logic = CreateLogic();
            logic.AddBall(100, 100, 10, 0, 0);

            bool result = logic.AddBall(120, 100, 10, 0, 0);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddBall_FarApart_BothAdded()
        {
            var logic = CreateLogic();
            logic.AddBall(100, 100, 10, 0, 0);

            bool result = logic.AddBall(150, 100, 10, 0, 0);
            Assert.IsTrue(result);
            Assert.AreEqual(2, logic.GetBalls().Count);
        }

        [TestMethod]
        public void GetBalls_EmptyAtStart()
        {
            var logic = CreateLogic();
            Assert.AreEqual(0, logic.GetBalls().Count);
        }

        [TestMethod]
        public void GetBalls_ReturnsCopy_NotReference()
        {
            var logic = CreateLogic();
            logic.AddBall(100, 100, 10, 0, 0);

            var snapshot = logic.GetBalls();
            int countBefore = snapshot.Count;

            logic.AddBall(300, 300, 10, 0, 0);

            Assert.AreEqual(countBefore, snapshot.Count);
        }

        [TestMethod]
        public void UpdatePosition_BallMovesByVelocity()
        {
            var logic = CreateLogic();
            logic.AddBall(100, 100, 10, 5, 3);

            logic.UpdatePosition(600, 800);

            var ball = logic.GetBalls()[0];
            Assert.AreEqual(105, ball.X);
            Assert.AreEqual(103, ball.Y);
        }

        [TestMethod]
        public void UpdatePosition_BallBouncesOffLeftWall()
        {
            var logic = CreateLogic();
            logic.AddBall(2, 100, 10, -5, 0);

            logic.UpdatePosition(600, 800);

            var ball = logic.GetBalls()[0];
            Assert.IsTrue(ball.VelX > 0);
            Assert.IsTrue(ball.X >= 0);
        }

        [TestMethod]
        public void UpdatePosition_BallBouncesOffRightWall()
        {
            var logic = CreateLogic();
            logic.AddBall(785, 100, 10, 5, 0);

            logic.UpdatePosition(600, 800);

            var ball = logic.GetBalls()[0];
            Assert.IsTrue(ball.VelX < 0);
            Assert.IsTrue(ball.X + ball.Radius * 2 <= 800);
        }

        [TestMethod]
        public void UpdatePosition_BallBouncesOffTopWall()
        {
            var logic = CreateLogic();
            logic.AddBall(100, 2, 10, 0, -5);

            logic.UpdatePosition(600, 800);

            var ball = logic.GetBalls()[0];
            Assert.IsTrue(ball.VelY > 0);
            Assert.IsTrue(ball.Y >= 0);
        }

        [TestMethod]
        public void UpdatePosition_BallBouncesOffBottomWall()
        {
            var logic = CreateLogic();
            logic.AddBall(100, 585, 10, 0, 5);

            logic.UpdatePosition(600, 800);

            var ball = logic.GetBalls()[0];
            Assert.IsTrue(ball.VelY < 0);
            Assert.IsTrue(ball.Y + ball.Radius * 2 <= 600);
        }

        [TestMethod]
        public void UpdatePosition_TwoBallsCollide_VelocitiesChange()
        {
            var logic = CreateLogic();
            logic.AddBall(100, 100, 10, 3, 0);
            logic.AddBall(122, 100, 10, -3, 0);

            double velXa = logic.GetBalls()[0].VelX;
            double velXb = logic.GetBalls()[1].VelX;

            logic.UpdatePosition(600, 800);

            var balls = logic.GetBalls();
            Assert.AreNotEqual(velXa, balls[0].VelX);
            Assert.AreNotEqual(velXb, balls[1].VelX);
        }

        [TestMethod]
        public void UpdatePosition_TwoBallsCollide_NoOverlapAfter()
        {
            var logic = CreateLogic();
            logic.AddBall(100, 100, 10, 3, 0);
            logic.AddBall(122, 100, 10, -3, 0);

            logic.UpdatePosition(600, 800);

            var balls = logic.GetBalls();
            double ax = balls[0].X + balls[0].Radius;
            double bx = balls[1].X + balls[1].Radius;
            double ay = balls[0].Y + balls[0].Radius;
            double by = balls[1].Y + balls[1].Radius;

            double dist = Math.Sqrt(Math.Pow(ax - bx, 2) + Math.Pow(ay - by, 2));
            Assert.IsTrue(dist >= balls[0].Radius + balls[1].Radius - 0.01);
        }
    }
}