using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStat : MonoBehaviour
{
    [SerializeField]
    Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
            var itemGain = collision.GetComponent<ItemAddController>();
            if (itemGain.TryItemGet(item))
            {
                Destroy(gameObject, 0.2f);
            }
            itemGain.UseItemGet(item);
        }
    }

}
