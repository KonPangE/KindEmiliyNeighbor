using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    int isAttack = 0;
    public Vector3 pooos;
    Rigidbody2D rb;
    BoxCollider2D coll;
    Enemy enemy;

    public GameObject neeDle;

    int maxNeedel = 40;
    [SerializeField]
    float needleSpeed = 0.5f;


    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        //coll = GetComponent<CapsuleCollider2D>();
        coll = GetComponent<BoxCollider2D>();
        enemy = GetComponentInParent<Enemy>();
        coll.enabled = false;
    }



    IEnumerator AttackTime(Vector3 pos)
    {
        pooos = pos;
        if (pos.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        yield return new WaitForSeconds(0.9f);
        coll.enabled = true;
        isAttack = 1;
        Vector3 a = pos - transform.position;
        a.z = 0;
        a.Normalize();
        pooos = a;
        rb.velocity = Vector2.zero;
        rb.velocity = a * 150f;
        yield return new WaitForSeconds(0.2f);
        coll.enabled = false;
        yield return new WaitForSeconds(0.5f);
        
        isAttack = 0;
    }



    IEnumerator NeedleSpawn()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < maxNeedel; i++)
        {
            float angle = i * (360f / maxNeedel);

            GameObject _needle = Instantiate(neeDle, transform.position, Quaternion.identity);
            //Vector2 dir = Quaternion.Euler(0f, 0f, angle) * Vector2.up;

            Rigidbody2D _rb = _needle.GetComponent<Rigidbody2D>();
            _rb.AddForce(new Vector2(needleSpeed * Mathf.Cos(Mathf.PI * 2 * i / maxNeedel),
                            needleSpeed * Mathf.Sin(Mathf.PI * i * 2 / maxNeedel)));
            _needle.transform.Rotate(new Vector3(0f, 0f, 360 * i / maxNeedel));
            yield return null;
        }
    }

    public void AttackMove(Vector3 pos)
    {
        StartCoroutine(AttackTime(pos));
    }

    public void NeedleAttack()
    {
        StartCoroutine(NeedleSpawn());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //플레이어 공격 가능 상태 조건 하나 더 걸어서 그 조건이 참일때 발동하게
        if (collision.collider.CompareTag("Player") && enemy.state == Enemy.EnemyState.ATTACK)
        {
            Debug.Log("플레이어 데미지 들어감");
        }
    }


}
