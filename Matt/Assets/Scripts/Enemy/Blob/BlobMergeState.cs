using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class BlobMergeState : State<BlobAI>
{
    private static BlobMergeState _instance;

    private BlobMergeState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static BlobMergeState Instance
    {
        get
        {
            if (_instance == null)
            {
                new BlobMergeState();
            }

            return _instance;
        }
    }

    public override void EnterState(BlobAI _owner)
    {
        //Debug.Log("Entering Second state");
    }

    public override void ExitState(BlobAI _owner)
    {
        //Debug.Log("Exiting Second state");
    }

    public override void UpdateState(BlobAI _owner)
    {
        switch (_owner.state)
        {
            case BlobAI.State.Merge:
                break;

            case BlobAI.State.Approach:
                _owner.stateMachine.ChangeState(BlobApproachState.Instance);
                break;

            case BlobAI.State.Fight:
                _owner.stateMachine.ChangeState(BlobAttackState.Instance);
                break;

            default:
                Debug.Log("State Not Found");
                break;
        };

        //float distance = Vector2.Distance(transform.position, SkeleblobSpawn.transform.position);

        float z = Mathf.Atan2((_owner.SkeleblobSpawn.transform.position.y - _owner.transform.position.y), (_owner.SkeleblobSpawn.transform.position.x - _owner.transform.position.x)) * Mathf.Rad2Deg - 90;

        _owner.transform.eulerAngles = new Vector3(0, 0, z);

        _owner.transform.position = _owner.transform.position + _owner.gameObject.transform.up * _owner.speed * Time.deltaTime;
        //Will move the blobs towards the spawn point when this state is entered. 

        //if (distance <= 0.5)
        //{
        //    destroy.This; 
        //}
    }
}
