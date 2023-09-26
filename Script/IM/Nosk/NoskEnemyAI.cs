using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoskEnemyAI : MonoBehaviour
{
    ItemSpawn item;

    public enum State
    {
        WAIT,// ´ë±â
        START,
        IDLE,
        RUN,
        CRY,
        TURN,
        DIE,
        P_DIE
    }

    public State state = State.WAIT;


    Animator anim;
    NoskMove move;
    Damageable damageable;
    public bool playerIn = false;

    Transform playerTr;
    Transform enemyTr;
    bool playerDie = false;
    bool enemyDie = false;

    float collDawn = 2f;

    readonly int hashStart = Animator.StringToHash("Start");
    readonly int hashTurn = Animator.StringToHash("Turn");
    readonly int hashRun = Animator.StringToHash("Run");
    readonly int hashCry = Animator.StringToHash("Cry");
    readonly int hashDie = Animator.StringToHash("Die");
    readonly int hashPlayerIn = Animator.StringToHash("PlayerIn");
    readonly int hashIdle = Animator.StringToHash("isIdle");


    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        move = GetComponent<NoskMove>();
        damageable = GetComponent<Damageable>();
        item = GetComponent<ItemSpawn>();
        enemyTr = GetComponent<Transform>();
        StartCoroutine(Action());
        StartCoroutine(CheckState());
        StartCoroutine(CheckIn());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator CheckIn()
    {
        yield return new WaitForSeconds(0.1f);

        while (!playerDie)
        {
            yield return new WaitForSeconds(0.1f);
            float dist = Vector2.Distance(playerTr.position, enemyTr.position);
            if(dist < 10)
            {
                playerIn = true;
                break;
            }


        }
    }



    IEnumerator CheckState()
    {
        yield return new WaitForSeconds(0.1f);

        while (!playerDie)
        {
            yield return new WaitForSeconds(0.1f);
            if(!damageable.islive)
            {
                state = State.DIE; 
            }
            else if(GameManager.Instance.playerDie)
            {
                state = State.P_DIE;
            }
            

        }
    }


    IEnumerator Action()
    {
        yield return new WaitForSeconds(0.1f);

        while (!playerDie)
        {
            yield return new WaitForSeconds(0.1f);

            switch (state)
            {
                case State.WAIT:
                    move.Stop();
                    yield return new WaitForSeconds(0.1f);
                    if(playerIn)
                    {
                        anim.SetBool(hashPlayerIn, true);
                        anim.SetBool(hashIdle, false);
                        state = State.START;
                    }

                    break;
                case State.START:
                    move.Stop();
                    anim.SetTrigger(hashStart);
                    anim.SetBool(hashIdle, true);
                    yield return new WaitForSeconds(3f);
                    
                    state = State.RUN;
                    break;
                case State.IDLE:
                    anim.SetBool(hashCry, false);
                    anim.SetBool(hashRun, false);
                    anim.SetBool(hashTurn, false);
                    
                    yield return new WaitForSeconds(collDawn);
                    state = State.RUN;
                    break;
                case State.RUN:
                    anim.SetBool(hashRun, true);
                    break;
                case State.CRY:
                    move.Stop();
                    anim.SetBool(hashTurn, false);
                    anim.SetBool(hashCry, true);
                    yield return new WaitForSeconds(2f);
                    state = State.IDLE;
                    break;
                case State.TURN:
                    move.Stop();
                    move.Turning = true;
                    anim.SetBool(hashRun, false);
                    anim.SetBool(hashTurn, true);
                    yield return new WaitForSeconds(1f);
                    state = State.CRY;
                    break;
                case State.DIE:
                    move.Stop();
                    anim.SetBool(hashRun, false);
                    yield return new WaitForSeconds(0.1f);
                    anim.SetBool("islive", false);
                    anim.SetTrigger(hashDie);
                    item.SpawnItem();
                    
                    Destroy(gameObject, 2f);
                    StopAllCoroutines();

                    break;
                case State.P_DIE:
                    move.Stop();
                    anim.SetBool(hashRun, false);
                    StopAllCoroutines();
                    break;
            }


        }
    }
}
