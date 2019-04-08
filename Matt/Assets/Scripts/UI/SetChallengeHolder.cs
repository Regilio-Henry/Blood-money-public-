using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChallengeHolder : MonoBehaviour
{
    GameObject controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("GameController");
        controller.GetComponent<ChallengeBuilder>().UpdateChallenge(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
