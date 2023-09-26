using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEnemyLongAttack : MonoBehaviour
{
    public GameObject bullet;
    SubEnemyAI subEnemy;
    Vector2 firePos1, firePos2;


    void Start()
    {
        subEnemy = GetComponent<SubEnemyAI>();
        StartCoroutine(Attack());
    }

   
    void Update()
    {
        
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.1f);
        if (subEnemy != null)
        {
            while(!subEnemy.playerDie)
            {
                firePos1 = transform.GetChild(0).transform.position;
                firePos2 = transform.GetChild(1).transform.position;
                yield return new WaitForSeconds(0.1f);
                if(subEnemy.state == SubEnemyAI.SubEnemyState.ATTACK)
                {
                    if(transform.position.x > subEnemy.playerTr.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    }
                    yield return new WaitForSeconds(0.7f);
                    GameObject _fire1 = Instantiate(bullet, firePos1, Quaternion.identity);
                    GameObject _fire2 = Instantiate(bullet, firePos2, Quaternion.identity);
                    yield return new WaitForSeconds(0.1f);
                    Bullet _bullet1 = _fire1.GetComponent<Bullet>();
                    Bullet _bullet2 = _fire2.GetComponent<Bullet>();
                    _bullet1.FirePos(subEnemy.playerTr.position);
                    _bullet2.FirePos(subEnemy.playerTr.position);
                    yield return new WaitForSeconds(1f);
                }

            }
        }
    }

}
