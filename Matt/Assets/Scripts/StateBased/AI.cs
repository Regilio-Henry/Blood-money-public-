using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

//Currently just a timer to switch states using a bool to switch between states every 5 seconds. 
public class AI : MonoBehaviour
{
    public bool switchState = false;
    public float gameTimer;
    public int seconds = 0;
    public Transform enemy;
    public float speed;
    public Transform player;
    public Rigidbody2D rb;

    public StateMachine<AI> stateMachine { get; set; }


    private void Start()
    {
        stateMachine = new StateMachine<AI>(this);
        stateMachine.ChangeState(FirstState.Instance); // use instance that already exists.
        gameTimer = Time.time;
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update() //Used to switch between states and call the statemachine update. 
    {
        if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
            Debug.Log(seconds); //log for testing. 
        }

        if (seconds == 5)
        {
            seconds = 0; //reset seconds. 
            switchState = !switchState; //switch the state to the opposite state. 
        }

        stateMachine.Update(); //when update is called on ai it will call update in the state. 
    }
}
