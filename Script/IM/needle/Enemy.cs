//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    ItemSpawn item;
    public enum EnemyState
    {
        IDLE,
        TRACE,
        ATTACK,
        COLLDOWN,
        PAGECANGE,
        DIE,
        P_DIE
    }
    EnemyStatData enemyStat;

    public EnemyState state = EnemyState.IDLE;
    public Transform playerTr;
    Transform enemyTr;
    EnemyMove enemyMove;
    EnemyAttack enemyAttack;
    [SerializeField]
    float traceDist = 10f;
    [SerializeField]
    float attackDist = 6f;

    Damageable damageable;
    Animator anim;

    bool playerDie = false;

    readonly int hashWalk = Animator.StringToHash("Walk");
    readonly int hashAttack1 = Animator.StringToHash("Attack1");
    readonly int hashAttack2 = Animator.StringToHash("Attack2");
    readonly int hashPageCange = Animator.StringToHash("PageCange");
    readonly int hashDie = Animator.StringToHash("Die");
    

    int _pahse;
    public int a;
    int b = 0;
    float dist;
    private void Awake()
    {
        StartCoroutine(CheckState());
        StartCoroutine(StateAction());
        StartCoroutine(StopMoveing());
        StartCoroutine(PahseCange());
    }
    void Start()
    {
        playerTr = GameObject.Find("Player").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        enemyStat = GetComponent<EnemyStatData>();
        damageable = GetComponent<Damageable>();
        item = GetComponent<ItemSpawn>();
        anim.SetBool("islive", true);
        anim.SetBool(hashDie, false);
        _pahse = enemyStat.pahse;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CheckState()
    {
        yield return new WaitForSeconds(0.1f);

        if (!playerDie)
        {
            while (!playerDie || state != EnemyState.DIE)
            {
                yield return new WaitForSeconds(0.1f);
                dist = Vector2.Distance(playerTr.position, enemyTr.position);
                _pahse = enemyStat.pahse;
                if(!damageable.islive)
                {
                    state = EnemyState.DIE;
                    continue;
                }


                if (state == EnemyState.COLLDOWN || state == EnemyState.PAGECANGE || state == EnemyState.ATTACK)
                {
                    continue;
                }

                if (dist > traceDist && a > 6 && b < 1 && state != EnemyState.IDLE)
                {
                    state = EnemyState.IDLE;
                    b = 1;
                }
                else if (dist < traceDist)
                {
                    if (attackDist > dist)
                    {
                        state = EnemyState.ATTACK;
                    }
                    else
                    {
                        state = EnemyState.TRACE;
                    }
                }
                else
                {
                    state = EnemyState.IDLE;
                }
                
            }
        }
    }


    IEnumerator StateAction()
    {
        yield return new WaitForSeconds(0.1f);
        if (!playerDie)
        {
            while (!playerDie)
            { 
                yield return new WaitForSeconds(0.1f);
                switch (state)
                {
                    case EnemyState.IDLE:
                        enemyMove.MoveStop();
                        anim.SetBool(hashWalk, false);
                        yield return new WaitForSeconds(2f);
                        b = 1;
                        break;
                    case EnemyState.TRACE:
                        enemyMove.TRACE = playerTr.position;
                        anim.SetBool(hashWalk, true);
                        break;
                    case EnemyState.ATTACK:
                        enemyMove.MoveStop();
                        float patun = Random.Range(0, 10);
                        if (patun < 7)
                        {
                            enemyAttack.AttackMove(playerTr.position);
                            anim.SetTrigger(hashAttack1);
                        }
                        else if(patun > 6 && _pahse > 0)
                        {
                            //°¡½Ã »Ñ¸®´Â Èð»Ñ¸®´Â µ¿ÀÛ 
                            enemyAttack.NeedleAttack();
                            anim.SetTrigger(hashAttack2);
                        }
                        else
                        {
                            enemyAttack.AttackMove(playerTr.position);
                            anim.SetTrigger(hashAttack1);
                        }
                        yield return new WaitForSeconds(1f);
                        state = EnemyState.COLLDOWN;
                        break;
                    case EnemyState.DIE:
                        enemyMove.MoveStop();
                        anim.SetBool(hashWalk, false);
                        anim.SetBool("islive", false);
                        GameManager.Instance.bossLive = 0;
                        item.SpawnItem();
                        Destroy(gameObject, 1.5f);
                        anim.SetTrigger(hashDie);
                        
                        StopAllCoroutines();
                        break;
                    case EnemyState.P_DIE:
                        break;
                    case EnemyState.COLLDOWN:
                        enemyMove.MoveStop();
                        anim.SetBool(hashWalk, false);
                        yield return new WaitForSeconds(2f);
                        state = EnemyState.TRACE;
                        break;
                    case EnemyState.PAGECANGE:
                        /*enemyMove.MoveStop();
                        anim.SetBool(hashWalk, false);
                        anim.SetTrigger(hashPageCange);
                        StopCoroutine(PageCange());*/
                        yield return new WaitForSeconds(3f);
                        state = EnemyState.TRACE;
                        
                        
                        break;
                }
               
            }
        }
    }

    IEnumerator StopMoveing()
    {
        while (!playerDie)
        {
            a = Random.Range(0, 10);
            if (b == 1)
            {
                yield return new WaitForSeconds(5f);
                b = 0;
            }
            yield return new WaitForSeconds(3f);
        }

    }

    IEnumerator PahseCange()
    {
        while(!playerDie)
        {
            yield return new WaitForSeconds(0.1f);

            if(enemyStat.curHp < enemyStat.maxHp / 1.5)
            {
                enemyStat.pahse = 1;
                state = EnemyState.PAGECANGE;
                enemyMove.MoveStop();
                anim.SetBool(hashWalk, false);
                anim.SetTrigger(hashPageCange);
                yield return new WaitForSeconds(3f);
                state = EnemyState.TRACE;
                break;
            }
        }
    }
}
