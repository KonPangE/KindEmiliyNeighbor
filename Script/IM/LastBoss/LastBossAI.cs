using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossAI : MonoBehaviour
{
    
    public enum State
    {
        OFF, //���� �� ��� ����
        PATUNSTART, //�����̱� ����
        IDLE, //�⺻ ����
        ATTACK1, //����1
        ATTACK2, //����2
        COLLDAWN,//���
        DIE,//���
        P_DIE, //�÷��̾� ���
        CANGEPAHSE
    }
    public State state; // ����

    //���ʹ� ����
    EnemyStatData enemyStat;
    LastBossBulletAttack bulletAttack;
    Damageable damageable;
    Animator anim; //�ִϸ�����
    public Transform playerTr; // �÷��̾��� Ʈ������
    bool playerDie = false; //�÷��̾� ��� ��������
    public int playerIn = 0; //�÷��̾ �����뿡 ���Դ���
    public int phase = -1; //���� �� ������ ����

    int handOn; //Ȱ��ȭ�� ������ �� üũ ���� ����
    int stateCange = 0; //���� ��ȭ�� �ص� �Ǵ� �������� Ȯ���ϴ� ����(?)

    //���� ���Ͽ� ���̴� ������Ʈ                        
    [SerializeField]
    List<GameObject> Hands;

    //�ִϸ����� �Ķ����
    readonly int hashAttack1 = Animator.StringToHash("isAttack2"); //bool
    readonly int hashAttack2 = Animator.StringToHash("isAttack1"); //bool
    readonly int hashStart = Animator.StringToHash("StartMotion"); //Ʈ����
    readonly int hashPlayerIn = Animator.StringToHash("PlayerIn"); //bool
    readonly int hashPahse = Animator.StringToHash("CangePahse");
    readonly int hashDie = Animator.StringToHash("Die");
    private void Awake()
    {
        enemyStat = GetComponent<EnemyStatData>();
        phase = enemyStat.pahse;
        StartCoroutine(StateCheck());
        StartCoroutine(StateAction());
        StartCoroutine(PaahseCange());
    }

    // Start is called before the first frame update
    void Start()
    {
        handOn = 0;
        //phase = lastBossData._pahse;
        anim = GetComponent<Animator>();
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bulletAttack = GetComponent<LastBossBulletAttack>();
        damageable = GetComponent<Damageable>();
        
        for (int i = 0; i < 6; i++)
        {
            Hands.Add(transform.GetChild(i).gameObject);
            Hands[i].SetActive(false);
        }
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

            phase = enemyStat.pahse;
            yield return new WaitForSeconds(0.1f);
            
            if (playerIn < 1 || stateCange > 0)
            {
                continue;
            }
            else if(!damageable.islive)
            {
                state = State.DIE;
                break;
            }
            if (state == State.IDLE)
            {
                switch (Random.Range(0, 2))
                {
                    case 0:
                        state = State.ATTACK1;
                        break;
                    case 1:
                        state = State.ATTACK2;
                        break;

                    default:
                        state = State.IDLE;
                        break;
                }
            }

        }

    }

    //���¿� ���� �׼�
    IEnumerator StateAction()
    {
        while (!playerDie)
        {
            yield return new WaitForSeconds(0.1f);
            if (playerIn < 1)
                continue;

            switch (state)
            {
                case State.PATUNSTART: //�� �ִ�
                    stateCange = 0;
                    anim.SetBool(hashPlayerIn, true);
                    anim.SetTrigger(hashStart);
                    yield return new WaitForSeconds(1f);
                    state = State.IDLE;
                    break;
                case State.IDLE: //�� �ִ�
                    anim.SetBool(hashAttack1, false);
                    anim.SetBool(hashAttack2, false);
                    yield return new WaitForSeconds(1f);
                    stateCange = 0;
                    break;
                case State.ATTACK1: //����1
                    stateCange++;
                    anim.SetBool(hashAttack1, true);
                    yield return new WaitForSeconds(0.5f);
                    if (phase < 1)
                    {
                        //���� ���� ��ȯ�Ǿ� �ִ� ����
                        //������ �� �ϳ����� Ȱ��ȭ / Ȱ��ȭ �Ǵ� ������ ���� ����
                        Hands[Random.Range(0, Hands.Count)].SetActive(true);

                    }
                    else if (phase < 2)
                    {
                        //������ �� �� ���� Ȱ��ȭ / Ȱ��ȭ �Ǵ� ������ ���� ����
                        while (handOn < 2)
                        {
                            yield return null;
                            int count = Random.Range(0, Hands.Count);
                            if (!Hands[count].activeSelf)
                            {
                                Hands[count].SetActive(true);
                                handOn++;
                            }

                        }

                    }
                    else
                    {
                        //������ �� 1 ~ 3���� Ȱ��ȭ / Ȱ��ȭ �Ǵ� ������ ���� ����
                        while (handOn < 3)
                        {
                            yield return null;
                            int count = Random.Range(0, Hands.Count);
                            if (!Hands[count].activeSelf)
                            {
                                Hands[count].SetActive(true);
                                handOn++;
                            }

                        }
                    }
                    anim.SetBool(hashAttack2, false);
                    yield return new WaitForSeconds(3f);
                    state = State.COLLDAWN;
                    break;
                case State.ATTACK2: //����2
                    if (phase > 1)
                    {
                        anim.SetBool(hashAttack2, true);
                        state = State.COLLDAWN;
                    }
                    else
                    {
                        state = State.ATTACK1;
                    }

                    break;
                case State.COLLDAWN: //���
                    bulletAttack.bulletAttack = 0;
                    anim.SetBool(hashAttack1, false);
                    anim.SetBool(hashAttack2, false);
                    yield return new WaitForSeconds(2f);
                    handOn = 0;
                    state = State.IDLE;
                    //stateCange = 0;
                    break;
                case State.DIE: //���
                    anim.SetBool(hashAttack1, false);
                    anim.SetBool(hashAttack2, false);
                    anim.SetTrigger(hashDie);
                    GameManager.Instance.bossLive = 0;
                    GameManager.Instance.lastboDie = true;
                    

                    StopAllCoroutines();
                    break;
                case State.P_DIE: //�÷��̾� ���
                    break;
                case State.CANGEPAHSE: //������ ����
                    stateCange = 1;
                    anim.SetTrigger(hashPahse);
                    yield return new WaitForSeconds(2f);
                    state = State.IDLE;
                    break;

            }
        }
        
    }

    IEnumerator PaahseCange()
    {
        yield return new WaitForSeconds(0.1f);
        while (!playerDie)
        {
            yield return new WaitForSeconds(0.1f);
            if (enemyStat.curHp < enemyStat.maxHp / 1.5 && phase < 2)
            {
                state = State.CANGEPAHSE;
                enemyStat.pahse = 2;
            }
            else if(enemyStat.curHp < enemyStat.maxHp / 3 && phase < 3)
                {
                state = State.CANGEPAHSE;
                enemyStat.pahse = 3;
            }
        }
    }


}
