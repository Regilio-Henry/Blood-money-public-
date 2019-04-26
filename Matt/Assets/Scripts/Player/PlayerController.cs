using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    Vector3 movement;
    public Animator animator;
    TrailRenderer DashTrail;

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

    public GameObject SpellAttackPreFab;
    public Transform SpellAttackFirePoint;

    public GameObject ArrowPreFab;
    public Transform FirePoint;

    [SerializeField]
    float SpellAttackRate = 1f;
    private float NextSpell;

    [SerializeField]
    float RangedAttackRate = .5f;
    private float NextRangedAttack;

    public ScreenShake screenShake;
    [SerializeField]
    public float shakeMag = .4f;
    [SerializeField]
    public float shakeDur = .15f;

    [SerializeField]
    GameObject gameController;



    public delegate void playerEvents();
    public static event playerEvents onDodge;
    //public static event playerEvents 

    public AudioClip thunderSound;
    public AudioClip arrowSound;
    public AudioClip dashSound;
    public AudioClip slashSound;

    public AudioSource audioSource;



    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController");
        DashTrail = GameObject.Find("DashTrail").GetComponent<TrailRenderer>();
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
        if (Time.timeScale != 0)
        {
            if (!Input.GetKey(KeyCode.LeftShift))//only move if the player isnt holding shift
            {
                movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);

                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Magnitude", movement.magnitude);

                transform.position = transform.position + movement.normalized * Speed * Time.deltaTime;
            }
            else
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 0);
                animator.SetFloat("Magnitude", 0);
                animator.SetTrigger("ShiftHeld");
            }
        }
    }

    void HandleDodge()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > NextDash && movement.magnitude != 0) // space  used for the dash, could easily be changed
            {
                NextDash = Time.time + DashRate;
                //dash in the direction of movement
                DashTrail.emitting = true;
                transform.position = (transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * DashDistance, Input.GetAxisRaw("Vertical") * DashDistance)); //dash distance can be changed and will determine how far the dash goes
                StartCoroutine(screenShake.Shake(shakeDur, shakeMag));

                if (onDodge != null)
                {
                    onDodge();
                }
                audioSource.PlayOneShot(dashSound);
                Invoke("EndTrail", .15f);
            }
        }

    }

    public void EndTrail()
    {
        DashTrail.emitting = false;
    }

    public bool checkForAbility(string name)
    {
        foreach(Ability a in gameController.GetComponent<ChallengeBuilder>().selectedAbilites)
        {
            if(a.weaponName == name)
            {
                return true;
            }
        }

        return false;
    }

    public void setKillcount(string tag)
    {
        if ("Skeleton" == tag)
        {

        }

        if ("Spider" == tag)
        {

        }
    }

    void HandleAttack()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))//if the user is holding down shift, do a ranged attack
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > NextRangedAttack) // Shift + Left Mouse Button  -   Ranged Attack
                {
                    if (checkForAbility("arrow"))
                    {
                        NextRangedAttack = Time.time + RangedAttackRate;
                        if (this.GetComponent<AmmoBar>().GetAmmo() >= 1)
                        {
                            Instantiate(ArrowPreFab, FirePoint.position, FirePoint.rotation);
                            this.GetComponent<AmmoBar>().ChangeAmmo(-1);
                            audioSource.PlayOneShot(arrowSound);
                        }
                    }

                }
                else if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > NextSpell) // Shift + Right Mouse Button    -   Spell Attack
                {
                    if (checkForAbility("storm"))
                    {
                        Vector3 AttackPos;
                        AttackPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        AttackPos.z = 0;
                        AttackPos.y += 2.0f;
                        audioSource.PlayOneShot(thunderSound);
                        SpellAttackFirePoint.transform.position = AttackPos;

                        Instantiate(SpellAttackPreFab, SpellAttackFirePoint.position, SpellAttackFirePoint.rotation);

                        NextSpell = Time.time + SpellAttackRate;
                    }

                }
            }
            else // if the user isnt holding down shift, do a normal melee attack
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > NextAttack)
                {
                    audioSource.PlayOneShot(slashSound);
                    NextAttack = Time.time + AttackRate;
                    animator.SetTrigger("Attack");
                }
            }
        }
    }
}
