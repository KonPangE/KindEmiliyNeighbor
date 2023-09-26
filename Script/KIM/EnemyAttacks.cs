using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public EnemyDate enemy;
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
}
