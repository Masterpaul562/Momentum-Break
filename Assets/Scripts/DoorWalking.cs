using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWalking : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Transform exit;
    private float y;

    private void Start()
    {
        exit = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        y= Input.GetAxisRaw("Vertical");
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(y ==1 && other.gameObject == player)
        {
            player.transform.position = exit.position;
            player.GetComponent<Move_Player>().hasEnteredRoom = true;
        }
    }
}
