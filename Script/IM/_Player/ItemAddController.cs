using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddController : MonoBehaviour
{

    PlayerStateData playerStateData;

    [SerializeField]
    PlayerUI playerUI;

    void Start()
    {
        playerStateData = GetComponent<PlayerStateData>();
    }

    public void UseItemGet(Item abil)
    {
        if (abil.itemType == Item.ItemType.UseItem)
        {
            if (playerUI.slots[0].item != null)
            {
                if (playerUI.slots[1].item != null)
                {
                    return;
                }
                else
                {
                    playerUI.slots[1].ItemAdd(abil);
                }
            }
            else
            {
                playerUI.slots[0].ItemAdd(abil);
            }
        }
        else if(abil.itemType == Item.ItemType.AbilityItem)
        {
            playerStateData.StatUp(abil);
        }
    }


    public bool TryItemGet(Item _item)
    {
        if (_item.itemType == Item.ItemType.UseItem)
        {
            if (playerUI.slots[0].item == null || playerUI.slots[1].item == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(_item.itemType == Item.ItemType.AbilityItem)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
