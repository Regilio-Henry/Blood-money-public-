using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMasterVolume : MonoBehaviour
{
    private void Start()
    {
        if(PlayerPrefs.HasKey("MasterVol"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("MasterVol");
        }
    }
    public void SetMasterVol(float vol)
    {
        AudioListener.volume = vol;
    }
}
