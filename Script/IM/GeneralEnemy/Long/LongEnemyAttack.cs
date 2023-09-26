using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongEnemyAttack : MonoBehaviour
{
    public GameObject bullet;
    EnemyAI enemyAI;
    Vector2 firePos;
    Damageable damageable;
   

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        StartCoroutine(LongAttack());
        damageable = GetComponent<Damageable>();
        //firePos = transform.GetChild(1).transform.position;
    }

   /* public void LongAttack()
    {
        GameObject a = Instantiate(bullet, transform.position, Quaternion.identity);
        Bullet _bullet = a.GetComponent<Bullet>();
        _bullet.FirePos(enemyAI.playerTr.position);
 
    }*/


    IEnumerator LongAttack()
    {
        yield return new WaitForSeconds(0.1f);
        while (damageable.islive)
        {
            yield return new WaitForSeconds(0.1f);
            firePos = transform.GetChild(1).transform.position;
            if (enemyAI.state == EnemyAI.GeneralEnemyState.ATTACK)
            {
                if(transform.position.x > enemyAI.playerTr.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
                yield return new WaitForSeconds(0.7f);
                GameObject a = Instantiate(bullet, firePos, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                Bullet _bullet = a.GetComponent<Bullet>();
                _bullet.FirePos(enemyAI.playerTr.position);
                yield return new WaitForSeconds(1f);

            }
        }
    }


    
}
