using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/New Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        UseItem,
        AbilityItem
    }

    //������ ���� ��ȣ
    public int itemIndex;
    //������ �̸�
    public string itemName;
    //������ Ÿ��
    public ItemType itemType;
    //���ݷ�
    public int damage;
    //���� �ӵ�
    public float attackSpeed;
    //�ִ� HP ������
    public int maxHealth;
    //ȸ�� ��Ÿ�� ����
    public float collDawn;
    //ȸ�� �ӵ�
    public float dashforce;

    //�̵��ӵ�
    public float moveSpeed;
    //����
    public float jumpForce;
    //������ �̹���
    public Sprite itemImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
