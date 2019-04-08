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

    void OnEnable()
    {
        PlayerController.onDodge += IncrementValue;
    }

    void OnDisable()
    {
        PlayerController.onDodge -= IncrementValue;
    }

    void IncrementValue()
    {
        var dodge = containsEnumType(ChallengeType.Dodge);

        if (dodge != null)
        {
            var amount = containsEnumParameter(ChallengeParameters.Amount);

            if (amount != null)
            {
                dodgeTotal++;
                foreach (ChallengeContainer chall in amount)
                {
                    chall.CheckAmountChallenge(dodgeTotal);
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
