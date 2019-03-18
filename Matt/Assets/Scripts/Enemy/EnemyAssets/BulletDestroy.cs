using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{/*
    public Transform bullet;
    public float colissionRadius = 0.4f;
    public bool collided = false;
    public LayerMask whatToCollideWith;

    void FixedUpdate()
    {
        collided = Physics2D.OverlapCircle(bullet.position, colissionRadius, whatToCollideWith); 

        if (collided) Destroy(gameObject);
        if (!GetComponent<Renderer>().isVisible) Destroy(gameObject);
        
    }
    */
    void OnTriggerStay2D(Collider2D col) //On collision with an object with the EnemyAttack tag it will reduce the hp of the player.
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") || col.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }

        if (!GetComponent<Renderer>().isVisible) Destroy(gameObject);
    }
}
