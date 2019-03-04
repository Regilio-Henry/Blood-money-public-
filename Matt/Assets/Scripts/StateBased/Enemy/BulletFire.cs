using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public float bulletForce = 750.0f;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "FirePoint") GetComponent<Rigidbody2D>().AddForce(transform.right * bulletForce);
    }

}
