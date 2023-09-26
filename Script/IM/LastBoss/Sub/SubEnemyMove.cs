using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SubEnemyMove : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;


    Rigidbody2D rb;

    Vector2 traceTarget;
    public Vector2 TRACETARGET
    {
        get { return traceTarget; }
        set
        {
            traceTarget = value;
            TraceMove(traceTarget);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //플레이어 추격하는 거
    //플레이어로부터 멀어지는거
    //움직임 멈추는거

    void TraceMove(Vector2 target)
    {
        Vector2 tr = transform.position;
        if (target.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);   
        }
        Vector2 dist = target - tr;
        dist.Normalize();
        //transform.Translate((speed * Time.fixedDeltaTime) * dist);
        rb.velocity = speed * dist;

    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }
}
