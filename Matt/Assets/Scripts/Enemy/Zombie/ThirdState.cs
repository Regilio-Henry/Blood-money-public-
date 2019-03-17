using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class ThirdState : State<AI>
{
    private static ThirdState _instance;

    private ThirdState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ThirdState Instance
    {
        get
        {
            if (_instance == null)
            {
                new ThirdState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        //Debug.Log("Entering Third state");
    }

    public override void ExitState(AI _owner)
    {
        //Debug.Log("Exiting Third state");
    }

    public override void UpdateState(AI _owner)
    {
        switch (_owner.state)
        {
            case AI.State.Flee:
                _owner.stateMachine.ChangeState(SecondState.Instance);
                break;

            case AI.State.Approach:
                _owner.stateMachine.ChangeState(FirstState.Instance);
                break;

            case AI.State.Fight:
                break;

            default:
                Debug.Log("State Not Found");
                break;
        };
        // float z = Mathf.Atan2((_owner.player.transform.position.y - _owner.transform.position.y), (_owner.player.transform.position.x - _owner.transform.position.x)) * Mathf.Rad2Deg + 45;

        // _owner.transform.eulerAngles = new Vector3(0, 0, z);

        _owner.transform.Rotate((Vector3.forward * Time.deltaTime) * 1000);

        //_owner.rb.AddForce(_owner.gameObject.transform.up * _owner.speed);
    }
}
