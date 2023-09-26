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

    //아이템 고유 번호
    public int itemIndex;
    //아이템 이름
    public string itemName;
    //아이템 타입
    public ItemType itemType;
    //공격력
    public int damage;
    //공격 속도
    public float attackSpeed;
    //최대 HP 증가량
    public int maxHealth;
    //회피 쿨타임 감소
    public float collDawn;
    //회피 속도
    public float dashforce;

    //이동속도
    public float moveSpeed;
    //점프
    public float jumpForce;
    //아이템 이미지
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
