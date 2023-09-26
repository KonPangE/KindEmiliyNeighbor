using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class EnemyDate : ScriptableObject
{
    //�̸�
    public string name;
    //���� ü��
    public int maxHP;
    //���� ���� ü��
    public int curHP;
    //���� ���ݷ�
    public int damage;
    //���� ���� ����
    public int pahse;

    public Vector2 knockback = Vector2.zero;

    private void Awake()
    {
    }

}
