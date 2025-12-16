using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItems : MonoBehaviour
{
    private ItemManager im;
    private HpManagerPlayer hpm;
    [SerializeField] private int hp;
    [SerializeField] private CountPanelUI potionHPUI;
    void Start()
    {
        im = GetComponent<ItemManager>();
        hpm = GetComponent<HpManagerPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        UsingHealingPotion();
    }

    private void UsingHealingPotion()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach(KeyValuePair<GameObject, int> item in im.inventory)
            {
                PickUp invItem = item.Key.GetComponent<PickUp>();
                if (invItem.itemID == "PotionHP")
                {
                    if (item.Value > 0)
                    {
                        hpm.Heal(invItem.load);
                        im.inventory[item.Key] -= 1;
                        print("Usaste PotionHP. Vida: " + hpm.Hp);
                        im.PrintInventory();
                        
                        // Update UI
                        potionHPUI.AddQuantity(invItem.itemID, -1);
                        
                        break;
                    }   
                }
            }
        }
    }
}
