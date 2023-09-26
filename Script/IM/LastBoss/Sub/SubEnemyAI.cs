using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEnemyAI : MonoBehaviour
{
    public enum Type
    {
        LENEMY,//�ٰŸ�
        RENEMY //���Ÿ�
    }

    public enum SubEnemyState
    {
        STARTEFFECT,//���� �ִϸ��̼� ��� �� �߰� / ��� ���·� ��ȯ���ֱ� ����
        IDLE, //�� �ִ� ���� 
        TARCE, //�߰� 
        ATTACK, //����
        COLLDAWN,// ��� ����
        DIE, //���
        P_DIE // �÷��̾� ���
    }

    [SerializeField]
    Type type; //���� ���ʹ� Ÿ��
    [SerializeField]
    public SubEnemyState state; //���� ���ʹ� ����

    public bool playerDie = false;
    bool enemyDie = false;
    float attackDist;

    public Transform playerTr; //�÷��̾� Ʈ������
    Transform enemyTr; //���ʹ� Ʈ���� ��

    Animator anim; //�ִϸ�����
    SubEnemyMove move; //���ʹ� ������ ��ũ��Ʈ
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

    //���� ��ȭ
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

    //���¿� ���� ����
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
                    //���� ���� 
                    cangeCansle = 1;
                    move.Stop();
                    anim.SetBool(hashWalk, false);
                    anim.SetTrigger(hashAttack);
                    yield return new WaitForSeconds(1f);
                    state = SubEnemyState.COLLDAWN;
                    break;
                case SubEnemyState.COLLDAWN:
                    //�ش� ��ġ�� ���
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
