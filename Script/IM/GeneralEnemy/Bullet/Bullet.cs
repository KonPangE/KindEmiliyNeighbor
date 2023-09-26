using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Vector2 dir;

    readonly int hashHit = Animator.StringToHash("isHit");

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            rb.velocity = Vector3.zero;
            anim.SetBool(hashHit, true);
            Destroy(this.gameObject, 1f);
        }
        else if(collision.gameObject.layer == 10)
        {
            rb.velocity = Vector3.zero;
            anim.SetBool(hashHit, true);
            Destroy(this.gameObject, 1f);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Destroy(gameObject, 3f);
    }


    public void FirePos(Vector2 pos)
    {
        dir = pos - new Vector2(transform.position.x, transform.position.y);
        dir.Normalize();
        rb.velocity = dir * 15f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
