using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using static robot_fight.Model.Field;

namespace robot_fight.Model
{
    public class Ground
    {
        private const int exitWidth = 3;
        private List<List<FieldClass>>? _fields = new List<List<FieldClass>>();
        private List<Box>? _boxes = new List<Box>();
        private int _width;
        private int _height;
        private Random _random = new Random();

        public int Width { get { return _width; } set { _width = value; } }
        public int Height { get { return _height; } set { _height = value; } }
        public List<List<FieldClass>>? Fields { get { return _fields; } set { _fields = value; } }
        public List<Box> Boxes { get { return _boxes; } set { _boxes = value; } }
        public int ExitWidth { get { return exitWidth; } }

        public Ground(int width, int height)
        {
            this._width = width + 6;
            this._height = height + 6;
        }

        public void GenerateGround(int width, int height, int life)
        {
            width = width + 6;
            height = height + 6;
            for (int i = 0; i < width; i++)
            {
                Fields?.Add(new List<FieldClass>());
                for (int j = 0; j < height; j++)
                {
                    Fields?.ElementAt(i).Add(new FieldClass(Field.Empty, i, j, life));
                }
            }
        }

        public bool isItWall(int x, int y)
        {
            return x == 0 || x == 1 ||x == 2 || x == Height-1 || x == Height - 2 ||
                x == Height - 3 || y == 0 || y == 1 || y == 2 ||
                y == Width-1 || y == Width - 2 || y == Width - 3;
        }

