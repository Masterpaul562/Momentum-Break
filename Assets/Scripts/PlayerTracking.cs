using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -3); ; 
    }
}
