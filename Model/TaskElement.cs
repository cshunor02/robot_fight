using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robot_fight.Model
{
    public class TaskElement
    {

        public Colors Color { get { return _color; } set { _color = value; } }
        public int Location { get { return _location; } set { _location = value; } }
        public TaskElement(Colors color, int location)
        {
            _color = color;
            _location = location;
        }

        private Colors _color;
        private int _location;
    }
}
