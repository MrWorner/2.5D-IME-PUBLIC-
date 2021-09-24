using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { None, North, East, South, West, NE, SE, NW, SW};//Направление
public enum LineDirection {None, Horizontal, Vertical};//Направление
public enum MathDirection {None, X, Y};//Направление

public static class MG_Direction
{
    public static Direction[] Basic = new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West };
    public static Direction[] Diagonal = new Direction[] { Direction.NE, Direction.SE, Direction.NW, Direction.SW };
    public static Direction[] All = new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West, Direction.NE, Direction.SE, Direction.NW, Direction.SW};

    /// <summary>
    /// Получить противоположное направление
    /// </summary>
    /// <param name="_direction"></param>
    /// <returns></returns>
    public static Direction ReverseDir(Direction _direction)
    {
        switch (_direction)
        {
            case Direction.North:
                {
                    return Direction.South;
                }
            case Direction.South:
                {
                    return Direction.North;
                }
            case Direction.East:
                {
                    return Direction.West;
                }
            case Direction.West:
                {
                    return Direction.East;
                    //break;
                }
            case Direction.NE:
                {
                    return Direction.SW;
                }
            case Direction.NW:
                {
                    return Direction.SE;
                }
            case Direction.SE:
                {
                    return Direction.NW;
                }
            case Direction.SW:
                {
                    return Direction.NE;
                }
            default: return Direction.North;
        }
    }

    /// <summary>
    /// Повернуть направление по часовой либо против часовой
    /// </summary>
    /// <param name="_dir"></param>
    /// <param name="_clockwise"></param>
    /// <returns></returns>
    public static Direction RotateDir(Direction _dir, bool _clockwise)
    {
        if (_clockwise)
        {
            switch (_dir)
            {
                case Direction.North:
                    {
                        return Direction.East;
                    }
                case Direction.South:
                    {
                        return Direction.West;
                    }
                case Direction.East:
                    {
                        return Direction.South;
                    }
                case Direction.West:
                    {
                        return Direction.North;
                        //break;
                    }
                case Direction.NE:
                    {
                        return Direction.SE;
                    }
                case Direction.NW:
                    {
                        return Direction.NE;
                    }
                case Direction.SE:
                    {
                        return Direction.SW;
                    }
                case Direction.SW:
                    {
                        return Direction.SE;
                    }
                default: return Direction.North;
            }
        }
        else
        {
            switch (_dir)
            {
                case Direction.North:
                    {
                        return Direction.West;
                    }
                case Direction.South:
                    {
                        return Direction.East;
                    }
                case Direction.East:
                    {
                        return Direction.North;
                    }
                case Direction.West:
                    {
                        return Direction.South;
                        //break;
                    }
                case Direction.NE:
                    {
                        return Direction.NW;
                    }
                case Direction.NW:
                    {
                        return Direction.SW;
                    }
                case Direction.SE:
                    {
                        return Direction.NE;
                    }
                case Direction.SW:
                    {
                        return Direction.SE;
                    }
                default: return Direction.North;
            }
        }

    }
}
