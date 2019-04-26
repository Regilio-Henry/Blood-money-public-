using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttack : MonoBehaviour
{

    public Animator SpellAnimator;

    // Start is called before the first frame update
    void Start()
    {
        SpellAnimator.SetTrigger("Lightning");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Wall" && other.gameObject.layer != 9)
        {
            Destroy(other.gameObject);
        }
    }
}

                   
