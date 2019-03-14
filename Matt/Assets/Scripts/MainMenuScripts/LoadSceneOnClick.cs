using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public Animator animator;
    private int SceneIndex;
    public void LoadByIndex(int sceneIndex)
    {
        animator.SetTrigger("FadeOut");
        SceneIndex = sceneIndex;
        Invoke("OnFadeComplete", 1.0f);
    }

    public void OnFadeComplete()
    {
        EditorSceneManager.LoadScene(SceneIndex);
    }
}
