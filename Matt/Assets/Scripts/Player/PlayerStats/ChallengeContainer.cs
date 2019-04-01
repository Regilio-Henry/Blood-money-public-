using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChallengeContainer
{
    [SerializeField]
    public string challengeName;
    [SerializeField]
    public bool ChallengeComplete;
    [SerializeField]
    public Challenge challenge;

    public delegate void challengeEvents();
    public static event challengeEvents onComplete;

    public void CheckAmountChallenge(int currentValue)
    {
        if (challenge.parameters == ChallengeParameters.Amount)
        {
            if (currentValue >= challenge.challengeValue)
            {
                ChallengeComplete = true;
            }
        }

        if (ChallengeComplete)
        {
            onComplete();
        }
    }
}
