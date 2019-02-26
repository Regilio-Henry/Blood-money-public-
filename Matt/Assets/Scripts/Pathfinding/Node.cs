using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool _obstructed;
    public Vector2 _position;
    public Node _Parent;
    public int _gridX;
    public int _gridY;


    public int gCost;
    public int hCost;

    public int Fcost { get { return gCost + hCost; } }

    public Node(bool obstrcucted, Vector2 position, int gridX, int gridY)
    {
        _obstructed = obstrcucted;
        _position = position;
        _gridX = gridX;
        _gridY = gridY;
    }

}
