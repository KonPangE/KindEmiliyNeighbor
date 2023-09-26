using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemyData : MonoBehaviour
{
    public EnemyDate enemyDate;

    public int maxHp;
    public int curHp;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = enemyDate.maxHP;
        curHp = maxHp;
        damage = enemyDate.damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
