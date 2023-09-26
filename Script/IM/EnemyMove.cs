using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Rigidbody2D rb;

    public Vector2 startPoint;
    SpriteRenderer spr;
    // float moveSpeed = 3f;
    public float speed = 3f;


    bool patrolling;
    public bool PATROLLING
    {
        get { return patrolling; }
        set
        {
            patrolling = value;
            if(patrolling)
            {
                Move();
            }
        }
    }


    Vector2 trace;
    public  Vector2 TRACE
    {
        get { return trace; }
        set
        {
            trace = value;
            TraceTarGet(trace);

        }
    }

    void TraceTarGet(Vector2 pos)
    {
        
        //플레이어 추격 구현 - 나중에 다른 좋은 방식이 생기면 바꿔주자.
        if (pos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rb.velocity = new Vector2(5f, rb.velocity.y);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            rb.velocity = new Vector2(-5f, rb.velocity.y);
        }
     
    }

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        startPoint = transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
         
    }



   

    void Move()
    {
        if(speed > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }


    public void MoveStop()
    {
        rb.velocity = Vector2.zero;
    }

    public void OnDamage(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
