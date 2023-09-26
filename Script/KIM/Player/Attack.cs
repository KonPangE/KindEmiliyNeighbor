using UnityEngine;

public class Attack : MonoBehaviour
{

    public PlayerStateData playerStateData;
    Vector2 knockback;

    private void Awake()
    {
        knockback = playerStateData.knockback;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 transknockback = transform.rotation.y > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            //데미지 함수 호출
            bool getdamage = damageable.Damage(playerStateData.damage, transknockback);

            if (getdamage)
                Debug.Log(collision.name + "hit" + playerStateData.damage);
        }
    }
}
