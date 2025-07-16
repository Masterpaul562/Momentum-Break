using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchColliders : MonoBehaviour
{
    private int knockbackX;
    private int knockbackY;
    public CameraShake cameraShake;
    [SerializeField] private GameObject player;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            SetKnockback();
            other.gameObject.GetComponent<EnemyControler>().BaseHit(1, knockbackX, knockbackY);
            if (knockbackX == 20)
            {
                StartCoroutine(cameraShake.Shake(0.1f, 0.2f));
            } else
            {
                StartCoroutine(cameraShake.Shake(0.07f, 0.1f));
            }
        }
       
    }
    public void SetKnockback()
    {
        knockbackX= player.GetComponent<Move_Player>().knockbackX; knockbackY =player.GetComponent<Move_Player>().knockbackY;
    }
}
