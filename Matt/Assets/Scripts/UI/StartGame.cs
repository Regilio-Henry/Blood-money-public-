using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    GameObject gameController;
    GameObject Canvas;
    private void Start()
    {
        gameController = GameObject.Find("GameController");
        Canvas = GameObject.Find("Canvas");

    }
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
        gameController.GetComponent<ChallengeBuilder>().selectedAbilites = Canvas.gameObject.GetComponent<AbilityContainer>().selectedAbilites;
    }
}
