using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> gameItems;
    public Dictionary<GameObject, int> inventory = new Dictionary<GameObject, int>();

    // Start is called before the first frame update
    void Start()
    {
        gameItems = new List<GameObject>(Resources.LoadAll<GameObject>("Pick"));

        // cargar items al inventario
        for (int i = 0; i < gameItems.Count; i++)
            inventory.Add(gameItems[i], 0);

        PrintInventory(); 
    }

    public void AddItem(string itemID, int count)
    {
        foreach (KeyValuePair<GameObject, int> item in inventory)
        {
            PickUp invItem = item.Key.GetComponent<PickUp>();
            if (invItem.itemID == itemID)
            {
                inventory[item.Key] += count;
                print("Has recogido: " + itemID + " x" + count + ". Ahora tienes: " + inventory[item.Key]);
                break;
            }
        }
    }

    public void PrintInventory()
    {
        foreach (KeyValuePair<GameObject, int> item in inventory)
            print("Item: " + item.Key + " - Cantidad: " + item.Value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
