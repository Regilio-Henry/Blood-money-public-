using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cellIndex : MonoBehaviour
{
    GameObject canvas;
    GameObject equipButton;
    public int index;
    public bool selected = false;
    public bool locked = false;



    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        equipButton = GameObject.Find("EquipButton");
    }

    public void LockCheck()
    {
        if (locked)
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }

    public void checkSelection()
    {
        if (selected)
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        
    }

    public void setSelectionIndex()
    {
        canvas.GetComponent<AbilityContainer>().getIndexFromChild(index);
    }
}
