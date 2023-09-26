using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoskMove : MonoBehaviour
{
    Rigidbody2D rb;
    NoskEnemyAI noskEnemy;
    float moveSpeed = -15f;
    float upSpeed = 15f;

    int up = 0;
    float curupTime = 0f;
    float upTime = 0.501f;



    bool Turn = false;
    public bool Turning
    {
        get { return Turn; }
        set
        {
            Turn = value;
            if(Turn)
            {
                StartCoroutine(NoskTurn());
            }
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        noskEnemy = GetComponent<NoskEnemyAI>();
        StartCoroutine(NoskRun());
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
    //런, 턴 메서드 만들기

    IEnumerator NoskRun()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (up > 0)
            {
                while (curupTime < upTime)
                {
                    yield return new WaitForSeconds(0.1f);
                    curupTime += 0.1f;
                    rb.velocity = upSpeed * Vector2.up;
                }
                curupTime = 0f;
                Stop();
            }
            else if(noskEnemy.state == NoskEnemyAI.State.RUN)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
        }
    }

   


    IEnumerator NoskTurn()
    {
        yield return new WaitForSeconds(0.4f);
        if(moveSpeed > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    


    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10 && transform.position.y < -42.6)
        {
            up = 1;
            Stop();
        }
        else if(collision.CompareTag("WALL"))
        {
            moveSpeed *= -1;
            noskEnemy.state = NoskEnemyAI.State.TURN;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            up = 0;
        }
    }


}
