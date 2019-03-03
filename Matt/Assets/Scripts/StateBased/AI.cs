using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

 
public class AI : MonoBehaviour
{
    public bool switchState = false;
    public float gameTimer;
    public int seconds = 0;
    public Transform enemy;
    public float speed;
    public Transform player, sightStart, sightEnd;
    public Rigidbody2D rb;

    public bool rayTest = false; //bool for raycast to see if target is within range to enter attackstate
    public float TargetDistance;

   

    public StateMachine<AI> stateMachine { get; set; }


    float distance;
    public enum State //can add names of new states here
    {
        Approach, //first state
        Flee, //second state
        Fight //third state
    };

    public State state = State.Approach;

    private void Start()
    {
        stateMachine = new StateMachine<AI>(this);
        stateMachine.ChangeState(FirstState.Instance); // use instance that already exists.
        gameTimer = Time.time;
        rb = GetComponent<Rigidbody2D>();
        distance = Vector2.Distance(transform.position, player.transform.position);

    }

    void Raycasting()
    {
        // Debug.DrawLine(transform.position, player.position, Color.red); //just draws a line between sightStart object and sightEnd object.

        Vector2 direction = player.position - transform.position; //subtract origin from target to get direction
        Debug.DrawRay(transform.position, direction, Color.green);
        LayerMask playerMask = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, playerMask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player")
            {

                Debug.Log(distance + " " + hit.collider.gameObject.name);

            }
        }
    }

    

    private void Update() //Used to switch between states and call the statemachine update. 
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Raycasting();
         


        if (distance < 5 && distance > 2)
        {
            state = State.Flee; //fight = second state
            Debug.Log("State Change");
        }
        if (distance > 10)
        {
            state = State.Approach; //approach = first state 
        }
        if (distance <= 2)
        {
            state = State.Fight; //fight = third state
        }

        stateMachine.Update(); //when update is called on ai it will call update in the state. 


    }
}
