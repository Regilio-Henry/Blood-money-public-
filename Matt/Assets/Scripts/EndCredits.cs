using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float TimeToSkip;
    [SerializeField]
    private Image FillImg;

    float skipTimer = 0f;
    public Animator animator;

    private void Start()
    {
        FillImg = GameObject.Find("HoldFiller").GetComponent<Image>();
        TimeToSkip = 1;
    }
    void Update()
    {
        
        if (FillImg.fillAmount != 0 && !(Input.GetKey(KeyCode.Space)))
        {
            FillImg.fillAmount -= Time.deltaTime;
            skipTimer -= Time.deltaTime;
            Debug.Log(skipTimer);
            Debug.Log(FillImg.fillAmount);
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            skipTimer += Time.deltaTime;

            if (skipTimer > TimeToSkip)
            {
                FadeOut();
            }

            FillImg.fillAmount = skipTimer / TimeToSkip;
        }
    }

    // Update is called once per frame
    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
        Invoke("EndCred", 1.0f);
    }
    public void EndCred()
    {
        SceneManager.LoadScene(0);
    }
}
