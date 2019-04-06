using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeSong : MonoBehaviour
{

    public AudioClip SongOne;

    // Start is called before the first frame update
    void Start()
    {

       // Invoke("PlayNextTrack", this.GetComponent<AudioSource>().clip.length);
        

    }

    public void PlayNextTrack()
    {
       // this.GetComponent<AudioSource>().clip = SongOne;
       // this.GetComponent<AudioSource>().Play();
    }

}
