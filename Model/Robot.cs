using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace robot_fight.Model
{
    public class Robot
    {
        public static int circleTime = 0;
        private (int, int) _location;
        private List<Box> _boxHolding; //list of boxes which are in the "hand" of the robot
        private Direction _faceDirection;
        private bool _freeze; //can the robot freeze (has the ability)
        private int _freezed;  //if it is greater than 0, the robot cannot move
        private String _name;
        private int _r; //view radius
        private List<List<(FieldClass, int)>> _minimap;
        private (int, int) _minimapLoc; //location of robot on the minimap
        private List<(Box, int)> _boxesOnMinimap;  //robot has an own box list, it contains the boxes which are in the memory of the robot
        private int teamNumber;
        private List<List<FieldClass>> _view;  //what the robot see
        private int _groundHeight;
        private int _groundWidth;

        public (int, int) Location { get { return _location; } private set { _location = value; } }
        public List<Box> BoxHolding { get { return _boxHolding; } private set { _boxHolding = value; } }
        public Direction FaceDirection { get { return _faceDirection; } private set { _faceDirection = value; } }
        public bool Freeze { get { return _freeze; } set { _freeze = value; } }
        public int Freezed { get { return _freezed; } set { _freezed = value; } }
        public String Name { get { return _name; } private set { _name = value; } }
        public int R { get { return _r; } private set { _r = value; } }
        public List<List<(FieldClass, int)>> Minimap { get { return _minimap; } private set { _minimap = value; } }

        public List<List<FieldClass>> View { get { return _view; } private set { _view = value; } }

        public List<(Box, int)> BoxesOnMinimap { get { return _boxesOnMinimap; } private set { _boxesOnMinimap = value; } }


        public (int, int) MinimapLoc { get { return _minimapLoc; } private set { _minimapLoc = value; } }

        public int TeamNumber { get; set; }

        public Robot(int team, int loc1_, int loc2_, String name_, Direction faceDir_, int r_, List<List<FieldClass>> ground_, List<Box> boxes_)
        {
            //set values of data members
            TeamNumber = team;
            Location = (loc1_, loc2_);
            FaceDirection = faceDir_;
            Name = name_;
            BoxHolding = new List<Box>();
            Freeze = false;
            Freezed = 0;
            R = r_;
            _minimapLoc = (R, R);
            _minimap = new List<List<(FieldClass, int)>>();
            _view = new List<List<FieldClass>>();
            _groundHeight = ground_.Count;
            _groundWidth = ground_[0].Count;
            _boxesOnMinimap = new List<(Box, int)>();

            //fill _minimap with NoData FieldClasses
            for (int i = 0; i < 2 * R + 1; ++i)
            {
                List<(FieldClass, int)> queue = new List<(FieldClass, int) >();
                for (int j = 0; j < 2 * R + 1; ++j)
                {
                    queue.Add((new FieldClass(Field.NoData, Location.Item1+(i-R), Location.Item2+(j-R), 0), circleTime));
                }
                _minimap.Add(queue);
            }
            //fill _view with NoData FieldClasses
            for (int i = 0; i < 2 * R + 1; ++i)
            {
                List<FieldClass> queue = new List<FieldClass>();
                for (int j = 0; j < 2 * R + 1; ++j)
                {
                    queue.Add(new FieldClass(Field.NoData, i, j, 0));
                }
                _view.Add(queue);
            }

            MinimapSynchronization(ground_, boxes_);
            ViewSynchronization(ground_);
        }

        public void Move(Direction dir_)
        {
            //set the location if the robot moved
            switch (dir_)
            {
                case Direction.Up:
                    Location = (Location.Item1 - 1, Location.Item2);
                    MoveHoldingBoxes(dir_); //move the boxes which are holded by the robot
                    MinimapChanged(dir_); //increase the minimap
                    break;
                case Direction.Down:
                    Location = (Location.Item1 + 1, Location.Item2);
                    MoveHoldingBoxes(dir_);
                    MinimapChanged(dir_);
                    break;
                case Direction.Right:
                    Location = (Location.Item1, Location.Item2 + 1);
                    MoveHoldingBoxes(dir_);
                    MinimapChanged(dir_);
                    break;
                case Direction.Left:
                    Location = (Location.Item1, Location.Item2 - 1);
                    MoveHoldingBoxes(dir_);
                    MinimapChanged(dir_);
                    break;
                default:
                    break;
            }
        }

        public void MoveWithoutBoxes(Direction dir_)
        {
            //set the location if the robot moved, without its boxes
            //(we need it when robots move in convoy)
            switch (dir_)
            {
                case Direction.Up:
                    Location = (Location.Item1 - 1, Location.Item2);
                    MinimapChanged(dir_); //increase the minimap
                    break;
                case Direction.Down:
                    Location = (Location.Item1 + 1, Location.Item2);
                    MinimapChanged(dir_);
                    break;
                case Direction.Right:
                    Location = (Location.Item1, Location.Item2 + 1);
                    MinimapChanged(dir_);
                    break;
                case Direction.Left:
                    Location = (Location.Item1, Location.Item2 - 1);
                    MinimapChanged(dir_);
                    break;
                default:
                    break;
            }
        }

        //move the boxes which are holded by the robot, after the robot moved
        private void MoveHoldingBoxes(Direction dir_)
        {
            HashSet<Box> boxToMove = new HashSet<Box>();
            for (int i = 0; i < _boxHolding.Count; i++)
            {
                
                _boxHolding[i].MakeSetOfBox(boxToMove);
            }
            foreach(Box box in boxToMove)
            {
                box.ChangeLocation(dir_);
            }
        }
        
        //rotate
        public void ChangeFaceDirection(Direction dir_)
        {
            //set FaceDirection
            if (dir_ == Direction.Right)
            {
                switch (FaceDirection)
                {
                    case Direction.Up:
                        FaceDirection = Direction.Right;
                        break;
                    case Direction.Right:
                        FaceDirection = Direction.Down;
                        break;
                    case Direction.Down:
                        FaceDirection = Direction.Left;
                        break;
                    case Direction.Left:
                        FaceDirection = Direction.Up;
                        break;
                    default:
                        break;
                }
            }
            else if (dir_ == Direction.Left)
            {
                switch (FaceDirection)
                {
                    case Direction.Up:
                        FaceDirection = Direction.Left;
                        break;
                    case Direction.Right:
                        FaceDirection = Direction.Up;
                        break;
                    case Direction.Down:
                        FaceDirection = Direction.Right;
                        break;
                    case Direction.Left:
                        FaceDirection = Direction.Down;
                        break;
                    default:
                        break;
                }
            }

            HashSet<Box> boxToMove = new HashSet<Box>(); //which boxes are in connection with the robot
            for (int i = 0; i < _boxHolding.Count; i++)
            {
                _boxHolding[i].MakeSetOfBox(boxToMove);
            }
            //rotate every box
            foreach (Box box in boxToMove)
            {
                BoxRotation(box, dir_);
            }

        }

        //rotate one box
        private void BoxRotation(Box b, Direction dir_)
        {
            int rX = Location.Item1; //first coordinate of robot
            int rY = Location.Item2; //second coordinate of robot
            int bX = b.Location.Item1; //first coordinate of box
            int bY = b.Location.Item2; //second coordinate of box

            int diffX = bX - rX;
            int diffY = bY - rY;
            int newX;
            int newY;

            if (dir_ == Direction.Right)
            {
                newX = rX + diffY;
                newY = rY - diffX;


            }
            else
            {
                newX = rX - diffY;
                newY = rY + diffX;
            }
            b.Location = (newX, newY);


        }

        public void BoxPickUp(Box box_)
        {
            _boxHolding.Add(box_);
        }

        public void BoxDrop(Box box_)
        {
            _boxHolding.Remove(box_);
        }

       
        //robot freeze another robot, it lose its freeze ability
        public void Freezing()
        {
            Freeze = false;
        }

        //(after moving) increase the minimap
        private void MinimapChanged(Direction dir_)
        {
            List<(FieldClass, int)> new_row = new List<(FieldClass, int)>();
            int col_num = _minimap[0].Count;
            int row_num = _minimap.Count;
            switch (dir_)
            {
                case Direction.Up: //add new row to the top of the minimap, if it necessary
                    if (_minimapLoc.Item1 <= R)
                    {
                        for (int i = 0; i < col_num; i++)
                        {
                            new_row.Add((new FieldClass(Field.NoData, Location.Item1-R, Location.Item2+(i-R), 0), circleTime));
                        }
                        _minimap.Insert(0, new_row);
                    }
                    else
                    {
                        _minimapLoc.Item1--; //if we don't have to add a new row, the minimap location change
                    }
                    break;
                case Direction.Down:  //add new row to the bottom side of the minimap, if it necessary
                    if (row_num - _minimapLoc.Item1-1 <= R)
                    {
                        for (int i = 0; i < col_num; i++)
                        {
                            new_row.Add((new FieldClass(Field.NoData, Location.Item1+R, Location.Item2 + (i - R), 0), circleTime));
                        }
                        _minimap.Add(new_row);
                        
                    }
                    _minimapLoc.Item1++; //minimap location change
                    break;
                case Direction.Right:  //add new column to the right side of the minimap, if it necessary
                    if (col_num-_minimapLoc.Item2-1 <= R)
                    {
                        for (int i = 0; i < row_num; i++)
                        {
                            _minimap[i].Add((new FieldClass(Field.NoData, Location.Item1 + (i - R), Location.Item2+R, 0), circleTime));
                        }
                        
                    }
                    _minimapLoc.Item2++;  //minimap location change
                    break;
                case Direction.Left:   //add new column to the left side of the minimap, if it necessary
                    if (_minimapLoc.Item2 <= R)
                    {
                        for (int i = 0; i < row_num; i++)
                        {
                            _minimap[i].Insert(0, (new FieldClass(Field.NoData, Location.Item1 + (i - R), Location.Item2 - R, 0), circleTime));
                        }
                    }
                    else
                    {
                        _minimapLoc.Item2--;   //if we don't have to add new column, minimap location change
                    }
                    
                    break;
                default:
                    break;
            }
        }
        
        public void MinimapSynchronization(List<List<FieldClass>> ground_, List<Box> boxes_)
        {
            int actGroundLocX;
            int actGroundLocY;
            int actMinimapLocX;
            int actMinimapLocY;
            //synchronizate upper half of minimap
            for (int i = 0; i <= R; i++)
            {
                for (int j = -i; j <= i; j++)
                {
                    actGroundLocX = Location.Item1 - R + i; //X coord of the field what we have to check
                    actGroundLocY = Location.Item2 + j; //Y coord of thefield what we have to check
                    actMinimapLocX = _minimapLoc.Item1 - R + i; //X coord of the field what we have to set
                    actMinimapLocY = _minimapLoc.Item2 + j; //Y coord of the field what we have to set
                    MinimapSyncOneField(actGroundLocX, actGroundLocY, actMinimapLocX, actMinimapLocY, ground_, boxes_);
                }
            }
            //synchronizate lower half of minimap
            for (int i = (R - 1); i >= 0; i--)
            {
                for (int j = -i; j <= i; j++)
                {
                    actGroundLocX = Location.Item1 + R - i;
                    actGroundLocY = Location.Item2 + j;
                    actMinimapLocX = _minimapLoc.Item1 + R - i;
                    actMinimapLocY = _minimapLoc.Item2 + j;
                    MinimapSyncOneField(actGroundLocX, actGroundLocY, actMinimapLocX, actMinimapLocY, ground_, boxes_);
                }
            }
        }

        //synchronize one field on minimap based on the corresponding field of the ground
        private void MinimapSyncOneField(int actGroundLocX, int actGroundLocY, int actMinimapLocX, int actMinimapLocY, List<List<FieldClass>> ground_, List<Box> boxes_)
        {
            int groundX = ground_.Count;
            int groundY = ground_[0].Count;
            //check whether the field what we have to check is part of the ground
            if (0 <= (actGroundLocX) && (actGroundLocX) < groundX && 0 <= (actGroundLocY) && (actGroundLocY) < groundY)
            {
                _minimap[actMinimapLocX][actMinimapLocY].Item1.Type = ground_[actGroundLocX][actGroundLocY].Type;
                _minimap[actMinimapLocX][actMinimapLocY].Item1.Location = ground_[actGroundLocX][actGroundLocY].Location;
                (FieldClass, int) newElement = _minimap[actMinimapLocX][actMinimapLocY];
                newElement.Item2 = circleTime;
                _minimap[actMinimapLocX][actMinimapLocY] = newElement;

                //is there a box on the field
                int boxIndex = 0;
                while (boxIndex < boxes_.Count && boxes_[boxIndex].Location != (actGroundLocX, actGroundLocY))
                {
                    boxIndex++;
                }
                if (boxIndex < boxes_.Count) //yes, there is a box, we save it to _boxesOnMinimap
                {
                    _boxesOnMinimap.Add((new Box(actGroundLocX, actGroundLocY, boxes_[boxIndex].Color, boxes_[boxIndex].Life), circleTime));
                }
                else //no, there isn't a box
                {
                    List<(Box, int)> boxesToDelete = new List<(Box, int)>();
                    //is there a box in _boxesOnMinimap at the actual location
                    foreach (var b in _boxesOnMinimap)
                    {
                        if (b.Item1.Location == (actGroundLocX, actGroundLocY))
                        {
                            boxesToDelete.Add(b);
                        }
                    }
                    //if there is a box in _boxesOnMinimap, where we don't see, we delete it
                    foreach (var b in boxesToDelete)
                    {
                        BoxesOnMinimap.Remove(b);
                    }
                }

            }
            else //the field what we want to check is out of the ground
            {
                _minimap[actMinimapLocX][actMinimapLocY].Item1.Type = Field.NoData;
                _minimap[actMinimapLocX][actMinimapLocY].Item1.Location = (-1, -1); 

            }
        }

        //synchronizate the field in the view of the robot
        public void ViewSynchronization(List<List<FieldClass>> ground_)
        {
            int groundX = ground_.Count;
            int groundY = ground_[0].Count;
            //fill the table with no data fields
            for (int i = 0; i < 2 * R + 1; ++i)
            {
                for (int j = 0; j < 2 * R + 1; ++j)
                {
                    _view[i][j].Type = Field.NoData;
                }
            }

            //fill the fields of the table with real data, if robot see it
            //upper half
            for (int i = 0; i <= R; i++)
            {
                for (int j = -i; j <= i; j++)
                {
                    //check whether the field what we have to synchronizate is part of the ground
                    if (0 <= (Location.Item1 - R + i) && (Location.Item1 - R + i) < groundX && 0 <= (Location.Item2 + j) && (Location.Item2 + j) < groundY)
                    {
                        _view[R - R + i][R + j].Type = ground_[Location.Item1 - R + i][Location.Item2 + j].Type;
                        _view[R - R + i][R + j].Life = ground_[Location.Item1 - R + i][Location.Item2 + j].Life;
                        _view[R - R + i][R + j].Location = ground_[Location.Item1 - R + i][Location.Item2 + j].Location;
                    }
                    else
                    {
                        _view[R - R + i][R + j].Type = Field.NoData; //the fields is out of the ground, so we have no data
                    }
                }
            }
            //lower half
            for (int i = (R - 1); i >= 0; i--)
            {
                for (int j = -i; j <= i; j++)
                {
                    //check whether the field what we have to synchronizate is part of the ground
                    if (0 <= (Location.Item1 + R - i) && (Location.Item1 + R - i) < groundX && 0 <= (Location.Item2 + j) && (Location.Item2 + j) < groundY)
                    {
                        _view[R + R - i][R + j].Type = ground_[Location.Item1 + R - i][Location.Item2 + j].Type;
                        _view[R + R - i][R + j].Life = ground_[Location.Item1 + R - i][Location.Item2 + j].Life;
                        _view[R + R - i][R + j].Location = ground_[Location.Item1 + R - i][Location.Item2 + j].Location;
                    }
                    else
                    {
                        _view[R + R - i][R + j].Type = Field.NoData; //the fields is out of the ground, so we have no data
                    }
                }
            }
        }


        public void SynchMinimapWithAnotherRobot(Robot robot_)
        {
            //Synchronization of our minimap with minimap of robot_
            //size sync
            int _col_num = _minimap[0].Count; //our robot
            int _row_num = _minimap.Count; //our robot
            int col_num_ = robot_.Minimap[0].Count; //other robot
            int row_num_ = robot_.Minimap.Count;  //other robot

            //increase the minimap (4 direction)
            int diffX1 = Minimap[0][0].Item1.Location.Item1 - robot_.Minimap[0][0].Item1.Location.Item1; //difference up
            for (int i = 0; i < diffX1; i++)
            {
                GrowMinimap(Direction.Up);
                _minimapLoc.Item1++;
            }
            int diffX2 = robot_.Minimap[row_num_ - 1][0].Item1.Location.Item1 - Minimap[_row_num - 1][0].Item1.Location.Item1; //difference down 
            for (int i = 0; i < diffX2; i++)
            {
                GrowMinimap(Direction.Down);
            }
            int diffY1 = Minimap[0][0].Item1.Location.Item2 - robot_.Minimap[0][0].Item1.Location.Item2; //difference left
            for (int i = 0; i < diffY1; i++)
            {
                GrowMinimap(Direction.Left);
                _minimapLoc.Item2++;

            }
            int diffY2 = robot_.Minimap[0][col_num_-1].Item1.Location.Item2 - Minimap[0][_col_num-1].Item1.Location.Item2; //difference right
            for (int i = 0; i < diffY2; i++)
            {
                GrowMinimap(Direction.Right);
            }


            //data sync
            for (int i = 0; i < _minimap.Count; i++)
            {
                for (int j = 0; j < _minimap[i].Count; j++)
                {
                    if (_minimap[i][j].Item1.Location.Item1 < _groundHeight && _minimap[i][j].Item1.Location.Item1 >= 0 && _minimap[i][j].Item1.Location.Item2 < _groundWidth && _minimap[i][j].Item1.Location.Item2 >= 0)
                    {
                        //check the other robot has information about the actual field
                        bool found = false;
                        int k = 0;
                        int l = 0;
                        while (!found && k < robot_.Minimap.Count)
                        {
                            l = 0;
                            while(!found && l < robot_.Minimap[k].Count)
                            {
                                found = robot_.Minimap[k][l].Item1.Location == _minimap[i][j].Item1.Location;
                                if (!found)
                                {
                                    l++;
                                }
                            }
                            if (!found)
                            {
                                k++;
                            }
                        }
                        if (found) //other robot has information
                        {
                            (FieldClass, int) dataFromRobot_ = robot_.Minimap[k][l];
                            if ((dataFromRobot_.Item2 > _minimap[i][j].Item2 || _minimap[i][j].Item1.Type == Field.NoData) && dataFromRobot_.Item1.Type != Field.NoData)
                            {
                                //set data of our field to data of the other robot, if it has newer information
                                _minimap[i][j].Item1.Type = dataFromRobot_.Item1.Type;
                                var elem = _minimap[i][j];
                                elem.Item2 = dataFromRobot_.Item2;
                                _minimap[i][j] = elem;

                                //check whether we have a box at the actual position
                                int ourBoxIndex = 0;
                                while (ourBoxIndex < _boxesOnMinimap.Count && _boxesOnMinimap[ourBoxIndex].Item1.Location != _minimap[i][j].Item1.Location)
                                {
                                    ourBoxIndex++;
                                }
                                //check whether the other robot has a box at the actual position
                                int otherBoxIndex = 0;
                                while (otherBoxIndex < robot_.BoxesOnMinimap.Count && robot_.BoxesOnMinimap[otherBoxIndex].Item1.Location != _minimap[i][j].Item1.Location)
                                {
                                    otherBoxIndex++;
                                }

                                //if we have a box at the actual position, but the other robot say there isn't a box, we delete it
                                if(ourBoxIndex < _boxesOnMinimap.Count && otherBoxIndex >= robot_.BoxesOnMinimap.Count)
                                {
                                    _boxesOnMinimap.Remove(_boxesOnMinimap[ourBoxIndex]);
                                }
                                //if we haven't a box at the actual position, but the other robot say there is a box, we add it to our list
                                else if(ourBoxIndex >= _boxesOnMinimap.Count && otherBoxIndex < robot_.BoxesOnMinimap.Count)
                                {
                                    _boxesOnMinimap.Add(robot_.BoxesOnMinimap[otherBoxIndex]);
                                }
                                //if both of us has a box, we update our box
                                else if(ourBoxIndex < _boxesOnMinimap.Count && otherBoxIndex < robot_.BoxesOnMinimap.Count)
                                {
                                    _boxesOnMinimap[ourBoxIndex] = robot_.BoxesOnMinimap[otherBoxIndex];
                                }
                            }
                        }
                    }
                    
                }
            }

        }

        //increase minimap to the given diraction
        private void GrowMinimap(Direction dir_)
        {
            List<(FieldClass, int)> new_row = new List<(FieldClass, int)>();
            int col_num = _minimap[0].Count;
            int row_num = _minimap.Count;
            int x;
            int y;
            switch (dir_)
            {
                case Direction.Up: //add new row to the top of the minimap
                    x = _minimap[0][0].Item1.Location.Item1;
                    for (int i = 0; i < col_num; i++)
                    {
                        new_row.Add((new FieldClass(Field.NoData, x-1, _minimap[0][i].Item1.Location.Item2, 0), -1));
                    }
                    _minimap.Insert(0, new_row);
                    break;
                case Direction.Down:   //add new row to the bottom side of the minimap
                    x = _minimap[row_num-1][0].Item1.Location.Item1;
                    for (int i = 0; i < col_num; i++)
                    {
                        new_row.Add((new FieldClass(Field.NoData, x+1, _minimap[0][i].Item1.Location.Item2, 0), -1));
                    }
                    _minimap.Add(new_row);
                    break;
                case Direction.Left:   //add new column to the left side of the minimap
                    y = _minimap[0][0].Item1.Location.Item2;
                    for (int i = 0; i < row_num; i++)
                    {
                        _minimap[i].Insert(0, (new FieldClass(Field.NoData, _minimap[i][0].Item1.Location.Item1, y-1, 0),-1));
                    }
                    break;
                case Direction.Right:   //add new column to the right side of the minimap
                    y = _minimap[0][col_num-1].Item1.Location.Item2;
                    for (int i = 0; i < row_num; i++)
                    {
                        _minimap[i].Add((new FieldClass(Field.NoData, _minimap[i][0].Item1.Location.Item1, y+1, 0), -1));
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
