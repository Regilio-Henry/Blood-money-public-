using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject Player;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    float FollowTime = .2f;
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Player.transform.position;
        pos.z = -20;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, FollowTime);
    }
   
  

}
