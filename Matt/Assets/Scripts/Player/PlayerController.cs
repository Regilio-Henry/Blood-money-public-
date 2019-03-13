using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    Vector3 movement;
    public Animator animator;
    TrailRenderer DashTrail;

    GameObject SpellAttack;
    Animator SpellAnimator;

    [SerializeField]
    float Speed = 5;

    [SerializeField]
    float DashDistance = 4f;
    [SerializeField]
    float DashRate = .3f;
    private float NextDash;

    [SerializeField]
    float AttackRate = .3f;
    private float NextAttack;

    [SerializeField]
    float SpellAttackRate = 1f;
    private float NextSpell;

    public ScreenShake screenShake;
    [SerializeField]
    public float shakeMag = .4f;
    [SerializeField]
    public float shakeDur = .15f;


    
   

   

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        DashTrail = GameObject.Find("DashTrail").GetComponent<TrailRenderer>();
        SpellAttack = GameObject.Find("SpellAttackCrontroller");
        SpellAnimator = SpellAttack.GetComponent<Animator>();
    }

        // Update is called once per frame
        void Update()
    {
        HandleMovement();
        HandleDodge();
        HandleAttack();
        
    }

    void HandleMovement()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement.normalized * Speed * Time.deltaTime;
    }

    void HandleDodge()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > NextDash) // space and the B Button on the Xbox controller are used for the dash, could easily be changed
        {
            NextDash = Time.time + DashRate;
            //dash in the direction of movement
            DashTrail.emitting = true;
            transform.position=(transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * DashDistance, Input.GetAxisRaw("Vertical") * DashDistance)); //dash distance can be changed and will determine how far the dash goes
            StartCoroutine(screenShake.Shake(shakeDur, shakeMag));

            Invoke("EndTrail", .15f);
        }
    }

    public void EndTrail()
    {
        DashTrail.emitting = false;
    }

    void HandleAttack()
    {
        if (Input.GetKey(KeyCode.LeftShift))//if the user is holding down shift, do a ranged attack
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > NextSpell)
            {
                Vector3 AttackPos;
                AttackPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                AttackPos.z = 0;
                AttackPos.y += 2.0f;
                SpellAttack.transform.position = AttackPos;

                SpellAnimator.SetTrigger("Lightning");

                NextSpell = Time.time + SpellAttackRate;

            }
        }
        else // if the user isnt holding down shift, do a normal melee attack
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > NextAttack)
            {
                NextAttack = Time.time + AttackRate;
                animator.SetTrigger("Attack");
            }
        }
    }
}
