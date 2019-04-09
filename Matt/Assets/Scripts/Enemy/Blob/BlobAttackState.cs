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
            case BlobAI.State.Flee:
                _owner.stateMachine.ChangeState(BlobTransformState.Instance);
                break;

            case BlobAI.State.Approach:
                _owner.stateMachine.ChangeState(BlobMoveState.Instance);
                break;

            case BlobAI.State.Fight:
                break;

            default:
                Debug.Log("State Not Found");
                break;
        }

        ;

        float z = Mathf.Atan2((_owner.player.transform.position.y - _owner.transform.position.y),
                      (_owner.player.transform.position.x - _owner.transform.position.x)) * Mathf.Rad2Deg - 90;

        _owner.transform.eulerAngles = new Vector3(0, 0, z);

        //_owner.transform.Rotate((Vector3.forward * Time.deltaTime) * 1000);

        //_owner.rb.AddForce(_owner.gameObject.transform.up * _owner.standing);
    }
}
