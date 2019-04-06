using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreEnemyCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(11, 10);
    }
}