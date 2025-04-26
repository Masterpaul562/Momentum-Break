using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool shouldSpawn = true;
    public GameObject PlayerRef;
    public GameObject Enemy;
    void Update()
    {
        if (shouldSpawn)
        {
            shouldSpawn = false;
            StartCoroutine(shouldSpawnE());
        }
         IEnumerator shouldSpawnE()
        {
            yield return new WaitForSeconds(5);
            shouldSpawn = true;
            var newEnemy = Instantiate(Enemy, transform.position, transform.rotation);
            newEnemy.GetComponent<EnemyBase>().player = PlayerRef;
        }
    }
}
