using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

//Not a real ai just a timer to switch states. https://www.youtube.com/watch?v=PaLD1t-kIwM
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

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.GetComponent<EdgeCollider2D>())
    //    {
    //        float z = Mathf.Atan2((_owner.player.transform.position.y - _owner.transform.position.y), (_owner.player.transform.position.x - _owner.transform.position.x)) * Mathf.Rad2Deg - 90;

    //        _owner.transform.eulerAngles = new Vector3(0, 0, z);

    //        _owner.rb.AddForce(_owner.gameObject.transform.up * _owner.speed);
    //    }
    //}

    private void Start()
    {
        stateMachine = new StateMachine<AI>(this);
        stateMachine.ChangeState(FirstState.Instance);
        gameTimer = Time.time;
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        if (Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
            Debug.Log(seconds);
        }

        if (seconds == 5)
        {
            seconds = 0;
            switchState = !switchState;
        }

        stateMachine.Update();
    }
}
