using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class BlobAttackState : State<BlobAI>
{
    private static BlobAttackState _instance;

    private BlobAttackState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static BlobAttackState Instance
    {
        get
        {
            if (_instance == null)
            {
                new BlobAttackState();
            }

            return _instance;
        }
    }

    public override void EnterState(BlobAI _owner)
    {
        //Debug.Log("Entering Third state");
    }

    public override void ExitState(BlobAI _owner)
    {
        //Debug.Log("Exiting Third state");
    }

    public override void UpdateState(BlobAI _owner)
    {
        switch (_owner.state)
        {
            case BlobAI.State.Merge:
                _owner.stateMachine.ChangeState(BlobMergeState.Instance);
                break;

            case BlobAI.State.Approach:
                _owner.stateMachine.ChangeState(BlobApproachState.Instance);
                break;

            case BlobAI.State.Fight:
                break;

            default:
                Debug.Log("State Not Found");
                break;
        };

        _owner.transform.Rotate((Vector3.forward * Time.deltaTime) * 1000);
    }
}