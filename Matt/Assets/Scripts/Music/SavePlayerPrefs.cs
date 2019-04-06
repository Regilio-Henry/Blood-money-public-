using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    public void SavePrefs(float vol)
    {
        PlayerPrefs.SetFloat("MasterVol" , vol);
    }
}
