using System.Xml.Linq;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
//
namespace robot_fight.Model
{
    public class GameModel
    {
        Random rnd = new Random();

        private Ground _ground;
        private List<Robot> _players = new List<Robot>();
        private List<List<Robot>> _robots;
        private List<String> _robotGroupIDs;
        private List<Int32> _scores;
        private List<Task> _tasks;
        private List<(HashSet<Robot>, HashSet<Box>)> _convoys; //list of convoys which are currently on the ground (make easier to check can the robots move)
        private List<List<bool>> _robotsStepped;  //list to store wheter robots moved in a given circle (we have to set every element false before new circle)

        public Ground Ground { get { return _ground; } }
        public List<Robot> Players { get { return _players; } }
        public List<List<Robot>> Robots { get { return _robots; } set { _robots = value; } }
        public List<String> RobotGroupIDs { get { return _robotGroupIDs; } }
        public List<Int32> Scores { get { return _scores; } }
        public List<Task> Tasks { get { return _tasks; } }
        public List<List<bool>> RobotsStepped { get { return _robotsStepped;  } set { _robotsStepped = value; } }
        public GameModel(String roleOfUser, int rowNumber, int colNumber, int minValue, int maxValue)
        {
            _ground = new Ground(rowNumber, colNumber);
            _robots = new List<List<Robot>>();
            _robotGroupIDs = new List<String>();
            _tasks = new List<Task>();
            _scores = new List<int>();
            _convoys = new List<(HashSet<Robot>, HashSet<Box>)>();
            _robotsStepped = new List<List<bool>>();

            _tasks.Add(new Task(rnd.Next(100,200), (minValue, maxValue), "1. Feladat"));
            _tasks.Add(new Task(rnd.Next(100,200), (minValue, maxValue), "2. Feladat"));
        }

        // without belongings [with belongings -> next milestone]
        // we also use regular expressions

        // (int, int), string ... position in the list (not on the board), task
        // we get a dictioniory that includes information about
        // every robot in the _robots list, and what they have to do

