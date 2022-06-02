using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.transform.tag == "Player")
        {
            target.GetComponent<PlayerController>().RestartLevel();
            
        }
    }
}
