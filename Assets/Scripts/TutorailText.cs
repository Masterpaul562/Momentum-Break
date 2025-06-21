using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorailText : MonoBehaviour
{

    public GameObject Text;
  void OnTriggerEnter2D(Collider2D other)
    {
        Text.SetActive(true);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Text.SetActive(false);
    }
}
