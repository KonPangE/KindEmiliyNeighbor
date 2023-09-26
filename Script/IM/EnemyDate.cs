using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class EnemyDate : ScriptableObject
{
    //이름
    public string name;
    //몬스터 체력
    public int maxHP;
    //몬스터 현재 체력
    public int curHP;
    //몬스터 공격력
    public int damage;
    //몬스터 패턴 상태
    public int pahse;

    public Vector2 knockback = Vector2.zero;

    private void Awake()
    {
    }

}