        public void GenerateGround(int width, int height, int numOfWater, int life, int exitQuantity)
        {
            int limitCounter = exitQuantity * 4;
            int exitCount = 0;
            width = width + 6;
            height = height + 6;
            for (int i = 0; i < width; i++)
            {
                Fields?.Add(new List<FieldClass>());
                for (int j = 0; j < height; j++)
                {
                    if(isItWall(i, j))
                    {
                        Fields?.ElementAt(i).Add(new FieldClass(Field.Wall, i, j, life));

                    } else
                    {
                        Fields?.ElementAt(i).Add(new FieldClass(Field.Empty, i, j, life));
                    }
                }
            }

            while (exitCount < exitQuantity && limitCounter > 0)
            {
                int whichSide = _random.Next(0, 4);
                int x;
                int y;
                int count = 0;
                switch (whichSide)
                {
                    case 0:
                        do
                        {
                            x = 0;
                            y = _random.Next(3, Width - 6);
                            count++;
                        } while (Fields[x][y].Type != Wall && Fields[x][y + 1].Type != Wall && Fields[x][y + 2].Type != Wall && count < 10);

                        if (count == 10)
                        {
                            exitCount--;
                        }

                        Fields[x][y].Type = Field.Door;
                        Fields[x][y + 1].Type = Field.Door;
                        Fields[x][y + 2].Type = Field.Door;
                        Fields[x + 1][y].Type = Field.Door;
                        Fields[x + 1][y + 1].Type = Field.Door;
                        Fields[x + 1][y + 2].Type = Field.Door;
                        Fields[x + 2][y].Type = Field.Door;
                        Fields[x + 2][y + 1].Type = Field.Door;
                        Fields[x + 2][y + 2].Type = Field.Door;
                        Fields[x + 3][y].Type = Field.DropPlace;
                        Fields[x + 3][y + 1].Type = Field.DropPlace;
                        Fields[x + 3][y + 2].Type = Field.DropPlace;
                        break;
                    case 1:   
                        do
                        {
                            y = Width - 1;
                            x = _random.Next(3, Height - 6);
                            count++;
                        } while (Fields[x][y].Type != Wall && Fields[x+1][y].Type != Wall && Fields[x+2][y].Type != Wall && count < 10);

                        if (count == 10)
                        {
                            exitCount--;
                        }

                        Fields[x][y].Type = Field.Door;
                        Fields[x + 1][y].Type = Field.Door;
                        Fields[x + 2][y].Type = Field.Door;
                        Fields[x][y - 1].Type = Field.Door;
                        Fields[x + 1][y - 1].Type = Field.Door;
                        Fields[x + 2][y - 1].Type = Field.Door;
                        Fields[x][y - 2].Type = Field.Door;
                        Fields[x + 1][y - 2].Type = Field.Door;
                        Fields[x + 2][y - 2].Type = Field.Door;
                        Fields[x][y - 3].Type = Field.DropPlace;
                        Fields[x + 1][y - 3].Type = Field.DropPlace;
                        Fields[x + 2][y - 3].Type = Field.DropPlace;
                        break;
                    case 2:
                        do
                        {
                            x = Height - 1;
                            y = _random.Next(3, Width - 6);
                            count++;
                        } while (Fields[x][y].Type != Wall && Fields[x][y+1].Type != Wall && Fields[x][y+2].Type != Wall && count < 10);

                        if (count == 10)
                        {
                            exitCount--;
                        }

                        Fields[x][y].Type = Field.Door;
                        Fields[x][y + 1].Type = Field.Door;
                        Fields[x][y + 2].Type = Field.Door;
                        Fields[x - 1][y].Type = Field.Door;
                        Fields[x - 1][y + 1].Type = Field.Door;
                        Fields[x - 1][y + 2].Type = Field.Door;
                        Fields[x - 2][y].Type = Field.Door;
                        Fields[x - 2][y + 1].Type = Field.Door;
                        Fields[x - 2][y + 2].Type = Field.Door;
                        Fields[x - 3][y].Type = Field.DropPlace;
                        Fields[x - 3][y + 1].Type = Field.DropPlace;
                        Fields[x - 3][y + 2].Type = Field.DropPlace;
                        break;
                    case 3:         
                        do
                        {
                            y = 0;
                            x = _random.Next(3, Height - 6);
                            count++;
                        } while (Fields[x][y].Type != Wall && Fields[x+1][y].Type != Wall && Fields[x+2][y].Type != Wall && count < 10);

                        if (count == 10)
                        {
                            exitCount--;
                        }

                        Fields[x][y].Type = Field.Door;
                        Fields[x + 1][y].Type = Field.Door;
                        Fields[x + 2][y].Type = Field.Door;
                        Fields[x][y + 1].Type = Field.Door;
                        Fields[x + 1][y + 1].Type = Field.Door;
                        Fields[x + 2][y + 1].Type = Field.Door;
                        Fields[x][y + 2].Type = Field.Door;
                        Fields[x + 1][y + 2].Type = Field.Door;
                        Fields[x + 2][y + 2].Type = Field.Door;
                        Fields[x][y + 3].Type = Field.DropPlace;
                        Fields[x + 1][y + 3].Type = Field.DropPlace;
                        Fields[x + 2][y + 3].Type = Field.DropPlace;
                        break;

                    default:
                        break;
                }
                exitCount++;
                limitCounter--;
            }

            for (int i = 0; i < numOfWater; i++)
            {
                int x;
                int y;
                do
                {
                    x = _random.Next(3, width - 3);
                    y = _random.Next(3, height - 3);
                    
                } while (Fields[x][y].Type != Field.Empty);
                Fields[x][y].Type = Field.Water;
            }
        }

        public void GenerateNewBoxes(int quantity)
        {
            int x;
            int y;
            int randomColor;
            for (int i = 0; i < quantity; i++)
            {
                bool success;
                do
                {
                    success = true;
                    x = _random.Next(3, Width - 3);
                    y = _random.Next(3, Height - 3);
                    foreach (Box b in Boxes)
                    {
                        if(b.Location == (x,y))
                        {
                            success = false;
                            break;
                        }
                    }
                } while (Fields[x][y].Type != Field.Empty || !success);
                randomColor = _random.Next(0, 5);
                Colors color = (Colors)randomColor;
                NewBox(x, y, color, 2);
                
                //this will generate new boxes at the beggining, based on the tasks.After that we won't use it because random generated boxes will be enough to create.
            }
        }

        public bool NewWater(int x, int y)
        {
            Fields.ElementAt(x)[y].Type = Field.Water;
            return true;
        }

        public bool NewBox(int x, int y, Colors color, int life)
        {
            Array values = Enum.GetValues(typeof(Colors));
            _boxes.Add(new Box(x, y, color, life));

            return true;
        }

        public void NewDoor(int x, int y)
        {
            Fields.ElementAt(x)[y].Type = Field.Door;

        }
    }
}
