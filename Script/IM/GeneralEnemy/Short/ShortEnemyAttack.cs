using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortEnemyAttack : MonoBehaviour
{
    EnemyAI enemyAI;
    BoxCollider2D boxCollider;
    public EnemyDate enemy;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //플레이어 체력 몬스터 공격력 만큼 체력 감소
            Debug.Log("플레이어 체력 감소/근거리 몹");
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 transknockback = transform.rotation.y > -1 ? enemy.knockback : new Vector2(-(enemy.knockback.x), enemy.knockback.y);
            //데미지 함수 호출 
            bool getdamage = damageable.Damage(enemy.damage, transknockback);
            if (getdamage)
                Debug.Log(collision.name + "hit" + enemy.damage);
        }
    }

    IEnumerator AttackLayer()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (enemyAI.state == EnemyAI.GeneralEnemyState.ATTACK && enemyAI.attackCheack < 1)
            {
                yield return new WaitForSeconds(0.5f);
                boxCollider.enabled = true;
                yield return new WaitForSeconds(0.1f);
                boxCollider.enabled = false;
            }
           
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        enemyAI = transform.parent.GetComponent<EnemyAI>();
        boxCollider.enabled = false;
        StartCoroutine(AttackLayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
