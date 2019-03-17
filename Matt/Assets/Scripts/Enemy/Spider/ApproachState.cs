using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class ApproachState : State<SpiderAI>
{
    private static ApproachState _instance; //only declared one time. can only be created from within the state class. 

    private ApproachState()
    {
        if (_instance != null)
        {
            return;
        }


        _instance = this;
    }

    public static ApproachState Instance
    {
        get
        {
            if (_instance == null)
            {
                new ApproachState(); //if the state doesnt already exist, create it. 
            }

            return _instance;
        }
    }

    public override void EnterState(SpiderAI _owner) //This is performed once upon entering state. 
    {
        //Debug.Log("Entering first state");
    }

    public override void ExitState(SpiderAI _owner) //this is performed once upon exiting state.
    {
        //Debug.Log("Exiting first state");
    }

    public override void UpdateState(SpiderAI _owner) //this is performed on update
    {
        //Debug.Log(_owner.state);
        switch (_owner.state)
        {
            case SpiderAI.State.Flee:
                _owner.stateMachine.ChangeState(FleeState.Instance);
                break;

            case SpiderAI.State.Approach:
                break;

            case SpiderAI.State.Fight:
                _owner.stateMachine.ChangeState(AttackState.Instance);
                break;

            default:
                Debug.Log("State Not Found");
                break;
        };

        float z = Mathf.Atan2((_owner.player.transform.position.y - _owner.transform.position.y), (_owner.player.transform.position.x - _owner.transform.position.x)) * Mathf.Rad2Deg - 90;

        _owner.transform.eulerAngles = new Vector3(0, 0, z);

        _owner.rb.AddForce(_owner.gameObject.transform.up * _owner.speed);



    }
}
