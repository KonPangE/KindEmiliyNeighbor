using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 a;
    public float moveSpeed = 15f;
    public float upSpeed = 15f;

    public int c = 0;
    public float b = 0;
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        a = Vector2.right;
        StartCoroutine(UP());

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            c = 1;
           
            rb.velocity = Vector2.zero;
        }
        else if(collision.CompareTag("WALL"))
        {
            moveSpeed *= -1;
        }
    }



    /*IEnumerator UP()
    {
        yield return null;
        while (true)
        {

            yield return new WaitForSeconds(0.1f);
            if (c > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.up * -2f, Vector2.right,15f);
                Debug.DrawRay(transform.up * -2f, Vector2.right, Color.white, 15f);
                
                if (hit.collider != null && hit.collider.gameObject.layer == 10)
                {
                    rb.velocity = new Vector2(0f, moveSpeed);
                }

            }
            else
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }


        }

    }*/



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            c = 0;
            rb.velocity = Vector2.zero;

        }
    }
     IEnumerator UP()
     {
         yield return null;
         while (true)
         {
             yield return new WaitForSeconds(0.1f);
             if (c > 0)
             {
                 while (b < 0.501f)
                 {
                     yield return new WaitForSeconds(0.1f);
                     b += 0.1f;
                     rb.velocity = upSpeed * Vector2.up;
                 }
                b = 0;
                rb.velocity = Vector2.zero;
             }
             else
             {
                 rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
             }


         }

     }

}
