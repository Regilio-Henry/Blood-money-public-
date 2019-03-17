using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class SecondState : State<AI>
{
    private static SecondState _instance;

    private SecondState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static SecondState Instance
    {
        get
        {
            if (_instance == null)
            {
                new SecondState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        //Debug.Log("Entering Second state");
    }

    public override void ExitState(AI _owner)
    {
        //Debug.Log("Exiting Second state");
    }

    public override void UpdateState(AI _owner)
    {
        switch (_owner.state)
        {
            case AI.State.Flee:
                break;

            case AI.State.Approach:
                _owner.stateMachine.ChangeState(FirstState.Instance);
                break;

            case AI.State.Fight:
                _owner.stateMachine.ChangeState(ThirdState.Instance);
                break;

            default:
                Debug.Log("State Not Found");
                break;
        };

        float z = Mathf.Atan2((_owner.player.transform.position.y - _owner.transform.position.y), (_owner.player.transform.position.x - _owner.transform.position.x)) * Mathf.Rad2Deg + 90;

        _owner.transform.eulerAngles = new Vector3(0, 0, z);

        //_owner.transform.localEulerAngles = _owner.transform.eulerAngles + Vector3.forward(0, 190, -2 * _owner.transform.eulerAngles.z);

        _owner.rb.AddForce(_owner.gameObject.transform.up * _owner.speed);

        //_owner.transform.Rotate((Vector3.forward * Time.deltaTime) * 1000);
    }
}
