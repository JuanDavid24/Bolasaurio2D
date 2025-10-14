using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.potions += 1;
            Debug.Log("Poción recogida! Total: " + player.potions);
            gameObject.SetActive(false);
        }
    }
}
