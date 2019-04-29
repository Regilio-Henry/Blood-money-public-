using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

 
public class AI : MonoBehaviour
{
    public bool switchState = false;
    public float gameTimer;
    //public float startTime;
    public int seconds = 0;
    public Transform enemy;
    public float speed;
    public Transform sightStart, sightEnd;
    public Rigidbody2D rb;
    public GameObject player;
    public GameObject SkeleblobSpawn;
    public bool rayTest = false; //bool for raycast to see if target is within range to enter attackstate
    public float TargetDistance;

    EnemyHealth enemyHealth; //Starting testing for adding hp. calls the EnemyHealth script. 

    public StateMachine<AI> stateMachine { get; set; }

    public delegate void EnemyEvents();
    public static event EnemyEvents onkillSkeleton;
    

    private void OnDestroy()
    {
        onkillSkeleton();
    }

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
        //startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        SkeleblobSpawn = GameObject.Find("SkeleblobSpawn");
        distance = Vector2.Distance(transform.position, player.transform.position);
        distance = Vector2.Distance(transform.position, SkeleblobSpawn.transform.position);
        enemyHealth = GetComponent<EnemyHealth>();  //Gets the compnonent for enemy health. 
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
        gameTimer = (int)(Time.timeSinceLevelLoad % 60f);
        distance = Vector2.Distance(transform.position, player.transform.position);
        Raycasting();
        

        if (distance > 1.5 && gameTimer < 60 && gameTimer > 70)
        {
            state = State.Approach; //approach = first state 
        }
        if (gameTimer >= 60 && gameTimer <= 70)
        {
            state = State.Flee; //flee = second state
            //Debug.Log("State Change");
        }
        if (distance <= 1.5 && gameTimer < 60 && gameTimer > 70)
        {
            state = State.Fight; //fight = third state
        }

        stateMachine.Update(); //when update is called on ai it will call update in the state. 
    }
    void OnTriggerEnter2D(Collider2D col)
    {       
        if (col.gameObject.tag == "SkeleblobSpawn" && state == State.Flee)
        {            
            Destroy(gameObject);
        }
    }
}
