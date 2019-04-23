using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
 
    public GameObject[] enemy;                // The enemy prefab to be spawned.
    [SerializeField]
    public float spawnTime = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    int enemyIndex;

    void Start()
    {
        //transform.GetChild(0).GetChild(0).name;
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        if(Random.value < .3f) //get a random value between 0 and 1. if it is below .3 (which is a 30% chance) set enemyIndex to 0 (this is a spider)
        {
            enemyIndex = 0;
        }
        else // else the value will be above .3 (This is a 70% chance) this will set the enemy index to 1 (this is a skeleton)
        {
            enemyIndex = 1;
        }

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
