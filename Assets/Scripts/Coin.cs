using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.transform.tag == "Player")
        {

            GameManager.instance.UpdateCoin();
            AudioManager.instance.PlayAudio(0);
            Destroy(gameObject);
        }
    }
}
