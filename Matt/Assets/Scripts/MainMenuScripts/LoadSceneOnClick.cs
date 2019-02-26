using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadByIndex(int sceneIndex)
    {
        EditorSceneManager.LoadScene(sceneIndex);
    }
}
