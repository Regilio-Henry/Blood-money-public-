using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSlider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("MasterVol"))
        {
            this.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterVol");
        }
        else
        {
           this.GetComponent<Slider>().value = AudioListener.volume;
        } 
    }
}
