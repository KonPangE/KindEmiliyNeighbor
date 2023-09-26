using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    ItemSpawn item;
    //�Ϲ� ���� ����
    public enum GeneralEnemyType
    {
        SHORT,//�ٰŸ�
        LONG,//���Ÿ�
        HEALER,//����
        WALK//�׳� ���ƴٴϴ�
    }

    //�Ϲ� ���� ����
    public enum GeneralEnemyState
    {
        IDLE,//�� �ִ� 
        PATROLL,//����
        TRACE,//�߰�
        HIT,//�ǰ�
        ATTACK,//����
        COLLDAWN,//���� �� ���
        HEAL,//��
        DIE,//���
        PLAYER_DIE//�÷��̾� ���
    }

    public GeneralEnemyType type;
    public GeneralEnemyState state = GeneralEnemyState.PATROLL;

    HealerAttack enemyHeal;
    public Transform playerTr;
    Transform enemyTr;
    Damageable damageable;

    //���� ������ ��ũ��Ʈ �߰�
    EnemyMove enemyMove;
    //���� ���� ��ũ��Ʈ �߰�

    Animator anim;
    [Range(0f, 20f)]
    [SerializeField]
    float traceDist = 10f;
    [Range(0f, 20f)]
    [SerializeField]
    float attackDist = 5f;

    public int attackCheack = 0;
    bool playerDie = false;
    bool enemyDie = false;
    int Cange = 0;

    readonly int hashWalk = Animator.StringToHash("isWalk");
    readonly int hashAttack = Animator.StringToHash("Attack");
    readonly int hashDie = Animator.StringToHash("Die");


    private void Awake()
    {
        StartCoroutine(EnemyTypeChack());
    }
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        enemyMove = GetComponent<EnemyMove>();
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        item = GetComponentInChildren<ItemSpawn>();
        if(type == GeneralEnemyType.HEALER)
        {
            enemyHeal = GetComponent<HealerAttack>();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Ÿ�� üũ �� Ÿ�Կ� �´� �ڷ�ƾ ����
    IEnumerator EnemyTypeChack()
    {
        yield return new WaitForSeconds(0.1f);

        switch (type)
        {
            case GeneralEnemyType.SHORT: //�ٰŸ� ���
                StartCoroutine(ShortState());
                StartCoroutine(ShortAction());
                traceDist = 10f;
                attackDist = 5f;
                break;
            case GeneralEnemyType.HEALER: //���� ���
                StartCoroutine(HealerState());
                StartCoroutine(HealerStateAction());
                traceDist = 0f;
                attackDist = 0f;
                break;
            case GeneralEnemyType.WALK: //�׳� ���ƴٴϴ� ��� 
                StartCoroutine(ShortState());
                StartCoroutine(ShortAction());
                traceDist = 7f;
                attackDist = 7f;
                break;
            case GeneralEnemyType.LONG: //���Ÿ� ���
                StartCoroutine(LongState());
                StartCoroutine(LongAction());
                traceDist = 20f;
                attackDist = 17f;
                break;
        }
    }

    //�ٰŸ� ���� üũ
    IEnumerator ShortState()
    {
        yield return new WaitForSeconds(0.1f);

        if (!playerDie)
        {
            while (!enemyDie)
            {
                yield return new WaitForSeconds(0.1f);
                
                if(!damageable.islive)
                {
                    state = GeneralEnemyState.DIE;
                    continue;
                }
                if (state == GeneralEnemyState.ATTACK || state == GeneralEnemyState.IDLE || state == GeneralEnemyState.COLLDAWN)
                {
                    continue;
                }
                float dist = Vector2.Distance(playerTr.position, enemyTr.position);
                if (dist < traceDist)
                {
                    if (dist < attackDist)
                    {
                        state = GeneralEnemyState.ATTACK;
                    }
                    else
                    {
                        state = GeneralEnemyState.TRACE;
                    }
                }
                else
                {
                    state = GeneralEnemyState.PATROLL;
                }

            }
        }
    }


    //�ٰŸ� �׼�
    IEnumerator ShortAction()
    {
        yield return new WaitForSeconds(0.1f);
        if (!playerDie)
        {
            while (!enemyDie)
            {
                yield return new WaitForSeconds(0.1f);

                switch (state)
                {
                    case GeneralEnemyState.IDLE:
                        enemyMove.MoveStop();
                        anim.SetBool(hashWalk, false);
                        yield return new WaitForSeconds(2f);
                       
                        state = GeneralEnemyState.PATROLL;
                        break;
                    case GeneralEnemyState.PATROLL: //����
                        enemyMove.PATROLLING = true;
                        anim.SetBool(hashWalk, true);

                        break;
                    case GeneralEnemyState.TRACE: //�߰�
                        enemyMove.TRACE = playerTr.position;
                        anim.SetBool(hashWalk, true);
                        break;
                    case GeneralEnemyState.ATTACK: //����
                        enemyMove.MoveStop();
                        anim.SetBool(hashWalk, false);
                        anim.SetTrigger(hashAttack);
                        yield return new WaitForSeconds(0.3f);
                        attackCheack = 1;
                        state = GeneralEnemyState.COLLDAWN;
                        break;
                    case GeneralEnemyState.HIT: //�ǰ�
                        break;
                    case GeneralEnemyState.DIE: //���
                        enemyDie = true;
                        enemyMove.MoveStop();
                        anim.SetBool(hashWalk, false);
                        anim.SetTrigger(hashDie);
                        anim.SetBool("islive", false);
                        item.SpawnItem();
                        yield return new WaitForSeconds(1f);
                        Destroy(gameObject);
                        StopAllCoroutines();
                        break;
                    case GeneralEnemyState.PLAYER_DIE: //�÷��̾� ���
                        break;
                    case GeneralEnemyState.COLLDAWN:
                        switch (Random.Range(0, 3))
                        {
                            case 0:
                                enemyMove.MoveStop();
                                anim.SetBool(hashWalk, false);
                                attackCheack = 0;
                                yield return new WaitForSeconds(1f);
                                state = GeneralEnemyState.TRACE;
                              
                                break;
                            case 1:
                                enemyMove.PATROLLING = true;
                                anim.SetBool(hashWalk, true);
                                attackCheack = 0;
                                yield return new WaitForSeconds(1f);
                                state = GeneralEnemyState.TRACE;
                                
                                break;
                            case 2:
                                enemyMove.TRACE = playerTr.position;
                                anim.SetBool(hashWalk, true);
                                attackCheack = 0;
                                yield return new WaitForSeconds(1f);
                                state = GeneralEnemyState.TRACE;
                               
                                break;
                        }
                        break;
                }
            }
        }
    }




    //���Ÿ� ����
    IEnumerator LongState()
    {
        yield return new WaitForSeconds(0.1f);

        if (!playerDie)
        {
            while (damageable.islive)
            {
                yield return new WaitForSeconds(0.1f);
                
                if (state == GeneralEnemyState.IDLE || state == GeneralEnemyState.ATTACK || state == GeneralEnemyState.COLLDAWN)
                {
                    continue;
                }
                float dist = Vector2.Distance(playerTr.position, enemyTr.position);
                if (dist < traceDist)
                {
                    if (dist < attackDist)
                    {
                        state = GeneralEnemyState.ATTACK;
                    }
                    else
                    {
                        state = GeneralEnemyState.TRACE;
                    }
                }
                else
                {
                    state = GeneralEnemyState.PATROLL;
                }

            }
        }


    }


    //���Ÿ� �׼�
    IEnumerator LongAction()
    {
        yield return new WaitForSeconds(0.1f);
        if (!playerDie)
        {
            while (damageable.islive)
            {
                yield return new WaitForSeconds(0.1f);

                switch (state)
                {
                    case GeneralEnemyState.IDLE:
                        enemyMove.MoveStop();
                        anim.SetBool(hashWalk, false);
                        yield return new WaitForSeconds(2f);
                        state = GeneralEnemyState.PATROLL;
                        break;
                    case GeneralEnemyState.PATROLL: //����
                        enemyMove.PATROLLING = true;
                        anim.SetBool(hashWalk, true);
                        break;
                    case GeneralEnemyState.TRACE: //�߰�
                        enemyMove.TRACE = playerTr.position;
                        anim.SetBool(hashWalk, true);

                        break;
                    case GeneralEnemyState.ATTACK: //����
                        enemyMove.MoveStop();
                        anim.SetBool(hashWalk, false);
                        anim.SetTrigger(hashAttack);
                        yield return new WaitForSeconds(1f);
                        state = GeneralEnemyState.COLLDAWN;
                        break;
                    case GeneralEnemyState.HIT: //�ǰ�
                        break;
                    case GeneralEnemyState.DIE: //���
                        enemyMove.MoveStop();
                        anim.SetTrigger(hashDie);
                        item.SpawnItem();
                        Destroy(gameObject, 1f);
                        StopAllCoroutines();

                        break;
                    case GeneralEnemyState.PLAYER_DIE: //�÷��̾� ���
                        break;
                    case GeneralEnemyState.COLLDAWN:
                        switch (Random.Range(0, 3))
                        {
                            case 0:
                                enemyMove.MoveStop();
                                anim.SetBool(hashWalk, false);
                                attackCheack = 0;
                                yield return new WaitForSeconds(2f);
                                state = GeneralEnemyState.TRACE;
                                break;
                            case 1:
                                enemyMove.PATROLLING = true;
                                anim.SetBool(hashWalk, true);
                                attackCheack = 0;
                                yield return new WaitForSeconds(2f);
                                state = GeneralEnemyState.TRACE;
                                break;
                            case 2:
                                enemyMove.TRACE = playerTr.position;
                                anim.SetBool(hashWalk, true);
                                attackCheack = 0;
                                yield return new WaitForSeconds(2f);
                                state = GeneralEnemyState.TRACE;
                                break;
                        }
                        break;
                }
            }
            item.SpawnItem();
            Destroy(gameObject);


        }
    }

    //����
    IEnumerator HealerState()
    {
        yield return new WaitForSeconds(0.1f);

        if (!playerDie)
        {
            while (damageable.islive)
            {
                yield return new WaitForSeconds(0.1f);
                if (state == GeneralEnemyState.COLLDAWN)
                {
                    continue;
                }


                if (state == GeneralEnemyState.IDLE)
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            state = GeneralEnemyState.HEAL;
                            break;
                        case 1:
                            state = GeneralEnemyState.COLLDAWN;
                            break;
                        default:
                            state = GeneralEnemyState.HEAL;
                            break;

                    }
                }




            }
        }


    }

    //���� �׼�
    IEnumerator HealerStateAction()
    {
        yield return new WaitForSeconds(0.1f);

        while (damageable.islive)
        {
            yield return new WaitForSeconds(0.1f);
            switch (state)
            {
                case GeneralEnemyState.IDLE:
                    enemyMove.MoveStop();
                    
                    break;
                case GeneralEnemyState.COLLDAWN:
                    yield return new WaitForSeconds(10f);
                    enemyHeal.HealColl = 0;
                    state = GeneralEnemyState.IDLE;
                    break;
                case GeneralEnemyState.HEAL:
                    enemyHeal.HealStart();
                    break;
                case GeneralEnemyState.DIE:
                    enemyMove.MoveStop();
                    break;
                case GeneralEnemyState.PLAYER_DIE:
                    break;

            }
        }
    }

    IEnumerator WalkAction()
    {
        yield return new WaitForSeconds(0.1f);
        if (!playerDie)
        {
            while (damageable.islive)
            {
                yield return new WaitForSeconds(0.1f);

                switch (state)
                {
                    
                    case GeneralEnemyState.PATROLL: //����
                        enemyMove.PATROLLING = true;
                        anim.SetBool(hashWalk, true);
                        break;
                    case GeneralEnemyState.TRACE: //�߰�
                        enemyMove.TRACE = playerTr.position;
                        anim.SetBool(hashWalk, true);

                        break;
                    case GeneralEnemyState.ATTACK: //����
                        enemyMove.MoveStop();
                        anim.SetBool(hashWalk, false);
                        anim.SetTrigger(hashAttack);
                        yield return new WaitForSeconds(0.3f);
                        attackCheack = 1;
                        yield return new WaitForSeconds(0.7f);
                        state = GeneralEnemyState.COLLDAWN;
                        break;
                    case GeneralEnemyState.HIT: //�ǰ�
                        break;
                    case GeneralEnemyState.DIE: //���
                        break;
                    case GeneralEnemyState.PLAYER_DIE: //�÷��̾� ���
                        break;
                    case GeneralEnemyState.COLLDAWN:
                        enemyMove.PATROLLING = true;
                        anim.SetBool(hashWalk, true);
                        yield return new WaitForSeconds(3f);
                        state = GeneralEnemyState.PATROLL;
                        attackCheack = 0;
                        break;
                }
            }
        }
    }

   

}
