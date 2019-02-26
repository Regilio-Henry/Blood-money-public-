using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScript : MonoBehaviour
{
    public GameObject PauseMenuPanel;
    public void Continue()
    {
        PauseOnButtonPress.isShowing = false;
        PauseMenuPanel.SetActive(PauseOnButtonPress.isShowing);
        Time.timeScale = 1;
    }
}