        // IMPORTANT! 
        // -> THE REQUESTS HAVE TO BE ORDERED BASED ON THE TIME
        // THEY CAME IN (so if 2. group / 3. robot made a request
        // before 2. group / 2. robot, then (2, 3), request HAS
        // TO BE BEFORE (2, 2), request in the dictionary
        public bool[] Step(Dictionary<(int, int), string> _dictionary)
        {
            bool[] successfullnessOfSteps = new bool[_dictionary.Count];
            for (int i = 0; i < _dictionary.Count; i++)
            {
                successfullnessOfSteps[i] = true;
            }
            int counter = 0;

            foreach (var element in _dictionary)
            {
                var par = element.Key;
                
                bool isThereARobotInTheGroupAWithNumber = _robots.Count > par.Item1;
                if(isThereARobotInTheGroupAWithNumber)
                {
                    isThereARobotInTheGroupAWithNumber = isThereARobotInTheGroupAWithNumber && _robots[par.Item1].Count > par.Item2;
                }
                if (!isThereARobotInTheGroupAWithNumber)
                {
                    successfullnessOfSteps[counter] = false;
                }

                else
                {
                    Robot rtod2 = _robots[element.Key.Item1][element.Key.Item2];
                    if (rtod2.Freezed > 0)
                    {
                        successfullnessOfSteps[counter] = false;
                        rtod2.Freezed--;
                    }
                    else if (_robotsStepped[element.Key.Item1][element.Key.Item2])
                    {
                        successfullnessOfSteps[counter] = true;
                    }
                    else
                    {
                        // What to do

                        switch (element.Value)
                        {
                            case var _ when Regex.IsMatch(element.Value, @"Move (North|West|East|South)"):
                                {
                                    (bool, HashSet<Robot>) move = canMove(_robots[element.Key.Item1][element.Key.Item2], element.Value.Substring("Move ".Length), _dictionary);

                                    if (move.Item1)
                                    {
                                        var pair = element.Key;
                                        Robot r = _robots[element.Key.Item1][element.Key.Item2];
                                        if (move.Item2.Count() == 1)
                                        {
                                            r.Move(makeDirection(element.Value.Substring("Move ".Length)));
                                            _robotsStepped[element.Key.Item1][element.Key.Item2] = true;

                                        }
                                        else
                                        {
                                            int i = 0;

                                            while (i < _convoys.Count && _convoys[i].Item1 != move.Item2)
                                            {
                                                i++;
                                            }
                                            if (i < _convoys.Count)
                                            {
                                                MoveRobotsInConvoy(i, makeDirection(element.Value.Substring("Move ".Length)));
                                                for (int j = 0; j < Robots.Count; j++)
                                                {
                                                    for (int k = 0; k < Robots[j].Count; k++)
                                                    {
                                                        if (_convoys[i].Item1.Contains(Robots[j][k]))
                                                        {
                                                            _robotsStepped[j][k] = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        //Check if Ground is Freeze
                                        if (Ground.Fields[r.Location.Item1][r.Location.Item2].Type == Field.Freeze)
                                        {
                                            Ground.Fields[r.Location.Item1][r.Location.Item2].Type = Field.Empty;
                                            r.Freeze = true;
                                        }
                                    }
                                    else
                                    {
                                        successfullnessOfSteps[counter] = false;
                                    }
                                }; break;

                            case var _ when Regex.IsMatch(element.Value, @"BoxPickUp (North|West|East|South)"):
                                {
                                    var pair = element.Key;
                                    (bool, Box) _canWePickUpBox = canPickUpBox(_robots[element.Key.Item1][element.Key.Item2], element.Value.Substring("BoxPickUp ".Length));
                                    if (_canWePickUpBox.Item1)
                                    {
                                        Box b = _canWePickUpBox.Item2;
                                        RobotPickUpBox(_robots[pair.Item1][pair.Item2], b);
                                    }
                                    else
                                    {
                                        successfullnessOfSteps[counter] = false;
                                    }

                                    _robotsStepped[element.Key.Item1][element.Key.Item2] = true;
                                }; break;

                            case var _ when Regex.IsMatch(element.Value, @"ChangeFaceDirection (North|West|East|South)"):
                                {
                                    var pair = element.Key;
                                    bool canWeRotate = true;
                                    int convoyIndex = 0;
                                    while (convoyIndex < _convoys.Count && !_convoys[convoyIndex].Item1.Contains(_robots[pair.Item1][pair.Item2]))
                                    {
                                        convoyIndex++;
                                    }
                                    if (convoyIndex < _convoys.Count && _convoys[convoyIndex].Item1.Count > 1)
                                    {
                                        canWeRotate = false;
                                    }
                                    if (!canWeRotate)
                                    {
                                        successfullnessOfSteps[counter] = false;
                                    }
                                    else
                                    {
                                        canWeRotate = canRotate(_robots[pair.Item1][pair.Item2], makeDirection(element.Value.Substring("ChangeFaceDirection ".Length)));
                                        if (canWeRotate)
                                        {
                                            _robots[pair.Item1][pair.Item2].ChangeFaceDirection(makeDirection(element.Value.Substring("ChangeFaceDirection ".Length)));
                                        }

                                        successfullnessOfSteps[counter] = canWeRotate;
                                    }
                                    _robotsStepped[element.Key.Item1][element.Key.Item2] = true;

                                }; break;
                            case "Wait":
                                {
                                    _robotsStepped[element.Key.Item1][element.Key.Item2] = true;
                                }; break;
                            case var _ when Regex.IsMatch(element.Value, @"ClearField (North|West|East|South)"):
                                {
                                    int deltay = element.Value.Substring("ClearField ".Length) == "North" ?
                                        -1 : element.Value.Substring("ClearField ".Length) == "South" ? 1 : 0;
                                    int deltax = element.Value.Substring("ClearField ".Length) == "West" ?
                                        -1 : element.Value.Substring("ClearField ".Length) == "East" ? 1 : 0;
                                    var pair = element.Key;
                                    (int, int) location = _robots[pair.Item1][pair.Item2].Location;
                                    successfullnessOfSteps[counter] =
                                    ClearField((location.Item1 + deltay, location.Item2 + deltax));
                                    _robotsStepped[element.Key.Item1][element.Key.Item2] = true;
                                }; break;
                            case "Freeze":
                                {
                                    var pair = element.Key;
                                    Robot r = _robots[pair.Item1][pair.Item2]; //Our robot

                                    if (r.Freeze == false)
                                    {
                                        successfullnessOfSteps[counter] = false;
                                        _robotsStepped[element.Key.Item1][element.Key.Item2] = true;
                                        break;
                                    }

                                    int x = r.Location.Item2;
                                    int y = r.Location.Item1;
                                    switch (r.FaceDirection)
                                    {
                                        case Direction.Up: y--; break;
                                        case Direction.Down: y++; break;
                                        case Direction.Left: x--; break;
                                        case Direction.Right: x++; break;
                                    }

                                    Robot found = null;
                                    for (int i = 0; i < Robots.Count; i++)
                                    {
                                        foreach (Robot robots in Robots[i])
                                            if ((y, x) == robots.Location)
                                            {
                                                found = robots;
                                                break;
                                            }
                                    }
                                    if (found != null)
                                    {
                                        found.Freezed = 3;
                                        r.Freezing();
                                    }

                                    successfullnessOfSteps[counter] = true;
                                    _robotsStepped[element.Key.Item1][element.Key.Item2] = true;
                                }; break;
                            // North_West means that I want to attach the box North
                            // from my robot with the box that I reach this way:
                            // I go one step to North from my robot (here will be
                            // the box I want to attach with the other one), then
                            // I go one step to the West (here will be the other box)
                            case var _ when Regex.IsMatch(element.Value, @"AttachBoxes (North_West|North_East|North_North|West_South|West_West|West_North|East_South|East_East|East_North|South_West|South_South|South_East)"):
                                {

                                    (int, int) locationOfBox1 = boxesToAttach(_robots[element.Key.Item1][element.Key.Item2].Location, element.Value.Substring("AttachBoxes ".Length)).Item1;
                                    (int, int) locationOfBox2 = boxesToAttach(_robots[element.Key.Item1][element.Key.Item2].Location, element.Value.Substring("AttachBoxes ".Length)).Item2;

                                    // We select the "request tuple"
                                    // Based on this request tuple we can decide,
                                    // which boxes should be attached

                                    List<(int, int)> otherRobots = new List<(int, int)>();
                                    foreach (var e in _dictionary)
                                    {
                                        if (e.Key != element.Key && e.Value.Length > "AttachBoxes ".Length)
                                        {
                                            if ((locationOfBox1, locationOfBox2) == boxesToAttach(_robots[e.Key.Item1][e.Key.Item2].Location, _dictionary[e.Key].Substring("AttachBoxes ".Length)) || (locationOfBox2, locationOfBox1) == boxesToAttach(_robots[e.Key.Item1][e.Key.Item2].Location, _dictionary[e.Key].Substring("AttachBoxes ".Length)))
                                            {
                                                otherRobots.Add(e.Key);
                                            }
                                        }
                                    }

                                    if (otherRobots.Count != 0)
                                    {
                                        var pair = element.Key;
                                        Box box1 = null!;
                                        Box box2 = null!;
                                        foreach (Box b in _ground.Boxes)
                                        {
                                            if (b.Location == locationOfBox2)
                                            {
                                                box1 = b;
                                            }
                                            if (b.Location == locationOfBox1)
                                            {
                                                box2 = b;
                                            }
                                        }

                                        Robot robot2 = null!;
                                        foreach (var group in _robots)
                                        {
                                            foreach (Robot robot in group)
                                            {
                                                //if (robot.Location == _robots[stream.ElementAt(0).Item1][stream.ElementAt(0).Item2].Location)
                                                if (robot.Location == _robots[otherRobots[0].Item1][otherRobots[0].Item2].Location)
                                                {
                                                    robot2 = robot;
                                                }
                                            }
                                        }
                                        _robotsStepped[element.Key.Item1][element.Key.Item2] = true;
                                        _robotsStepped[otherRobots[0].Item1][otherRobots[0].Item2] = true;
                                        if (box1 != null! && box2 != null!)
                                        {
                                            MakeConnection(box1, box2, _robots[pair.Item1][pair.Item2], robot2);
                                        }
                                        else
                                        {
                                            successfullnessOfSteps[counter] = false;
                                        }
                                    }
                                    else
                                    {
                                        successfullnessOfSteps[counter] = false;
                                    }

                                }; break;
                            case var _ when Regex.IsMatch(element.Value, @"DetachBoxes (North_West|North_East|North_North|West_South|West_West|West_North|East_South|East_East|East_North|South_West|South_South|South_East)"):
                                {
                                    var pair = element.Key;
                                    (int, int) location1 = _robots[pair.Item1][pair.Item2].Location;
                                    (int, int) location2 = _robots[pair.Item1][pair.Item2].Location;

                                    location1 = boxesToAttach(location1, element.Value.Substring("DetachBoxes ".Length)).Item1;
                                    location2 = boxesToAttach(location2, element.Value.Substring("DetachBoxes ".Length)).Item2;
                                    Box box1 = null!;
                                    Box box2 = null!;
                                    foreach (Box b in _ground.Boxes)
                                    {
                                        if (b.Location == location1)
                                        {
                                            box1 = b;
                                        }
                                        if (b.Location == location2)
                                        {
                                            box2 = b;
                                        }
                                    }
                                    if (box1 != null && box2 != null)
                                    {
                                        if (box1.Connections.Contains(box2))
                                        {
                                            OffConnection(box1, box2);
                                            successfullnessOfSteps[counter] = true;
                                        }
                                        else
                                        {
                                            successfullnessOfSteps[counter] = false;
                                        }
                                    }

                                    _robotsStepped[element.Key.Item1][element.Key.Item2] = true;


                                }; break;
                            case var _ when Regex.IsMatch(element.Value, @"BoxPutDown (North|West|East|South)"):
                                {
                                    var pair = element.Key;
                                    Robot r = _robots[pair.Item1][pair.Item2];

                                    Box box = null!;
                                    String whichDirection = element.Value.Substring("BoxPutDown ".Length);

                                    int deltax = whichDirection == "West" ? -1 : whichDirection == "East" ? 1 : 0;
                                    int deltay = whichDirection == "North" ? -1 : whichDirection == "South" ? 1 : 0;

                                    foreach (Box b in r.BoxHolding)
                                    {
                                        if (b.Location == (r.Location.Item1 + deltay, r.Location.Item2 + deltax))
                                        {
                                            box = b;
                                        }
                                    }

                                    if (box != null)
                                    {
                                        RobotDropBox(r, box);
                                    }
                                    else
                                    {
                                        successfullnessOfSteps[counter] = false;
                                    }
                                    _robotsStepped[element.Key.Item1][element.Key.Item2] = true;
                                }; break;
                            default: successfullnessOfSteps[counter] = false; break;
                        }
                    }
                    counter++;
                    var pair2 = element.Key;

                }
                
            }
            foreach (var group in Robots)
            {
                foreach (Robot r in group)
                {
                    r.ViewSynchronization(Ground.Fields);
                    r.MinimapSynchronization(Ground.Fields, Ground.Boxes);
                    MinimapSynchBetweenRobots(r);
                }
            }
            
            return successfullnessOfSteps;
                
        }

        public (bool, HashSet<Robot>) canMove(Robot r, string whichDirection, Dictionary<(int, int), String> _dictionary)
        {
            bool isRobotInAConvoy = false;
            int index = 0;

            for(int i = 0; i < _convoys.Count && !isRobotInAConvoy; i++)
            {
                if (_convoys[i].Item1.Contains(r))
                {
                    isRobotInAConvoy = true;
                    index = i;
                }
            }

            if (!isRobotInAConvoy)
            {

                List<Box> boxesOfTheConvoy = r.BoxHolding;
                (int, int) locationOfRobot = r.Location;

                bool movedElementsWouldRemainOnTheBoard;
                bool l1;
                bool l2;
                switch (whichDirection)
                {
                    case "North":

                        l1 = locationOfRobot.Item1 - 1 >= 0;
                        l2 = true;

                        foreach (Box b in boxesOfTheConvoy)
                        {
                            l2 = l2 && b.Location.Item1 - 1 >= 0;
                        }
                        
                        movedElementsWouldRemainOnTheBoard = l1 && l2; break;

                    case "East":

                        l1 = locationOfRobot.Item2 + 1 < _ground.Width;
                        l2 = true;

                        foreach (Box b in boxesOfTheConvoy)
                        {
                            l2 = l2 && b.Location.Item2 + 1 < _ground.Width;
                        }

                        movedElementsWouldRemainOnTheBoard = l1 && l2 ; break;

                    case "South":

                        l1 = locationOfRobot.Item1 + 1 < _ground.Height;
                        l2 = true;

                        foreach (Box b in boxesOfTheConvoy)
                        {
                            l2 = l2 && b.Location.Item1 + 1 < _ground.Height;
                        }

                        movedElementsWouldRemainOnTheBoard = l1 && l2 ; break;

                    case "West":

                        l1 = locationOfRobot.Item2 - 1 >= 0;
                        l2 = true;

                        foreach (Box b in boxesOfTheConvoy)
                        {
                            l2 = l2 && b.Location.Item2 - 1 >= 0;
                        }

                        movedElementsWouldRemainOnTheBoard = l1 && l2 ; break;

                    default: movedElementsWouldRemainOnTheBoard = false; break;
                }
                
                if (!movedElementsWouldRemainOnTheBoard)
                {
                    return (false, null!);
                }
                else
                {
                    bool elementsOfWouldArriveOnEmptyField = true;

                    int deltax = whichDirection == "West" ? -1 : whichDirection == "East" ? 1 : 0;
                    int deltay = whichDirection == "North" ? -1 : whichDirection == "South" ? 1 : 0;
                    elementsOfWouldArriveOnEmptyField =
                    elementsOfWouldArriveOnEmptyField &&
                    (_ground.Fields![r.Location.Item1 + deltay]
                    [r.Location.Item2 + deltax].Type == Field.Empty ||
                    _ground.Fields![r.Location.Item1 + deltay]
                    [r.Location.Item2 + deltax].Type == Field.Freeze ||
                    _ground.Fields![r.Location.Item1 + deltay]
                    [r.Location.Item2 + deltax].Type == Field.DropPlace);
                    
                    foreach(Box b in Ground.Boxes)
                    {
                        if (b.Location == (r.Location.Item1 + deltay, r.Location.Item2 + deltax) && !boxesOfTheConvoy.Contains(b))
                        {
                            elementsOfWouldArriveOnEmptyField = false;
                        }
                    }
                    
                    foreach(var group in _robots)
                    {
                        foreach (Robot robotTest in group)
                        {
                            if (robotTest.Location == (r.Location.Item1 + deltay, r.Location.Item2 + deltax))
                            {
                                elementsOfWouldArriveOnEmptyField = false;
                            }
                        }
                    }
                    
                    
                    foreach (Box box in boxesOfTheConvoy)
                    {
                        elementsOfWouldArriveOnEmptyField =
                        elementsOfWouldArriveOnEmptyField &&
                        (_ground.Fields![box.Location.Item1 + deltay]
                        [box.Location.Item2 + deltax].Type == Field.Empty ||
                        _ground.Fields![box.Location.Item1 + deltay]
                        [box.Location.Item2 + deltax].Type == Field.Freeze ||
                        _ground.Fields![box.Location.Item1 + deltay]
                        [box.Location.Item2 + deltax].Type == Field.Door ||
                        _ground.Fields![box.Location.Item1 + deltay]
                        [box.Location.Item2 + deltax].Type == Field.DropPlace);
                        
                        foreach (Box b in Ground.Boxes)
                        {
                            if (b.Location == (box.Location.Item1 + deltay, box.Location.Item2 + deltax) && !boxesOfTheConvoy.Contains(b))
                            {
                                elementsOfWouldArriveOnEmptyField = false;
                            }
                        }
                        
                        foreach (var group in _robots)
                        {
                            foreach (Robot robotTest in group)
                            {
                                if (robotTest.Location == (box.Location.Item1 + deltay, box.Location.Item2 + deltax) && robotTest != r)
                                {
                                    elementsOfWouldArriveOnEmptyField = false;
                                }
                            }
                        }
                        
                    }

                    HashSet<Robot> setOfOneRobot = new HashSet<Robot>();
                    setOfOneRobot.Add(r);

                    return (elementsOfWouldArriveOnEmptyField, setOfOneRobot);
                }
            } else
            {
                HashSet<Robot> robotsOfTheConvoy = _convoys[index].Item1;
                HashSet<Box> boxesOfTheConvoy = _convoys[index].Item2;

                (int, int) locationOfRobot = r.Location;
                foreach (Box box in r.BoxHolding)
                {
                    HashSet<Box> boxSet = new HashSet<Box>();
                    box.MakeSetOfBox(boxSet);
                    foreach(Box b in boxSet)
                    {
                        boxesOfTheConvoy.Add(b);
                    }
                }


                // Now, we have to watch, whether all robots of the
                // robotsOfTheConvoy list want to move in the same direction
                bool allRobotsOfTheConvoyWantToMoveInTheSameDirection = true;
                for(int i = 0; i < _dictionary.Count; i++)
{
                    for (int j = 0; j < _convoys.Count; j++)
                    {
                        if (_convoys[j].Item1.Contains(_robots[_dictionary.ElementAt(i).Key.Item1][_dictionary.ElementAt(i).Key.Item2]))
                        {
                            allRobotsOfTheConvoyWantToMoveInTheSameDirection =
                            allRobotsOfTheConvoyWantToMoveInTheSameDirection &&
                            _dictionary.ElementAt(i).Value == "Move " + whichDirection;
                        }
                    }
                }

                if (!allRobotsOfTheConvoyWantToMoveInTheSameDirection)
                {
                    return (false, null!);
                }
                else
                {
                    bool movedElementsWouldRemainOnTheBoard;
                    bool l1;
                    bool l2;
                    bool l3;
                    switch (whichDirection)
                    {
                        case "North":

                            l1 = locationOfRobot.Item1 - 1 >= 0;
                            l2 = true;
                            l3 = true;
                            foreach (Box b in boxesOfTheConvoy)
                            {
                                l2 = l2 && b.Location.Item1 - 1 >= 0;
                            }
                            foreach (Robot robot in robotsOfTheConvoy)
                            {
                                l3 = l3 && robot.Location.Item1 - 1 >= 0;
                            }
                            movedElementsWouldRemainOnTheBoard = l1 && l2 && l3; break;

                        case "East":

                            l1 = locationOfRobot.Item2 + 1 < _ground.Width;
                            l2 = true;
                            l3 = true;
                            foreach (Box b in boxesOfTheConvoy)
                            {
                                l2 = l2 && b.Location.Item2 + 1 < _ground.Width;
                            }
                            foreach (Robot robot in robotsOfTheConvoy)
                            {
                                l3 = l3 && robot.Location.Item2 + 1 < _ground.Width;
                            }
                            movedElementsWouldRemainOnTheBoard = l1 && l2 && l3; break;

                        case "South":

                            l1 = locationOfRobot.Item1 + 1 < _ground.Height;
                            l2 = true;
                            l3 = true;
                            foreach (Box b in boxesOfTheConvoy)
                            {
                                l2 = l2 && b.Location.Item1 + 1 < _ground.Height;
                            }
                            foreach (Robot robot in robotsOfTheConvoy)
                            {
                                l3 = l3 && robot.Location.Item1 + 1 < _ground.Height;
                            }

                            movedElementsWouldRemainOnTheBoard = l1 && l2 && l3; break;

                        case "West":

                            l1 = locationOfRobot.Item2 - 1 >= 0;
                            l2 = true;
                            l3 = true;
                            foreach (Box b in boxesOfTheConvoy)
                            {
                                l2 = l2 && b.Location.Item2 - 1 >= 0;
                            }
                            foreach (Robot robot in robotsOfTheConvoy)
                            {
                                l3 = l3 && robot.Location.Item2 - 1 >= 0;
                            }

                            movedElementsWouldRemainOnTheBoard = l1 && l2 && l3; break;

                        default: movedElementsWouldRemainOnTheBoard = false; break;
                    }
                    
                    if (!movedElementsWouldRemainOnTheBoard)
                    {
                        return (false, null!);
                    }
                    else
                    {
                        bool elementsOfTheConvoyWouldArriveOnEmptyField = true;

                        int deltax = whichDirection == "West" ? -1 : whichDirection == "East" ? 1 : 0;
                        int deltay = whichDirection == "North" ? -1 : whichDirection == "South" ? 1 : 0;

                        foreach (Robot robot in robotsOfTheConvoy)
                        {
                            elementsOfTheConvoyWouldArriveOnEmptyField =
                            elementsOfTheConvoyWouldArriveOnEmptyField &&
                            (_ground.Fields![robot.Location.Item1 + deltay]
                            [robot.Location.Item2 + deltax].Type == Field.Empty ||
                            _ground.Fields![robot.Location.Item1 + deltay]
                            [robot.Location.Item2 + deltax].Type == Field.Freeze ||
                            _ground.Fields![robot.Location.Item1 + deltay]
                            [robot.Location.Item2 + deltax].Type == Field.DropPlace);

                            foreach (Box b in Ground.Boxes)
                            {
                                if (b.Location == (robot.Location.Item1 + deltay, robot.Location.Item2 + deltax) && !boxesOfTheConvoy.Contains(b))
                                {
                                    elementsOfTheConvoyWouldArriveOnEmptyField = false;
                                }
                            }
                            foreach (var group in _robots)
                            {
                                foreach (Robot robotTest in group)
                                {
                                    if (robotTest.Location == (robot.Location.Item1 + deltay, robot.Location.Item2 + deltax) && !robotsOfTheConvoy.Contains(robotTest))
                                    {
                                        elementsOfTheConvoyWouldArriveOnEmptyField = false;
                                    }
                                }
                            }
                        }


                        foreach (Box box in boxesOfTheConvoy)
                        {
                            elementsOfTheConvoyWouldArriveOnEmptyField =
                            elementsOfTheConvoyWouldArriveOnEmptyField &&
                            (_ground.Fields![box.Location.Item1 + deltay]
                            [box.Location.Item2 + deltax].Type == Field.Empty ||
                            _ground.Fields![box.Location.Item1 + deltay]
                            [box.Location.Item2 + deltax].Type == Field.Freeze ||
                            _ground.Fields![box.Location.Item1 + deltay]
                            [box.Location.Item2 + deltax].Type == Field.Door ||
                            _ground.Fields![box.Location.Item1 + deltay]
                            [box.Location.Item2 + deltax].Type == Field.DropPlace);

                            foreach (Box b in Ground.Boxes)
                            {
                                if (b.Location == (box.Location.Item1 + deltay, box.Location.Item2 + deltax) && !boxesOfTheConvoy.Contains(b))
                                {
                                    elementsOfTheConvoyWouldArriveOnEmptyField = false;
                                }
                            }
                            foreach (var group in _robots)
                            {
                                foreach (Robot robotTest in group)
                                {
                                    if (robotTest.Location == (box.Location.Item1 + deltay, box.Location.Item2 + deltax) && !robotsOfTheConvoy.Contains(robotTest))
                                    {
                                        elementsOfTheConvoyWouldArriveOnEmptyField = false;
                                    }
                                }
                            }
                        }

                        return (elementsOfTheConvoyWouldArriveOnEmptyField,
                        robotsOfTheConvoy);

                    }
                }
            } 
        }

        private (bool, Box) canPickUpBox(Robot r, string whichDirection)
        {
            (int, int) locationOfRobot = r.Location;
            int x = r.Location.Item2;
            int y = r.Location.Item1;
            switch (whichDirection)
            {
                case "North": y--; break;
                case "East": x++; break;
                case "South": y++; break;
                case "West": x--; break;
                default:; break;
            }
            bool isThereABox = isThereABoxWithTheSameLocation(y, x, _ground.Boxes).Item1;
            Box boxToPickUp = isThereABoxWithTheSameLocation(y, x, _ground.Boxes).Item2;

            if (isThereABox) { return (true, boxToPickUp); } else { return (false, null!); }
        }

        private bool canRotate(Robot robot_, Direction whichDirection)
        {
            bool result = true; //can we rotate
            //if the robot is in a convoy with other robots, it cannot rotate
            int convoyIndex = 0;
            while (convoyIndex < _convoys.Count && !_convoys[convoyIndex].Item1.Contains(robot_)) { convoyIndex++; }
            if (convoyIndex < _convoys.Count && _convoys[convoyIndex].Item1.Count > 1) { 
                
                return false; 
            }
            int newLocX;
            int newLocY;
            HashSet<Box> setOfBoxes = new HashSet<Box>();  //set of boxes which are in connection with the robot
            foreach (Box b in robot_.BoxHolding)
            {
                b.MakeSetOfBox(setOfBoxes);
            }
            foreach (Box box in setOfBoxes)
            {
                //set the newLoc of the box
                int rX = robot_.Location.Item1; //first coordinate of robot
                int rY = robot_.Location.Item2; //second coordinate of robot
                int bX = box.Location.Item1; //first coordinate of box
                int bY = box.Location.Item2; //second coordinate of box

                int diffX = bX - rX;
                int diffY = bY - rY;
                

                if (whichDirection == Direction.Right)
                {
                    newLocX = rX + diffY;
                    newLocY = rY - diffX;
                }
                else
                {
                    newLocX = rX - diffY;
                    newLocY = rY + diffX;
                }

                //check whether is it a box at the new location
                foreach(Box b in Ground.Boxes)
                {
                    if (b.Location == (newLocX,newLocY) && !setOfBoxes.Contains(b))
                    {
                        result = false;
                    }
                }

                //check whether is it a robot at the new location
                foreach (var group in _robots)
                {
                    foreach(Robot r in group)
                    {
                        if (r.Location == (newLocX, newLocY))
                        {
                            result = false;
                        }
                    }
                }
                //check (newLocX,newLocY) is empty
                if (Ground.Fields[newLocX][newLocY].Type == Field.Wall || Ground.Fields[newLocX][newLocY].Type == Field.Water)
                {
                    result = false;
                }
            }

            return result;
        }

        private Func<string, Direction> makeDirection = text =>
        {
            return text == "North" ? Direction.Up :
                 text == "East" ? Direction.Right :
                 text == "South" ? Direction.Down :
                 Direction.Left;
        };

        public Func<int, int, List<Box>, (bool, Box)> isThereABoxWithTheSameLocation =
         (_locX, _locY, _boxes) =>
         {
             bool isThereABoxWithTheSameLocation = false;
             Box returnBox = null!;

             foreach (Box box in _boxes)
             {
                 if (box.Location.Item1 == _locX && box.Location.Item2 == _locY)
                 {
                     isThereABoxWithTheSameLocation = true;
                     returnBox = box;
                 }
             }

             return (isThereABoxWithTheSameLocation, returnBox);
         };

        //function to decide is there a box with on a location (minimap verion)
        //return bool and if there is a box, the reference of it, if there isn't a box: null
        public Func<int, int, List<(Box, int)>, (bool, Box)> isThereABoxWithTheSameLocationMinimap =
         (_locX, _locY, _boxes) =>
         {
             bool isThereABoxWithTheSameLocation = false;
             Box returnBox = null!;

             foreach (var box in _boxes)
             {
                 if (box.Item1.Location.Item1 == _locX && box.Item1.Location.Item2 == _locY)
                 {
                     isThereABoxWithTheSameLocation = true;
                     returnBox = box.Item1;
                 }
             }

             return (isThereABoxWithTheSameLocation, returnBox);
         };

        private ((int, int), (int, int)) boxesToAttach((int, int) tuple, String text)
        {
            (int, int) locationOfBox1 = tuple;
            (int, int) locationOfBox2 = tuple;

            switch (text)
            {
                case "North_West":
                    {
                        locationOfBox1.Item1--;
                        locationOfBox2.Item1--;
                        locationOfBox2.Item2--;
                    }; break;
                case "North_East":
                    {
                        locationOfBox1.Item1--;
                        locationOfBox2.Item1--;
                        locationOfBox2.Item2++;
                    }; break;
                case "North_North":
                    {
                        locationOfBox1.Item1--;
                        locationOfBox2.Item1 = locationOfBox2.Item1 - 2;
                    }; break;
                case "West_South":
                    {
                        locationOfBox1.Item2--;
                        locationOfBox2.Item2--;
                        locationOfBox2.Item1 = locationOfBox2.Item1 + 1;
                    }; break;
                case "West_West":
                    {
                        locationOfBox1.Item2--;
                        locationOfBox2.Item2 = locationOfBox2.Item2 - 2;
                    }; break;
                case "West_North":
                    {
                        locationOfBox1.Item2--;
                        locationOfBox2.Item2--;
                        locationOfBox2.Item1 = locationOfBox2.Item1 - 1;
                    }; break;
                case "East_South":
                    {
                        locationOfBox1.Item2++;
                        locationOfBox2.Item2++;
                        locationOfBox2.Item1 = locationOfBox2.Item1 + 1;
                    }; break;
                case "East_East":
                    {
                        locationOfBox1.Item2++;
                        locationOfBox2.Item2 = locationOfBox2.Item2 + 2;
                    }; break;
                case "East_North":
                    {
                        locationOfBox1.Item2++;
                        locationOfBox2.Item2++;
                        locationOfBox2.Item1 = locationOfBox2.Item1 - 1;
                    }; break;
                case "South_West":
                    {
                        locationOfBox1.Item1++;
                        locationOfBox2.Item1++;
                        locationOfBox2.Item2 = locationOfBox2.Item2 - 1;
                    }; break;
                case "South_South":
                    {
                        locationOfBox1.Item1++;
                        locationOfBox2.Item1 = locationOfBox2.Item1 + 2;
                    }; break;
                case "South_East":
                    {
                        locationOfBox1.Item1++;
                        locationOfBox2.Item1++;
                        locationOfBox2.Item2 = locationOfBox2.Item2 + 1;
                    }; break;
                default:; break;
            }

            return (locationOfBox1, locationOfBox2);
        }

        public bool ClearField((int, int) loc)
        {
            bool success = false;
            if (Ground.Fields![loc.Item1][loc.Item2].Type == Field.Water)
            {
                Ground.Fields[loc.Item1][loc.Item2].Life--;
                if (Ground.Fields[loc.Item1][loc.Item2].Life == 0)
                {
                    Ground.Fields[loc.Item1][loc.Item2].Type = Field.Empty;
                }
                success = true;
            }
            foreach (Box boxes in Ground.Boxes)
            {
                if (boxes.Location == loc)
                {
                    boxes.Life--;
                    if (boxes.Life == 0)
                    {
                        Ground.Boxes.Remove(boxes);
                    }
                    success = true;
                    break;
                }
            }
            
            return success;
        }
        private void MakeConnection(Box box1_, Box box2_, Robot robot1_, Robot robot2_)
        {
            box1_.MakeConnections(box2_);   //add the other box the list of connections of the box
            box2_.MakeConnections(box1_);  //add the other box the list of connections of the box

            bool isRobot1InAConvoy = false;
            bool isRobot2InAConvoy = false;
            bool isBox1InAConvoy = false;
            bool isBox2InAConvoy = false;
            int convoyOfRobot1 = -1;
            int convoyOfRobot2 = -1;
            int convoyOfBox1 = -1;
            int convoyOfBox2 = -1;
            //check whether box1,box2,robot1 or robot2 is in a convoy
            for (int i = 0; i < _convoys.Count; i++)
            {
                if (_convoys[i].Item1.Contains(robot1_))
                {
                    isRobot1InAConvoy = true;
                    convoyOfRobot1 = i;
                }
                if (_convoys[i].Item1.Contains(robot2_))
                {
                    isRobot2InAConvoy = true;
                    convoyOfRobot2 = i;
                }
                
                if (_convoys[i].Item2.Contains(box1_))
                {
                    isBox1InAConvoy = true;
                    convoyOfBox1 = i;
                }
                
                if (_convoys[i].Item2.Contains(box2_))
                {
                    isBox2InAConvoy = true;
                    convoyOfBox2 = i;
                }
                
            }

            //if robot1 in convoy: check wheter robot2 is in a convoy --> if yes, nite the convoys, if no: add robot2 to the convoy of robot1
            if (isRobot1InAConvoy && (robot1_.BoxHolding.Contains(box1_) || robot1_.BoxHolding.Contains(box2_)))
            {
                if (isRobot2InAConvoy && (robot2_.BoxHolding.Contains(box1_) || robot2_.BoxHolding.Contains(box2_)))
                {
                    if (convoyOfRobot1 != convoyOfRobot2)
                    {
                        foreach (Robot robot in _convoys[convoyOfRobot2].Item1)
                        {
                            _convoys[convoyOfRobot1].Item1.Add(robot);
                        }
                        foreach (Box box in _convoys[convoyOfRobot2].Item2)
                        {
                            _convoys[convoyOfRobot1].Item2.Add(box);
                        }
                        _convoys.Remove(_convoys[convoyOfRobot2]);
                    }
                    
                } else
                {
                    if (robot2_.BoxHolding.Contains(box1_) || robot2_.BoxHolding.Contains(box2_))
                    {
                        _convoys[convoyOfRobot1].Item1.Add(robot2_);
                    }
                }
                //add boxes to the common convoy
                _convoys[convoyOfRobot1].Item2.Add(box1_);
                _convoys[convoyOfRobot1].Item2.Add(box2_);
            }
            //if robot1 is not in a convoy, but robot2 is, add robot1 to the convoy
            else if (isRobot2InAConvoy && (robot2_.BoxHolding.Contains(box1_) || robot2_.BoxHolding.Contains(box2_)))
            {
                if (robot1_.BoxHolding.Contains(box1_) || robot1_.BoxHolding.Contains(box2_))
                {
                    _convoys[convoyOfRobot2].Item1.Add(robot1_);
                }
                //add boxes to the common convoy
                _convoys[convoyOfRobot2].Item2.Add(box1_);
                _convoys[convoyOfRobot2].Item2.Add(box2_);
            }

            //if box1 in convoy: check wheter box2 is in a convoy --> if yes, nite the convoys, if no: add box2 to the convoy of box1
            else if (isBox1InAConvoy)
            {
                if (isBox2InAConvoy)
                {
                    if (convoyOfBox1 != convoyOfBox2)
                    {
                        foreach (Robot robot in _convoys[convoyOfBox2].Item1)
                        {
                            _convoys[convoyOfBox1].Item1.Add(robot);
                        }
                        foreach (Box box in _convoys[convoyOfBox2].Item2)
                        {
                            _convoys[convoyOfBox1].Item2.Add(box);
                        }
                        _convoys.Remove(_convoys[convoyOfBox2]);
                    }
                    
                }
                else
                {
                    _convoys[convoyOfBox1].Item2.Add(box2_);
                }
                if (robot1_.BoxHolding.Contains(box1_) || robot1_.BoxHolding.Contains(box2_))
                {
                    _convoys[convoyOfBox1].Item1.Add(robot1_);
                }
                if (robot2_.BoxHolding.Contains(box1_) || robot2_.BoxHolding.Contains(box2_))
                {
                    _convoys[convoyOfBox1].Item1.Add(robot2_);
                }
            }
            //if box1 is not in a convoy, but box2 is, add box1 to the convoy
            else if (isBox2InAConvoy)
            {
                if (robot1_.BoxHolding.Contains(box1_) || robot1_.BoxHolding.Contains(box2_))
                {
                    _convoys[convoyOfBox2].Item1.Add(robot1_);
                }
                if (robot2_.BoxHolding.Contains(box1_) || robot2_.BoxHolding.Contains(box2_))
                {
                    _convoys[convoyOfBox2].Item1.Add(robot2_);
                }
            }
            //if robot1, robot2, box1, box2 are not in convoy, make a new convoy
            else
            {
                HashSet<Robot> newConvoyRobot = new HashSet<Robot>();
                if (robot1_.BoxHolding.Contains(box1_) || robot1_.BoxHolding.Contains(box2_))
                {
                    newConvoyRobot.Add(robot1_);
                }
                if (robot2_.BoxHolding.Contains(box1_) || robot2_.BoxHolding.Contains(box2_))
                {
                    newConvoyRobot.Add(robot2_);
                }
                HashSet<Box> newConvoyBox = new HashSet<Box>();
                newConvoyBox.Add(box1_);
                newConvoyBox.Add(box2_);
                _convoys.Add((newConvoyRobot, newConvoyBox));
            }
        }

        private void RobotPickUpBox(Robot robot_, Box box_)
        {
            robot_.BoxPickUp(box_);
            int convoyOfBox = 0;
            //check if the box is in a convoy
            while (convoyOfBox < _convoys.Count && !_convoys[convoyOfBox].Item2.Contains(box_))
            {
                convoyOfBox++;
            }
            if (convoyOfBox < _convoys.Count)
            {
                _convoys[convoyOfBox].Item1.Add(robot_);  //add robot to the convoy
            }
            int convoyOfRobot = 0;
            //check if the robot is in a convoy
            while (convoyOfRobot < _convoys.Count && !_convoys[convoyOfRobot].Item1.Contains(robot_))
            {
                convoyOfRobot++;
            }
            if (convoyOfRobot < _convoys.Count)
            {
                _convoys[convoyOfRobot].Item2.Add(box_);  //add box to the convoy
            }
        }
        private void RobotDropBox(Robot robot_, Box box_)
        {
            robot_.BoxDrop(box_);
            List<Box> boxes = new List<Box>();
            int convoyOfBox = 0;
            //check the box is in a convoy
            while (convoyOfBox < _convoys.Count && !_convoys[convoyOfBox].Item2.Contains(box_))
            {
                convoyOfBox++;
            }
            //if box is in a convoy, we have to remove the robot from it
            if (convoyOfBox < _convoys.Count)
            {
                if (_convoys[convoyOfBox].Item1.Contains(robot_))
                {
                    HashSet<Box> boxesHoldingByRobot = new HashSet<Box>();
                    foreach (Box box in robot_.BoxHolding)
                    {
                        box.MakeSetOfBox(boxesHoldingByRobot);
                    }

                    foreach (Box box in boxesHoldingByRobot)
                    {
                        if (_convoys[convoyOfBox].Item2.Contains(box))
                        {
                            return;
                        }
                    }
                    _convoys[convoyOfBox].Item1.Remove(robot_);
                }
                if (_convoys[convoyOfBox].Item1.Count > 0)
                {
                    return;
                }
            }
            //if robot put down a building at a dropplace, we check whether is it a task
            if (Ground.Fields[robot_.Location.Item1][robot_.Location.Item2].Type == Field.DropPlace)
            {
                HashSet<Box> connectedBox = new HashSet<Box>();  //set of the boxes what the robot dropped
                box_.MakeSetOfBox(connectedBox);
                foreach(Box b in connectedBox)
                {
                    //if the box is at door field, it can be a part of a task, and we have to remove it from the ground
                    if (Ground.Fields[b.Location.Item1][b.Location.Item2].Type == Field.Door)
                    {
                        boxes.Add(b);
                        Ground.Boxes.Remove(b);
                    }
                }
                //if the dropped building is one of the task, the team of the robot gets the point
                int whichTask = areBoxesIsATask(boxes);
                if (whichTask != -1)
                {
                    Scores[robot_.TeamNumber] += Tasks[whichTask].Score;
                }

            }
        }

        //detach boxes
        private void OffConnection(Box box1_, Box box2_) 
        {
            box1_.OffConnections(box2_);
            box2_.OffConnections(box1_);

            HashSet<Box> boxSet1 = new HashSet<Box>();
            box1_.MakeSetOfBox(boxSet1);
            HashSet<Box> boxSet2 = new HashSet<Box>();
            box2_.MakeSetOfBox(boxSet2);

            //check whether the boxes are still in one convoy
            bool hasCommonBox = false;
            foreach (Box box in boxSet1)
            {
                hasCommonBox = hasCommonBox || boxSet2.Contains(box);
            }

            if (hasCommonBox)
            {
                return;
            } 
            //if they don't have common box, we have to unjoint the convoy 
            else 
            {
                int convoyIndex = 0;
                while (convoyIndex < _convoys.Count && !_convoys[convoyIndex].Item2.Contains(box1_))
                {
                    convoyIndex++;
                }
                //create two new convoy
                HashSet<Robot> newRobots1 = new HashSet<Robot>();
                HashSet<Robot> newRobots2 = new HashSet<Robot>();
                foreach (Robot robot in _convoys[convoyIndex].Item1)
                {
                    HashSet<Box> boxesOfCurrentRobot = new HashSet<Box>();
                    foreach (Box box in robot.BoxHolding)
                    {
                        box.MakeSetOfBox(boxesOfCurrentRobot);
                    }
                    bool hasCommonBox1 = false;
                    foreach (Box box in boxSet1)
                    {
                        hasCommonBox1 = hasCommonBox1 || boxesOfCurrentRobot.Contains(box);
                    }
                    if (hasCommonBox1)
                    {
                        newRobots1.Add(robot);
                    } else
                    {
                        newRobots2.Add(robot);
                    }
                }
                //remove the old convoy, and add two new
                _convoys.Remove(_convoys[convoyIndex]);
                if (boxSet1.Count > 1)
                {
                    _convoys.Add((newRobots1, boxSet1));
                }
                if (boxSet2.Count > 1)
                {
                    _convoys.Add((newRobots2, boxSet2));
                }
            }
        }
        public string Stringify()
        {
            string finalmessage = "";

            for (int i = 0; i < Ground.Fields.Count; i++)
            {
                for (int j = 0; j < Ground.Fields[i].Count; j++)
                {
                    //Mező típusa
                    switch (Ground.Fields[i][j].Type)
                    {
                        case Field.NoData:
                            finalmessage += "ND:";
                            break;
                        case Field.Water:
                            finalmessage += "W:";
                            break;
                        case Field.Empty:
                            finalmessage += "E:";
                            break;
                        case Field.Door:
                            finalmessage += "D:";
                            break;
                        case Field.Wall:
                            finalmessage += "L:";
                            break;
                        case Field.DropPlace:
                            finalmessage += "DP:";
                            break;
                        case Field.Freeze:
                            finalmessage += "F:";
                            break;
                        default:
                            finalmessage += "?:";
                            break;
                    }

                    //Van-e itt játékos
                    bool _foundplayer = false;

                    for (int k = 0; k < Robots.Count; k++)
                    {
                        for (int l = 0; l < Robots[k].Count; l++)
                        {
                            Robot r = Robots[k][l];
                            if (r.Location == Ground.Fields[i][j].Location)
                            {
                                _foundplayer = true;
                                finalmessage += "Player:" + r.TeamNumber + ":";
                                switch (r.FaceDirection)
                                {
                                    case Direction.Up:
                                        finalmessage += "Up:";
                                        break;
                                    case Direction.Down:
                                        finalmessage += "Down:";
                                        break;
                                    case Direction.Left:
                                        finalmessage += "Left:";
                                        break;
                                    case Direction.Right:
                                        finalmessage += "Right:";
                                        break;
                                }
                                break;
                            }
                        }
                    }
                    if (!_foundplayer) finalmessage += "NP:";

                    //Van-e itt doboz
                    bool _boxExist = false;
                    foreach (var box in _ground.Boxes)
                    {
                        if (box.Location != Ground.Fields[i][j].Location) continue;

                        _boxExist = true;

                        switch (box.Color)
                        {
                            case Colors.Purple:
                                finalmessage += "Purple-";
                                break;
                            case Colors.Red:
                                finalmessage += "Red-";
                                break;
                            case Colors.Green:
                                finalmessage += "Green-";
                                break;
                            case Colors.Blue:
                                finalmessage += "Blue-";
                                break;
                            case Colors.Yellow:
                                finalmessage += "Yellow-";
                                break;
                            case Colors.Brown:
                                finalmessage += "Brown-";
                                break;
                        }
                        if (box.Connections.Count > 0)
                        {
                            finalmessage += "1-";
                        }
                        else
                        {
                            bool found = false;

                            for (int z = 0; z < Robots.Count; z++)
                            {
                                for (int q = 0; q < Robots.ElementAt(z).Count; q++)
                                {
                                    foreach (Box boxes in Robots[z][q].BoxHolding)
                                    {
                                        if (boxes.Location == box.Location)
                                        {
                                            found = true; break;
                                        }
                                    }
                                }
                            }

                            finalmessage += !found ? "0" : "1";
                        }
                    }
                    if (_boxExist == false)
                    {
                        finalmessage += "NoBox";
                    }

                    finalmessage += "!";
                }
                finalmessage += "%";
            }

            return finalmessage;
        }

        private void MoveRobotsInConvoy(int convoyIndex_, Direction dir_)
        {
            //robots move without their boxes
            foreach (Robot robot in _convoys[convoyIndex_].Item1)
            {
                robot.MoveWithoutBoxes(dir_);
            }
            //boxes move
            foreach (Box box in _convoys[convoyIndex_].Item2)
            {
                box.ChangeLocation(dir_);
            }
        }

        public string RobotStringify(Robot robot)
        {
            string finalmessage = "@clientview_";

            finalmessage += robot.TeamNumber + "_" + robot.Name + "_";

            switch(robot.FaceDirection)
            {
                case Direction.Up:
                    finalmessage += "Up_";
                    break;
                case Direction.Down:
                    finalmessage += "Down_";
                    break;
                case Direction.Left:
                    finalmessage += "Left_";
                    break;
                case Direction.Right:
                    finalmessage += "Right_";
                    break;
            }

            finalmessage += robot.Freezed > 0 ? "F_" : "NF_";

            finalmessage += robot.Freeze ? "Yes_+" : "No_+";

            for(int i = 0; i < robot.View.Count; i++)
            {
                for(int j = 0; j < robot.View.ElementAt(i).Count; j++)
                {
                    //Mező típusa
                    switch (robot.View![i][j].Type)
                    {
                        case Field.NoData:
                            finalmessage += "ND:";
                            break;
                        case Field.Water:
                            finalmessage += "W:";
                            break;
                        case Field.Empty:
                            finalmessage += "E:";
                            break;
                        case Field.Door:
                            finalmessage += "D:";
                            break;
                        case Field.Wall:
                            finalmessage += "L:";
                            break;
                        case Field.DropPlace:
                            finalmessage += "DP:";
                            break;
                        case Field.Freeze:
                            finalmessage += "F:";
                            break;
                        default:
                            finalmessage += "?:";
                            break;
                    }

                    //Van-e itt játékos
                    bool _foundplayer = false;

                    for(int k = 0; k < Robots.Count; k++)
                    {
                        for(int l = 0; l < Robots[k].Count; l++)
                        {
                            Robot r = Robots[k][l];
                            if (r.Location == robot.View[i][j].Location && r != robot)
                            {
                                _foundplayer = true;
                                finalmessage += "Player:" + r.TeamNumber + ":";
                                switch (r.FaceDirection)
                                {
                                    case Direction.Up:
                                        finalmessage += "Up:";
                                        break;
                                    case Direction.Down:
                                        finalmessage += "Down:";
                                        break;
                                    case Direction.Left:
                                        finalmessage += "Left:";
                                        break;
                                    case Direction.Right:
                                        finalmessage += "Right:";
                                        break;
                                }
                                break;
                            }
                        }
                    }
                    if (!_foundplayer) finalmessage += "NP:";

                    //Van-e itt doboz
                    bool _boxExist = false;
                    foreach (var box in _ground.Boxes)
                    {
                        if (box.Location != robot.View[i][j].Location) continue;

                        _boxExist = true;

                        switch (box.Color)
                        {
                            case Colors.Purple:
                                finalmessage += "Purple-";
                                break;
                            case Colors.Red:
                                finalmessage += "Red-";
                                break;
                            case Colors.Green:
                                finalmessage += "Green-";
                                break;
                            case Colors.Blue:
                                finalmessage += "Blue-";
                                break;
                            case Colors.Yellow:
                                finalmessage += "Yellow-";
                                break;
                            case Colors.Brown:
                                finalmessage += "Brown-";
                                break;
                        }
                        if (box.Connections.Count > 0)
                        {
                            finalmessage += "1-";
                        } else
                        {
                            bool found = false;

                            for (int z = 0; z < Robots.Count; z++)
                            {
                                for (int q = 0; q < Robots.ElementAt(z).Count; q++)
                                {
                                    foreach(Box boxes in Robots[z][q].BoxHolding)
                                    {
                                        if (boxes.Location == box.Location)
                                        {
                                            found = true; break;
                                        }
                                    }
                                }
                            }

                            finalmessage += !found ? "0" : "1";
                        }
                    }
                    if (_boxExist == false)
                    {
                        finalmessage += "NoBox";
                    }

                    finalmessage += "!";
                }
                finalmessage += "%";
            }

            //Mi a minimapje
            finalmessage += "#";

            for(int i = 0; i < robot.Minimap.Count; i++)
            {
                for(int j = 0; j < robot.Minimap[0].Count; j++)
                {
                    switch (robot.Minimap[i][j].Item1.Type)
                    {
                        case Field.NoData:
                            finalmessage += "ND/";
                            break;
                        case Field.Water:
                            finalmessage += "W/";
                            break;
                        case Field.Empty:
                            finalmessage += "E/";
                            break;
                        case Field.Wall:
                            finalmessage += "L/";
                            break;
                        case Field.Door:
                            finalmessage += "D/";
                            break;
                        case Field.Freeze:
                            finalmessage += "F/";
                            break;
                        default:
                            finalmessage += "E/";
                            break;
                    }
                    if (i == robot.MinimapLoc.Item1 && j == robot.MinimapLoc.Item2)
                    {
                        finalmessage += "ROBOT/";
                    } else
                    {
                        finalmessage += "NOBOT/";
                    }

                    //(bool, Box) check = isThereABoxWithTheSameLocationMinimap(robot.Location.Item1 + i - robot.MinimapLoc.Item1, robot.Location.Item2 + j - robot.MinimapLoc.Item2, robot.BoxesOnMinimap);
                    (bool, Box) check = isThereABoxWithTheSameLocationMinimap(i + (robot.Location.Item1 - robot.MinimapLoc.Item1), j + (robot.Location.Item2 - robot.MinimapLoc.Item2), robot.BoxesOnMinimap);
                    //(bool, Box) check = isThereABoxWithTheSameLocationMinimap(i,j, robot.BoxesOnMinimap);

                    if (check.Item1 == true)
                    {
                        finalmessage += "BOX*";
                        switch(check.Item2.Color)
                        {
                            case Colors.Purple:
                                finalmessage += "Purple$";
                                break;
                            case Colors.Red:
                                finalmessage += "Red$";
                                break;
                            case Colors.Green:
                                finalmessage += "Green$";
                                break;
                            case Colors.Blue:
                                finalmessage += "Blue$";
                                break;
                            case Colors.Yellow:
                                finalmessage += "Yellow$";
                                break;
                            case Colors.Brown:
                                finalmessage += "Brown$";
                                break;
                        }
                    } else
                    {
                        finalmessage += "NoBox$";
                    }
                }
                finalmessage += "="; //Sor vége
            }

            return finalmessage;
        }

        private void MinimapSynchBetweenRobots(Robot robot_)
        {
           //if there is a robot in the view of the given robot, and they are in the same team, we synchronizate their minimaps
            for (int i = 0; i < robot_.View.Count; i++)
           {
               for (int j = 0; j < robot_.View[i].Count; j++)
               {
                    if (robot_.View[i][j].Type != Field.NoData)
                    {
                        (int, int) loc = robot_.View[i][j].Location;
                        foreach (var group in Robots)
                        {
                            foreach (Robot r in group)
                            {
                                if (r != robot_ && r.TeamNumber == robot_.TeamNumber && r.Location == loc)
                                {
                                    robot_.SynchMinimapWithAnotherRobot(r);
                                }
                            }
                        }
                    }
               }
           }
        }

        public int areBoxesIsATask(List<Box> boxes_)
        {
            if (boxes_.Count == 0)
            {
                return -1;
            }
            bool result = true;
            //search for the first box
            int minX = boxes_[0].Location.Item1;
            int minY = boxes_[0].Location.Item2;
            for (int i = 1; i < boxes_.Count; i++) {
                if (boxes_[i].Location.Item1 < minX)
                {
                    minX = boxes_[i].Location.Item1;
                }
                if (boxes_[i].Location.Item2 < minY)
                {
                    minY = boxes_[i].Location.Item2;
                }
            }
            (int, int) firstBoxOutLoc = (minX, minY);


            //check first task
            int taskElementCounter = 0; //how many boxes we have to have
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (Tasks[0].Boxes[j * 3 + k] != null)
                    {
                        taskElementCounter++;
                        (int, int) searchedBoxLoc = (firstBoxOutLoc.Item1 + j, firstBoxOutLoc.Item2 + k); //which location we have to have a box
                        Box searchedBox = null!;
                        foreach (Box b in boxes_)
                        {
                            if (b.Location == searchedBoxLoc)
                            {
                                searchedBox = b;
                            }
                        }
                        //if we didn't find a box, we don't complete this task
                        if (searchedBox == null)
                        {
                            result = false;
                        }
                        //if we found a box, we can complete this task, check the color
                        else
                        {
                            result = result && Tasks[0].Boxes[j * 3 + k].Color == searchedBox.Color;

                        }
                    }
                }
            }
            //if we have enough, and not more box, and they are in the necessary color, we completed the task
            if (result && boxes_.Count == taskElementCounter)
            {
                return 0; //return the index of the task whihc we completed
            }


            //check second task
            result = true;
            taskElementCounter = 0;   //how many boxes we have to have
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (Tasks[1].Boxes[j * 3 + k] != null)
                    {
                        taskElementCounter++;
                        (int, int) searchedBoxLoc = (firstBoxOutLoc.Item1 + j, firstBoxOutLoc.Item2 + k); //which location we have to have a box
                        Box searchedBox = null!;
                        foreach (Box b in boxes_)
                        {
                            if (b.Location == searchedBoxLoc)
                            {
                                searchedBox = b;
                            }
                        }
                        //if we didn't find a box, we don't complete this task
                        if (searchedBox == null)
                        {
                            result = false;
                        }
                        //if we found a box, we can complete this task, check the color
                        else
                        {
                            result = result && Tasks[1].Boxes[j * 3 + k].Color == searchedBox.Color;
                        }
                    }
                }
            }
            //if we have enough, and not more box, and they are in the necessary color, we complete the task
            if (result && boxes_.Count == taskElementCounter)
            {
                return 1;  //return the index of the task whihc we completed
            }

            return -1; //return -1, if we didn't complete any task
        }

        public void Shuffle()
        {
            foreach (var box in _ground.Boxes)
            {
                int x = rnd.Next(3, _ground.Width - 3);
                int y = rnd.Next(3, _ground.Height - 3);

                bool robotsHolding = true;
                bool inConnection = false;
                if (box.Connections.Count > 0)
                {
                    inConnection = true;
                }
                foreach (var robots in _robots)
                { 
                    foreach (var robot in robots)
                    {
                        HashSet<Box> allBoxHolding = new HashSet<Box>();
                        foreach(Box b in robot.BoxHolding)
                        {
                            b.MakeSetOfBox(allBoxHolding);
                        }
                        if (allBoxHolding.Contains(box) && robotsHolding)
                        { 
                                robotsHolding = false;
                        }
                    }
                }


                if(robotsHolding && !inConnection)
                {

                    bool isFieldEmpty = true;

                    do
                    {
                        x = rnd.Next(3, _ground.Width - 3);
                        y = rnd.Next(3, _ground.Height - 3);
                        isFieldEmpty = true;
                        foreach (var robots in _robots)
                        {
                            foreach (var robot in robots)
                            {
                                if (robot.Location == (x, y) && isFieldEmpty)
                                {
                                    isFieldEmpty = false;
                                }
                            }
                        }


                        foreach (var b in _ground.Boxes)
                        {
                            if (b.Location == (x, y))
                            {
                                isFieldEmpty = false;
                            }
                        }



                    } while (_ground.Fields[x][y].Type != Field.Empty || !isFieldEmpty);

                    box.Location = (x, y);
                }


            }

            for(int i = 0; i < 2; i++) 
            {
                bool isFieldEmpty = true;
                int x;
                int y;

                do
                {
                    x = rnd.Next(3, _ground.Width - 3);
                    y = rnd.Next(3, _ground.Height - 3);
                    isFieldEmpty = true;
                    foreach (var robots in _robots)
                    {
                        foreach (var robot in robots)
                        {
                            if (robot.Location == (x, y) && isFieldEmpty)
                            {
                                isFieldEmpty = false;
                            }
                        }
                    }


                    foreach (var b in _ground.Boxes)
                    {
                        if (b.Location == (x, y))
                        {
                            isFieldEmpty = false;
                        }
                    }



                } while (_ground.Fields[x][y].Type != Field.Empty || !isFieldEmpty);
                _ground.Fields[x][y].Type = Field.Freeze;
            }
        }

        public int GameEnd()
        {
            int winner = -1;
            int winnerScore = 0;
            for (int i = 0; i < Scores.Count; i++)
            {
                if (winnerScore < Scores[i])
                {
                    winnerScore = Scores[i];
                    winner = i;
                }
            }

            return winner;
        }

        //initialize the list of robots and scores
        public void fillRobotsList(string robotname_, int numOfTeams_, int gamerView_)
        {
            string[] robotnames = robotname_.Split(',');

            for (int i = 0; i < robotnames.Length; i++)
            {
                Robots.Add(new List<Robot>());
                Scores.Add(0);
            }

            int nextTeam = 0;

            for (int i = 0; i < robotnames.Length; i++)
            {
                //genarete random location
                int x, y;
                bool isFieldEmpty;
                do
                {
                    x = rnd.Next(3, Ground.Height - 3);
                    y = rnd.Next(3, Ground.Width - 3);
                    isFieldEmpty = true;
                    //check whether random field is empty
                    if (Ground.Fields[x][y].Type != Field.Empty)
                    {
                        isFieldEmpty = false;
                    }

                    //check whether is a robot at the random field
                    int j = 0;
                    int k = 0;
                    while (isFieldEmpty && j < Robots.Count)
                    {
                        k = 0;
                        while (isFieldEmpty && k < Robots[j].Count)
                        {
                            if (Robots[j][k].Location == (x, y))
                            {
                                isFieldEmpty = false;
                            }
                            k++;
                        }
                        j++;
                    }
                    // check whether is a box at the random field
                    j = 0;
                    while (isFieldEmpty && j < Ground.Boxes.Count)
                    {
                        if (Ground.Boxes[j].Location == (x, y))
                        {
                            isFieldEmpty = false;
                        }
                        j++;
                    }
                } while (!isFieldEmpty);
                //if we have an empty random field, we create a new robot
                Robot robot = new Robot(nextTeam, x, y, robotnames[i], Direction.Up, gamerView_, Ground.Fields, Ground.Boxes);
                Robots[nextTeam].Add(robot);
                nextTeam++;
                if (nextTeam == numOfTeams_)
                {
                    nextTeam = 0;
                }
            }
        }


        public void createRandomBoxes(Task task, int life)
        {
            //the parameter is the new task, i need the boxes from it.
            List<Box> boxes = new List<Box>();
            foreach (var taskBox in task.Boxes)
            {
                int x = 5;
                int y = 5;
                bool isFieldEmpty = true;

                do
                {
                    x = rnd.Next(3, _ground.Height - 3);
                    y = rnd.Next(3, _ground.Width - 3);
                    isFieldEmpty = true;
                    foreach (var robots in _robots)
                    {
                        foreach (var robot in robots)
                        {
                            if (robot.Location == (x, y) && isFieldEmpty)
                            {
                                isFieldEmpty = false;
                            }
                        }
                    }

                    foreach (var b in _ground.Boxes)
                    {
                        if (b.Location == (x, y))
                        {
                            isFieldEmpty = false;
                        }
                    }
                } while (_ground.Fields[x][y].Type != Field.Empty || !isFieldEmpty);

                if (taskBox != null)
                {
                    _ground.Boxes.Add(new Box(x, y, taskBox.Color, life));
                }
            }
            
        }

        public void createRandomWaters(int life)
        {
            int x = 5;
            int y = 5;
            bool isFieldEmpty = true;


            for (int i = 0; i < _ground.Width / 2; i++) 
            {
                do
                {
                    x = rnd.Next(3, _ground.Width - 3);
                    y = rnd.Next(3, _ground.Height - 3);
                    isFieldEmpty = true;
                    foreach (var robots in _robots)
                    {
                        foreach (var robot in robots)
                        {
                            if (robot.Location == (x, y) && isFieldEmpty)
                            {
                                isFieldEmpty = false;
                            }
                        }
                    }


                    foreach (var b in _ground.Boxes)
                    {
                        if (b.Location == (x, y))
                        {
                            isFieldEmpty = false;
                        }
                    }



                } while (_ground.Fields[x][y].Type != Field.Empty || !isFieldEmpty);
                _ground.Fields[x][y].Type = Field.Water;
                _ground.Fields[x][y].Life = life;
            }
            
        }

    }
}