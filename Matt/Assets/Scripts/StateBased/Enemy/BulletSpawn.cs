using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject SpiderWebAttack;
    public Transform SpawnPointWeb;
    public float gameTimer;
    public int seconds = 0;
    //public bool attack = false; 

    private void Start()
    {
        gameTimer = Time.time;
    }
    void FixedUpdate()
    {

        if(Time.time > gameTimer +1)
        {
            //attack = false; 
            gameTimer = Time.time;
            seconds++;
        }

        if (seconds == 5)
        {
            Instantiate(SpiderWebAttack, SpawnPointWeb.position, SpawnPointWeb.rotation);
            seconds = 0;
        }

        //bool attack = Input.GetButtonDown("Fire1");

        //if (attack) Instantiate(SpiderWebAttack, SpawnPointWeb.position, SpawnPointWeb.rotation);

        
    }
}
