using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using robot_fight.Model;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RobotTest
{
    [TestClass]
    public class RobotFightGameModelTest
    {

        // TEST CASES 1 [BASIC TEST CASES, with one robot]

        // TOPIC 1: wait with 1 robot
        // TOPIC 2: move with one robot
            // first: little movements, sometimes successful, sometimes not
            // second: make a walk, stepping on empty fields
            // third: make a walk, trying to step on waters
            // fourth: make a walk, trying to step on boxes
            // five: make a walk, trying to step on field where there are other robots
            // six: make a walk with the robot, aiming to go out of the field
        // TOPIC 3: pick up a box with a robot
        // TOPIC 4: put down a box with a robot
        // TOPIC 5: change face direction
        // TOPIC 6: clear field test
        // TOPIC 7: freeze a robot
        // TOPIC 8: attach boxes
        // TOPIC 9: detach boxes

        // TEST CASES 2 [with more robots / convoy / convoys]
        
        // TOPIC 10: play a game with more robots, convoys

        private Robot _testRobot = null!;
        private Box _testBox1 = null!;
        private Box _testBox2 = null!;
        private robot_fight.Model.Task _testTask1 = null!;
        private robot_fight.Model.Task _testTask2 = null!;
        private GameModel _testGameModel1 = null!;
        private Dictionary<(int, int), string> _dictionary = null!;

        [TestInitialize]
        public void InitializeGameModel()
        {
            _testGameModel1 = new GameModel("", 10, 10, 0, 0);

            _testGameModel1.Ground.GenerateGround(10, 10, 2);

            _testGameModel1.Ground.NewWater(1, 5);
            _testGameModel1.Ground.NewWater(1, 8);
            _testGameModel1.Ground.NewWater(2, 2);
            _testGameModel1.Ground.NewWater(6, 8);
            _testGameModel1.Ground.NewWater(7, 3);
            _testGameModel1.Ground.NewWater(7, 6);
            _testGameModel1.Ground.NewWater(8, 1);
            _testRobot = new Robot(0, 5, 5, "Robi", Direction.Up, 3, _testGameModel1.Ground.Fields!, _testGameModel1.Ground.Boxes);
            _testGameModel1.Robots = new List<List<Robot>>();
            _testGameModel1.RobotsStepped = new List<List<bool>>();
            List<Robot> group0 = new List<Robot>();
            List<bool> group0Bool = new List<bool>();
            _testGameModel1.Robots.Add(group0);
            _testGameModel1.RobotsStepped.Add(group0Bool);
            _testGameModel1.Robots[0].Add(_testRobot);
            _testGameModel1.RobotsStepped[0].Add(false);
            

            _testGameModel1.Ground.NewBox(4, 5, Colors.Red, 2);
            _testGameModel1.Ground.NewBox(3, 5, Colors.Green, 2);
            _testBox1 = _testGameModel1.Ground.Boxes.ElementAt(0);
            _testBox2 = _testGameModel1.Ground.Boxes.ElementAt(1);

            _testBox1.MakeConnections(_testBox2);
            _testBox2.MakeConnections(_testBox1);

            _dictionary = new Dictionary<(int, int), string>();
        }

        // TOPIC 1

        [TestMethod]
        public void WaitWithOneRobot()
        {
            _dictionary.Add((0, 0), "Wait");
            _testGameModel1.Step(_dictionary);
            bool[] arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.AreEqual(arrayOfSuccesses.Length, 1);
            Assert.IsTrue(arrayOfSuccesses[0]);
        }

        // TOPIC 2

        // first

        [TestMethod]
        public void MoveWestWithOneRobot()
        {
            _dictionary.Add((0, 0), "Move West");
            _testGameModel1.Step(_dictionary);
            bool[] arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.AreEqual(arrayOfSuccesses.Length, 1);
            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 4));
            _dictionary.Clear();
        }

        [TestMethod]
        public void MoveSouthWithOneRobot()
        {
            _dictionary.Add((0, 0), "Move South");
            _testGameModel1.Step(_dictionary);
            bool[] arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.AreEqual(arrayOfSuccesses.Length, 1);
            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (6, 5));
        }

        [TestMethod]
        public void MoveEastWithOneRobot()
        {
            _dictionary.Add((0, 0), "Move East");
            _testGameModel1.Step(_dictionary);
            bool[] arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.AreEqual(arrayOfSuccesses.Length, 1);
            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 6));
        }

        [TestMethod]
        public void MoveNorthWithOneRobot()
        {
            _dictionary.Add((0, 0), "Move North");
            _testGameModel1.Step(_dictionary);
            bool[] arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.AreEqual(arrayOfSuccesses.Length, 1);
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 5));
        }

        // second

        [TestMethod]
        public void MakeAWalkWithOneRobotOnEmptyWay()
        {
            bool[] arrayOfSuccesses = null!;

            _dictionary.Add((0, 0), "Move West");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 4));
            _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();

            _dictionary.Add((0, 0), "Move West");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 3));
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "Move West");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 2));
            _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();

            _dictionary.Add((0, 0), "Move North");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
                Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (4, 2));
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "Move East");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (4, 3));
            _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();

            _dictionary.Add((0, 0), "Move North");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (3, 3));
            _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();

            _dictionary.Add((0, 0), "Move East");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (3, 4));
            _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();

            _dictionary.Add((0, 0), "Move South");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (4, 4));
            _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();

            _dictionary.Add((0, 0), "Move South");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 4));
            _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();
        }

        // third
        [TestMethod]
        public void TryToStepOnWater()
        {
            _testGameModel1.Ground.NewWater(5, 4);
            _testGameModel1.Ground.NewWater(5, 6);

            bool[] arrayOfSuccesses = null!;

            _dictionary.Add((0, 0), "Move West");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 5));
            _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();

            _dictionary.Add((0, 0), "Move East");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 5));
            _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();

            _dictionary.Add((0, 0), "Move South");
                        _testGameModel1.Step(_dictionary);
                        _testGameModel1.RobotsStepped[0][0] = false;
                        _dictionary.Clear();
                        _testGameModel1.Ground.NewWater(7, 5);
            _dictionary.Add((0, 0), "Move South");
                        _testGameModel1.Step(_dictionary);
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                _testGameModel1.RobotsStepped[0][0] = false;
                Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (6, 5));
                _dictionary.Clear();

            _testGameModel1.Ground.NewWater(5, 5);
            _dictionary.Add((0, 0), "Move North");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (6, 5));
            _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();
        }

        // fourth

        [TestMethod]
        public void TryToStepOnBoxes()
        {
            _testGameModel1.Ground.NewBox(5, 4, Colors.Red, 2);
            _testGameModel1.Ground.NewBox(5, 6, Colors.Red, 2);

            bool[] arrayOfSuccesses = null!;

            _dictionary.Add((0, 0), "Move West");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 5));
            _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();

            _dictionary.Add((0, 0), "Move East");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 5));
            _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();

            _dictionary.Add((0, 0), "Move South");
                _testGameModel1.Step(_dictionary);
                _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();
                _testGameModel1.Ground.NewBox(7, 5, Colors.Red, 2);
            _dictionary.Add((0, 0), "Move South");
                _testGameModel1.Step(_dictionary);
                _testGameModel1.RobotsStepped[0][0] = false;
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (6, 5));
                _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();

            _testGameModel1.Ground.NewBox(5, 5, Colors.Red, 2);
            _dictionary.Add((0, 0), "Move North");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                _testGameModel1.RobotsStepped[0][0] = false;
                Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (6, 5));
                _dictionary.Clear();
        }

        // fifth

        [TestMethod]
        public void MakeAWalkWithOneRobotOnFieldWithOtherRobots()
        {
            Robot _testrobotA = new Robot(0, 5, 4, "RobotA", Direction.Up, 3, _testGameModel1.Ground.Fields!, _testGameModel1.Ground.Boxes);
            Robot _testRobotB = new Robot(0, 5, 6, "RobotB", Direction.Up, 3, _testGameModel1.Ground.Fields!, _testGameModel1.Ground.Boxes);
            _testGameModel1.Robots[0].Add(_testrobotA);
            _testGameModel1.RobotsStepped[0].Add(false);
            _testGameModel1.Robots[0].Add(_testRobotB);
            _testGameModel1.RobotsStepped[0].Add(false);

            bool[] arrayOfSuccesses = null!;

            _dictionary.Add((0, 0), "Move West");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                _testGameModel1.RobotsStepped[0][0] = false;
                Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 5));
                _dictionary.Clear();

            _dictionary.Add((0, 0), "Move East");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                _testGameModel1.RobotsStepped[0][0] = false;
                Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 5));
                _dictionary.Clear();

            _dictionary.Add((0, 0), "Move South");
                _testGameModel1.Step(_dictionary);
                _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();
                    Robot _testRobotC = new Robot(0, 5, 5, "RobotC", Direction.Up, 3, _testGameModel1.Ground.Fields!, _testGameModel1.Ground.Boxes);
                    _testGameModel1.Robots[0].Add(_testRobotC);
                    _testGameModel1.RobotsStepped[0].Add(false);
                _dictionary.Add((0, 0), "Move North");
                _testGameModel1.Step(_dictionary);
                _testGameModel1.RobotsStepped[0][0] = false;
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (6, 5));
                _dictionary.Clear();
        }

        // six

        [TestMethod]
        public void MakeAWalkWithOneRobotOnFieldWithFences()
        {
            bool[] arrayOfSuccesses = null!;

            for(int i = 4; i >= 0; i--)
            {
                _dictionary.Add((0, 0), "Move West");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
                Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, i));
                _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();
            }

            _dictionary.Add((0, 0), "Move West");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, 0));
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            for (int i = 1; i < _testGameModel1.Ground.Width; i++)
            {
                _dictionary.Add((0, 0), "Move East");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
                Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, i));
                _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();
            }

            _dictionary.Add((0, 0), "Move East");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (5, _testGameModel1.Ground.Width - 1));
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            for (int i = 4; i >= 0; i--)
            {
                _dictionary.Add((0, 0), "Move North");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
                Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (i, _testGameModel1.Ground.Width - 1));
                _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();
            }

            _dictionary.Add((0, 0), "Move North");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (0, _testGameModel1.Ground.Width - 1));
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            for (int i = 1; i < _testGameModel1.Ground.Height; i++)
            {
                _dictionary.Add((0, 0), "Move South");
                arrayOfSuccesses = _testGameModel1.Step(_dictionary);
                Assert.IsTrue(arrayOfSuccesses[0]);
                Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (i, _testGameModel1.Ground.Width - 1));
                _testGameModel1.RobotsStepped[0][0] = false;
                _dictionary.Clear();
            }

            _dictionary.Add((0, 0), "Move South");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(_testGameModel1.Robots.ElementAt(0).ElementAt(0).Location == (_testGameModel1.Ground.Height - 1, _testGameModel1.Ground.Width - 1));
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();
        }

        // TOPIC 3

        [TestMethod]
        public void PickUpBoxTest()
        {
            bool[] arrayOfSuccesses = null!;

            _dictionary.Add((0, 0), "BoxPickUp North");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();
            Assert.IsTrue(arrayOfSuccesses[0]);
            _dictionary.Add((0, 0), "BoxPickUp East");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            _dictionary.Add((0, 0), "BoxPickUp South");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            _dictionary.Add((0, 0), "BoxPickUp West");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();
            Assert.IsTrue(arrayOfSuccesses[0] == false);

            _testGameModel1.Ground.NewBox(6, 5, Colors.Red, 2);
            _testBox1 = _testGameModel1.Ground.Boxes.ElementAt(2);
            _dictionary.Add((0, 0), "BoxPickUp South");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _testGameModel1.Ground.NewBox(5, 4, Colors.Red, 2);
            _testBox1 = _testGameModel1.Ground.Boxes.ElementAt(3);
            _dictionary.Add((0, 0), "BoxPickUp West");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _testGameModel1.Ground.NewBox(5, 6, Colors.Red, 2);
            _testBox1 = _testGameModel1.Ground.Boxes.ElementAt(4);
            _dictionary.Add((0, 0), "BoxPickUp East");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            int loc1 = 0;
            int loc2 = 0;
            int loc3 = 0;
            int loc4 = 0;

            foreach(Box b in _testRobot.BoxHolding)
            {
                switch(b.Location)
                {
                    case (4, 5): loc1++; break;
                    case (6, 5): loc2++; break;
                    case (5, 4): loc3++; break;
                    case (5, 6): loc4++; break;
                    default:; break;
                }
            }

            Assert.IsTrue(loc1 == 1 && loc2 == 1 && loc3 == 1 && loc4 == 1);
        }

        // TOPIC 4

        [TestMethod]
        public void PutDownBoxTest()
        {
            bool[] arrayOfSuccesses = null!;

            _dictionary.Add((0, 0), "BoxPickUp North");
            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _testGameModel1.Ground.NewBox(6, 5, Colors.Red, 2);
            _testBox1 = _testGameModel1.Ground.Boxes.ElementAt(2);
            _dictionary.Add((0, 0), "BoxPickUp South");
            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _testGameModel1.Ground.NewBox(5, 4, Colors.Red, 2);
            _testBox1 = _testGameModel1.Ground.Boxes.ElementAt(3);
            _dictionary.Add((0, 0), "BoxPickUp West");
            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _testGameModel1.Ground.NewBox(5, 6, Colors.Red, 2);
            _testBox1 = _testGameModel1.Ground.Boxes.ElementAt(4);
            _dictionary.Add((0, 0), "BoxPickUp East");
            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "BoxPutDown North");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "BoxPutDown East");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "BoxPutDown South");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "BoxPutDown West");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            int loc1 = 0;
            int loc2 = 0;
            int loc3 = 0;
            int loc4 = 0;

            foreach (Box b in _testRobot.BoxHolding)
            {
                switch (b.Location)
                {
                    case (4, 5): loc1++; break;
                    case (6, 5): loc2++; break;
                    case (5, 4): loc3++; break;
                    case (5, 6): loc4++; break;
                    default:; break;
                }
            }

            Assert.IsTrue(loc1 == 0 && loc2 == 0 && loc3 == 0 && loc4 == 0);
        }

        // TOPIC 5

        [TestMethod]
        public void ChangeFaceDirectionTest()
        {
            bool[] arrayOfSuccesses = null!;

            _dictionary.Add((0, 0), "ChangeFaceDirection West");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testRobot.FaceDirection == Direction.Left);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "ChangeFaceDirection East");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testRobot.FaceDirection == Direction.Up);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

        }

        // TOPIC 6

        [TestMethod]
        public void ClearFieldTest()
        {
            bool[] arrayOfSuccesses = null!;

            _dictionary.Add((0, 0), "ClearField North");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "ClearField North");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "ClearField North");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();
            
            _testGameModel1.Ground.NewWater(4, 5);

            _dictionary.Add((0, 0), "ClearField North");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "ClearField North");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "ClearField North");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

        }

        // TOPIC 7

        [TestMethod]
        public void FreezeTest()
        {
            bool[] arrayOfSuccesses = null!;

            Robot _testRobot1 = new Robot(2, 4, 5, "Robi2", Direction.Up, 3, _testGameModel1.Ground.Fields!, _testGameModel1.Ground.Boxes);
            _testGameModel1.Robots[0].Add(_testRobot1);
            _testGameModel1.RobotsStepped[0].Add(false);

            _dictionary.Add((0, 0), "Freeze");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            // We can freeze an other robot only then if we have already taken up the ability to be able to freeze a robot
            Assert.IsTrue(arrayOfSuccesses[0] == false);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            _testGameModel1.Ground.Fields![6][5].Type = Field.Freeze;

            _dictionary.Add((0, 0), "Move South");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testRobot.Location == (6, 5));

            // Now we stepped on a field that was a freeze-field, so until we don't freeze an other robot, we'll have
            // this ability

            _dictionary.Add((0, 0), "Move North");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testRobot.Location == (5, 5));

            _dictionary.Add((0, 0), "Freeze");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            _testGameModel1.RobotsStepped[0][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testRobot.Freezed <= 0);
            Assert.IsTrue(_testRobot1.Freezed > 0);

        }

        // TOPIC 8

        [TestMethod]
        public void AttachBoxes()
        {
            Robot _testRobot10 = new Robot(1, 7, 4, "TestRobot__10", Direction.Up, 3, _testGameModel1.Ground.Fields!, _testGameModel1.Ground.Boxes);
            Robot _testRobot11 = new Robot(1, 7, 5, "TestRobot__11", Direction.Up, 3, _testGameModel1.Ground.Fields!, _testGameModel1.Ground.Boxes);
            List<Robot> group1 = new List<Robot>();
            group1.Add(_testRobot10);
            group1.Add(_testRobot11);
            _testGameModel1.Robots.Add(group1);

            List<bool> group1Bool = new List<bool>();
            _testGameModel1.RobotsStepped.Add(group1Bool);
            _testGameModel1.RobotsStepped[1].Add(false);
            _testGameModel1.RobotsStepped[1].Add(false);

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "AttachBoxes South_East");
            _dictionary.Add((1, 1), "AttachBoxes South_West");

            _testGameModel1.Ground.NewBox(8, 4, Colors.Red, 2);
            _testGameModel1.Ground.NewBox(8, 5, Colors.Green, 2);
            _testBox1 = _testGameModel1.Ground.Boxes.ElementAt(2);
            _testBox2 = _testGameModel1.Ground.Boxes.ElementAt(3);

            bool[] arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(arrayOfSuccesses[1]);
            Assert.IsTrue(arrayOfSuccesses[2]);

            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;

            _dictionary.Clear();

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "AttachBoxes North_East");
            _dictionary.Add((1, 1), "AttachBoxes North_West");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(arrayOfSuccesses[1] == false);

            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;

            _dictionary.Clear();

            Assert.IsTrue(_testGameModel1.Ground.Boxes[2].Connections.Contains(_testGameModel1.Ground.Boxes[3]));

        }

        // TOPIC 9

        [TestMethod]
        public void DetachBoxes()
        {
            Robot _testRobot10 = new Robot(1, 7, 4, "TestRobot__10", Direction.Up, 3, _testGameModel1.Ground.Fields!, _testGameModel1.Ground.Boxes);
            Robot _testRobot11 = new Robot(1, 7, 5, "TestRobot__11", Direction.Up, 3, _testGameModel1.Ground.Fields!, _testGameModel1.Ground.Boxes);
            List<Robot> group1 = new List<Robot>();
            group1.Add(_testRobot10);
            group1.Add(_testRobot11);
            _testGameModel1.Robots.Add(group1);

            List<bool> group1Bool = new List<bool>();
            _testGameModel1.RobotsStepped.Add(group1Bool);
            _testGameModel1.RobotsStepped[1].Add(false);
            _testGameModel1.RobotsStepped[1].Add(false);

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "AttachBoxes South_East");
            _dictionary.Add((1, 1), "AttachBoxes South_West");

            _testGameModel1.Ground.NewBox(8, 4, Colors.Red, 2);
            _testGameModel1.Ground.NewBox(8, 5, Colors.Green, 2);
            _testBox1 = _testGameModel1.Ground.Boxes.ElementAt(2);
            _testBox2 = _testGameModel1.Ground.Boxes.ElementAt(3);

            _testGameModel1.Step(_dictionary);

            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;

            _dictionary.Clear();

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "DetachBoxes South_East");
            _dictionary.Add((1, 1), "DetachBoxes South_West");
            bool[] arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(arrayOfSuccesses[1]);
            Assert.IsFalse(arrayOfSuccesses[2]);

            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;

            _dictionary.Clear();

            Assert.IsTrue(_testGameModel1.Ground.Boxes[2].Connections.Contains(_testGameModel1.Ground.Boxes[3]) == false);
        }

        // TOPIC 10

        // SET UP TABLE

        // TOBI picks up 3 boxes next to him
        // TOBI tries to rotate, but it's unsuccessfull (because of the 4. box next to him)
        // TOBI picks up the 4. box next to him
        // TOBI tries to rotate, now it's successfull
        // BOBI and ROBI attach the two boxes they can
        // BOBI and ROBI each pick up one of the boxes they just attached
        // BOBI and ROBI want to move to east - both would like to go to east, and they can, so the step will be successfull
        // BOBI and ROBI could move to west and south, but BOBI wants to move to west, ROBI wants to move to south - so the step is unsuccessfull
        // We make a convoy - BOBI and ROBI go to south, DODI picks up the box left to him, BOBI picks up all the boxes around him he still hasn't been attached to
        // BOBI wants to go to west, but nobody else wants, so they stay at the place where they are
        // Everybody in BOBI's convoy wants to go to west, and they can, so they go to west
        // BOBI wants to rotate its whole convoy 90 degrees to the left - but convoys can't rotate, so the step will be unsuccessfull
        // DODI puts down the box it holds
        // DODI clears all water that are to the left from the convoy, then goes to the north, so that the convoy can go to the west
        // The convoy moves to the west, but it can go until just 3 steps, because else they would step out from the board table

        [TestMethod]
        public void playGameWithMoreRobots()
        {
            // SET UP TABLE

            Robot _testRobotBobi = new Robot(1, 7, 4, "Bobi", Direction.Up, 0, _testGameModel1.Ground.Fields!, _testGameModel1.Ground.Boxes);
            Robot _testRobotDodi = new Robot(1, 9, 4, "Dodi", Direction.Up, 1, _testGameModel1.Ground.Fields!, _testGameModel1.Ground.Boxes);
            List<Robot> group1 = new List<Robot>();
            group1.Add(_testRobotBobi);
            group1.Add(_testRobotDodi);
            _testGameModel1.Robots.Add(group1);
            Robot _testRobotTobi = new Robot(2, 3, 1, "Tobi", Direction.Up, 0, _testGameModel1.Ground.Fields!, _testGameModel1.Ground.Boxes);
            List<Robot> group2 = new List<Robot>();
            group2.Add(_testRobotTobi);
            _testGameModel1.Robots.Add(group2);

            List<bool> group1Bool = new List<bool>();
            _testGameModel1.RobotsStepped.Add(group1Bool);
            _testGameModel1.RobotsStepped[1].Add(false);
            _testGameModel1.RobotsStepped[1].Add(false);

            List<bool> group2Bool = new List<bool>();
            _testGameModel1.RobotsStepped.Add(group2Bool);
            _testGameModel1.RobotsStepped[2].Add(false);

            _testGameModel1.Ground.NewBox(2, 1, Colors.Green, 2);
            _testGameModel1.Ground.NewBox(3, 0, Colors.Green, 2);
            _testGameModel1.Ground.NewBox(4, 1, Colors.Green, 2);
            _testGameModel1.Ground.NewBox(3, 2, Colors.Green, 2);
            _testGameModel1.Ground.NewBox(4, 4, Colors.Green, 2);
            _testGameModel1.Ground.NewBox(5, 4, Colors.Green, 2);
            _testGameModel1.Ground.NewBox(6, 4, Colors.Green, 2);
            _testGameModel1.Ground.NewBox(8, 4, Colors.Green, 2);
            _testGameModel1.Ground.NewBox(8, 6, Colors.Red, 2);
            _testGameModel1.Ground.NewBox(9, 5, Colors.Red, 2);
            
            _testBox1 = _testGameModel1.Ground.Boxes.ElementAt(0);
            _testBox2 = _testGameModel1.Ground.Boxes.ElementAt(1);
            Box _textBox3 = _testGameModel1.Ground.Boxes.ElementAt(2);
            Box _textBox4 = _testGameModel1.Ground.Boxes.ElementAt(3);
            Box _textBox5 = _testGameModel1.Ground.Boxes.ElementAt(4);
            Box _textBox6 = _testGameModel1.Ground.Boxes.ElementAt(5);
            Box _textBox7 = _testGameModel1.Ground.Boxes.ElementAt(6);
            Box _textBox8 = _testGameModel1.Ground.Boxes.ElementAt(7);
            Box _textBox9 = _testGameModel1.Ground.Boxes.ElementAt(8);
            Box _textBox10 = _testGameModel1.Ground.Boxes.ElementAt(9);
            Box _textBox11 = _testGameModel1.Ground.Boxes.ElementAt(10);
            Box _textBox12 = _testGameModel1.Ground.Boxes.ElementAt(11);

            // TOBI picks up 3 boxes next to him

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "BoxPickUp North");
            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "BoxPickUp East");
            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "BoxPickUp South");
            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testRobotTobi.BoxHolding.Count == 3);

            // TOBI tries to rotate, but it's unsuccessfull (because of the 4. box next to him)

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "ChangeFaceDirection West");
            bool[] arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(arrayOfSuccesses[3] == false);

            // TOBI picks up the 4. box next to him

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "BoxPickUp West");
            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testRobotTobi.BoxHolding.Count == 4);

            // TOBI tries to rotate, now it's successfull

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "ChangeFaceDirection West");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(arrayOfSuccesses[3]);
            Assert.IsTrue(_testRobotTobi.FaceDirection == Direction.Left);

            // BOBI and ROBI attach the two boxes they can

            _dictionary.Add((0, 0), "AttachBoxes West_South");
            _dictionary.Add((1, 0), "AttachBoxes North_North");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();
            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(arrayOfSuccesses[1]);

            Assert.IsTrue(_textBox8.Connections.Count == 1);
            Assert.IsTrue(_textBox9.Connections.Count == 1);

            // BOBI and ROBI each pick up one of the boxes they just attached

            _dictionary.Add((0, 0), "BoxPickUp West");
            _dictionary.Add((1, 0), "BoxPickUp North");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();
            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(arrayOfSuccesses[1]);
            Assert.IsTrue(_testRobot.BoxHolding.Count == 1);
            Assert.IsTrue(_testRobotBobi.BoxHolding.Count == 1);

            // BOBI and ROBI want to move to east - both would like to go to east, and they can, so the step will be successfull

            _dictionary.Add((0, 0), "Move East");
            _dictionary.Add((1, 0), "Move East");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");
            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(_testRobot.Location == (5, 6));
            Assert.IsTrue(_testRobotBobi.Location == (7, 5));
            foreach(Box b in _testRobot.BoxHolding)
            {
                Assert.IsTrue(b.Location == (5, 5));
            }
            foreach (Box b in _testRobotBobi.BoxHolding)
            {
                Assert.IsTrue(b.Location == (6, 5));
            }

            // BOBI and ROBI could move to west and south, but BOBI wants to move to west, ROBI wants to move to south - so the step is unsuccessfull

            _dictionary.Add((0, 0), "Move South");
            _dictionary.Add((1, 0), "Move West");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(arrayOfSuccesses[1] == false);
            Assert.IsTrue(_testRobot.Location == (5, 6));
            Assert.IsTrue(_testRobotBobi.Location == (7, 5));
            foreach (Box b in _testRobot.BoxHolding)
            {
                Assert.IsTrue(b.Location == (5, 5));
            }
            foreach (Box b in _testRobotBobi.BoxHolding)
            {
                Assert.IsTrue(b.Location == (6, 5));
            }

            // We make a convoy - BOBI and ROBI go to south, DODI picks up the box left to him, BOBI picks up all the boxes around him he still hasn't been attached to

            _dictionary.Add((0, 0), "Move South");
            _dictionary.Add((1, 0), "Move South");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(arrayOfSuccesses[1]);

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "BoxPickUp West");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "BoxPickUp East");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "BoxPickUp South");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testRobotBobi.BoxHolding.Count == 4);

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "BoxPickUp East");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testRobotDodi.BoxHolding.Count == 1);
            Assert.IsTrue(_testRobotBobi.BoxHolding.Contains(_testRobotDodi.BoxHolding[0]));

            // BOBI wants to go to west, but nobody else wants, so they stay at the place where they are

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Move West");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(arrayOfSuccesses[1] == false);

            // Everybody in BOBI's convoy wants to go to west, and they can, so they go to west

            _dictionary.Add((0, 0), "Move West");
            _dictionary.Add((1, 0), "Move West");
            _dictionary.Add((1, 1), "Move West");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(arrayOfSuccesses[1]);
            Assert.IsTrue(arrayOfSuccesses[2]);

            Assert.IsTrue(_testRobot.Location == (6, 5));
            Assert.IsTrue(_testRobotBobi.Location == (8, 4));
            Assert.IsTrue(_testRobotDodi.Location == (9, 3));

            Assert.IsTrue(_testRobot.BoxHolding[0].Location == (6, 4));

            int counter = 0;

            foreach(Box b in _testRobotBobi.BoxHolding)
            {
                if(b.Location == (7,4) || b.Location == (8,3) || b.Location == (8,5) || b.Location == (9,4))
                {
                    counter++;
                }
            }

            Assert.IsTrue(counter == 4);

            Assert.IsTrue(_testRobotDodi.BoxHolding[0].Location == (9,4));

            // BOBI wants to rotate its whole convoy 90 degrees to the left - but convoys can't rotate, so the step will be unsuccessfull

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "ChangeFaceDirection West");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(arrayOfSuccesses[1] == false);

            Assert.IsTrue(_testRobot.Location == (6, 5));
            Assert.IsTrue(_testRobotBobi.Location == (8, 4));
            Assert.IsTrue(_testRobotDodi.Location == (9, 3));

            Assert.IsTrue(_testRobot.BoxHolding[0].Location == (6, 4));

            counter = 0;

            foreach (Box b in _testRobotBobi.BoxHolding)
            {
                if (b.Location == (7, 4) || b.Location == (8, 3) || b.Location == (8, 5) || b.Location == (9, 4))
                {
                    counter++;
                }
            }

            Assert.IsTrue(counter == 4);

            // DODI puts down the box it holds

            Assert.IsTrue(_testRobotDodi.BoxHolding[0].Location == (9, 4));

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "BoxPutDown East");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testRobotDodi.BoxHolding.Count == 0);

            // DODI clears all water that are to the left from the convoy, then goes to the north, so that the convoy can go to the west

            // (A) DODI clears the water west to the convoy

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Move West");
            _dictionary.Add((2, 0), "Wait");

            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Move West");
            _dictionary.Add((2, 0), "Wait");

            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testRobotDodi.Location == (9, 1));

            Assert.IsTrue(_testGameModel1.Ground.Fields![8][1].Life == 2);

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "ClearField North");
            _dictionary.Add((2, 0), "Wait");

            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testGameModel1.Ground.Fields![8][1].Life == 1);

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "ClearField North");
            _dictionary.Add((2, 0), "Wait");

            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testGameModel1.Ground.Fields![8][1].Type != Field.Water);

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Move North");
            _dictionary.Add((2, 0), "Wait");

            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Move North");
            _dictionary.Add((2, 0), "Wait");

            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Move East");
            _dictionary.Add((2, 0), "Wait");

            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testRobotDodi.Location == (7, 2));

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "ClearField East");
            _dictionary.Add((2, 0), "Wait");

            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "ClearField East");
            _dictionary.Add((2, 0), "Wait");

            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(_testGameModel1.Ground.Fields![7][3].Type != Field.Water);

            // (B) DODI moves to the north, so the convoy can go to the west

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Move North");
            _dictionary.Add((2, 0), "Wait");

            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            _dictionary.Add((0, 0), "Wait");
            _dictionary.Add((1, 0), "Wait");
            _dictionary.Add((1, 1), "Move North");
            _dictionary.Add((2, 0), "Wait");

            _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            // The convoy moves to the west, but it can go until just 3 steps, because else they would step out from the board table

            _dictionary.Add((0, 0), "Move West");
            _dictionary.Add((1, 0), "Move West");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(arrayOfSuccesses[1]);

            _dictionary.Add((0, 0), "Move West");
            _dictionary.Add((1, 0), "Move West");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(arrayOfSuccesses[1]);

            _dictionary.Add((0, 0), "Move West");
            _dictionary.Add((1, 0), "Move West");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(arrayOfSuccesses[0]);
            Assert.IsTrue(arrayOfSuccesses[1]);

            _dictionary.Add((0, 0), "Move West");
            _dictionary.Add((1, 0), "Move West");
            _dictionary.Add((1, 1), "Wait");
            _dictionary.Add((2, 0), "Wait");

            arrayOfSuccesses = _testGameModel1.Step(_dictionary);
            _testGameModel1.RobotsStepped[0][0] = false;
            _testGameModel1.RobotsStepped[1][0] = false;
            _testGameModel1.RobotsStepped[1][1] = false;
            _testGameModel1.RobotsStepped[2][0] = false;
            _dictionary.Clear();

            Assert.IsTrue(arrayOfSuccesses[0] == false);
            Assert.IsTrue(arrayOfSuccesses[1] == false);

            Assert.IsTrue(_testRobot.Location == (6,2));
            Assert.IsTrue(_testRobotBobi.Location == (8, 1));

            Assert.IsTrue(_testRobot.BoxHolding[0].Location == (6, 1));

            counter = 0;

            foreach (Box b in _testRobotBobi.BoxHolding)
            {
                if (b.Location == (8, 0) || b.Location == (7, 1) || b.Location == (9, 1) || b.Location == (8, 2))
                {
                    counter++;
                }
            }

            Assert.IsTrue(counter == 4);

        }

        //Testing fillRobotsList method
        [TestMethod]
        public void testFillRobotList()
        {
            string robotname = "a,b,c,d,e";
            int numOfTeams = 2;
            GameModel testGM = new GameModel("teszt", 10, 10, 10, 20);
            testGM.Ground.GenerateGround(10, 10, 2);
            testGM.fillRobotsList(robotname, numOfTeams, 3);
            Assert.AreEqual(testGM.Robots[0].Count, 3);
            Assert.AreEqual(testGM.Robots[1].Count, 2);
            Assert.AreEqual(testGM.Robots[0][0].Name, "a" );
            Assert.AreEqual(testGM.Robots[0][1].Name, "c");
            Assert.AreEqual(testGM.Robots[0][2].Name, "e");
            Assert.AreEqual(testGM.Robots[1][0].Name, "b");
            Assert.AreEqual(testGM.Robots[1][1].Name, "d");
        }
    }
}