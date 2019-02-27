using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStuff // could be System.Serializable or namespace so is viewable in unity as its not a Monobehaviour.
{

    public class StateMachine<T>  //other files not just AI can use the state machine 
    {
        public State<T> currentState { get; private set; } //doesnt declare previous or future states
        public T Owner; // the object using the state machine 

        public StateMachine(T _o) //sets the starting state to null. 
        {
            Owner = _o;
            currentState = null;
        }

        public void ChangeState(State<T> _newstate) //how machine goes through states.
        {
            if (currentState != null)
                currentState.ExitState(Owner);
            currentState = _newstate;
            currentState.EnterState(Owner);
        }

        public void Update() //
        {
            if (currentState != null)
                currentState.UpdateState(Owner);
        }
    }

    public abstract class State<T> //abstract as its what all state files will be based on. 
    {
        public abstract void EnterState(T _owner); // done once when you enter the state 
        public abstract void ExitState(T _owner); // done once you exit the state
        public abstract void UpdateState(T _owner); // done on update while in state
    }

}
