using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public string itemID;
    public int count = 1;
    public int load = 1;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            ItemManager itemMgr = collision.GetComponent<ItemManager>();
            itemMgr.PrintInventory();
            itemMgr.AddItem(itemID, count);
            gameObject.SetActive(false);
        }
    }
}
