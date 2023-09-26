using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatData : MonoBehaviour
{
    [SerializeField]
    EnemyDate enemyDate;

    //�ִ� ü��
    public int maxHp;
    //���� ü��
    public int curHp;
    //���ݷ�
    public int damage;
    //������
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
