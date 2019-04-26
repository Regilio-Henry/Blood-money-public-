using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public int healthTotal;
    public int playerHealthTotal;

    public int speed;
    public int xpTotal;
    public int swingSpeed;
    public int dodgeTotal;

    public int totalkills;
    int batkills;
    public int spiderkills;
    public int skeletonkills;
    

    void OnEnable()
    {
        PlayerController.onDodge += IncrementDodge;
        SpiderAI.onkillSpider += IncrementSpiderKill;
        AI.onkillSkeleton += IncrementSkeletonKill;
    }

    void OnDisable()
    {
        PlayerController.onDodge -= IncrementDodge;
        SpiderAI.onkillSpider -= IncrementSpiderKill;
    }

    void IncrementSpiderKill()
    {
        spiderkills++;
        IncrementKill();
    }


    void IncrementSkeletonKill()
    {
        skeletonkills++;
        IncrementKill();
    }

    void IncrementKill()
    {
        var amount = containsEnumParameter(ChallengeParameters.Amount);

        if (amount != null)
        {
            var kill = containsEnumType(ChallengeType.Kill);

            if (kill != null)
            {
                totalkills++;

                var spider = containsEnumSpecifier(ChallengeSpecifier.Spider);
                var skeleton = containsEnumSpecifier(ChallengeSpecifier.Skeleton);
                var all = containsEnumSpecifier(ChallengeSpecifier.All);

                if (all != null)
                {
                    foreach (ChallengeContainer chall in all)
                    {
                        chall.CheckAmountChallenge(totalkills);
                    }
               }

                if (spider != null)
                {
                    foreach (ChallengeContainer chall in spider)
                    {
                        chall.CheckAmountChallenge(spiderkills);
                    }
                }
                
                if (skeleton != null)
                {
                    foreach (ChallengeContainer chall in skeleton)
                    {
                        chall.CheckAmountChallenge(skeletonkills);
                    }
                }
            }
            
        }
    }

    //Increment value when triggered
    void IncrementDodge()
    {
        var amount = containsEnumParameter(ChallengeParameters.Amount);

        if (amount != null)
        {
            var dodge = containsEnumType(ChallengeType.Dodge);

            if (dodge != null)
            {
                var none = containsEnumSpecifier(ChallengeSpecifier.None);

                dodgeTotal++;

                if (none != null)
                {
                    foreach (ChallengeContainer chall in dodge)
                    {
                        chall.CheckAmountChallenge(dodgeTotal);
                    }
                }
            }
        }
    }

   List<ChallengeContainer> containsEnumType(ChallengeType type)
    {
        List<ChallengeContainer> challengeList = new List<ChallengeContainer>();

        foreach (ChallengeContainer c in GetComponent<ChallengeBuilder>().challenges)
        {
            if (c.challenge.type == type)
            {
                challengeList.Add(c);
            }
        }
        return challengeList;
    }

    //check if challenge list contains type of enum
    List<ChallengeContainer> containsEnumSpecifier(ChallengeSpecifier specifier)
    {
        List<ChallengeContainer> challengeList = new List<ChallengeContainer>();
        foreach (ChallengeContainer c in GetComponent<ChallengeBuilder>().challenges)
        {
            if (c.challenge.specifier == specifier)
            {
                challengeList.Add(c);
            }
        }
        return challengeList;
    }

    //check if challenge list contains type of enum
    List<ChallengeContainer> containsEnumParameter(ChallengeParameters parameter)
    {
        List<ChallengeContainer> challengeList = new List<ChallengeContainer>();
        foreach (ChallengeContainer c in GetComponent<ChallengeBuilder>().challenges)
        {
            if (c.challenge.parameters == parameter)
            {
                challengeList.Add(c);
            }
        }
        return challengeList;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
