using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MatchThree.Scripts;
using UnityEngine;

public enum Direction
{
    None = -1,
    Left = 0,
    Right = 1,
    Up = 2,
    Down = 3
}

public class Neighbour
{
    public Direction Direction;
    public MatchThreeCell Cell;
}

public class MatchThreeCell : MonoBehaviour
{
    public MatchThreeCandy Candy;
    private readonly Neighbour[] neighbours = new Neighbour[4];

    public MatchThreeCell GetNeighbour(Direction direction)
    {
        /*
        Такая же реализация 
        foreach (var neighbour in neighbours)
        {
            if (neighbour.Direction == direction)
            {
                return neighbour.Cell;
            }
        }
        return null;*/

        return neighbours.FirstOrDefault(n => n != null && n.Direction == direction)?.Cell;
    }

    public void SetNeighbour(Direction direction, MatchThreeCell cell)
    {
        if (GetNeighbour(direction))
        {
            return;
        }

        neighbours[(int) direction] = new Neighbour()
        {
            Direction = direction,
            Cell = cell
        };
    }
}