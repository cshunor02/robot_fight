using robot_fight.Model;
using System.Xml.Linq;

namespace RobotTest
{
    [TestClass]
    public class RobotFightModelTest
    {

        private Robot _testRobot = null!;
        private Ground _testGround = null!;
        private Box _testBox1 = null!;
        private Box _testBox2 = null!;
        private robot_fight.Model.Task _testTask1 = null!;
        private robot_fight.Model.Task _testTask2 = null!;

        [TestInitialize]
        public void Initialize()
        {
            _testGround = new Ground(10, 10);
            _testGround.GenerateGround(10, 10, 2);
            _testGround.NewWater(1, 5);
            _testGround.NewWater(1, 8);
            _testGround.NewWater(2, 2);
            _testGround.NewWater(5, 2);
            _testGround.NewWater(6, 8);
            _testGround.NewWater(7, 3);
            _testGround.NewWater(7, 6);
            _testGround.NewWater(8, 1);
            

            _testGround.NewBox(4, 5, Colors.Red, 2);
            _testGround.NewBox(3, 5, Colors.Green, 2);
            _testBox1 = _testGround.Boxes.ElementAt(0);
            _testBox2 = _testGround.Boxes.ElementAt(1);
            
            _testBox1.MakeConnections(_testBox2);
            _testBox2.MakeConnections(_testBox1);

            _testRobot = new Robot(0, 5, 5, "Robi", Direction.Up, 3, _testGround.Fields, _testGround.Boxes);
        }

        //Testing Robot class
        [TestMethod]
        public void TestMinimapInitialization()
        {
            Assert.AreEqual(_testRobot.Minimap.Count, 2*_testRobot.R+1);
            Assert.AreEqual(_testRobot.Minimap[0].Count, 2*_testRobot.R+1);
            Assert.AreEqual(_testRobot.Minimap[2][3].Item1.Type,Field.Empty);
            Assert.AreEqual(_testRobot.Minimap[2][3].Item2, 0);
            Assert.AreEqual(_testRobot.Minimap[0][3].Item1.Type, Field.Empty);
            Assert.AreEqual(_testRobot.Minimap[0][3].Item2, 0);
            Assert.AreEqual(_testRobot.Minimap[0][2].Item1.Type, Field.NoData);
            Assert.AreEqual(_testRobot.Minimap[0][2].Item2, 0);
            Assert.AreEqual(_testRobot.Minimap[5][4].Item1.Type, Field.Water);
            Assert.AreEqual(_testRobot.Minimap[5][4].Item2, 0);
            Assert.AreEqual(_testRobot.Minimap[3][0].Item1.Type, Field.Water);
            Assert.AreEqual(_testRobot.Minimap[3][0].Item2, 0);
            Assert.AreEqual(_testRobot.BoxesOnMinimap.Count, 2);
            Assert.AreEqual(_testRobot.MinimapLoc, (3, 3));
        }

        [TestMethod]
        public void TestViewInitialization()
        {
            Assert.AreEqual(_testRobot.View.Count, 2 * _testRobot.R + 1);
            Assert.AreEqual(_testRobot.View[0].Count, 2 * _testRobot.R + 1);
            Assert.AreEqual(_testRobot.View[2][3].Type, Field.Empty);
            Assert.AreEqual(_testRobot.View[0][3].Type, Field.Empty);
            Assert.AreEqual(_testRobot.View[0][2].Type, Field.NoData);
            Assert.AreEqual(_testRobot.View[5][4].Type, Field.Water);
            Assert.AreEqual(_testRobot.View[3][0].Type, Field.Water);
        }

