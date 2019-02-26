using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    // Start is called before the first frame update
    List<Node> path = new List<Node>();
    float distanceFromPoint = 1.0f;
    public float enemySpeed;
    bool pathFinished = false;
    bool pathStarted = false;
    int counter = 0;
    float distance = 0;
    List<Node> lastPath = new List<Node>();
    public bool chasePath = false;

    void Start()
    {
       
    }

    void pathFollow()
    {
        if (GetComponent<findPath>().grid.finalPath != null)
        {
            path = GetComponent<findPath>().finalPath;

            pathStarted = true;
            if (counter < path.Count)
            {
                float distance = Vector3.Distance(path[counter]._position, transform.position);
                transform.position = Vector2.MoveTowards(transform.position, path[counter]._position, enemySpeed * Time.deltaTime);
            }

            if (distance <= distanceFromPoint && path == lastPath)
            {
                counter++;

                if (counter >= path.Count || path != lastPath)
                {
                    counter = 0;
                    pathFinished = true;
                }
            }

            lastPath = path;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (chasePath)
        {
            pathFollow();
        }
    }
}
