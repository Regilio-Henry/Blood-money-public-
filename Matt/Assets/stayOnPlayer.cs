using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stayOnPlayer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position = GameObject.Find("Player").transform.position;
    }
}
