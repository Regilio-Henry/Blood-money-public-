using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animator animator;
    Rigidbody2D body;
    GameObject DashOb;
    TrailRenderer DashTrail;

    float horizontal;
    float vertical;

    [SerializeField]
    float moveLimiter = 0.7f;
    [SerializeField]
    public float runSpeed = 20;   
    [SerializeField]
    float dashDistance = 30f;

    public ScreenShake screenShake;
    [SerializeField]
    public float shakeMag = .4f;
    [SerializeField]
    public float shakeDur = .15f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

        DashOb = GameObject.Find("Trail");
        DashTrail = DashOb.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {                   // i have split up the code into functions to make it easier to read and less confusing.
        HandleDodge();      
        HandleMovement();
        HandleMelee();
    }



    private void HandleMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");    //gets the input from the input manager called Horizontal this is set to the keys A and D as well as the x axsis of the left joystick
        vertical = Input.GetAxisRaw("Vertical");        //gets the input from the input manager called Vertical this is set to the keys W and S as well as the Y axsis of the left joystick

        if (horizontal != 0 && vertical != 0) // Check for diagonal movement so that i can make diaginal movement the same speed as horizontal and vertical movement
        { 
            body.velocity = new Vector2((horizontal * runSpeed) * moveLimiter, (vertical * runSpeed) * moveLimiter); // move at less speed
            animator.SetBool("Moving", true); //sets a bool in the animator to true so it knows to play the moving animation
        }

        else // if not moving diagonally
        {
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed); // move at normal speed
            animator.SetBool("Moving", true);//sets a bool in the animator to true so it knows to play the moving animation
        }
        if (body.velocity == new Vector2(0, 0)) // if theres no velocity the player is not moving so set the "Moving" Bool to false
        {
            animator.SetBool("Moving", false);
        }
        if(horizontal > 0)
        {
            animator.SetInteger("dir", 1);
        }
        else if (horizontal < 0)
        {
            animator.SetInteger("dir", 3);
        }
        else if (vertical > 0)
        {
            animator.SetInteger("dir", 0);
        }
        else if (vertical < 0)
        {
            animator.SetInteger("dir", 2);
        }


    }

    private void HandleDodge()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1)) // space and the B Button on the Xbox controller are used for the dash, could easily be changed
        {

            //dash in the direction of movement

            DashTrail.emitting = true;
            body.MovePosition(transform.position + new Vector3(horizontal * dashDistance, vertical * dashDistance)); //dash distance can be changed and will determine how far the dash goes
            StartCoroutine(screenShake.Shake(shakeDur, shakeMag));

            Invoke("DoSomething", .15f);

        }
    }

    public void DoSomething()
    {
        DashTrail.emitting = false;
    }

    private void HandleMelee()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Joystick1Button0)) // if the left mouse button or A button on the Xbox controller is pressed then set the attacking bool to true
        {
            animator.SetBool("Attacking", true); // set the bool to true so it will play the attacking animation
        }
        else
        {
            animator.SetBool("Attacking", false);// set the bool to false if the above if statement doesnt return true. this stops the attacking animation fron being played to many times
        }
/*
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Joystick1Button2))// if the righ mouse button or X button on the Xbox controller is pressed then set the blocking bool to true
        {
            animator.SetBool("Blocking", true);
            //could add code here so that the player takes reduced damage.
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.Joystick1Button2)) // when the user lets go of the keys set the blocking bool to false
        {
            animator.SetBool("Blocking", false);
           
        }*/
    }
}

