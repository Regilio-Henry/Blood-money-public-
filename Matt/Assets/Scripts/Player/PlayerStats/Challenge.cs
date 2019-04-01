using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ChallengeType
{
    None,
    Dodge,
    Blocks,
    Kill
}

public enum ChallengeParameters
{
    None,
    Amount,
    AmountInSeconds
}

public enum ChallengeSpecifier
{
    None,
    Enitity,
    Tag
}

[System.Serializable]
public class Challenge
{
    [SerializeField]
    public int challengeValue = 0;
    [SerializeField]
    public ChallengeParameters parameters = ChallengeParameters.None;
    [SerializeField]
    public ChallengeType type = ChallengeType.None;
    [SerializeField]
    public ChallengeSpecifier specifier = ChallengeSpecifier.None;

}
