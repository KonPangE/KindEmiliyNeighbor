using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerAttack : MonoBehaviour
{
    //주변 범위 내 몬스터 콜라이더 검출해서 담을 변수
    Collider2D[] colls;
    Vector2 enemyTr;
    readonly LayerMask layer = LayerMask.NameToLayer("ENEMY");

    public int HealColl = 0;


    // Start is called before the first frame update
    void Start()
    {
        enemyTr = transform.position;
        colls = Physics2D.OverlapCircleAll(enemyTr, 45f, layer);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void HealStart()
    {
        StartCoroutine(Heal());
    }

    IEnumerator Heal()
    {
        if(HealColl > 0)
        {
            yield break;
        }
        HealColl = 1;
        yield return null;
        for(int i = 0; i < colls.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            if(colls[i] != null )
            {
                //회복 동작
            }
        }
    }

    

}
