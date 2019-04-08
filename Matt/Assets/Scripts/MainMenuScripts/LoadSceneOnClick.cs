using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public Animator animator;
    private int SceneIndex;
    private bool fade = false;

    public void Update()
    {
        if(fade)
        {
            AudioListener.volume -= Time.deltaTime * .5f;
        }
    }

    public void LoadByIndex(int sceneIndex)
    {
        animator.SetTrigger("FadeOut");
        fade = true;
        SceneIndex = sceneIndex;
        Invoke("OnFadeComplete", 1.0f);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public void LoadByIndexInstant(int sceneIndex)
    {

        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }
}
