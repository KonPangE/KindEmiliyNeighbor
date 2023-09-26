using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public EnemyDate enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 transknockback = transform.position.x < collision.transform.position.x  ? enemy.knockback : new Vector2(-(enemy.knockback.x), enemy.knockback.y);
            //������ �Լ� ȣ�� 
            bool getdamage = damageable.Damage(enemy.damage, transknockback);
            if (getdamage)
                Debug.Log(collision.name + "hit" + enemy.damage);
        }


    }
}
