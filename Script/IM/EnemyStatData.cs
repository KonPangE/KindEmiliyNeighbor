using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatData : MonoBehaviour
{
    [SerializeField]
    EnemyDate enemyDate;

    //최대 체력
    public int maxHp;
    //현재 체력
    public int curHp;
    //공격력
    public int damage;
    //페이즈
    public int pahse;
    //
    public string _name;

    // Start is called before the first frame update
    void Start()
    {
        if(enemyDate.name != null)
        _name = enemyDate.name;

        maxHp = enemyDate.maxHP;
        curHp = maxHp;
        damage = enemyDate.damage;
        pahse = enemyDate.pahse;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
