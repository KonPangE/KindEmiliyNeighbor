using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEnemyAI : MonoBehaviour
{
    public enum Type
    {
        LENEMY,//근거리
        RENEMY //원거리
    }

    public enum SubEnemyState
    {
        STARTEFFECT,//스폰 애니메이션 재생 후 추격 / 대기 상태로 전환해주기 위함
        IDLE, //서 있는 상태 
        TARCE, //추격 
        ATTACK, //공격
        COLLDAWN,// 대기 상태
        DIE, //사망
        P_DIE // 플레이어 사망
    }

    [SerializeField]
    Type type; //서브 에너미 타입
    [SerializeField]
    public SubEnemyState state; //서브 에너미 상태

    public bool playerDie = false;
    bool enemyDie = false;
    float attackDist;

    public Transform playerTr; //플레이어 트랜스폼
    Transform enemyTr; //에너미 트랜스 폼

    Animator anim; //애니메이터
    SubEnemyMove move; //에너미 움직임 스크립트
    Damageable damageable;

    int cangeCansle = 1;


    readonly int hashIdle = Animator.StringToHash("isIdle");
    readonly int hashWalk = Animator.StringToHash("isWalk");
    readonly int hashAttack = Animator.StringToHash("Attack");
    //readonly int hashSpawn = Animator.StringToHash("Spawn");
    readonly int hashDie = Animator.StringToHash("Die");
    readonly int _hashSpawn = Animator.StringToHash("isSpawn");


    private void Awake()
    {
        StartCoroutine(StateCheck());
        StartCoroutine(Action());
    }

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        move = GetComponent<SubEnemyMove>();
        damageable = GetComponent<Damageable>();
        anim.SetBool("islive", true);

        if (type == Type.LENEMY)
        {
            attackDist = 5;
        }
        else
        {
            attackDist = 15;
        }
        state = SubEnemyState.STARTEFFECT;

    }

    // Update is called once per frame
    void Update()
    {

    }

    //상태 변화
    IEnumerator StateCheck()
    {
        yield return new WaitForSeconds(0.1f);
        while (!playerDie)
        {
            yield return new WaitForSeconds(0.1f);
            float dir = Vector2.Distance(transform.position, playerTr.position);
            if(!damageable.islive)
            {
                state = SubEnemyState.DIE;
            }
            else if(GameManager.Instance.playerDie)
            {
                state = SubEnemyState.P_DIE;
            }

            if(cangeCansle > 0)
            {
                continue;
            }

            if (dir < attackDist)
            {
                state = SubEnemyState.ATTACK;
            }
            else
            {
                state = SubEnemyState.TARCE;
            }


        }

    }

    //상태에 따른 동작
    IEnumerator Action()
    {
        yield return new WaitForSeconds(0.1f);
        while (!playerDie)
        {
            yield return new WaitForSeconds(0.1f);
            switch (state)
            {
                case SubEnemyState.STARTEFFECT:
                    //anim.SetTrigger(hashSpawn);
                    anim.SetBool(_hashSpawn, true);
                    yield return new WaitForSeconds(0.1f);
                    anim.SetBool(_hashSpawn, false);
                    yield return new WaitForSeconds(2f);
                   
                    cangeCansle = 0;
                    state = SubEnemyState.TARCE;
                    break;
                case SubEnemyState.IDLE:
                    yield return new WaitForSeconds(2f);
                    anim.SetBool(hashIdle, true);
                    state = SubEnemyState.TARCE;
                    break;
                case SubEnemyState.TARCE:
                    move.TRACETARGET = playerTr.position;
                    anim.SetBool(hashWalk, true);
                    
                    break;
                case SubEnemyState.ATTACK:
                    //공격 로직 
                    cangeCansle = 1;
                    move.Stop();
                    anim.SetBool(hashWalk, false);
                    anim.SetTrigger(hashAttack);
                    yield return new WaitForSeconds(1f);
                    state = SubEnemyState.COLLDAWN;
                    break;
                case SubEnemyState.COLLDAWN:
                    //해당 위치에 대기
                    move.Stop();
                    yield return new WaitForSeconds(2f);
                    state = SubEnemyState.TARCE;
                    cangeCansle = 0;
                    break;
                case SubEnemyState.DIE:
                    move.Stop();
                    anim.SetTrigger(hashDie);
                    anim.SetBool("islive", false);
                    cangeCansle = 1;
                    Destroy(gameObject, 1f);
                    StopAllCoroutines();

                    break;
                case SubEnemyState.P_DIE:
                    move.Stop();
                    cangeCansle = 1;
                   
                    StopAllCoroutines();

                    break;


            }

        }
    }

}
