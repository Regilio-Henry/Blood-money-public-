using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobility : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0)) ;
    //}

    void FixedUpdate()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = rot; //transformaion required points to mouse 
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        float input = Input.GetAxis("Vertical");
        rb.AddForce (gameObject.transform.up * speed * input);

        
    }
}
