using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugPath
{
    public List<Node> _nodeList;
    public Color _debugColor;
    public string _ID;

    public debugPath(List<Node> nodeList, Color debugColor, string id)
    {
        _nodeList = nodeList;
        _debugColor = debugColor;
        _ID = id;
    }
}