        [TestMethod]
        public void TestRobotRotation()
        {
            _testRobot.ChangeFaceDirection(Direction.Left);
            Assert.AreEqual(_testRobot.FaceDirection, Direction.Left);
            _testRobot.ChangeFaceDirection(Direction.Left);
            Assert.AreEqual(_testRobot.FaceDirection, Direction.Down);
            _testRobot.ChangeFaceDirection(Direction.Right);
            Assert.AreEqual(_testRobot.FaceDirection, Direction.Left);
            _testRobot.ChangeFaceDirection(Direction.Right);
            Assert.AreEqual(_testRobot.FaceDirection, Direction.Up);
            _testRobot.ChangeFaceDirection(Direction.Right);
            Assert.AreEqual(_testRobot.FaceDirection, Direction.Right);
            _testRobot.ChangeFaceDirection(Direction.Right);
            Assert.AreEqual(_testRobot.FaceDirection, Direction.Down);

            Assert.AreEqual(_testRobot.Location, (5, 5));
        }

        [TestMethod]
        public void TestRobotMoveWithOutBoxes()
        {
            _testRobot.MoveWithoutBoxes(Direction.Right);
            Assert.AreEqual(_testRobot.Location, (5, 6));
            _testRobot.MoveWithoutBoxes(Direction.Up);
            Assert.AreEqual(_testRobot.Location, (4, 6));
            _testRobot.MoveWithoutBoxes(Direction.Left);
            Assert.AreEqual(_testRobot.Location, (4, 5));
            _testRobot.MoveWithoutBoxes(Direction.Down);
            Assert.AreEqual(_testRobot.Location, (5, 5));

        }

        [TestMethod]
        public void TestRobotMove()
        {
            _testRobot.BoxPickUp(_testBox1);
            _testRobot.Move(Direction.Right);
            Assert.AreEqual(_testRobot.Location, (5, 6));
            Assert.AreEqual(_testBox1.Location, (4,6));
            Assert.AreEqual(_testBox2.Location, (3,6));
            _testRobot.Move(Direction.Up);
            Assert.AreEqual(_testRobot.Location, (4, 6));
            Assert.AreEqual(_testBox1.Location, (3, 6));
            Assert.AreEqual(_testBox2.Location, (2, 6));
            _testRobot.Move(Direction.Left);
            Assert.AreEqual(_testRobot.Location, (4, 5));
            Assert.AreEqual(_testBox1.Location, (3, 5));
            Assert.AreEqual(_testBox2.Location, (2, 5));
            _testRobot.Move(Direction.Down);
            Assert.AreEqual(_testRobot.Location, (5, 5));
            Assert.AreEqual(_testBox1.Location, (4, 5));
            Assert.AreEqual(_testBox2.Location, (3, 5));

        }

        [TestMethod]
        public void TestMinimapSynchAfterMove()
        {
            Assert.AreEqual(_testRobot.Minimap[4][6].Item1.Type, Field.NoData);
            _testRobot.Move(Direction.Right);
            _testRobot.MinimapSynchronization(_testGround.Fields, _testGround.Boxes);
            Assert.AreEqual(_testRobot.Minimap.Count, 2 * _testRobot.R + 1);
            Assert.AreEqual(_testRobot.Minimap[0].Count, 2 * _testRobot.R + 2);
            Assert.AreEqual(_testRobot.MinimapLoc, (3, 4));
            Assert.AreEqual(_testRobot.Minimap[2][3].Item1.Type, Field.Empty);
            Assert.AreEqual(_testRobot.Minimap[0][3].Item1.Type, Field.Empty);
            Assert.AreEqual(_testRobot.Minimap[0][2].Item1.Type, Field.NoData);
            Assert.AreEqual(_testRobot.Minimap[5][4].Item1.Type, Field.Water);
            Assert.AreEqual(_testRobot.Minimap[3][0].Item1.Type, Field.Water);
            Assert.AreEqual(_testRobot.Minimap[4][6].Item1.Type, Field.Water);
            Assert.AreEqual(_testRobot.Minimap[3][7].Item1.Type, Field.Empty);
            Assert.AreEqual(_testRobot.Minimap[4][7].Item1.Type, Field.NoData);

        }

