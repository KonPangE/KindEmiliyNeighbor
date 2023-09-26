using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossAI : MonoBehaviour
{
    
    public enum State
    {
        OFF, //시작 전 대기 상태
        PATUNSTART, //움직이기 시작
        IDLE, //기본 상태
        ATTACK1, //공격1
        ATTACK2, //공격2
        COLLDAWN,//대기
        DIE,//사망
        P_DIE, //플레이어 사망
        CANGEPAHSE
    }
    public State state; // 상태

    //에너미 스탯
    EnemyStatData enemyStat;
    LastBossBulletAttack bulletAttack;
    Damageable damageable;
    Animator anim; //애니메이터
    public Transform playerTr; // 플레이어의 트랜스폼
    bool playerDie = false; //플레이어 사망 상태인지
    public int playerIn = 0; //플레이어가 보스룸에 들어왔는지
    public int phase = -1; //현재 몇 페이즈 인지

    int handOn; //활성화된 레이저 암 체크 위한 변수
    int stateCange = 0; //상태 변화를 해도 되는 상태인지 확인하는 변수(?)

    //공격 패턴에 쓰이는 오브젝트                        
    [SerializeField]
    List<GameObject> Hands;

    //애니메이터 파라미터
    readonly int hashAttack1 = Animator.StringToHash("isAttack2"); //bool
    readonly int hashAttack2 = Animator.StringToHash("isAttack1"); //bool
    readonly int hashStart = Animator.StringToHash("StartMotion"); //트리거
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

    //상태 변화
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

    //상태에 따른 액션
    IEnumerator StateAction()
    {
        while (!playerDie)
        {
            yield return new WaitForSeconds(0.1f);
            if (playerIn < 1)
                continue;

            switch (state)
            {
                case State.PATUNSTART: //서 있는
                    stateCange = 0;
                    anim.SetBool(hashPlayerIn, true);
                    anim.SetTrigger(hashStart);
                    yield return new WaitForSeconds(1f);
                    state = State.IDLE;
                    break;
                case State.IDLE: //서 있는
                    anim.SetBool(hashAttack1, false);
                    anim.SetBool(hashAttack2, false);
                    yield return new WaitForSeconds(1f);
                    stateCange = 0;
                    break;
                case State.ATTACK1: //공격1
                    stateCange++;
                    anim.SetBool(hashAttack1, true);
                    yield return new WaitForSeconds(0.5f);
                    if (phase < 1)
                    {
                        //소형 몬스터 소환되어 있는 상태
                        //레이저 암 하나씩만 활성화 / 활성화 되는 레이저 암은 랜덤
                        Hands[Random.Range(0, Hands.Count)].SetActive(true);

                    }
                    else if (phase < 2)
                    {
                        //레이저 암 두 개씩 활성화 / 활성화 되는 레이저 암은 랜덤
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
                        //레이저 암 1 ~ 3개식 활성화 / 활성화 되는 레이저 암은 랜덤
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
                case State.ATTACK2: //공격2
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
                case State.COLLDAWN: //대기
                    bulletAttack.bulletAttack = 0;
                    anim.SetBool(hashAttack1, false);
                    anim.SetBool(hashAttack2, false);
                    yield return new WaitForSeconds(2f);
                    handOn = 0;
                    state = State.IDLE;
                    //stateCange = 0;
                    break;
                case State.DIE: //사망
                    anim.SetBool(hashAttack1, false);
                    anim.SetBool(hashAttack2, false);
                    anim.SetTrigger(hashDie);
                    GameManager.Instance.bossLive = 0;
                    GameManager.Instance.lastboDie = true;
                    

                    StopAllCoroutines();
                    break;
                case State.P_DIE: //플레이어 사망
                    break;
                case State.CANGEPAHSE: //페이즈 변경
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
