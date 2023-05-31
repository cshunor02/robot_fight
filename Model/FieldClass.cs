using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robot_fight.Model
{
    public class FieldClass
    {
        private (int, int) _location;
        private int _life;
        private Field? _type;

        public (int, int) Location { get { return _location; } set { _location = value; } }
        public int Life { get { return _life; } set { _life = value; } }
        public Field? Type { get { return _type; } set { _type = value; } }

        public FieldClass(Field field, int x, int y, int life)
        {
            Type = field;
            Life = life;
            Location = (x, y);
        }

    }
}
