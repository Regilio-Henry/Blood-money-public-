using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class BlobMoveState : State<BlobAI>
{
    private static BlobMoveState _instance; //only declared one time. can only be created from within the state class. 

    private BlobMoveState()
    {
        if (_instance != null)
        {
            return;
        }


        _instance = this;
    }

    public static BlobMoveState Instance
    {
        get
        {
            if (_instance == null)
            {
                new BlobMoveState(); //if the state doesnt already exist, create it. 
            }

            return _instance;
        }
    }

    public override void EnterState(BlobAI _owner) //This is performed once upon entering state. 
    {
        //Debug.Log("Entering first state");
    }

    public override void ExitState(BlobAI _owner) //this is performed once upon exiting state.
    {
        //Debug.Log("Exiting first state");
    }

    public override void UpdateState(BlobAI _owner) //this is performed on update
    {
        //Debug.Log(_owner.state);
        switch (_owner.state)
        {
            case BlobAI.State.Flee:
                _owner.stateMachine.ChangeState(BlobTransformState.Instance);
                break;

            case BlobAI.State.Approach:
                break;

            case BlobAI.State.Fight:
                _owner.stateMachine.ChangeState(BlobAttackState.Instance);
                break;

            default:
                Debug.Log("State Not Found");
                break;
        };

        float z = Mathf.Atan2((_owner.player.transform.position.y - _owner.transform.position.y), (_owner.player.transform.position.x - _owner.transform.position.x)) * Mathf.Rad2Deg - 90;

        _owner.transform.eulerAngles = new Vector3(0, 0, z);

        //  _owner.rb.AddForce(_owner.gameObject.transform.up * _owner.speed);
        _owner.transform.position = _owner.transform.position + _owner.gameObject.transform.up * _owner.speed * Time.deltaTime;
    }
}
