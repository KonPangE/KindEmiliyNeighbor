using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    int itemCount = 0;
    Image itemImage;
    

    void Start()
    {
        //itemImage = GetComponentInChildren<Image>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //������ ȹ��
    public void ItemAdd(Item _item)
    {
        if(itemCount > 0)
        {
            return;
        }

        item = _item;
        itemImage.sprite = _item.itemImage;
        itemImage.color = new Color(1, 1, 1, 1);
    }

    //������ ��� �Ǵ� ����� ������ �����ϰ� ���� ���� �Լ��� ����ų� ���� �ϳ�
    public void ItemUse()
    {

    }

    public void RemoveItem()
    {
        item = null;
        itemImage.sprite = null;
        itemImage.color = new Color(0, 0, 0, 0);
    }

}
