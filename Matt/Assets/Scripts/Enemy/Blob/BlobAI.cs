using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class BlobAI : MonoBehaviour
{
    public bool switchState = false;
    //public float startTime;
    public float gameTimer;
    public int seconds = 0;
    public Transform enemy;
    public float speed;
    public float standing;
    public Transform sightStart, sightEnd;
    public GameObject player;
    public GameObject SkeleblobSpawn;
    public Rigidbody2D rb;

    public bool rayTest = false; //bool for raycast to see if target is within range to enter AttackState
    public float TargetDistance;

    public delegate void EnemyEvents();
    //public static event EnemyEvents onkillSpider;

    public StateMachine<BlobAI> stateMachine { get; set; }


    //private void OnDestroy()
    //{
    //    onkillSpider();
    //}

    float distance;
    public enum State //can add names of new states here
    {
        Approach, //first state
        Merge, //second state
        Fight //third state
    };

    public State state = State.Approach;

    private void Start()
    {
        stateMachine = new StateMachine<BlobAI>(this);
        stateMachine.ChangeState(BlobApproachState.Instance); // use instance that already exists.
        //startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        SkeleblobSpawn = GameObject.Find("SkeleblobSpawn");
        distance = Vector2.Distance(transform.position, player.transform.position);
        distance = Vector2.Distance(transform.position, SkeleblobSpawn.transform.position);

    }

    void Raycasting()
    {
        // Debug.DrawLine(transform.position, player.position, Color.red); //just draws a line between sightStart object and sightEnd object.

        Vector2 direction = player.transform.position - transform.position; //subtract origin from target to get direction
        Debug.DrawRay(transform.position, direction, Color.green);
        LayerMask playerMask = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, playerMask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player")
            {

                //Debug.Log(distance + " " + hit.collider.gameObject.name);

            }
        }
    }



    private void Update() //Used to switch between states and call the statemachine update. 
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Raycasting();
        gameTimer = (int)(Time.timeSinceLevelLoad % 60f);


        if (gameTimer >= 59 && gameTimer <= 70)
        {
            state = State.Merge;
        }
        else if (distance > 1.5 && gameTimer < 59 && gameTimer > 70)
        {
            state = State.Approach; //approach = first state 
        }      
        else if (distance <= 1.5 && gameTimer < 59 && gameTimer > 70)
        {
            state = State.Fight; //fight = third state
        }

        stateMachine.Update(); //when update is called on ai it will call update in the state. 
    }
}
