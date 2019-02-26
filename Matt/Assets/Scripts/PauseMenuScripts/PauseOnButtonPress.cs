using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnButtonPress : MonoBehaviour
{

    public GameObject PauseMenuPanel; // Assign in inspector
    public static bool isShowing;

    void Start()
    {
        PauseMenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            isShowing = !isShowing;
            PauseMenuPanel.SetActive(isShowing);
        }
    }
}
