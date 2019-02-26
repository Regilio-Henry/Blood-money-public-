using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public int gridWidth;
    public int gridHeight;
    List<GameObject> nodes = new List<GameObject>();
    //GameObject grid;
    [SerializeField]
    GameObject node;
    [SerializeField]
    Node[,] grid;
    public GameObject player;
    public List<Node> finalPath;
    public float NodeRadius;
    public float DistanceBetweenNodes;
    float NodeDiameter;
    List<GameObject> nodeList = new List<GameObject>();
    List<debugPath> pathList = new List<debugPath>();
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        NodeDiameter = NodeRadius * 2;
        CreateGrid();
        
    }

    public void addPath(debugPath newpath)
    {
        foreach (debugPath dP in pathList)
        {
            if (dP._ID == newpath._ID)
            {
                pathList.Remove(dP);
            }
        }

        pathList.Add(newpath);
    }

    // Update is called once per frame
    void Update()
    {
        //DrawNodes();
    }

    public Node NodeFromWorldPoint(Vector2 WorldPosition)
    {
        float ixPos = ((WorldPosition.x + gridWidth / 2) / gridWidth);
        float iyPos = ((WorldPosition.y + gridHeight / 2) / gridHeight);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((gridWidth - 1) * ixPos);
        int iy = Mathf.RoundToInt((gridHeight - 1) * iyPos);

        return grid[ix, iy];
    }


    public List<Node> GetNeighbouringNodes(Node node)
    {
        List<Node> neighbouringNodes = new List<Node>();
        int xCheck;
        int yCheck;

        //Check right side
        xCheck = node._gridX + 1;
        yCheck = node._gridY;

        if (xCheck >= 0 && xCheck < gridWidth)
        {
            if (yCheck >= 0 && yCheck < gridHeight)
            {
                neighbouringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Check left side
        xCheck = node._gridX - 1;
        yCheck = node._gridY;

        if (xCheck >= 0 && xCheck < gridWidth)
        {
            if (yCheck >= 0 && yCheck < gridHeight)
            {
                neighbouringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //Check top side
        xCheck = node._gridX;
        yCheck = node._gridY + 1;

        if (xCheck >= 0 && xCheck < gridWidth)
        {
            if (yCheck >= 0 && yCheck < gridHeight)
            {
                neighbouringNodes.Add(grid[xCheck, yCheck]);
            }
        }


        //Check bottom side
        xCheck = node._gridX;
        yCheck = node._gridY - 1;

        if (xCheck >= 0 && xCheck < gridWidth)
        {
            if (yCheck >= 0 && yCheck < gridHeight)
            {
                neighbouringNodes.Add(grid[xCheck, yCheck]);
            }
        }


        return neighbouringNodes;
    }

    void CreateGrid()
    {
        grid = new Node[gridWidth, gridHeight];

        //Get the real world position of the bottom left of the grid.
        Vector3 bottomLeft = transform.position - Vector3.right * gridWidth / 2 - Vector3.up * gridHeight / 2;
        for (int x = 0; x < gridHeight; x++)
        {
            for (int y = 0; y < gridWidth; y++)
            {
                //Get the world co ordinates of the bottom left of the graph
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * NodeDiameter + NodeRadius) + Vector3.up * (y * NodeDiameter + NodeRadius);

                bool obstructed =  Physics2D.OverlapCircle(worldPoint, NodeRadius, mask); //Check if the node is a Wall
                Node newNode = new Node(obstructed, worldPoint, x, y);
                grid[x, y] = newNode;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Draw a wire cube with the given dimensions from the Unity inspector
        Gizmos.DrawWireCube(transform.position, new Vector2(gridWidth, gridHeight));

        if (grid != null)//If the grid is not empty
        {

            foreach (Node n in grid)//Loop through every node in the grid
            {
                if (!n._obstructed)//If the current node is not a wall node
                {
                    Gizmos.color = Color.white;//Set the color of the node
                }
                else
                {
                    Gizmos.color = Color.yellow;//Set the color of the node
                }

                //foreach (debugPath dP in pathList)
                //{
                //    if (dP._nodeList != null)//If the final path is not empty
                //    {
                //        if (dP._nodeList.Contains(n))//If the current node is in the final path
                //        {
                //            Gizmos.color = dP._debugColor;//Set the color of that node
                //        }
                //    }
                //}



                if (finalPath != null)//If the final path is not empty
                {
                    if (finalPath.Contains(n))//If the current node is in the final path
                    {
                        Gizmos.color = Color.red;
                    }
                }

                        Gizmos.DrawCube(n._position, Vector3.one * (NodeDiameter - DistanceBetweenNodes));//Draw the node at the position of the node.
            }
        }
    }

    void DrawNodes()
    {
       
        foreach (Node n in grid)
        {
            GameObject _node = Instantiate(node);
            Node playerNode = NodeFromWorldPoint(player.transform.position);
            nodeList.Add(_node);

            //sets node position on grid
            _node.transform.position = n._position;
            if (playerNode == n)
            {
                _node.GetComponent<SpriteRenderer>().color = Color.blue;
            }

            if (!n._obstructed)//If the current node is not a wall node
            {
                _node.GetComponent<SpriteRenderer>().color = Color.white;//Set the color of the node
            }
            else
            {
                _node.GetComponent<SpriteRenderer>().color =  Color.yellow;//Set the color of the node
            }


            if (finalPath != null)//If the final path is not empty
            {
                if (finalPath.Contains(n))//If the current node is in the final path
                {
                    _node.GetComponent<SpriteRenderer>().color = Color.red;//Set the color of that node
                }

            }
        }
    }
}
