using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    GameObject ChallengeBuilder;
    private void Start()
    {
        ChallengeBuilder = GameObject.Find("GameController");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
       // ChallengeBuilder.GetComponent<ChallengeBuilder>().OnSceneStart();
    }
}
