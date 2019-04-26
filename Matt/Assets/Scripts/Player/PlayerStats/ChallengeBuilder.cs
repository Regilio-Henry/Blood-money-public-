using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeBuilder : MonoBehaviour
{
    [SerializeField]
    public ChallengeContainer[] challenges;
    private GameObject challengeHolder;
    public GameObject challengePrefab;
    public Color completionColour;
    public List<Ability> selectedAbilites;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }



    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        print(scene.name);

        if (scene.name != "SampleScene")
        {
            GetComponent<EnemySpawner>().enabled = false;
        }
        else
        {
            GetComponent<EnemySpawner>().enabled = true;
        }
    }

    public void UpdateChallenge(GameObject ch)
    {
        challengeHolder = null;
        challengeHolder = ch;
        UpdateChallenges();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChallengeContainer.onComplete += UpdateChallenges;
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
    }

    void OnDisable()
    {
        ChallengeContainer.onComplete -= UpdateChallenges;

    }

    void UpdateChallenges()
    {
        ClearChallenges();
        foreach (ChallengeContainer c in challenges)
        {
            var challengeInstance = Instantiate(challengePrefab);
            Text ChallengeNameText = challengeInstance.GetComponent<Text>();
            string ChallengeName = c.challengeName;
            Text ChallengeDescriptionText = challengeInstance.transform.GetChild(0).GetComponent<Text>();

            if (c.ChallengeComplete)
            {
                ChallengeName += "(Complete)";
                ChallengeNameText.color = completionColour;
                ChallengeDescriptionText.color = completionColour;
            }

            ChallengeNameText.text = ChallengeName;
            ChallengeDescriptionText.text = ChallengeDescriptionBuilder(c);
            if (challengeHolder != null)
            {
                challengeInstance.transform.parent = challengeHolder.transform;
            }
        }
    }


    void ClearChallenges()
    {
        if (challengeHolder != null)
        {
            foreach (Transform child in challengeHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    string ChallengeDescriptionBuilder(ChallengeContainer container)
    {
        string challengeDescription = "";

        if (container.challenge.type == ChallengeType.Kill)
        {
            if(container.challenge.parameters == ChallengeParameters.Amount)
            {
                if (container.challenge.specifier == ChallengeSpecifier.All)
                {
                    challengeDescription = "Kill " + container.challenge.challengeValue + " Enemies";
                }

                if (container.challenge.specifier == ChallengeSpecifier.Skeleton)
                {
                    challengeDescription = "Kill " + container.challenge.challengeValue + " Skeletons";
                }

                if (container.challenge.specifier == ChallengeSpecifier.Spider)
                {
                    challengeDescription = "Kill " + container.challenge.challengeValue + " Spiders";
                }
            }
        }

        if (container.challenge.type == ChallengeType.Dodge)
        {
            if (container.challenge.parameters == ChallengeParameters.Amount)
            {              
                    challengeDescription = "Dodge " + container.challenge.challengeValue + " Times";
            }
        }

        return challengeDescription;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
