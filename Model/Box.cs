using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robot_fight.Model
{
    public class Box
    {
        private (int, int) _location;
        private Colors _color;
        private HashSet<Box> _connections;
        private int _life;

        public (int, int) Location { get { return _location; } set { _location = value; } }
        public Colors Color { get { return _color; } private set { _color = value; } }
        public HashSet<Box> Connections { get { return _connections; } private set { _connections = value; } }
        //store the box references which are in connection with this box
        public int Life { get { return _life; } set { _life = value; } }

        public Box(int coord1_, int coord2_, Colors color_, int life_)
        {
            //set the fields
            Location = (coord1_, coord2_);
            Color = color_;
            Connections = new HashSet<Box>();
            Life = life_;
        }

        public void ChangeLocation(Direction dir_)
        {
            //set the location if the box move
            switch (dir_)
            {
                case Direction.Up:
                    Location = (Location.Item1 - 1, Location.Item2);
                    break;
                case Direction.Down:
                    Location = (Location.Item1 + 1, Location.Item2);
                    break;
                case Direction.Right:
                    Location = (Location.Item1, Location.Item2 + 1);
                    break;
                case Direction.Left:
                    Location = (Location.Item1, Location.Item2 - 1);
                    break;
                default:
                    break;
            }
        }

        public void MakeConnections(Box box_)
        {
            Connections.Add(box_);
        }

        public void OffConnections(Box box_)
        {
            Connections.Remove(box_);
        }

        //make a box set of every box which is in connection with this box
        public void MakeSetOfBox(HashSet<Box> set_)
        {
            set_.Add(this);
            for (int i = 0; i < Connections.Count; i++)
            {
                foreach(var elem in Connections)
                {
                    if (!set_.Contains(elem))
                    {
                        elem.MakeSetOfBox(set_);
                    }
                }
            }
        }
    }
}
