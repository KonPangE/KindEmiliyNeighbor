using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soon : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("°¡½Ã ¹ÚÈû");
            Destroy(gameObject);
        }
    }
}
