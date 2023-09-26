using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public EnemyMove move;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ENEMY"))
        {
            move = collision.transform.parent.GetComponent<EnemyMove>();
           
                move.speed *= -1f;
            
        }
    }

}
