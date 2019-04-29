using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != 12 && other.gameObject.tag != "Wall" && other.gameObject.layer != 9)
            Destroy(other.gameObject);
    }
}
