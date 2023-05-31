using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robot_fight.Model;

public class Task
{
    public List<TaskElement> Boxes { get { return _boxes; } set { _boxes = value; } }
    public int StepCount { get { return _stepCount; } set { _stepCount = value; } }
    public int Score { get { return _score; } set { _score = value; } }
    public string Name { get { return _name; } set { _name = value; } }


    public Task(int stepCount, (int, int) score, String name)
    {
        _boxes = new List<TaskElement>();
        _stepCount = stepCount;
        
        _name = name;

        for (int i = 0; i < 9; i++)
        {
            Boxes.Add(null);
        }

        int howManyCube = random.Next(1, 6);
        int taskElementLocation;
        int randomColor;
        Colors color;

        taskElementLocation = 0;
        randomColor = random.Next(0, 6);
        color = (Colors)randomColor;
        Boxes[taskElementLocation] = new TaskElement(color, taskElementLocation);

        for (int i = 0; i < howManyCube - 1; i++)
        {
                do
                {
                    taskElementLocation = random.Next(0, 8);
                    randomColor = random.Next(0, 6);
                    color = (Colors)randomColor;

                } while (!isItNeighbour(taskElementLocation) || Boxes[taskElementLocation] != null);
            Boxes[taskElementLocation] = new TaskElement(color, taskElementLocation);

        }

        int valtozo = Convert.ToInt32((score.Item2 - score.Item1) / 5) * howManyCube;
        _score = valtozo;

    }

    private bool isItNeighbour(int taskElementLocation)
    {
        switch (taskElementLocation)
        {
            case 0:
                return Boxes[1] != null || Boxes[3] != null;
            case 1:
                return Boxes[0] != null || Boxes[2] != null || Boxes[4] != null;
            case 2:
                return Boxes[1] != null || Boxes[5] != null;
            case 3:
                return Boxes[0] != null || Boxes[4] != null || Boxes[6] != null;
            case 4:
                return Boxes[1] != null || Boxes[5] != null || Boxes[3] != null || Boxes[7] != null;
            case 5:
                return Boxes[2] != null || Boxes[4] != null || Boxes[8] != null;
            case 6:
                return Boxes[3] != null || Boxes[7] != null;
            case 7:
                return Boxes[4] != null || Boxes[6] != null || Boxes[8] != null;
            case 8:
                return Boxes[5] != null || Boxes[7] != null;
            default:
                return false;
        }
    }

    public void DecreaseStepCount()
    {
        _stepCount--;
    }

    private List<TaskElement> _boxes;
    private int _stepCount;
    private int _score;
    private string _name;
    private Random random = new Random();
}
