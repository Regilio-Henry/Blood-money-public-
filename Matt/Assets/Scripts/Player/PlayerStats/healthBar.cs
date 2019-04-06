using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class healthBar : MonoBehaviour
{
    public int totalHealth;
    public Transform healthContainer;
    public GameObject healthSlot;
    List<GameObject> healthSlots = new List<GameObject>();
    float currentHealth;
    float currentAmount;

    public float IFrameTime = 1.0f;
    SpriteRenderer player;
    float NextHit;

    /*
    public float IFramesTimer;
    float FlickerRate = .1f;
   
    float flickerTimer = .0f;  */

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;       
        for (int i = 0; i < totalHealth; i++)
        {
            var slot = Instantiate(healthSlot);

            slot.transform.SetParent(healthContainer);
            healthSlots.Add(slot);
        }
        player = GameObject.Find("Player").GetComponent<SpriteRenderer>();
    }


    //Changes the current health value
    public void ChangeHealth(float value)
    {
        currentHealth += value;
        UpdateHealth();
    }

    void OnTriggerStay2D(Collider2D col) //On collision with an object with the EnemyAttack tag it will reduce the hp of the player.
    {
        if (col.gameObject.tag == "EnemyAttack" && Time.time > NextHit)
        {
            NextHit = Time.time + IFrameTime;
            ChangeHealth(-0.5f);
            InvokeRepeating("IFrames", 0, .1f);
        }
    }

    void IFrames()
    {
        player.enabled = !player.enabled;

        if (Time.time > NextHit)
        {
            CancelInvoke();
            player.enabled = true;
        }
    }

    //Updates the ui health
    public void UpdateHealth()
    {
        if (currentHealth >= 0)
        {
            if ((currentHealth % 1) != 0)
            {
                for (int i = 0; i < Mathf.Floor(currentHealth) - 1; i++)
                {
                    healthContainer.GetChild(i).GetComponent<Image>().fillAmount = 1;
                }

                healthContainer.GetChild(Mathf.FloorToInt(currentHealth)).GetComponent<Image>().fillAmount = .5f;


                for (int j = Mathf.FloorToInt(currentHealth) + 1; j < totalHealth; j++)
                {
                    healthContainer.GetChild(j).GetComponent<Image>().fillAmount = 0;
                }
            }
            else
            {
                for (int i = 0; i < Mathf.Floor(currentHealth); i++)
                {
                    healthContainer.GetChild(i).GetComponent<Image>().fillAmount = 1;
                }

                for (int j = Mathf.FloorToInt(currentHealth); j < totalHealth; j++)
                {
                    healthContainer.GetChild(j).GetComponent<Image>().fillAmount = 0;
                }
            }
        }
        else
        {
            for (int i = 0; i < totalHealth; i++)
            {
                healthContainer.GetChild(i).GetComponent<Image>().fillAmount = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(currentHealth);
        if (currentHealth <= 0) //if the players health is 0 it will destroy the player. 
        {
            Debug.Log("You died");
            Destroy(GameObject.Find("Player"));
        }
    }
}
