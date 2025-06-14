using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Vector3 position;
    public GameObject PlayerRef;
    public GameObject Enemy;
    public int count;
    
    void Start()
    {
     position = new Vector3(Random.Range(55, 70), transform.position.y);
        for (int i = 0; i < 6; i++)
        {
            var newEnemy = Instantiate(Enemy, position, transform.rotation);
            newEnemy.GetComponent<EnemyBase>().player = PlayerRef;
            newEnemy.GetComponent<EnemyBase>().Spawner = this.gameObject;
            position = new Vector3(Random.Range(55, 70), transform.position.y);
            count = i+1;
        }
    }
   
}
