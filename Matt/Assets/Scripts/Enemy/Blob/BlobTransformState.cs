using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class BlobTransformState : State<BlobAI>
{
    private static BlobTransformState _instance;

    private BlobTransformState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static BlobTransformState Instance
    {
        get
        {
            if (_instance == null)
            {
                new BlobTransformState();
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
            case BlobAI.State.Flee:
                break;

            case BlobAI.State.Approach:
                _owner.stateMachine.ChangeState(BlobMoveState.Instance);
                break;

            case BlobAI.State.Fight:
                _owner.stateMachine.ChangeState(BlobAttackState.Instance);
                break;

            default:
                Debug.Log("State Not Found");
                break;
        };

        float z = Mathf.Atan2((_owner.player.transform.position.y - _owner.transform.position.y), (_owner.player.transform.position.x - _owner.transform.position.x)) * Mathf.Rad2Deg + 90;

        _owner.transform.eulerAngles = new Vector3(0, 0, z);

        //_owner.transform.localEulerAngles = _owner.transform.eulerAngles + Vector3.forward(0, 190, -2 * _owner.transform.eulerAngles.z);

        _owner.transform.position = _owner.transform.position + _owner.gameObject.transform.up * _owner.speed * Time.deltaTime;
        //  _owner.rb.AddForce(_owner.gameObject.transform.up * _owner.speed);

        //_owner.transform.Rotate((Vector3.forward * Time.deltaTime) * 1000);
    }
}