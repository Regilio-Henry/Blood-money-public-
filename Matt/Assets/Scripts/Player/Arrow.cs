using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        Vector3 MousePos;
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 VectorToTarget = MousePos - transform.position;
        var dir = (VectorToTarget).normalized;

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //rotate towards the mouse
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        rb.velocity = VectorToTarget * speed;

    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg - 180f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
        else if(col.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }

}
/*
Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
Vector3 dir = (Input.mousePosition - sp).normalized;

float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
float spread = Random.Range(-10, 10);
Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle + spread));

// Instantiate the bullet using our new rotation
GameObject bullet = (GameObject)GameObject.Instantiate(bulletPrefab, transform.position, bulletRotation);
*/