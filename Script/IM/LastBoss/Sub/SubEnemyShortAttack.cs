using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEnemyShortAttack : MonoBehaviour
{
    [SerializeField]
    SubEnemyAI subEnemy;
    BoxCollider2D coll;

    void Start()
    {
        subEnemy = GetComponentInParent<SubEnemyAI>();
        coll = GetComponent<BoxCollider2D>();
        StartCoroutine(Hit());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && subEnemy.state == SubEnemyAI.SubEnemyState.ATTACK)
        {
            Debug.Log("근접 서브 몹 공격 히트");
        }
    }

    IEnumerator Hit()
    {
        while(!subEnemy.playerDie)
        {
            yield return new WaitForSeconds(0.1f);
            if(subEnemy.state == SubEnemyAI.SubEnemyState.ATTACK)
            {
                yield return new WaitForSeconds(0.2f);
                coll.enabled = true;
            }
            else
            {
                coll.enabled = false;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
