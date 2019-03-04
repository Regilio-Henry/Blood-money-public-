using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class AttackState : State<SpiderAI>
{
    private static AttackState _instance;

    private AttackState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static AttackState Instance
    {
        get
        {
            if (_instance == null)
            {
                new AttackState();
            }

            return _instance;
        }
    }

    public override void EnterState(SpiderAI _owner)
    {
        Debug.Log("Entering Third state");
    }

    public override void ExitState(SpiderAI _owner)
    {
        Debug.Log("Exiting Third state");
    }

    public override void UpdateState(SpiderAI _owner)
    {
        switch (_owner.state)
        {
            case SpiderAI.State.Flee:
                _owner.stateMachine.ChangeState(FleeState.Instance);
                break;

            case SpiderAI.State.Approach:
                _owner.stateMachine.ChangeState(ApproachState.Instance);
                break;

            case SpiderAI.State.Fight:
                break;

            default:
                Debug.Log("State Not Found");
                break;
        };

        float z = Mathf.Atan2((_owner.player.transform.position.y - _owner.transform.position.y), (_owner.player.transform.position.x - _owner.transform.position.x)) * Mathf.Rad2Deg - 90;

        _owner.transform.eulerAngles = new Vector3(0, 0, z);

        //_owner.transform.Rotate((Vector3.forward * Time.deltaTime) * 1000);

        //_owner.rb.AddForce(_owner.gameObject.transform.up * _owner.standing);
    }
}