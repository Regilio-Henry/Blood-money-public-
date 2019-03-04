using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject SpiderWebAttack;
    public Transform SpawnPointWeb;

    
    void FixedUpdate()
    {
        bool attack = Input.GetButtonDown("Fire1");

        if (attack) Instantiate(SpiderWebAttack, SpawnPointWeb.position, SpawnPointWeb.rotation);

        
    }
}
