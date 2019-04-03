using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class FleeState : State<SpiderAI>
{
    private static FleeState _instance;

    private FleeState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FleeState Instance
    {
        get
        {
            if (_instance == null)
            {
                new FleeState();
            }

            return _instance;
        }
    }

    public override void EnterState(SpiderAI _owner)
    {
        //Debug.Log("Entering Second state");
    }

    public override void ExitState(SpiderAI _owner)
    {
        //Debug.Log("Exiting Second state");
    }

    public override void UpdateState(SpiderAI _owner)
    {
        switch (_owner.state)
        {
            case SpiderAI.State.Flee:
                break;

            case SpiderAI.State.Approach:
                _owner.stateMachine.ChangeState(ApproachState.Instance);
                break;

            case SpiderAI.State.Fight:
                _owner.stateMachine.ChangeState(AttackState.Instance);
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
