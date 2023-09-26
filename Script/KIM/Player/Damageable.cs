
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageablehit;
    Animator anim;
    PlayerStateData playerState;
    EnemyStatData enemyStat;
    public UIManager UIManager;


    public int Maxhealth { get { return maxhealth; } set { maxhealth = value; } }
    int maxhealth;

    public bool islive = true;
    public int Health { get { return currenthealth; } set { Debug.Log("헬스 프로퍼티"); currenthealth = value; } }

    public int currenthealth;



    public bool isinvincible = false; //무적 여부
    public bool Ishit { get { return anim.GetBool("ishit"); } private set { anim.SetBool("ishit", value); } }
    public float invincibletime = 0.25f; //무적시간

    private void Awake()
    {

        islive = true;

        anim = GetComponent<Animator>();
        UIManager = FindObjectOfType<UIManager>();
        if (GetComponent<EnemyStatData>() != null)
        {
            enemyStat = GetComponent<EnemyStatData>();
            Maxhealth = enemyStat.maxHp;
            currenthealth = enemyStat.curHp;
            Health = Maxhealth;
        }
        else if (GetComponent<PlayerStateData>() != null)
        {
            playerState = GetComponent<PlayerStateData>();
            Maxhealth = playerState.Maxhealth;
            currenthealth = playerState.curHp;
            Health = Maxhealth;

        }

    }

    public bool Damage(int damage, Vector2 knockback)
    {
        if (islive)
        {
            if (playerState != null && !isinvincible)
            {
                var data = playerState;
                data.curHp -= damage;
                currenthealth = data.curHp;
                isinvincible = true;
                Ishit = true;
                anim.SetBool("islive", true);
                damageablehit?.Invoke(damage, knockback);
                TextEvent.Damaged.Invoke(gameObject, damage);
                StartCoroutine(Invincible());

                if (currenthealth <= 0)
                {
                    islive = false;
                    anim.SetBool("islive", false);
                    UIManager.GameOver();
                }


            }
            else if (enemyStat != null)
            {
                var data = enemyStat;
                data.curHp -= damage;
                currenthealth = data.curHp;
                anim.SetBool("islive", true);
                damageablehit?.Invoke(damage, knockback);
                TextEvent.Damaged.Invoke(gameObject, damage);
                if (currenthealth <= 0)
                {
                    islive = false;
                    anim.SetBool("islive", false);
                }
                if (currenthealth <= 0 && enemyStat._name == "LastBoss")
                {
                    islive = false;
                    anim.SetBool("islive", false);
                    UIManager.EndGame();
                }


            }
            return true;

        }
        return false;
    }

    IEnumerator Invincible()
    {

        if (isinvincible)
        {

            yield return new WaitForSeconds(0.2f);
            isinvincible = false;
            Ishit = false;

        }


    }


}
