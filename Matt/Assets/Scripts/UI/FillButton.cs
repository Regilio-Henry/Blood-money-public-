using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{

    [SerializeField]
    public float TimeToSkip;
    [SerializeField]
    private Image FillImg;

    float skipTimer = 0f;
    public Animator animator;
    bool StopUnfilling = false;

    private void Start()// Start is called before the first frame update
    {
        FillImg = GameObject.Find("HoldFiller").GetComponent<Image>();
        TimeToSkip = 1;
    }


    void Update()    // Update is called once per frame
    {

        if (FillImg.fillAmount != 0 && !(Input.GetKey(KeyCode.Space)))
        {
            if (!StopUnfilling)
            {
                FillImg.fillAmount -= Time.deltaTime;
                skipTimer -= Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            skipTimer += Time.deltaTime;

            if (skipTimer > TimeToSkip)
            {
                FadeOut();
                StopUnfilling = true;

            }

            FillImg.fillAmount = skipTimer / TimeToSkip;
        }
    }


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
