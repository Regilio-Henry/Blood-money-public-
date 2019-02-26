using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findPath : MonoBehaviour
{
    public Transform _startPosition;
    public Transform _targetPosition;

    [HideInInspector]
    public Grid grid;
    public GameObject gameController;
    public List<Node> finalPath;
    public Color debugColour;
    public string Id;

    void Start()
    {
        grid = gameController.GetComponent<Grid>();
        Id = gameObject.name + "ID";
    }

    // Update is called once per frame
    void Update()
    {
        FindPath(_startPosition.position, _targetPosition.position);
    }

    void FindPath(Vector2 startPost, Vector2 targetPost)
    {
        //start node is current position the gameobject is at but as a position in the array
        //Node TargetNode = the node the player is at 
        Node startNode = grid.NodeFromWorldPoint(_startPosition.position);
        Node targetNode = grid.NodeFromWorldPoint(_targetPosition.position);


        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(startNode);

        //Fcost comparison and adding nodes
        while (OpenList.Count > 0)
        {
            Node currentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                //Get Closest Node to target
                if (OpenList[i].Fcost < currentNode.Fcost || OpenList[i].Fcost == currentNode.Fcost && OpenList[i].hCost < currentNode.hCost)
                {
                    currentNode = OpenList[i];
                }
            }

            //Remove the node closest to target and add to closed list
            OpenList.Remove(currentNode);
            ClosedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
            }

            foreach (Node neighbourNode in grid.GetNeighbouringNodes(currentNode))
            {
                //If the neighbour is a wall or is in the closed list skip it
                if (neighbourNode._obstructed || ClosedList.Contains(neighbourNode))
                {
                    continue;
                }

                int moveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighbourNode);

                if (moveCost < neighbourNode.gCost || !OpenList.Contains(neighbourNode))
                {
                    neighbourNode.gCost = moveCost;
                    neighbourNode.hCost = GetManhattenDistance(neighbourNode, targetNode);
                    neighbourNode._Parent = currentNode;

                    if (!OpenList.Contains(neighbourNode))
                    {
                        OpenList.Add(neighbourNode);
                    }
                }
            }
        }
    }

    int GetManhattenDistance(Node nodeA, Node nodeB)
    {
        int ix = Mathf.Abs(Mathf.RoundToInt(nodeA._gridX - nodeB._gridX));
        int iy = Mathf.Abs(Mathf.RoundToInt(nodeA._gridY - nodeB._gridY));

        return ix + iy;
    }

    void GetFinalPath(Node startNode, Node endNode)
    {
        finalPath = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode._Parent;
        }

        finalPath.Reverse();
        grid.finalPath = finalPath;
        //grid.addPath(new debugPath(finalPath, debugColour, Id));

    }
}