        [TestMethod]
        public void TestViewSynchAfterMove()
        {
            _testRobot.Move(Direction.Right);
            _testRobot.ViewSynchronization(_testGround.Fields);
            Assert.AreEqual(_testRobot.View.Count, 2 * _testRobot.R + 1);
            Assert.AreEqual(_testRobot.View[0].Count, 2 * _testRobot.R + 1);
            Assert.AreEqual(_testRobot.View[2][3].Type, Field.Empty);
            Assert.AreEqual(_testRobot.View[0][3].Type, Field.Empty);
            Assert.AreEqual(_testRobot.View[0][2].Type, Field.NoData);
            Assert.AreEqual(_testRobot.View[5][3].Type, Field.Water);
            Assert.AreEqual(_testRobot.View[3][0].Type, Field.Empty);
            Assert.AreEqual(_testRobot.View[4][5].Type, Field.Water);
            Assert.AreEqual(_testRobot.View[4][6].Type, Field.NoData);
        }

        [TestMethod]
        public void TestFreeze()
        {
            _testRobot.Freeze = true;
            _testRobot.Freezing();
            Assert.IsFalse(_testRobot.Freeze);
        }
        [TestMethod]
        public void TestPickUpAndDropBox()
        {
            Assert.AreEqual(_testRobot.BoxHolding.Count, 0);
            _testRobot.BoxPickUp(_testBox1);
            Assert.AreEqual(_testRobot.BoxHolding.Count, 1);
            Assert.AreEqual(_testRobot.BoxHolding[0], _testBox1);
            _testRobot.BoxDrop(_testBox1);
            Assert.AreEqual(_testRobot.BoxHolding.Count, 0);

        }

        //Testing Task check method
        [TestMethod]
        public void TestAreBoxesIsATaskMethod()
        {
            GameModel gm = new GameModel("test", 10, 10, 20, 40);
            List<robot_fight.Model.Task> tasks = new List<robot_fight.Model.Task>();
            tasks = gm.Tasks;
            tasks[0].Boxes = new List<TaskElement>();
            tasks[0].Boxes.Add(new TaskElement(Colors.Red, 0));
            tasks[0].Boxes.Add(new TaskElement(Colors.Green, 1));
            while (tasks[0].Boxes.Count < 9)
            {
                tasks[0].Boxes.Add(null);
            }
            List<Box> boxes = new List<Box>();
            boxes.Add(new Box(5, 6, Colors.Red, 3));
            boxes.Add(new Box(5, 7, Colors.Green, 3));
            Assert.AreEqual(gm.areBoxesIsATask(boxes), 0);
            boxes.Add(new Box(5, 6, Colors.Red, 3));
            boxes.Add(new Box(5, 7, Colors.Green, 3));
            boxes.Add(new Box(5, 8, Colors.Blue, 3));
            Assert.AreEqual(gm.areBoxesIsATask(boxes), -1);
            boxes.Add(new Box(5, 6, Colors.Red, 3));
            boxes.Add(new Box(5, 7, Colors.Yellow, 3));
            Assert.AreEqual(gm.areBoxesIsATask(boxes), -1);
        }


        //Testing Task class
        [TestMethod]
        public void TestDecreaseStepCount()
        {

            TaskElement testTaskElement1 = new TaskElement(Colors.Blue, 2);
            TaskElement testTaskElement2 = new TaskElement(Colors.Red, 3);
            List<TaskElement> boxes;
            boxes= new List<TaskElement>();
            boxes.Add(testTaskElement1);
            boxes.Add(testTaskElement2);
            int stepCount = 3;
            (int,int) score = (1,100);
            String name = "test";
            _testTask1 = new robot_fight.Model.Task(stepCount, score, name);
            _testTask2 = new robot_fight.Model.Task(stepCount, score, name);
            _testTask1.DecreaseStepCount();
            Assert.AreEqual(_testTask1.StepCount + 1, _testTask2.StepCount);
        }

        //Testing Ground class


    }
}